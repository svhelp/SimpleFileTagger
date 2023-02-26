using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Models.Plain;

namespace Contracts.CommandModels
{
    public class UpdateLocationCommandResultModel
    {
        public List<LocationPlainModel> Locations { get; set; }

        public List<TagPlainModel> CreatedTags { get; set; }
    }
}
