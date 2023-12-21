using Microsoft.AspNetCore.Mvc;
using SwapiClient.Application;
using SwapiClient.Domain;
using SwapiClient.Swagger.OperationFilter;
using Swashbuckle.AspNetCore.Annotations;

namespace SwapiClient.Controllers
{
    [ApiController]
    [Route("swapi")]
    public class SwapiClientController : ControllerBase
    {
        private readonly ILogger<SwapiClientController> _logger;
        private readonly ISwapiWrapper _swapiWrapper;

        public SwapiClientController(ILogger<SwapiClientController> logger
            , ISwapiWrapper swapiWrapper)
        {
            _logger = logger;
            _swapiWrapper = swapiWrapper;
        }

        [HttpGet("public/characters", Name = "GetCharacters")]
        [SwaggerOperationFilter(typeof(ApiKeyCustomHeaderOperationFilter))]
        public IEnumerable<Domain.PersonBasicInfo> GetCharacters([FromQuery] int page = 1)
        {
            return _swapiWrapper.GetPersonsBasicInfo(page);
        }

        [HttpGet("protected/characters/download", Name = "GetCharactersDownload")]
        [SwaggerOperationFilter(typeof(TokenCustomHeaderOperationFilter))]
        public IEnumerable<PersonBasicInfo> GetCharactersDownload([FromQuery] int page = 1)
        {
            return _swapiWrapper.GetPersonsDetailedInfo(page);
        }
    }
}