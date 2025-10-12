namespace AspireEverything.FrameworkApi.Data;

public interface IFrameworkApiContext : IDisposable
{
    DbSet<Framework> Frameworks { get; }
    void DefeatChangeTracking(object entity);
    int SaveChanges();
}

public class FrameworkApiContext : DbContext, IFrameworkApiContext
{
    public FrameworkApiContext(DbContextOptions<FrameworkApiContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLowerCaseNamingConvention();
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Framework> Frameworks => Set<Framework>();

    public void DefeatChangeTracking(object entity) =>
        this.Entry(entity).State = EntityState.Modified;

}
