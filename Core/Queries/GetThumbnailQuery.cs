using AutoMapper;
using Contracts.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Queries
{
    public class GetThumbnailQuery : QueryBase<Guid, ThumbnailModel>
    {
        public GetThumbnailQuery(IMapper mapper, TaggerContext context) : base(mapper, context)
        {
        }

        public override ThumbnailModel Run(Guid model)
        {
            var thumbnail = Context.Thumbnails.FirstOrDefault(t => t.Id == model);

            if (thumbnail == null)
            {
                throw new Exception("Thumbnail not found");
            }

            return Mapper.Map<ThumbnailModel>(thumbnail);
        }
    }
}
