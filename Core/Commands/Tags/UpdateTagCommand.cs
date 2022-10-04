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
    public class UpdateTagCommand : CommandBase<SimpleModel, CommandResult>
    {
        public UpdateTagCommand(TaggerContext context) : base(context)
        {
        }

        public override CommandResult Run(SimpleModel model)
        {
            var tagToUpdate = Context.Tags.FirstOrDefault(t => t.Id == model.Id);

            if (tagToUpdate == null)
            {
                return GetErrorResult("Tag not found.");
            }

            tagToUpdate.Name = model.Name;

            Context.SaveChanges();

            return GetSuccessfulResult();
        }
    }
}
