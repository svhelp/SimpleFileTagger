using Contracts.CommandModels;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.TagGroups
{
    public class RemoveTagFromGroupCommand : CommandBase<UpdateGroupCommandModel, CommandResult>
    {
        public RemoveTagFromGroupCommand(TaggerContext context) : base(context)
        {
        }

        public override CommandResult Run(UpdateGroupCommandModel model)
        {
            var group = Context.TagGroups.FirstOrDefault(g => g.Name == model.GroupName);

            if (group == null)
            {
                return GetErrorResult("Tag group not found.");
            }

            foreach (var tag in model.TagIds)
            {
                var tagToRemove = group.Tags.FirstOrDefault(t => t.Id == tag);

                if (tagToRemove == null)
                {
                    return GetErrorResult("Tag not found in the group.");
                }

                group.Tags.Remove(tagToRemove);
            }

            Context.SaveChanges();

            return GetSuccessfulResult();
        }
    }
}
