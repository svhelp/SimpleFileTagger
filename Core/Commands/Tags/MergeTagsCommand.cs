using Contracts.CommandModels;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Tags
{
    public class MergeTagsCommand : CommandBase<MergeTagsCommandModel, CommandResultWith<Guid>>
    {
        public MergeTagsCommand(TaggerContext Context)
            : base(Context)
        {
        }

        public override CommandResultWith<Guid> Run(MergeTagsCommandModel model)
        {
            if (model.TagIds.Count < 2)
            {
                return GetErrorResult("Not enough tags for merge.");
            }

            var tagsToMerge = Context.Tags.Where(t => model.TagIds.Contains(t.Id)).ToList();

            var tagGroups = tagsToMerge.Select(t => t.GroupId).Distinct();

            if (tagGroups.Count() > 1)
            {
                return GetErrorResult("Impossible to merge tags with different groups.");
            }

            var firstTag = tagsToMerge.First();

            foreach (var tag in tagsToMerge)
            {
                foreach (var location in tag.Locations)
                {
                    location.Tags.Remove(tag);
                    location.Tags.Add(firstTag);
                }
            }

            Context.SaveChanges();

            return GetSuccessfulResult(firstTag.Id);
        }
    }
}
