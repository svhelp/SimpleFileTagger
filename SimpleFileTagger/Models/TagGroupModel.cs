using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileTagger.Models
{
    internal class TagGroupModel : ModelBase
    {
        public List<TagModel> Tags { get; set; }
    }
}
