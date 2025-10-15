namespace AspireEverything.WebBlazor.Models;

public class Vote
{
    [Key]
    public int Id { get; set; }
    public int FrameworkId { get; set; }
    public int Score { get; set; }
}
