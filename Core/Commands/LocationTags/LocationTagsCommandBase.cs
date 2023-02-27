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
using Core.Constants;
using Contracts.Models.Plain;

namespace Core.Commands.LocationTags
{
    public abstract class LocationTagsCommandBase<T, V>
        : CommandBase<T, V>
        where V : CommandResult, new()
    {
        protected LocationTagsCommandBase(TaggerContext context, IMapper mapper)
            : base(context)
        {
            Mapper = mapper;
            BeaconSettings = context.Settings.FirstOrDefault(s => s.Code == SettingsCodes.Beacons);
        }

        private SettingsEntity BeaconSettings { get; }

        protected IMapper Mapper { get; }

        protected List<LocationPlainModel> ProcessLocation(string path, Action<LocationEntity> processor, bool isRecoursive)
        {
            List<LocationEntity> result;

            var directory = new DirectoryInfo(path);

            if (!directory.Exists)
            {
                throw new Exception("Directory does not exist.");
            }

            var possibleParent = Context.Locations
                .Where(l => path.StartsWith(l.Path))
                .ToList()
                .OrderBy(l => Path.GetFullPath(l.Path).Split(Path.DirectorySeparatorChar).Length)
                .LastOrDefault();

            if (isRecoursive)
            {
                result = ProcessLocationRecoursively(directory, processor, new List<LocationEntity>(), possibleParent);
            } else {
                var processedLocation = ProcessSingleLocation(path, processor, possibleParent);
                result = new List<LocationEntity> { processedLocation };
            }

            return Mapper.Map<List<LocationPlainModel>>(result);
        }

        private List<LocationEntity> ProcessLocationRecoursively(DirectoryInfo directory, Action<LocationEntity> processor, List<LocationEntity> result, LocationEntity parent = null)
        {
            var location = ProcessSingleLocation(directory.FullName, processor, parent);
            result.Add(location);

            foreach (var subDirectory in directory.GetDirectories())
            {
                ProcessLocationRecoursively(subDirectory, processor, result, location);
            }

            return result;
        }


        private LocationEntity ProcessSingleLocation(string path, Action<LocationEntity> processor, LocationEntity parent = null)
        {
            var beaconFileProcessor = new BeaconFileProcessor(path);

            var beaconId = beaconFileProcessor.GetExistingBeacon();
            var location = GetLocationEntity(beaconId, path, parent);

            processor(location);

            Context.SaveChanges();

            if (BeaconSettings != null && BeaconSettings.Enabled && beaconId == null)
            {
                beaconFileProcessor.CreateBeacon(location);
            }

            return location;
        }

        private LocationEntity GetLocationEntity(Guid? beaconId, string path, LocationEntity parent = null)
        {
            var locationName = Path.GetFileName(path);
            var locationByPath = Context.Locations.FirstOrDefault(l => l.Path == path);

            if (locationByPath != null)
            {
                if (beaconId == null || beaconId == locationByPath.Id)
                {
                    return locationByPath;
                }

                var locationById = GetOrCreateLocationById(beaconId, path, locationName, parent);

                locationByPath.Path = string.Empty;

                return locationById;
            }

            return GetOrCreateLocationById(beaconId, path, locationName, parent);
        }

        private LocationEntity GetOrCreateLocationById(Guid? id, string path, string locationName, LocationEntity parent = null)
        {
            var locationById = Context.Locations.FirstOrDefault(l => l.Id == id);

            if (locationById == null)
            {
                //TODO: finish children insertion
                //var children = parent.Children.Where(l => l.Path.StartsWith(path)).ToList();

                locationById = new LocationEntity
                {
                    Parent = parent,
                    //Children = children,
                    Tags = new List<TagEntity>(),
                };

                if (id.HasValue)
                {
                    locationById.Id = id.Value;
                }

                Context.Locations.Add(locationById);
            }

            locationById.Path = path;
            locationById.Name = locationName;

            return locationById;
        }
    }
}
