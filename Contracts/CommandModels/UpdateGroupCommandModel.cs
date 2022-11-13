using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CommandModels
{
    public class UpdateGroupCommandModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsRequired { get; set; }

        public List<Guid> TagIds { get; set; }
    }
}
