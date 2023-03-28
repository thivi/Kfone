using System;
using System.Collections.Generic;
using System.Text;

namespace Kfone.Core.Models
{
    public class Promotion
    {
        public string code {  get; set; }
        public Device item { get; set; }
        public float percent { get; set; }
        public string name { get; set; }
        public string image { get; set; }
    }
}
