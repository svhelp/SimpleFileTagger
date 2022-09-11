using Contracts.CommandModels;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Thumbnail
{
    public class SetThumbnailCommand : CommandBase<SetThumbnailCommandModel, CommandResultWith<Guid>>
    {
        public SetThumbnailCommand(TaggerContext context) : base(context)
        {
        }

        public override CommandResultWith<Guid> Run(SetThumbnailCommandModel model)
        {
            var tag = Context.Tags.FirstOrDefault(t => t.Id == model.TagId);

            if (tag == null)
            {
                return GetErrorResult("Tag does not exist.");
            }

            if (tag.Thumbnail != null)
            {
                Context.Thumbnails.Remove(tag.Thumbnail);
            }

            var newThumbnail = new ThumbnailEntity
            {
                Image = model.Thumbnail,
            };

            tag.Thumbnail = newThumbnail;

            Context.SaveChanges();

            return GetSuccessfulResult(newThumbnail.Id);
        }
    }
}
