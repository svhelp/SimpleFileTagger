using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Locations
{
    public class MarkLocationsNotFoundCommand : CommandBase<List<Guid>, CommandResult>
    {
        public MarkLocationsNotFoundCommand(TaggerContext context)
            : base(context)
        {
        }

        public override CommandResult Run(List<Guid> locationIds)
        {
            var locationsToMark = Context.Locations.Where(l => locationIds.Contains(l.Id)).ToList();

            foreach(var location in locationsToMark)
            {
                location.NotFound = true;
            }

            Context.SaveChanges();

            return GetSuccessfulResult();
        }
    }
}
