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

namespace Core.Commands
{
    public abstract class CommandBase<T>
    {
        protected static void ProcessLocation(TaggerContext context, string path, Action<LocationEntity> processor)
        {
            var beaconFilePath = BeaconFileProcessor.GetDataFilePath(path);

            using var beaconFileProcessor = new BeaconFileProcessor(beaconFilePath);

            var existingId = beaconFileProcessor.GetExistingBeacon();

            LocationEntity location = existingId != null
                ? context.Locations.First(l => l.Id == existingId)
                : new LocationEntity
                {
                    Path = path,
                    Tags = new List<TagEntity>(),
                };

            processor(location);

            if (existingId != null)
            {
                context.SaveChanges();
                return;
            }

            context.Locations.Add(location);
            context.SaveChanges();

            beaconFileProcessor.CreateBeacon(location);
        }

        public abstract void Run(T model);
    }
}
