using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.Legacy
{
    public class TaggerDirectoryInfo : ModelBase
    {
        public string Path { get; set; }

        public string Name { get; set; }

        public List<TaggerDirectoryInfo> Children { get; set; } = new List<TaggerDirectoryInfo>();

        public List<SimpleModel> Tags { get; set; } = new List<SimpleModel>();
    }
}
