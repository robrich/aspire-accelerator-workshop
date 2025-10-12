namespace AspireEverything.VoteData;

public interface IVoteApiContext : IDisposable
{
    DbSet<Vote> Votes { get; }
    void DefeatChangeTracking(object entity);
    int SaveChanges();
}

public class VoteApiContext : DbContext, IVoteApiContext
{
    public VoteApiContext(DbContextOptions<VoteApiContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLowerCaseNamingConvention();
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Vote> Votes => Set<Vote>();

    public void DefeatChangeTracking(object entity) =>
        this.Entry(entity).State = EntityState.Modified;

}
