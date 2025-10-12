namespace AspireEverything.FrameworkApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FrameworkController(IFrameworkRepository frameworkRepository, ILogger<FrameworkController> logger) : ControllerBase
{

    // Get all
    [OutputCache(Duration = 1)] // seconds
    [HttpGet]
    public List<Framework> GetAll()
    {
        logger.LogInformation("Getting all frameworks");
        return frameworkRepository.GetAll();
    }

    // Get by id
    [OutputCache(VaryByRouteValueNames = new[] { "id" }, Duration = 1)] // seconds
    [HttpGet("{id}")]
    public ActionResult<Framework> GetById(int id)
    {
        logger.LogInformation($"Getting framework by id {id}");
        Framework? framework = frameworkRepository.GetById(id);
        return framework switch
        {
            null => NotFound(),
            _ => Ok(framework)
        };
    }

    // New
    [HttpPost]
    public ActionResult<Framework> Post([FromBody] Framework model)
    {
        logger.LogInformation($"Creating new framework {model.Name}");
        frameworkRepository.Save(model);
        var url = Url.Action(nameof(GetById), new { id = model.Id });
        return Created(url, model);
    }

    // Update
    [HttpPut("{id}")]
    public ActionResult<Framework> Put(int id, [FromBody] Framework model)
    {
        logger.LogInformation($"Updating framework id {id} to {model.Name}");
        Framework? framework = frameworkRepository.GetById(id);
        if (framework == null)
        {
            return NotFound();
        }
        framework.Name = model.Name;
        frameworkRepository.Save(framework);
        return Ok(framework);
    }

    // Delete
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        logger.LogInformation($"Deleting framework id {id}");
        frameworkRepository.Delete(id);
        return Ok();
    }

}
