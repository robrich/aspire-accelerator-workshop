namespace AspireEverything.VoteAdd;

public class VoteScoreFunction(ILogger<VoteScoreFunction> logger, IVoteChangeService voteChangeService)
{
    [Function("VoteScore")]
    [OpenApiOperation(operationId: "Run")]
    [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "The framework id")]
    [OpenApiRequestBody("application/json", typeof(VotePostModel))]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "vote/score/{id:int}")] HttpRequest req, int? id)
    {
        if (id < 1)
        {
            return new BadRequestObjectResult(new { message = "Please set the framework id"});
        }

        VotePostModel? model = await GetRequestBody<VotePostModel>(req.Body);
        if (model == null)
        {
            return new BadRequestObjectResult("Missing or invalid body.");
        }
        if (model == null || model.Score == 0 || model.Score < -1 || model.Score > 1)
        {
            return new BadRequestObjectResult(new { message = "Please set the post body's score to -1 or 1" });
        }

        logger.LogInformation($"Update vote for framework id {id} by {model.Score}");
        var vote = voteChangeService.ChangeVote(id ?? 0, model.Score);

        return new OkObjectResult(vote) { ContentTypes = { "application/json" } };
    }

    // because there's no model binding in Azure Functions so [FromBody] doesn't work
    private async Task<T?> GetRequestBody<T>(Stream body)
    {
        string requestBody = await new StreamReader(body).ReadToEndAsync();
        try
        {
            return JsonSerializer.Deserialize<T>(requestBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (JsonException ex)
        {
            logger.LogWarning(ex, "Invalid JSON payload: " + requestBody);
            return default;
        }
    }
}
