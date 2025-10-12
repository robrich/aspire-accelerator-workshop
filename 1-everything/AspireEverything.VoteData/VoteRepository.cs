namespace AspireEverything.VoteData;

public interface IVoteRepository
{
    Vote? GetById(int id);
    Vote? GetByFrameworkId(int id);
    List<Vote> GetAll();
    int Save(Vote Vote);
}

public class VoteRepository(IVoteApiContext db) : IVoteRepository
{
    public Vote? GetById(int id) =>
        db.Votes.Find(id);

    public Vote? GetByFrameworkId(int id) => (
        from f in db.Votes
        where f.FrameworkId == id
        select f
    ).FirstOrDefault();

    public List<Vote> GetAll() => (
        from f in db.Votes
        select f
    ).ToList();
    
    public int Save(Vote Vote)
    {
        if (Vote.Id == 0)
        {
            db.Votes.Add(Vote);
        }
        else
        {
            db.Votes.Update(Vote);
            db.DefeatChangeTracking(Vote);
        }
        return db.SaveChanges();
    }

}
