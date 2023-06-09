﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Kfone.Core.Models
{
    public class Customer
    {
        public string name { get; set; }
        public DateTimeOffset dob { get; set; }
        public string contactNo { get; set; }
        public string address { get; set; }
        public Tiers tier { get; set; }
        public string profilePic { get; set; }
        public string email { get; set; }
    }
}
