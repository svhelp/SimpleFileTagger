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

namespace Core.Commands.LocationTags
{
    public class SetLocationTagsCommand : LocationTagsCommandBase<UpdateTagsCommandModel>
    {
        public SetLocationTagsCommand(TaggerContext context)
            : base(context)
        {
        }

        public override void Run(UpdateTagsCommandModel model)
        {
            ProcessLocation(Context, model.Path, location => SetTags(location, model.Tags));
        }

        private void SetTags(LocationEntity location, string[] tags)
        {
            var existingTags = Context.Tags.Where(t => tags.Contains(t.Name)).ToList();
            var notDuplicatedExistingTags = existingTags.Where(et => !location.Tags.Contains(et)).ToList();
            var newTags = tags
                .Where(t => !existingTags.Any(et => et.Name == t))
                .Select(t => new TagEntity
                {
                    Name = t,
                })
                .ToList();

            var tagsToRemove = location.Tags.Where(t => !tags.Contains(t.Name));

            foreach (var tagToRemove in tagsToRemove)
            {
                location.Tags.Remove(tagToRemove);
            }

            var tagsToAdd = notDuplicatedExistingTags.Concat(newTags);

            foreach (var tag in tagsToAdd)
            {
                location.Tags.Add(tag);
            }
        }
    }
}
