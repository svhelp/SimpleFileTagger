using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.Plain
{
    public class TagPlainModel : SimpleModel
    {
        public Guid GroupId { get; set; }

        public Guid ThumbnailId { get; set; }
    }
}
