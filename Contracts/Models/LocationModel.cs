using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    public class LocationModel : ModelBase
    {
        public string Path { get; set; }

        public string Name { get; set; }

        public List<SimpleModel> Tags { get; set; }
    }
}
