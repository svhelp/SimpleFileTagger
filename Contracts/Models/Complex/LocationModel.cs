using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.Complex
{
    public class LocationModel : SimpleModel
    {
        public string Path { get; set; }

        public List<LocationModel> Children { get; set; }

        public List<Guid> TagIds { get; set; }
    }
}
