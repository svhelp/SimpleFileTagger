using AutoMapper;
using Contracts.Models;
using Core.Processors;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Queries
{
    public class GetLocationDataQuery : QueryBase<string, TaggerDirectoryInfo>
    {
        public GetLocationDataQuery(IMapper mapper, TaggerContext context)
            : base(mapper, context)
        {
        }

        public override TaggerDirectoryInfo Run(string path)
        {
            var existingLocation = Context.Locations.FirstOrDefault(x => x.Path == path);

            if (existingLocation != null)
            {
                return Mapper.Map<TaggerDirectoryInfo>(existingLocation);
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

            return Mapper.Map<TaggerDirectoryInfo>(existingLocation);
        }
    }
}
