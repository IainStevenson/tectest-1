using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using tectest1.Api.Domain;


namespace tectest1.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class MeterReadingController : ControllerBase
    {
        private readonly ILogger<MeterReadingController> _logger;
        private readonly IMediator _mediator;   

        public MeterReadingController(
            ILogger<MeterReadingController> logger , 
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        
        [HttpPost]
        [Consumes("application/octet-stream")]
        [Route("meter-reading-uploads")]         
        public async Task<IActionResult> Post()
        {
            _logger.LogInformation("{method} started for meter reading file", nameof(Post));
 
            var body = Request.Body;

            _logger.LogInformation("{method} Reading content for request {id}", nameof(Post), Request.HttpContext?.Connection?.Id ?? "none");

            using StreamReader reader = new(body, Encoding.UTF8);
            var rawFileData = await reader.ReadToEndAsync();

            var response = await _mediator.Send(new RawMeterReadingFilePostRequest() {  Content = rawFileData});

            _logger.LogInformation("{method} returning response", nameof(Post));

            return Ok(response);

        }
    }
}