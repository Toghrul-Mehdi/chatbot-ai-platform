using ChatBot.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.API.Controllers;

/// <summary>
/// Controller for health check operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly IAiServiceClient _aiServiceClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="HealthController"/> class.
    /// </summary>
    public HealthController(IAiServiceClient aiServiceClient)
    {
        _aiServiceClient = aiServiceClient;
    }

    /// <summary>
    /// Checks the health of the API and dependent services.
    /// </summary>
    /// <returns>Health status information.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var aiHealthy = await _aiServiceClient.CheckHealthAsync();

        return Ok(new
        {
            status = "ok",
            aiServiceStatus = aiHealthy ? "ok" : "unreachable",
            timestamp = DateTime.UtcNow
        });
    }
}
