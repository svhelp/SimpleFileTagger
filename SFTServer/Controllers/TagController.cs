using Contracts.CommandModels;
using Contracts.Models;
using Contracts.Models.Plain;
using Core.Commands;
using Core.Commands.Tags;
using Core.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SFTServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        public TagController(GetTagsQuery getTagsQuery, CreateTagCommand createTagCommand, RemoveTagCommand removeTagCommand, MergeTagsCommand mergeTagsCommand, UpdateTagCommand updateTagCommand)
        {
            GetTagsQuery = getTagsQuery;
            CreateTagCommand = createTagCommand;
            RemoveTagCommand = removeTagCommand;
            MergeTagsCommand = mergeTagsCommand;
            UpdateTagCommand = updateTagCommand;
        }

        private GetTagsQuery GetTagsQuery { get; }
        private CreateTagCommand CreateTagCommand { get; }
        private UpdateTagCommand UpdateTagCommand { get; }
        private RemoveTagCommand RemoveTagCommand { get; }
        private MergeTagsCommand MergeTagsCommand { get; }


        [HttpGet]
        public IEnumerable<TagPlainModel> Get()
        {
            return GetTagsQuery.Run(null);
        }

        [HttpPost]
        public CommandResultWith<Guid> Create(SimpleNamedModel model)
        {
            return CreateTagCommand.Run(model);
        }

        [HttpPut]
        public CommandResult Update(UpdateTagCommandModel model)
        {
            return UpdateTagCommand.Run(model);
        }

        [HttpDelete]
        public CommandResult Remove([FromQuery] Guid id)
        {
            return RemoveTagCommand.Run(id);
        }

        [HttpPut]
        public CommandResult Merge(MergeTagsCommandModel model)
        {
            return MergeTagsCommand.Run(model);
        }
    }
}
