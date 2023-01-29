using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CommandModels
{
    public class RemoveLocationCommandModel
    {
        public Guid LocationId { get; set; }

        public bool IsRecoursive { get; set; }
    }
}
