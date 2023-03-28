using Kfone.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kfone.Core.Services
{
    public static class DeviceService
    {
        public static List<Device> Devices = new List<Device>(new Device[]
        {
            new Device{Name="Moto G4", Brand="Motorola", Price=70000, Type=DeviceType.Smartphone, Image="https://d11zer3aoz69xt.cloudfront.net/media/catalog/product/cache/1/image/1200x/9df78eab33525d08d6e5fb8d27136e95/m/o/motorola_moto_g_plus_4th_generation_16gb_2gb_ram_black_1_2.jpg"}
        });

        public static List<Device> GetDevices ()
        {
            return Devices;
        }

        public static void SetDevice (Device device)
        {
            Devices.Add (device);
        }

        public static void DeleteDevice (Device device)
        {
            Devices.Remove (device);
        }
    }
}
