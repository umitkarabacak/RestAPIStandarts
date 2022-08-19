using Microsoft.AspNetCore.Mvc;

namespace RestStandarts.API.Controllers
{
    [ApiController]
    [Route("api/countries/{countryId}/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;

        public CitiesController(ILogger<CitiesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get()
        {

            return Ok();
        }

        [HttpGet]
        [Route("{cityId}", Name = nameof(GetCity))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetCity(Guid cityId)
        {

            return Ok();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post([FromBody] IFormCollection createdRequest)
        {

            return CreatedAtRoute(nameof(GetCity), new
            {
                countryId = Guid.NewGuid(),
            }, new { asdasd = 5 });
        }

        [HttpPut]
        [Route("{cityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(Guid cityId, [FromBody] IFormCollection updatedRequest)
        {

            return NoContent();
        }

        [HttpDelete]
        [Route("{cityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(Guid cityId)
        {

            return NoContent();
        }
    }
}