using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    internal class TagGroupEntity : EntityBase
    {
        public virtual ICollection<TagEntity> Tags { get; set; }
    }
}
