using AutoMapper;
using Contracts.Models;
using Contracts.QueryModel;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Queries
{
    public class SearchLocationsQuery : QueryBase<string[], IEnumerable<TaggerDirectoryInfo>>
    {
        public SearchLocationsQuery(TaggerContext context, IMapper mapper)
            : base(mapper, context)
        {
        }

        public override IEnumerable<TaggerDirectoryInfo> Run(string[] model)
        {
            var tags = Context.Tags.Where(t => model.Contains(t.Name)).ToList();

            if (!tags.Any())
            {
                return new List<TaggerDirectoryInfo>();
            }

            var locations = tags.Skip(1)
                .Aggregate(tags[0].Locations,
                    (acc, t) => acc.Intersect(t.Locations).ToList());

            return Mapper.Map<List<TaggerDirectoryInfo>>(locations);
        }
    }
}
