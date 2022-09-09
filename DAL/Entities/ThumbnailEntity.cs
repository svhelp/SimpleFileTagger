using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ThumbnailEntity : EntityBase
    {
        public byte[] Image { get; set; }

        [ForeignKey(nameof(Tag))]
        public Guid TagId { get; set; }

        public virtual TagEntity Tag { get; set; }
    }
}
