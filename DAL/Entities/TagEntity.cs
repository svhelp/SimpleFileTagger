using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class TagEntity : EntityBase
    {
        public string Name { get; set; }

        [ForeignKey(nameof(Group))]
        public Guid GroupId { get; set; }

        public virtual TagGroupEntity Group { get; set; }
    }
}
