using Contracts.Models;
using Core.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SFTServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        public SearchController(SearchLocationsQuery searchLocationsQuery)
        {
            SearchLocationsQuery = searchLocationsQuery;
        }

        private SearchLocationsQuery SearchLocationsQuery { get; }

        [HttpGet]
        public IEnumerable<TaggerDirectoryInfo> Get([FromQuery] string[] tags)
        {
            return SearchLocationsQuery.Run(tags);
        }
    }
}
