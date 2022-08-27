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
    public class GetLocationDataQuery
    {
        protected IMapper Mapper { get; }

        public GetLocationDataQuery()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CoreMapperProfile>();
            });

            Mapper = mapperConfig.CreateMapper();
        }

        public TaggerDirectoryInfo Run(string path)
        {
            using var context = new TaggerContext();

            var existingLocation = context.Locations.FirstOrDefault(x => x.Path == path);

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

            existingLocation = context.Locations.FirstOrDefault(x => x.Id == existingId);

            if (existingLocation == null)
            {
                return null;
            }

            return Mapper.Map<TaggerDirectoryInfo>(existingLocation);
        }
    }
}
