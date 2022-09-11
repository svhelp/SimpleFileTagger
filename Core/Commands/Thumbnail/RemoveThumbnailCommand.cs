using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Thumbnail
{
    public class RemoveThumbnailCommand : CommandBase<Guid, CommandResult>
    {
        public RemoveThumbnailCommand(TaggerContext context) : base(context)
        {
        }

        public override CommandResult Run(Guid model)
        {
            var tag = Context.Tags.FirstOrDefault(t => t.Id == model);

            if (tag == null)
            {
                return GetErrorResult("Tag does not exist.");
            }

            if (tag.Thumbnail == null)
            {
                return GetErrorResult("Tag does not have a thumbnail.");
            }

            Context.Thumbnails.Remove(tag.Thumbnail);
            Context.SaveChanges();

            return GetSuccessfulResult();
        }
    }
}
