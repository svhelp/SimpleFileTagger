using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class LocationEntity : EntityBase
    {
        // Temp field
        public string Path { get; set; }

        public string Name { get; set; }

        [ForeignKey(nameof(Parent))]
        public Guid? ParentId { get; set; }

        public virtual LocationEntity? Parent { get; set; }

        public virtual ICollection<LocationEntity> Children { get; set; }

        public virtual ICollection<TagEntity> Tags { get; set; }
    }
}
