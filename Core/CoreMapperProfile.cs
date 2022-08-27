using AutoMapper;
using Contracts.Models;
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
            CreateMap<LocationEntity, TaggerDirectoryInfo>();
            CreateMap<TagEntity, TagModel>();
            CreateMap<TagGroupEntity, TagGroupModel>();
        }
    }
}
