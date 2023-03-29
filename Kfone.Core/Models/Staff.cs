using System;
using System.Collections.Generic;
using System.Text;

namespace Kfone.Core.Models
{
    public class Staff
    {
        public string name {  get; set; }
        public string contactno { get; set; }
        public string address { get; set; }
        public Roles roles { get; set; }
        public string profilePic { get; set; }
        public DateTime joinedDate { get; set; }
        public DateTime dob { get; set; }
    }
}
