using AutoMapper;
using DAL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Core.Processors;

namespace Core.Commands.LocationTags
{
    public abstract class LocationTagsCommandBase<T> : CommandBase<T>
    {
        protected static void ProcessLocation(TaggerContext context, string path, Action<LocationEntity> processor)
        {
            var beaconFileProcessor = new BeaconFileProcessor(path);

            var beaconId = beaconFileProcessor.GetExistingBeacon();
            var location = GetLocationEntity(context, beaconId, path);

            processor(location);

            context.SaveChanges();

            if (beaconId == null)
            {
                beaconFileProcessor.CreateBeacon(location);
            }
        }

        private static LocationEntity GetLocationEntity(TaggerContext context, Guid? beaconId, string path)
        {
            var locationName = Path.GetFileName(path);
            var locationByPath = context.Locations.FirstOrDefault(l => l.Path == path);

            if (locationByPath != null)
            {
                if (beaconId == null || beaconId == locationByPath.Id)
                {
                    return locationByPath;
                }

                var locationById = GetOrCreateLocationById(context, beaconId, path, locationName);

                locationByPath.Path = string.Empty;

                return locationById;
            }

            return GetOrCreateLocationById(context, beaconId, path, locationName);
        }

        private static LocationEntity GetOrCreateLocationById(TaggerContext context, Guid? id, string path, string locationName)
        {
            var locationById = context.Locations.FirstOrDefault(l => l.Id == id);

            if (locationById == null)
            {
                locationById = new LocationEntity
                {
                    Tags = new List<TagEntity>(),
                };

                if (id.HasValue)
                {
                    locationById.Id = id.Value;
                }

                context.Locations.Add(locationById);
            }

            locationById.Path = path;
            locationById.Name = locationName;

            return locationById;
        }
    }
}
