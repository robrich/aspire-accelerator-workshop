namespace AspireEverything.VoteGet;

public class VoteGetFunction(ILogger<VoteGetFunction> logger, IVoteRepository voteRepository)
{
    [OutputCache(VaryByRouteValueNames = new[] { "id" }, Duration = 1)] // seconds
    [Function("VoteGet")]
    [OpenApiOperation(operationId: "Run")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "vote/get")] HttpRequest req)
    {
        logger.LogInformation("Get all votes");
        var votes = voteRepository.GetAll();
        return new OkObjectResult(votes) { ContentTypes = { "application/json" } };
    }
}
