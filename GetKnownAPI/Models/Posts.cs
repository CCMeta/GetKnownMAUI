﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetKnownAPI.Models
{
    public class Posts
    {
        public int id { get; set; }
        public string content { get; set; }
        public string created_at { get; set; }
        public int uid { get; set; }
        public string avatar { get; set; }
        public string nickname { get; set; }

    }
}
