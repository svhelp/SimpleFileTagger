using AutoMapper;
using Contracts.CommandModels;
using Contracts.Models;
using Contracts.Models.Complex;
using Contracts.Models.Plain;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CoreMapperProfile : Profile
    {
        public CoreMapperProfile()
        {
            CreateMap<LocationEntity, UpdateLocationCommandResultModel>();
            CreateMap<TagGroupEntity, UpdateGroupTagsCommandResultModel>();

            CreateMap<LocationEntity, LocationModel>()
                .ForMember(x => x.TagIds, opt => opt.MapFrom(z => z.Tags.Select(t => t.Id)));

            CreateMap<ThumbnailEntity, ThumbnailPlainModel>();
            CreateMap<TagEntity, TagPlainModel>()
                .ForMember(x => x.ThumbnailId, opt => opt.MapFrom(z => z.Thumbnail != null ? z.Thumbnail.Id : default));
            CreateMap<TagGroupEntity, TagGroupPlainModel>()
                .ForMember(x => x.TagIds, opt => opt.MapFrom(z => z.Tags.Select(t => t.Id)));
            CreateMap<LocationEntity, LocationPlainModel>()
                .ForMember(x => x.TagIds, opt => opt.MapFrom(z => z.Tags.Select(t => t.Id)));
        }
    }
}
