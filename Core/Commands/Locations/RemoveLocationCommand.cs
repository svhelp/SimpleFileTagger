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
    public class RemoveLocationCommand : RecoursiveLocationCommandBase<RemoveLocationCommandModel, CommandResultWith<RemoveLocationCommandResultModel>>
    {
        public RemoveLocationCommand(TaggerContext context)
            : base(context)
        {
        }

        public override CommandResultWith<RemoveLocationCommandResultModel> Run(RemoveLocationCommandModel model)
        {
            var location = Context.Locations.FirstOrDefault(l => l.Id == model.LocationId);

            if (location == null)
            {
                throw new Exception("Location not found.");
            }

            var result = new RemoveLocationCommandResultModel();

            if (model.IsRecoursive)
            {
                result.OrphansParent = location.Parent?.Id;
                result.RemovedLocationIds = ProcessRecoursively(location, RemoveLocation, new List<Guid>());
            }
            else
            {
                var parentForOrphans = location.Parent;

                if (parentForOrphans != null)
                {
                    foreach (var child in location.Children)
                    {
                        child.Parent = parentForOrphans;
                    }

                    Context.SaveChanges();

                    result.OrphansParent = parentForOrphans.Id;
                }

                RemoveLocation(location);
                result.RemovedLocationIds = new List<Guid> { location.Id };
            }

            Context.SaveChanges();

            return GetSuccessfulResult(result);
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
