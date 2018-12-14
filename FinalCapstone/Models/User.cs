﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DriversLicense { get; set; }
        public string Address { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
    }
}
