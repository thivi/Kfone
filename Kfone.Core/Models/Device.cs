using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

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
        private string _image;
        public string name { get; set; }
        public float price { get; set; }
        public string brand { get; set; }
        public DeviceType type { get; set; }
        public string image
        {
            get;
            set;
        }

        override public string ToString()
        {
            return name;
        }
    }
}
