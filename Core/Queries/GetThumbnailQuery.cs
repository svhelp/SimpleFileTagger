﻿using AutoMapper;
using Contracts.Models.Plain;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Queries
{
    public class GetThumbnailQuery : QueryBase<Guid, ThumbnailPlainModel>
    {
        public GetThumbnailQuery(IMapper mapper, TaggerContext context) : base(mapper, context)
        {
        }

        public override ThumbnailPlainModel Run(Guid model)
        {
            var thumbnail = Context.Thumbnails.FirstOrDefault(t => t.TagId == model);

            if (thumbnail == null)
            {
                return null;
            }

            return Mapper.Map<ThumbnailPlainModel>(thumbnail);
        }
    }
}
