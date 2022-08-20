using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileTagger.Models
{
    internal class TagModel : ModelBase
    {
        public string Name { get; set; }

        public TagGroupModel Group { get; set; }
    }
}
