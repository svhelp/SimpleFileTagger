﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    public class TagGroupModel : ModelBase
    {
        public string Name { get; set; }

        public List<TagModel> Tags { get; set; }
    }
}
