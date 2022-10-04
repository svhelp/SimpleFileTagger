using Contracts.CommandModels;
using Contracts.Models;
using Contracts.Models.Plain;
using Core.Commands;
using Core.Commands.TagGroups;
using Core.Commands.Tags;
using Core.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SFTServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TagGroupController : ControllerBase
    {
        public TagGroupController(AddOrUpdateGroupCommand addOrUpdateGroupCommand, RemoveGroupCommand removeGroupCommand, RemoveTagFromGroupCommand removeTagFromGroupCommand, GetTagGroupsQuery getTagGroupsQuery, AddTagToGroupCommand addTagToGroupCommand)
        {
            AddOrUpdateGroupCommand = addOrUpdateGroupCommand;
            RemoveGroupCommand = removeGroupCommand;
            RemoveTagFromGroupCommand = removeTagFromGroupCommand;
            GetTagGroupsQuery = getTagGroupsQuery;
            AddTagToGroupCommand = addTagToGroupCommand;
        }

        private AddOrUpdateGroupCommand AddOrUpdateGroupCommand { get; }
        private RemoveGroupCommand RemoveGroupCommand { get; }
        private AddTagToGroupCommand AddTagToGroupCommand { get; }
        private RemoveTagFromGroupCommand RemoveTagFromGroupCommand { get; }
        private GetTagGroupsQuery GetTagGroupsQuery { get; }

        [HttpGet]
        public IEnumerable<TagGroupPlainModel> Get()
        {
            return GetTagGroupsQuery.Run(null);
        }

        [HttpPut]
        public CommandResultWith<UpdateGroupTagsCommandResultModel> Update(UpdateGroupCommandModel model)
        {
            return AddOrUpdateGroupCommand.Run(model);
        }

        [HttpDelete]
        public CommandResult Remove([FromQuery] Guid id)
        {
            return RemoveGroupCommand.Run(id);
        }

        [HttpPut]
        public CommandResult AddTag(UpdateTagGroupRelationCommandModel model)
        {
            return AddTagToGroupCommand.Run(model);
        }

        [HttpPut]
        public CommandResult RemoveTag(UpdateTagGroupRelationCommandModel model)
        {
            return RemoveTagFromGroupCommand.Run(model);
        }
    }
}
