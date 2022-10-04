using AutoMapper;
using Contracts.Models;
using Contracts.Models.Complex;
using Core.Processors;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Queries
{
    public class GetLocationDataQuery : QueryBase<string, LocationModel>
    {
        public GetLocationDataQuery(IMapper mapper, TaggerContext context)
            : base(mapper, context)
        {
        }

        public override LocationModel Run(string path)
        {
            var existingLocation = Context.Locations.FirstOrDefault(x => x.Path == path);

            if (existingLocation != null)
            {
                return Mapper.Map<LocationModel>(existingLocation);
            }

            var beaconFileProcessor = new BeaconFileProcessor(path);
            var existingId = beaconFileProcessor.GetExistingBeacon();

            if (existingId == null)
            {
                return null;
            }

            existingLocation = Context.Locations.FirstOrDefault(x => x.Id == existingId);

            if (existingLocation == null)
            {
                return null;
            }

            return Mapper.Map<LocationModel>(existingLocation);
        }
    }
}
