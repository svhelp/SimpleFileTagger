using Contracts.CommandModels;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Tags
{
    public class CreateTagCommand : CommandBase<SimpleNamedModel>
    {
        public override void Run(SimpleNamedModel model)
        {
            using var context = new TaggerContext();

            if (context.Tags.Any(t => t.Name == model.Name))
            {
                throw new ArgumentException("Tag with the name already exists.");
            }

            var newTag = new TagEntity
            {
                Name = model.Name,
            };

            context.Tags.Add(newTag);
            context.SaveChanges();
        }
    }
}
