using AutoMapper;
using Contracts.Models;
using Contracts.Models.Complex;
using Contracts.QueryModel;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Queries
{
    public class GetAllLocationsDataQuery : QueryBase<EmptyQueryModel, IEnumerable<LocationModel>>
    {
        public GetAllLocationsDataQuery(TaggerContext context, IMapper mapper)
            : base(mapper, context)
        {
        }

        public override IEnumerable<LocationModel> Run(EmptyQueryModel model)
        {
            var locations = Context.Locations.Where(l => l.ParentId == null).AsQueryable();

            return Mapper.Map<List<LocationModel>>(locations);
        }
    }
}
