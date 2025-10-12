namespace AspireEverything.VoteData;

public class Vote
{
    [Key]
    public int Id { get; set; }
    public int FrameworkId { get; set; }
    public int Score { get; set; }
}
