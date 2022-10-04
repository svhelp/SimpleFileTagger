using Contracts.CommandModels;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.TagGroups
{
    public class AddTagToGroupCommand : CommandBase<UpdateTagGroupRelationCommandModel, CommandResult>
    {
        public AddTagToGroupCommand(TaggerContext context) : base(context)
        {
        }

        public override CommandResult Run(UpdateTagGroupRelationCommandModel model)
        {
            var group = Context.TagGroups.FirstOrDefault(g => g.Id == model.GroupId);

            if (group == null)
            {
                return GetErrorResult("Tag group not found.");
            }

            var tagToAdd = Context.Tags.FirstOrDefault(t => t.Id == model.TagId);

            if (tagToAdd == null)
            {
                return GetErrorResult("Tag with the Id does not exist.");
            }

            group.Tags.Add(tagToAdd);

            Context.SaveChanges();

            return GetSuccessfulResult();
        }
    }
}
