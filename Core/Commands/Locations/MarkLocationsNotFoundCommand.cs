using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Locations
{
    public class MarkLocationsNotFoundCommand : RecoursiveLocationCommandBase<Guid, CommandResultWith<List<Guid>>>
    {
        public MarkLocationsNotFoundCommand(TaggerContext context)
            : base(context)
        {
        }

        public override CommandResultWith<List<Guid>> Run(Guid locationId)
        {
            var notFoundLocation = Context.Locations.FirstOrDefault(l => l.Id == locationId);

            if (notFoundLocation == null)
            {
                return GetErrorResult("Location not found in the database.");
            }

            var affectedLocationIds = ProcessRecoursively(notFoundLocation, MarkNotFound, new List<Guid>());

            Context.SaveChanges();

            return GetSuccessfulResult(affectedLocationIds);
        }

        private void MarkNotFound(LocationEntity location)
        {
            location.NotFound = true;
        }
    }
}
