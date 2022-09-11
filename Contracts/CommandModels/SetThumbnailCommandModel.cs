using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CommandModels
{
    public class SetThumbnailCommandModel
    {
        public Guid TagId { get; set; }

        public byte[] Thumbnail { get; set; }
    }
}
