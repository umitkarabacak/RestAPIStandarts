using Microsoft.AspNetCore.Mvc;

namespace RestStandarts.API.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly ILogger<CountriesController> _logger;
        private static List<Country> Countries;
        //https://en.wikipedia.org/wiki/List_of_ISO_3166_country_codes

        public CountriesController(ILogger<CountriesController> logger)
        {
            _logger = logger;
            if (Countries is null)
            {
                Countries = new List<Country>
                {
                    new Country { Id=Guid.Parse("6AD0FE84-1255-4EB0-BEEA-E259D1055495"), NumericCode="008", Name="Albania", AlphaCode2="AL", AlphaCode3="ALB", Description="The Republic of Albania" },
                    new Country { Id=Guid.Parse("5F2BC6BE-1655-49E8-AEBD-A43E3D501A06"), NumericCode="076", Name="Brazil", AlphaCode2="BR", AlphaCode3="BRA", Description="The Federative Republic of Brazil" },
                    new Country { Id=Guid.Parse("7F8B45F6-0CA1-47E2-80B0-5DB38384155F"), NumericCode="792", Name="Türkiye", AlphaCode2="TR", AlphaCode3="TUR", Description="The Republic of Turkey" },
                };
            }
        }

        /// <summary>
        /// Get country list
        /// </summary>
        /// <remarks>
        /// Sample Request
        /// 
        ///     GET     /api/countries
        ///     
        /// </remarks>
        /// <response code="200">Returns the country item list</response>
        /// <response code="400">Incorrect request content</response>
        /// <returns>Country List Items</returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<CountryListItem>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get()
        {
            var countries = Countries.Select(country => new CountryListItem
            {
                Id = country.Id,
                NumericCode = country.NumericCode,
                Name = country.Name,
                AlphaCode2 = country.AlphaCode2,
            });

            return await Task.FromResult(
                Ok(countries)
            );
        }

        /// <summary>
        /// Get country detail with countryId. (countryId is member item identifier)
        /// </summary>
        /// <param name="countryId">Country Id</param>
        /// <remarks>
        /// Sample Request
        /// 
        ///     GET /api/countries/:id
        ///     
        /// </remarks>
        /// <response code="200">Return the country detail</response>
        /// <response code="400">Incorrect request content</response>
        /// <response code="404">if the country item is null</response>
        /// <returns>Country Item Detail</returns>
        [HttpGet]
        [Route("{countryId:guid}", Name = nameof(GetCountry))]
        [ProducesResponseType(typeof(CountryDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetCountry(Guid countryId)
        {
            var currentCountry = Countries.FirstOrDefault(c =>
                c.Id.Equals(countryId)
            );

            if (currentCountry is null)
            {
                var notFoundErrorText = $"No records found matching your search criteria!\nSearch country Id :{countryId}";
                return NotFound(notFoundErrorText);
            }

            var country = new CountryDetail
            {
                Id = currentCountry.Id,
                AlphaCode2 = currentCountry.AlphaCode2,
                AlphaCode3 = currentCountry.AlphaCode3,
                Name = currentCountry.Name,
                NumericCode = currentCountry.NumericCode,
                Description = currentCountry.Description
            };

            return await Task.FromResult(
              Ok(country)
            );
        }

        /// <summary>
        /// Create a country.
        /// </summary>
        /// <param name="insertRequest">New country insert request</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/countries
        ///     {
        ///        "numericCode": "000",
        ///        "alphaCode2": "AA",
        ///        "alphaCode3": "AAA",
        ///        "name": "Name",
        ///        "description": "Description"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">Incorrect request content</response>
        /// <response code="404">If the country item is null</response>
        /// <returns>A newly created country</returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(CountryDetail), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post([FromBody] CountryInsert insertRequest)
        {
            var badRequestErrorText = string.Empty;

            if (!ModelState.IsValid)
            {
                //https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ModelState.Values.SelectMany(i => i.Errors).ToList().ForEach(error =>
                {
                    badRequestErrorText += $"Error message key: {error.ErrorMessage}, detail {error.Exception}\n";
                });

                return BadRequest(badRequestErrorText);
            }

            var anyNumericCode = Countries.Any(c => c.NumericCode.Equals(insertRequest.NumericCode));
            if (anyNumericCode)
            {
                badRequestErrorText = $"Numeric code is using another records!\nNumeric code is: '{insertRequest.NumericCode}'";
                return BadRequest(badRequestErrorText);
            }

            var anyAlphaCode2 = Countries.Any(c => c.AlphaCode2.Equals(insertRequest.AlphaCode2));
            if (anyAlphaCode2)
            {
                badRequestErrorText = $"Alpha code2 is using another records!\nAlpha code2 is: '{insertRequest.AlphaCode2}'";
                return BadRequest(badRequestErrorText);
            }

            var anyAlphaCode3 = Countries.Any(c => c.AlphaCode3.Equals(insertRequest.AlphaCode3));
            if (anyNumericCode)
            {
                badRequestErrorText = $"Alpha code3 is using another records!\nAlpha code3 is: '{insertRequest.AlphaCode3}'";
                return BadRequest(badRequestErrorText);
            }

            var country = new Country
            {
                Id = Guid.NewGuid(),
                AlphaCode2 = insertRequest.AlphaCode2,
                AlphaCode3 = insertRequest.AlphaCode3,
                Name = insertRequest.Name,
                NumericCode = insertRequest.NumericCode,
                Description = insertRequest.Description,
            };

            Countries.Add(country);

            return await Task.FromResult(
                CreatedAtRoute(nameof(GetCountry), new { countryId = country.Id, }, country)
            );
        }

        /// <summary>
        /// Update a country.
        /// </summary>
        /// <param name="countryId">Country Id</param>
        /// <param name="updatedRequest">Current country update request</param>        
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/countries/:countryId
        ///     {
        ///        "numericCode": "000",
        ///        "alphaCode2": "AA",
        ///        "alphaCode3": "AAA",
        ///        "name": "Name",
        ///        "description": "Description"
        ///     }
        ///
        /// </remarks>
        /// <response code="204"></response>
        /// <response code="400">Incorrect request content</response>
        /// <response code="404">If the country item is null</response>
        /// <returns></returns>
        [HttpPut]
        [Route("{countryId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(Guid countryId, [FromBody] CountryUpdate updatedRequest)
        {
            var badRequestErrorText = string.Empty;

            if (!countryId.Equals(updatedRequest.Id))
            {
                badRequestErrorText = $"There is an inconsistency in the submitted information!\n{countryId}/{updatedRequest.Id}";
                return BadRequest(badRequestErrorText);
            }

            if (!ModelState.IsValid)
            {
                //https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ModelState.Values.SelectMany(i => i.Errors).ToList().ForEach(error =>
                {
                    badRequestErrorText += $"Error message key: {error.ErrorMessage}, detail {error.Exception}\n";
                });

                return BadRequest(badRequestErrorText);
            }

            var currentCountry = Countries.FirstOrDefault(c =>
               c.Id.Equals(countryId)
            );

            if (currentCountry is null)
            {
                var notFoundErrorText = $"No records found matching your search criteria!\nSearch country Id :{countryId}";
                return NotFound(notFoundErrorText);
            }

            var numericCodeRecord = Countries.FirstOrDefault(c => c.NumericCode.Equals(updatedRequest.NumericCode));
            if (numericCodeRecord is not null && numericCodeRecord.Id != currentCountry.Id)
            {
                badRequestErrorText = $"Numeric code is using another records!\nNumeric code is: '{updatedRequest.NumericCode}'";
                return BadRequest(badRequestErrorText);
            }

            var alphaCode2Record = Countries.FirstOrDefault(c => c.AlphaCode2.Equals(updatedRequest.AlphaCode2));
            if (alphaCode2Record is not null && alphaCode2Record.Id != currentCountry.Id)
            {
                badRequestErrorText = $"Alpha code2 is using another records!\nAlpha code2 is: '{updatedRequest.AlphaCode2}'";
                return BadRequest(badRequestErrorText);
            }

            var alphaCode3Record = Countries.FirstOrDefault(c => c.AlphaCode3.Equals(updatedRequest.AlphaCode3));
            if (alphaCode3Record is not null && alphaCode3Record.Id != currentCountry.Id)
            {
                badRequestErrorText = $"Alpha code3 is using another records!\nAlpha code3 is: '{updatedRequest.AlphaCode3}'";
                return BadRequest(badRequestErrorText);
            }

            currentCountry.Name = updatedRequest.Name;
            currentCountry.AlphaCode2 = updatedRequest.AlphaCode2;
            currentCountry.AlphaCode3 = updatedRequest.AlphaCode3;
            currentCountry.NumericCode = updatedRequest.NumericCode;
            currentCountry.Description = updatedRequest.Description;

            return await Task.FromResult(
                NoContent()
            );
        }

        /// <summary>
        /// Delete country
        /// </summary>
        /// <param name="countryId">Country Id</param>
        /// <response code="204"></response>
        /// <response code="400">Incorrect request content</response>
        /// <response code="404">If the country item is null</response>
        /// <response code="409">Last list element cannot be deleted</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("{countryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(Guid countryId)
        {
            var currentCountry = Countries.FirstOrDefault(c =>
                c.Id.Equals(countryId)
            );

            if (currentCountry is null)
            {
                var notFoundErrorText = $"No records found matching your search criteria!\nSearch country Id :{countryId}";
                return NotFound(notFoundErrorText);
            }

            // this case is example
            if (Countries.Count == 1)
            {
                var conflictText = $"This record could not be deletedbecause there must be at least one record from the list!";

                return Conflict();
            }

            Countries.Remove(currentCountry);

            return await Task.FromResult(
                NoContent()
            );
        }
    }
}