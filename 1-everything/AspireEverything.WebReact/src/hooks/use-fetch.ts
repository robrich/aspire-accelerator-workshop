import { useCallback, useEffect, useState } from 'react';
import type { FetchResult } from '@/types/fetch-result';


export type UseFetchOptions = {
  url: string;
  method: 'GET' | 'POST' | 'PUT' | 'DELETE' | 'PATCH';
  loading: boolean; // do we start loading immediately?
};

export type RefreshOptions = {
  body?: unknown | undefined; // body for POST/PUT/PATCH
  url?: string; // override URL
};

export function useFetch<T>(args: UseFetchOptions) {

  const [results, setResults] = useState<FetchResult<T>>({
    data: null,
    error: null,
    status: null,
    loading: args.loading
  });

  const refresh = useCallback(async (opts?: RefreshOptions) => {
    const url = opts?.url ?? args.url;
    const body = opts?.body;

    if (!url || !args.method) {
      setResults({ loading: false, status: null, error: new Error('No URL or method provided'), data: null });
      return;
    }
    if (args.method !== 'GET' && !body) {
      // FRAGILE: ASSUME: they haven't clicked the button yet
      setResults({ loading: false, status: null, error: null, data: null });
      return;
    }

    setResults((r: FetchResult<T>) => ({ ...r, loading: true, status: null, error: null }));
    try {
      console.log(`${args.method} ${url}`);
      const res = await fetch(url, {
        method: args.method,
        headers: { 'Content-Type': 'application/json' },
        body: body ? JSON.stringify(body) : undefined
      });
      if (!res.ok) {
        setResults({ status: res.status, error: new Error(`HTTP error! status: ${res.status}`), loading: false, data: null });
        console.error('Fetch error', res.status);
        return;
      }
      const results: FetchResult<T> = {
        data: (await res.json()) as T | null,
        error: null,
        status: res.status,
	loading: false
      };
      console.log(`Fetch successful ${args.method} ${url}`, results.data);
      setResults(results);
    } catch (e) {
      console.error('Fetch exception', e);
      setResults({data: null, error: e as Error, loading: false, status: null});
    }
  }, [args.url, args.method]);

  useEffect(() => {
    if (args.loading) {
      void refresh();
    }
  }, [args.loading, refresh]);

  return { results, fetch: refresh };
}
