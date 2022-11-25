using Contracts.CommandModels;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Locations
{
    public class VerifyLocationsCommand : CommandBase<EmptyCommandModel, CommandResult>
    {
        public VerifyLocationsCommand(TaggerContext context)
            : base(context)
        {
        }

        public override CommandResult Run(EmptyCommandModel model)
        {
            var dbLocations = Context.Locations.AsQueryable().ToList();

            foreach (var location in dbLocations)
            {
                location.NotFound = !Directory.Exists(location.Path);
            }

            Context.SaveChanges();

            return GetSuccessfulResult();
        }
    }
}
