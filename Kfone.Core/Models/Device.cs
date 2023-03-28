using System;
using System.Collections.Generic;
using System.Text;

namespace Kfone.Core.Models
{
    public enum DeviceType
    {
        Smartphone,
        Tablet,
        Wearable
    }

    public class Device
    {
        public string name { get; set; }
        public float price { get; set; }
        public string brand { get; set; }
        public DeviceType type { get; set; }
        public string image { get; set; }

        override public string ToString()
        {
            return name;
        }
    }
}
