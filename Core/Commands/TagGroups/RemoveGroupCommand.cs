using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.TagGroups
{
    public class RemoveGroupCommand : CommandBase<Guid, CommandResult>
    {
        public RemoveGroupCommand(TaggerContext context)
            : base(context)
        {
        }

        public override CommandResult Run(Guid model)
        {
            var group = Context.TagGroups.FirstOrDefault(g => g.Id == model);

            if (group == null)
            {
                return GetErrorResult("Tag group not found.");
            }

            Context.TagGroups.Remove(group);
            Context.SaveChanges();

            return GetSuccessfulResult();
        }
    }
}
