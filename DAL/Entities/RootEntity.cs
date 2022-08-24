using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class RootEntity : EntityBase
    {
        public string Path { get; set; }

        [ForeignKey(nameof(RootLocation))]
        public Guid RootLocationId { get; set; }

        public virtual LocationEntity RootLocation { get; set; }
    }
}
