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
    public class SetTagsCommand : CommandBase<UpdateTagsCommandModel>
    {
        public override void Run(UpdateTagsCommandModel model)
        {
            using var context = new TaggerContext();

            ProcessLocation(context, model.Path, location => SetTags(context, location, model.Tags));
        }

        private void SetTags(TaggerContext context, LocationEntity location, string[] tags)
        {
            var existingTags = context.Tags.Where(t => tags.Contains(t.Name)).ToList();
            var notDuplicatedExistingTags = existingTags.Where(et => !location.Tags.Contains(et)).ToList();
            var newTags = tags
                .Where(t => !existingTags.Any(et => et.Name == t))
                .Select(t => new TagEntity
                {
                    Name = t,
                })
                .ToList();

            location.Tags = location.Tags.Where(t => !tags.Contains(t.Name)).ToList();

            var tagsToAdd = notDuplicatedExistingTags.Concat(newTags);

            foreach (var tag in tagsToAdd)
            {
                location.Tags.Add(tag);
            }
        }
    }
}
