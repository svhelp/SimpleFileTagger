using Contracts.CommandModels;
using Contracts.Models;
using Core.Commands;
using Core.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SFTServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<TagModel> Get()
        {
            var query = new GetTagsQuery();
            var result = query.Run(null);

            return result;
        }

        [HttpPut]
        public void Add(UpdateTagsCommandModel model)
        {
            var action = new AddTagCommand();
            action.Run(model);
        }

        [HttpPut]
        public void Set(UpdateTagsCommandModel model)
        {
            var action = new SetTagsCommand();
            action.Run(model);
        }

        [HttpPut]
        public void Delete(UpdateTagsCommandModel model)
        {
            var action = new RemoveTagCommand();
            action.Run(model);
        }
    }
}
