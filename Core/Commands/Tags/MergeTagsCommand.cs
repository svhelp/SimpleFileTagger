using Contracts.CommandModels;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Tags
{
    public class MergeTagsCommand : CommandBase<MergeTagsCommandModel, CommandResult>
    {
        public MergeTagsCommand(TaggerContext Context)
            : base(Context)
        {
        }

        public override CommandResult Run(MergeTagsCommandModel model)
        {
            if (model.TagIds.Count < 2)
            {
                return GetErrorResult("Not enough tags for merge.");
            }

            var mainTag = Context.Tags.FirstOrDefault(t => t.Id == model.MainTagId);

            if (mainTag == null)
            {
                return GetErrorResult("Main tag not found.");
            }

            var tagsIdsToRemove = model.TagIds.Where(id => id != model.MainTagId).ToList();
            var tagsToMerge = Context.Tags.Where(t => tagsIdsToRemove.Contains(t.Id)).ToList();

            foreach (var tag in tagsToMerge)
            {
                foreach (var location in tag.Locations)
                {
                    location.Tags.Remove(tag);
                    location.Tags.Add(mainTag);
                }

                Context.Remove(tag);
            }

            Context.SaveChanges();

            return GetSuccessfulResult();
        }
    }
}
