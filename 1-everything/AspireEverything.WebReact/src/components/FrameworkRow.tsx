import React, { useCallback, useState } from 'react';
import type { Framework } from '@/types/framework';
import type { FrameworkScore } from '@/types/framework-score';
import type { VoteScore } from '@/types/vote-score';


export type Props = {
  framework: FrameworkScore;
  onSaveFramework: (payload: Framework) => void | Promise<void>;
  onSaveVote: (payload: VoteScore) => void | Promise<void>;
};

export default function FrameworkRow({ framework, onSaveFramework, onSaveVote }: Props) {
  const [isEditing, setIsEditing] = useState(false);
  const [editName, setEditName] = useState(framework.name);

  const changeEdit = useCallback(() => {
    setIsEditing((v: boolean) => !v);
    if (isEditing) {
      setEditName(framework.name);
    }
  }, [framework.name, isEditing]);

  const saveEdit = useCallback(async (e: React.FormEvent) => {
    e.preventDefault();
    const updatedFramework: Framework = { id: framework.id, name: editName };
    setIsEditing(false);
    await onSaveFramework(updatedFramework);
  }, [editName, framework.id, onSaveFramework]);

  const voteUp = useCallback(() => {
    const newVote: VoteScore = { frameworkId: framework.id, score: 1 };
    void onSaveVote(newVote);
  }, [framework.id, onSaveVote]);
  const voteDown = useCallback(() => {
    const newVote: VoteScore = { frameworkId: framework.id, score: -1 };
    void onSaveVote(newVote);
  }, [framework.id, onSaveVote]);

  return (
    <>
      <td scope="row">
        <span className="pt-2 px-3 d-inline-block">{framework.id}</span>
      </td>
      {!isEditing ? (
        <td className="flex-grow-1" onClick={changeEdit}>
          <button className="btn btn-secondary mt-2 ms-2">
            <i className="fa-solid fa-pencil" />
          </button>
          <span className="pt-2 px-3 d-inline-block">{framework.name}</span>
        </td>
      ) : (
        <td>
          <div className="row">
            <div className="col-auto">
              <button className="btn btn-secondary mt-2 ms-2" onClick={changeEdit}>
                <i className="fa-solid fa-xmark" />
              </button>
            </div>
            <div className="col-auto">
              <form onSubmit={saveEdit}>
                <div className="d-flex">
                  <input value={editName} onChange={e => setEditName(e.target.value)} className="form-control me-2" required maxLength={200} />
                  <button type="submit" className="btn btn-primary mt-2">
                    <i className="fa-solid fa-floppy-disk" />
                  </button>
                </div>
              </form>
            </div>
          </div>
        </td>
      )}
      <td>
        <button className="btn btn-secondary mt-2 ms-2" onClick={voteDown}><i className="fa-solid fa-minus" /></button>
        <span className="pt-2 px-3 d-inline-block">{framework.score}</span>
        <button className="btn btn-secondary mt-2 ms-2" onClick={voteUp}><i className="fa-solid fa-plus" /></button>
      </td>
    </>
  );
}
