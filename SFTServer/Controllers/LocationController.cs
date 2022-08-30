using Contracts.Models;
using Core.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SFTServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        [HttpGet]
        public TaggerDirectoryInfo Get(string path)
        {
            var query = new GetLocationDataQuery();
            var data = query.Run(path);

            return data;
        }
    }
}
