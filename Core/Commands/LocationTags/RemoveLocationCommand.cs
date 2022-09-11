using Core.Processors;
using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.LocationTags
{
    public class RemoveLocationCommand : CommandBase<Guid, CommandResult>
    {
        public RemoveLocationCommand(TaggerContext context) : base(context)
        {
        }

        public override CommandResult Run(Guid model)
        {
            var location = Context.Locations.FirstOrDefault(l => l.Id == model);

            if (location == null)
            {
                return GetSuccessfulResult();
            }

            var beaconFileProcessor = new BeaconFileProcessor(location.Path);
            var existingBeaconId = beaconFileProcessor.GetExistingBeacon();

            if (existingBeaconId == model)
            {
                beaconFileProcessor.RemoveBeacon();
            }

            Context.Locations.Remove(location);
            Context.SaveChanges();

            return GetSuccessfulResult();
        }
    }
}
