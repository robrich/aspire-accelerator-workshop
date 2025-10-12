namespace AspireEverything.FrameworkApi.Data;

public interface IFrameworkRepository
{
    Framework? GetById(int id);
    List<Framework> GetAll();
    int Save(Framework framework);
    void Delete(int id);
}

public class FrameworkRepository(IFrameworkApiContext db) : IFrameworkRepository
{
    public Framework? GetById(int id) =>
        db.Frameworks.Find(id);

    public List<Framework> GetAll() => (
        from f in db.Frameworks
        orderby f.Name
        select f
    ).ToList();

    public int Save(Framework framework)
    {
        if (framework.Id == 0)
        {
            db.Frameworks.Add(framework);
        }
        else
        {
            db.Frameworks.Update(framework);
            db.DefeatChangeTracking(framework);
        }
        return db.SaveChanges();
    }

    public void Delete(int id)
    {
        Framework? framework = GetById(id);
        if (framework == null)
        {
            return; // the goal was to delete it, so if it's not there, we succeeded
        }

        db.Frameworks.Remove(framework);
        db.SaveChanges();
    }

}
