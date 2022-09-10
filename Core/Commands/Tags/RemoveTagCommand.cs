using DAL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Tags
{
    public class RemoveTagCommand : CommandBase<Guid>
    {
        public RemoveTagCommand(TaggerContext context)
            : base(context)
        {
        }

        public override void Run(Guid model)
        {
            var tagToRemove = Context.Tags.FirstOrDefault(t => t.Id == model);

            if (tagToRemove == null)
            {
                throw new ArgumentException("Tag does not exist.");
            }

            Context.Tags.Remove(tagToRemove);
            Context.SaveChanges();
        }
    }
}
