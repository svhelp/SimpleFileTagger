﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CommandModels
{
    public class UpdateTagsCommandModel
    {
        public string Path { get; set; }

        public string[] Tags { get; set; }
    }
}
