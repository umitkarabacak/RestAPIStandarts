using Microsoft.AspNetCore.Mvc;

namespace RestStandarts.API.Controllers
{
    [ApiController]
    [Route("api/countries/{countryId}/cities/{cityId}/districts")]
    public class DistrictsController : ControllerBase
    {
        private readonly ILogger<DistrictsController> _logger;

        public DistrictsController(ILogger<DistrictsController> logger)
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
        [Route("{districtId}", Name = nameof(GetDistrict))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetDistrict(Guid districtId)
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

            return CreatedAtRoute(nameof(GetDistrict), new
            {
                districtId = Guid.NewGuid(),
            }, new { asdasd = 5 });
        }

        [HttpPut]
        [Route("{districtId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(Guid districtId, [FromBody] IFormCollection updatedRequest)
        {

            return NoContent();
        }

        [HttpDelete]
        [Route("{districtId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(Guid districtId)
        {

            return NoContent();
        }
    }
}