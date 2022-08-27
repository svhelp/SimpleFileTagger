using Contracts.CommandModels;
using Contracts.Models;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Commands
{
    public class RemoveTagCommand : CommandBase<UpdateTagsCommandModel>
    {
        public override void Run(UpdateTagsCommandModel model)
        {
            using var context = new TaggerContext();

            ProcessLocation(context, model.Path, location => RemoveTags(context, location, model.Tags));
        }

        private void RemoveTags(TaggerContext context, LocationEntity location, string[] tags)
        {
            var tagsToRemove = location.Tags.Where(t => tags.Contains(t.Name));

            foreach (var tagToRemove in tagsToRemove)
            {
                location.Tags.Remove(tagToRemove);
            }
        }
    }
}
