using Contracts.CommandModels;
using Contracts.Models;
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
        public TagGroupController(AddTagToGroupCommand addTagToGroupCommand, RemoveGroupCommand removeGroupCommand, RemoveTagFromGroupCommand removeTagFromGroupCommand, GetTagGroupsQuery getTagGroupsQuery)
        {
            AddTagToGroupCommand = addTagToGroupCommand;
            RemoveGroupCommand = removeGroupCommand;
            RemoveTagFromGroupCommand = removeTagFromGroupCommand;
            GetTagGroupsQuery = getTagGroupsQuery;
        }

        private AddTagToGroupCommand AddTagToGroupCommand { get; }
        private RemoveGroupCommand RemoveGroupCommand { get; }
        private RemoveTagFromGroupCommand RemoveTagFromGroupCommand { get; }
        private GetTagGroupsQuery GetTagGroupsQuery { get; }

        [HttpGet]
        public IEnumerable<TagGroupModel> Get()
        {
            return GetTagGroupsQuery.Run(null);
        }

        [HttpPut]
        public CommandResultWith<UpdateLocationCommandModel> Add(UpdateGroupCommandModel model)
        {
            return AddTagToGroupCommand.Run(model);
        }

        [HttpDelete]
        public CommandResult Remove([FromQuery] Guid id)
        {
            return RemoveGroupCommand.Run(id);
        }

        [HttpPut]
        public CommandResult RemoveTag(UpdateGroupCommandModel model)
        {
            return RemoveTagFromGroupCommand.Run(model);
        }
    }
}
