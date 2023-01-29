using AutoMapper;
using Contracts.Models;
using Contracts.Models.Complex;
using Contracts.Models.Plain;
using Contracts.QueryModel;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Queries
{
    public class GetAllLocationsDataQuery : QueryBase<EmptyQueryModel, IEnumerable<LocationPlainModel>>
    {
        public GetAllLocationsDataQuery(TaggerContext context, IMapper mapper)
            : base(mapper, context)
        {
        }

        public override IEnumerable<LocationPlainModel> Run(EmptyQueryModel model)
        {
            var locations = Context.Locations.AsQueryable();

            return Mapper.Map<List<LocationPlainModel>>(locations);
        }
    }
}
