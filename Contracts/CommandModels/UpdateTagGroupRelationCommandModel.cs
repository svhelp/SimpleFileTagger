using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CommandModels
{
    public class UpdateTagGroupRelationCommandModel
    {
        public Guid GroupId { get; set; }

        public Guid TagId { get; set; }
    }
}
