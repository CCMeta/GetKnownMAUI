﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetKnownAPI.Models
{
    public class Users
    {
        public int id { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? nickname { get; set; }
        public string? avatar { get; set; }
        public string? intro { get; set; }

    }
}
