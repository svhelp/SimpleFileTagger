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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        public TagController(GetTagsQuery getTagsQuery, CreateTagCommand createTagCommand, RemoveTagCommand removeTagCommand, MergeTagsCommand mergeTagsCommand)
        {
            GetTagsQuery = getTagsQuery;
            CreateTagCommand = createTagCommand;
            RemoveTagCommand = removeTagCommand;
            MergeTagsCommand = mergeTagsCommand;
        }

        private GetTagsQuery GetTagsQuery { get; }
        private CreateTagCommand CreateTagCommand { get; }
        private RemoveTagCommand RemoveTagCommand { get; }
        private MergeTagsCommand MergeTagsCommand { get; }


        [HttpGet]
        public IEnumerable<TagModel> Get()
        {
            var result = GetTagsQuery.Run(null);

            return result;
        }

        [HttpPost]
        public void Create(SimpleNamedModel model)
        {
            CreateTagCommand.Run(model);
        }

        [HttpDelete]
        public void Remove(Guid id)
        {
            RemoveTagCommand.Run(id);
        }

        [HttpPut]
        public void Merge(MergeTagsCommandModel model)
        {
            MergeTagsCommand.Run(model);
        }
    }
}
