using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class SettingsEntity : EntityBase
    {
        public string Code { get; set; }

        public bool Enabled { get; set; }

        public string Config { get; set; }
    }
}
