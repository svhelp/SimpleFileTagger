using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    public class TaggerDirectoryInfo : ModelBase
    {
        public List<TagModel> Tags { get; set; } = new List<TagModel>();
    }
}
