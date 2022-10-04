using Contracts.CommandModels;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.TagGroups
{
    public class RemoveTagFromGroupCommand : CommandBase<UpdateTagGroupRelationCommandModel, CommandResult>
    {
        public RemoveTagFromGroupCommand(TaggerContext context) : base(context)
        {
        }

        public override CommandResult Run(UpdateTagGroupRelationCommandModel model)
        {
            var group = Context.TagGroups.FirstOrDefault(g => g.Id == model.GroupId);

            if (group == null)
            {
                return GetErrorResult("Tag group not found.");
            }

            var tagToRemove = group.Tags.FirstOrDefault(t => t.Id == model.TagId);

            if (tagToRemove == null)
            {
                return GetErrorResult("Tag not found in the group.");
            }

            group.Tags.Remove(tagToRemove);

            Context.SaveChanges();

            return GetSuccessfulResult();
        }
    }
}
