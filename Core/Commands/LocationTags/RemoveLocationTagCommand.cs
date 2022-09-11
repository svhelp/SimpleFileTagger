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
    public class RemoveLocationTagCommand : LocationTagsCommandBase<UpdateTagsCommandModel, CommandResult>
    {
        public RemoveLocationTagCommand(TaggerContext context)
            : base(context)
        {
        }

        public override CommandResult Run(UpdateTagsCommandModel model)
        {
            ProcessLocation(Context, model.Path, location => RemoveTags(location, model.Tags));

            return GetSuccessfulResult();
        }

        private void RemoveTags(LocationEntity location, string[] tags)
        {
            var tagsToRemove = location.Tags.Where(t => tags.Contains(t.Name));

            foreach (var tagToRemove in tagsToRemove)
            {
                location.Tags.Remove(tagToRemove);
            }
        }
    }
}
