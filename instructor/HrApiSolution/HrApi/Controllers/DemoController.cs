using Microsoft.AspNetCore.Mvc;

namespace HrApi.Controllers;

public class DemoController : ControllerBase
{
    private readonly ILogger<DemoController> _logger;

    public DemoController(ILogger<DemoController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/demo")]
    public async Task<ActionResult> DoIt(CancellationToken token)
    {
        var timeStarted = DateTime.Now;
        _logger.LogInformation($"About to make that big hairy call {timeStarted.Millisecond}");
        // so I'm going to start some big long running async call, like running a stored procedure in DB2,
        // calling into the USPS API to validate an address, etc.
        await Task.Delay(3000, token);
        _logger.LogInformation($"Finshed The Call that big hairy call {DateTime.Now.Millisecond}");
        return Ok($"All Done. Thanks (the one that started at {timeStarted.Millisecond}");

    }
}
