using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Models;

namespace Contracts.CommandModels
{
    public class UpdateLocationCommandResultModel : ModelBase
    {
        public string Path { get; set; }

        public string Name { get; set; }

        public List<SimpleModel> Tags { get; set; }
    }
}
