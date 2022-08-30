using Contracts.CommandModels;
using Contracts.Models;
using Core.Commands;
using Core.Commands.LocationTags;
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

        [HttpPut]
        public void AddTag(UpdateTagsCommandModel model)
        {
            var action = new AddLocationTagCommand();
            action.Run(model);
        }

        [HttpPut]
        public void SetTags(UpdateTagsCommandModel model)
        {
            var action = new SetLocationTagsCommand();
            action.Run(model);
        }

        [HttpPut]
        public void RemoveTag(UpdateTagsCommandModel model)
        {
            var action = new RemoveLocationTagCommand();
            action.Run(model);
        }
    }
}
