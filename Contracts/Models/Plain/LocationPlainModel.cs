using Contracts.Models.Complex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.Plain
{
    public class LocationPlainModel : SimpleModel
    {
        public string Path { get; set; }

        public bool NotFound { get; set; }

        public List<Guid> TagIds { get; set; }
    }
}
