using Contracts.CommandModels;
using Contracts.Models;
using Contracts.QueryModel;
using Core.Commands;
using Core.Commands.LocationTags;
using Core.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SFTServer.Controllers
{
    [Route("api/[controller]/[action]")]
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

        [HttpGet]
        public IEnumerable<TaggerDirectoryInfo> All()
        {
            var query = new GetAllLocationsDataQuery();
            var data = query.Run(new EmptyQueryModel());

            return data;
        }

        [HttpPut]
        public void AddTags(UpdateTagsCommandModel model)
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
        public void RemoveTags(UpdateTagsCommandModel model)
        {
            var action = new RemoveLocationTagCommand();
            action.Run(model);
        }
    }
}
