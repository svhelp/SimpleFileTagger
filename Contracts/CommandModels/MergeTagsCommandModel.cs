using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CommandModels
{
    public class MergeTagsCommandModel
    {
        public Guid MainTagId { get; set; }

        public List<Guid> TagIds { get; set; }
    }
}
