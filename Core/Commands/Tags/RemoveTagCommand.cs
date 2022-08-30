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
        public override void Run(Guid model)
        {
            using var context = new TaggerContext();

            var tagToRemove = context.Tags.FirstOrDefault(t => t.Id == model);

            if (tagToRemove == null)
            {
                throw new ArgumentException("Tag does not exist.");
            }

            context.Tags.Remove(tagToRemove);
            context.SaveChanges();
        }
    }
}
