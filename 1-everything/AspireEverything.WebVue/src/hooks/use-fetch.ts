import { ref, type Ref, watchEffect } from 'vue';
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

  const results: Ref<FetchResult<T>> = ref({
    data: null,
    error: null,
    status: null,
    loading: args.loading
  });

  const refresh = async (opts?: RefreshOptions): Promise<FetchResult<T>> => {
    let url = opts?.url;
    const body = opts?.body;

    if (!url) {
      url = args.url;
    }
    if (!url || !args.method) {
      // TODO: just ignore it? Is it not initialized yet?
      results.value.loading = false;
      results.value.status = null;
      results.value.error = new Error('No URL or method provided');
      return results.value;
    }
    if (args.method !== 'GET' && !body) {
      // FRAGILE: ASSUME: they haven't clicked the button yet
      results.value.loading = false;
      results.value.status = null;
      results.value.error = null;
      return results.value;
    }

    results.value.loading = true;
    results.value.status = null;
    results.value.error = null;
    try {
      console.log(`${args.method} ${url}`);
      const res = await fetch(url, {
        method: args.method,
        headers: { 'Content-Type': 'application/json' },
        body: body ? JSON.stringify(body) : undefined
      });
      results.value.status = res.status;
      if (!res.ok) {
        results.value.error = new Error(`HTTP error! status: ${res.status}`);
        console.error('Fetch error', results.value.status);
        return results.value;
      }
      results.value.data = (await res.json()) as T?;
      results.value.error = null;
      console.log(`Fetch successful ${args.method} ${url}`, results.value.data);
    } catch (e) {
      results.value.error = e as Error;
      console.error('Fetch exception', results.value.error);
    } finally {
      results.value.loading = false;
    }
    return results.value;
  };

  // Watch for changes in the URL if it's a ref, or fetch immediately if it's a string
  watchEffect(() => {
    refresh({});
  });

  return { results, fetch: refresh };
}
