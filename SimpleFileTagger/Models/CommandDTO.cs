using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileTagger.Models
{
    internal class CommandDTO
    {
        public string Path { get; set; }

        public string CommandType { get; set; }

        public string[] Tags { get; set; }
    }
}
