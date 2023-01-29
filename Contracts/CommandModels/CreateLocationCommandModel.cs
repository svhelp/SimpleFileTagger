using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CommandModels
{
    public class CreateLocationCommandModel
    {
        public string Path { get; set; }

        public bool IsRecoursive { get; set; }
    }
}
