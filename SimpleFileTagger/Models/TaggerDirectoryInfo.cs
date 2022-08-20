using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileTagger.Models
{
    internal class TaggerDirectoryInfo
    {
        public List<TagModel> Tags { get; set; } = new List<TagModel>();
    }
}
