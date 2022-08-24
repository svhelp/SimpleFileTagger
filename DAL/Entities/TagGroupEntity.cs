using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class TagGroupEntity : EntityBase
    {
        public virtual ICollection<TagEntity> Tags { get; set; }
    }
}
