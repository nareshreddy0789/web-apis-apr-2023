using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HrApi.Controllers;

public class DemoController : ControllerBase
{
    private readonly ILogger<DemoController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IOptions<FeaturesOptions> _options;
    public DemoController(ILogger<DemoController> logger, IConfiguration configuration, IOptions<FeaturesOptions> options)
    {
        _logger = logger;
        _configuration = configuration;
        _options = options;
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

    [HttpGet("/demo2")]
    public async Task<ActionResult> IsFeatureEnabled()
    {
        //var isEnabled = _configuration.GetValue<bool>("features:demo");
        //if (isEnabled)
        //{
        //    return Ok("Enabled");
        //} else
        //{
        //    return Ok("Not Enabled");
        //}

        var isEnabled = _options.Value.demo;
        if( isEnabled )
        {
            return Ok(_options.Value.trueMessage);
        } else
        {
            return Ok(_options.Value.falseMessage);
        }
    }
}
