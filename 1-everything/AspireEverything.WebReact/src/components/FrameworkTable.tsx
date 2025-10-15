import React, { useCallback, useMemo, useState } from 'react';
import { useFrameworkGet, useFrameworkNew, useFrameworkUpdate } from '@/hooks/use-framework';
import { useVoteGet, useVoteScore } from '@/hooks/use-vote';
import type { Framework } from '@/types/framework';
import FrameworkRow from '@/components/FrameworkRow';
import type { VoteScore } from '@/types/vote-score';


export default function FrameworkTable() {
  const { results: frameworks, fetch: refreshFrameworks } = useFrameworkGet();
  const { fetch: saveNewFramework } = useFrameworkNew();
  const { fetch: saveChangeFramework } = useFrameworkUpdate();
  const { results: votes, fetch: refreshVotes } = useVoteGet();
  const { fetch: saveVote } = useVoteScore();

  const [newName, setNewName] = useState('');

  const frameworkScores = useMemo(() => {
    return frameworks.data?.map((fw: Framework) => {
      const vote = votes.data?.find((v: { frameworkId: number; score: number }) => v.frameworkId === fw.id);
      return { ...fw, score: vote?.score ?? 0 };
    }) ?? [];
  }, [frameworks.data, votes.data]);

  const refresh = useCallback(async () => {
    await refreshFrameworks();
    await refreshVotes();
  }, [refreshFrameworks, refreshVotes]);

  const submitNewFramework = useCallback(async () => {
    const newFramework: Framework = { id: 0, name: newName };
    await saveNewFramework({ body: newFramework });
    setNewName('');
    await refresh();
  }, [newName, saveNewFramework, refresh]);

  const submitChangeFramework = useCallback(async (updatedFramework: Framework) => {
    await saveChangeFramework({ id: updatedFramework.id, body: updatedFramework });
    await refresh();
  }, [saveChangeFramework, refresh]);

  const submitVote = useCallback(async (vote: VoteScore) => {
    await saveVote(vote);
    await refresh();
  }, [saveVote, refresh]);

  if (frameworks.loading) {
    return <div className="text-center">Loading...</div>;
  }
  if (frameworks.error) {
    return <div className="text-center text-danger">Error: {frameworks.error.message}</div>;
  }

  return (
    <div>
      <div className="card">
        <div className="card-body">
          <form onSubmit={(e: React.FormEvent) => { e.preventDefault(); void submitNewFramework(); }}>
            <div className="row">
              <div className="col">
                <input type="text" className="form-control" id="name" value={newName} onChange={(e: React.ChangeEvent<HTMLInputElement>) => setNewName(e.target.value)} placeholder="Add a new Framework" required maxLength={200} />
              </div>
              <div className="col">
                <button type="submit" className="btn btn-primary">Add Framework</button>
              </div>
            </div>
          </form>
        </div>
      </div>
      <div className="row mt-4">
        <div className="col-md-8">
          <h2 className="text-center">Frameworks</h2>
        </div>
        <div className="col-md-4 text-end">
          <button className="btn btn-primary" onClick={() => void refresh()}>Refresh</button>
        </div>
      </div>
      <div className="row">
        <table className="table">
          <thead>
            <tr>
              <th scope="col">Id</th>
              <th scope="col">Name</th>
              <th scope="col">Votes</th>
            </tr>
          </thead>
          <tbody>
            {frameworkScores.map(framework => (
              <tr key={framework.id}>
                <FrameworkRow framework={framework} onSaveFramework={submitChangeFramework} onSaveVote={submitVote} />
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
