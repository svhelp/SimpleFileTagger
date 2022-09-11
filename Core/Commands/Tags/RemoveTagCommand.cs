using DAL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Tags
{
    public class RemoveTagCommand : CommandBase<Guid, CommandResult>
    {
        public RemoveTagCommand(TaggerContext context)
            : base(context)
        {
        }

        public override CommandResult Run(Guid model)
        {
            var tagToRemove = Context.Tags.FirstOrDefault(t => t.Id == model);

            if (tagToRemove == null)
            {
                return GetErrorResult("Tag does not exist.");
            }

            Context.Tags.Remove(tagToRemove);
            Context.SaveChanges();

            return GetSuccessfulResult();
        }
    }
}
