using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CommandModels
{
    public class UpdateGroupTagsCommandResultModel : ModelBase
    {
        public List<SimpleModel> Tags { get; set; }
    }
}
