import { useFetch } from './use-fetch';
import type { Framework } from '@/types/framework';
import type { FetchResult } from '@/types/fetch-result';


export type UpdateOptions = {
  id: number;
  body: Framework;
};

export function useFrameworkGet() {
  return useFetch<Framework[]>({ url: '/api/framework', method: 'GET', loading: true });
}
export function useFrameworkNew() {
  return useFetch<Framework>({ url: '/api/framework', method: 'POST', loading: false });
}
export function useFrameworkUpdate(): { fetch: ({ id, body }: UpdateOptions) => Promise<FetchResult<Framework>> } {
  const { fetch } = useFetch<Framework>({ url: '', method: 'PUT', loading: false });
  function saveChangeFramework({ id, body }: UpdateOptions): Promise<FetchResult<Framework>> {
    const url = `/api/framework/${id}`;
    return fetch({ body, url });
  };
  return { fetch: saveChangeFramework };
}
