import type { VoteScore } from '@/types/vote-score';
import { useFetch } from './use-fetch';
import type { Vote } from '@/types/vote';


export type UpdateOptions = {
  frameworkId: number;
  score: number;
};

export function useVoteGet() {
  return useFetch<Vote[]>({ url: '/api/vote/get', method: 'GET', loading: true });
}
export function useVoteScore(): { fetch: ({ frameworkId, score }: UpdateOptions) => Promise<void> } {
  const { fetch } = useFetch<VoteScore>({ url: '', method: 'POST', loading: false });
  function saveChangeVote({ frameworkId, score }: UpdateOptions): Promise<void> {
    const url = `/api/vote/score/${frameworkId}`;
    return fetch({ body: { score }, url });
  };
  return { fetch: saveChangeVote };
}
