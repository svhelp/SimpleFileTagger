﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    public class TaggerDirectoryInfo : ModelBase
    {
        public string Path { get; set; }

        public List<TaggerDirectoryInfo> Children { get; set; }

        public List<TagModel> Tags { get; set; } = new List<TagModel>();
    }
}