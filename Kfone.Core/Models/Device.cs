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
        public string Name { get; set; }
        public float Price { get; set; }
        public string Brand { get; set; }
        public DeviceType Type { get; set; }
        public string Image { get; set; }
    }
}
