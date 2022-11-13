using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.Plain
{
    public class TagGroupPlainModel : SimpleModel
    {
        public List<Guid> TagIds { get; set; }

        public bool IsRequired { get; set; }
    }
}
