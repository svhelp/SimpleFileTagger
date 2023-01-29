using Contracts.CommandModels;
using Core.Processors;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Locations
{
    public class RemoveLocationCommand : RecoursiveLocationCommandBase<RemoveLocationCommandModel, CommandResultWith<List<Guid>>>
    {
        public RemoveLocationCommand(TaggerContext context)
            : base(context)
        {
        }

        public override CommandResultWith<List<Guid>> Run(RemoveLocationCommandModel model)
        {
            var location = Context.Locations.FirstOrDefault(l => l.Id == model.LocationId);

            if (location == null)
            {
                return GetSuccessfulResult(new List<Guid> { model.LocationId });
            }

            List<Guid> affectedLocationIds;

            if (model.IsRecoursive)
            {
                affectedLocationIds = ProcessRecoursively(location, RemoveLocation, new List<Guid>());
            }
            else
            {
                RemoveLocation(location);
                affectedLocationIds = new List<Guid> { location.Id };
            }

            Context.SaveChanges();

            return GetSuccessfulResult(affectedLocationIds);
        }

        private void RemoveLocation(LocationEntity location)
        {
            var beaconFileProcessor = new BeaconFileProcessor(location.Path);
            var existingBeaconId = beaconFileProcessor.GetExistingBeacon();

            if (existingBeaconId == location.Id)
            {
                beaconFileProcessor.RemoveBeacon();
            }

            Context.Locations.Remove(location);
        }
    }
}
