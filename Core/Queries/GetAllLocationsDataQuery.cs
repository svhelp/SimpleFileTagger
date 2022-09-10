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
        public GetAllLocationsDataQuery(TaggerContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        private IMapper Mapper { get; }
        private TaggerContext Context { get; }

        public override IEnumerable<TaggerDirectoryInfo> Run(EmptyQueryModel model)
        {
            var locations = Context.Locations.AsQueryable();

            return Mapper.Map<List<TaggerDirectoryInfo>>(locations);
        }
    }
}
