using AutoMapper;
using Contracts.Models;
using Contracts.QueryModel;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Queries
{
    public class GetAllLocationsDataQuery : QueryBase<EmptyQueryModel, IEnumerable<TaggerDirectoryInfo>>
    {
        protected IMapper Mapper { get; }

        public GetAllLocationsDataQuery()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CoreMapperProfile>();
            });

            Mapper = mapperConfig.CreateMapper();
        }

        public override IEnumerable<TaggerDirectoryInfo> Run(EmptyQueryModel model)
        {
            using var context = new TaggerContext();

            var locations = context.Locations.AsQueryable();

            return Mapper.Map<List<TaggerDirectoryInfo>>(locations);
        }
    }
}
