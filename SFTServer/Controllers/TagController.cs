using Contracts.CommandModels;
using Contracts.Models;
using Core.Commands;
using Core.Commands.Tags;
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

        [HttpPost]
        public void Create(SimpleNamedModel model)
        {
            var command = new CreateTagCommand();
            command.Run(model);
        }

        [HttpDelete]
        public void Remove(Guid id)
        {
            var command = new RemoveTagCommand();
            command.Run(id);
        }

        [HttpPut]
        public void Merge(MergeTagsCommandModel model)
        {
            var command = new MergeTagsCommand();
            command.Run(model);
        }
    }
}
