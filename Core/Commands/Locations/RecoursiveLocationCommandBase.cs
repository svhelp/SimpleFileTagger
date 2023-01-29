using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Locations
{
    public abstract class RecoursiveLocationCommandBase<T, V>
        : CommandBase<T, V>
        where V : CommandResult, new()
    {
        protected RecoursiveLocationCommandBase(TaggerContext context)
            : base(context)
        {
        }

        protected static List<Guid> ProcessRecoursively(LocationEntity location, Action<LocationEntity> processor, List<Guid> affectedLocationIds)
        {
            affectedLocationIds.Add(location.Id);

            processor(location);

            foreach (var subLocation in location.Children)
            {
                ProcessRecoursively(subLocation, processor, affectedLocationIds);
            }

            return affectedLocationIds;
        }
    }
}
