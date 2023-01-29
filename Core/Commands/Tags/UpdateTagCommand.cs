using Contracts.CommandModels;
using Contracts.Models;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Tags
{
    public class UpdateTagCommand : CommandBase<UpdateTagCommandModel, CommandResult>
    {
        public UpdateTagCommand(TaggerContext context) : base(context)
        {
        }

        public override CommandResult Run(UpdateTagCommandModel model)
        {
            var tagToUpdate = Context.Tags.FirstOrDefault(t => t.Id == model.Id);

            if (tagToUpdate == null)
            {
                return GetErrorResult("Tag not found.");
            }

            tagToUpdate.Name = model.Name;

            TagGroupEntity? group = null;

            if (model.GroupId.HasValue)
            {
                group = Context.TagGroups.FirstOrDefault(gr => gr.Id == model.GroupId);

                if (group == null)
                {
                    return GetErrorResult("Group not found.");
                }
            }

            tagToUpdate.Group = group;

            Context.SaveChanges();

            return GetSuccessfulResult();
        }
    }
}
