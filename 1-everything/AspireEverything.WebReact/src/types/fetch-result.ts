export interface FetchResult<T> {
  data: T | null;
  error: Error | null;
  status: number | null;
  loading: boolean;
}
