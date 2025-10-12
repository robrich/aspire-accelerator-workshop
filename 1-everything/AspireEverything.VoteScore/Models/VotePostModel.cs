namespace AspireEverything.VoteScore.Models;

public class VotePostModel
{
    /// <summary>
    /// Specify to add or remove a vote: +1 or -1 respectively
    /// </summary>
    [Range(-1, 1)]
    public int Score { get; set; }
}
