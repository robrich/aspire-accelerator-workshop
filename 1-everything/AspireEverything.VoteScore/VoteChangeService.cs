namespace AspireEverything.VoteScore;

public interface IVoteChangeService
{
    Vote ChangeVote(int voteFrameworkId, int changeCount);
}

public class VoteChangeService(IVoteRepository voteRepository) : IVoteChangeService
{
    // FRAGILE: possible race condition
    public Vote ChangeVote(int voteFrameworkId, int changeCount)
    {
        if (voteFrameworkId < 1)
        {
            return new Vote();
        }
        var vote = voteRepository.GetByFrameworkId(voteFrameworkId);
        if (vote == null)
        {
            // FRAGILE: ASSUME: framework exists
            vote = new Vote
            {
                FrameworkId = voteFrameworkId
            };
        }
        vote.Score += changeCount;
        if (vote.Score < 0)
        {
            vote.Score = 0;
        }
        voteRepository.Save(vote);
        return vote;
    }

}
