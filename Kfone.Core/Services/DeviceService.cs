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
            new Device{name="Moto G40", brand="Motorola", price=70000, type=DeviceType.Smartphone, image="https://motorolain.vtexassets.com/arquivos/ids/157424-800-auto?width=800&height=auto&aspect=true"},
            new Device{name="Poco F5", brand="Pocophone", price=80000, type=DeviceType.Smartphone, image="https://transform.nws.ai/https%3A//cdn.thenewsroom.io/platform/story_media/1288797087/01-36.png/w_1200,c_limit/"},
            new Device{name="OnePlus 7 Nord", brand="OnePlus", price=170000, type=DeviceType.Smartphone, image="https://i0.wp.com/oasis.opstatics.com/content/dam/oasis/page/2022/operation/aug/0804/n20se/N20SE_Blue.png?w=1170&ssl=1"},
            new Device{name="Samsung A20", brand="Samsung", price=120000, type=DeviceType.Smartphone, image="https://cdn.shopify.com/s/files/1/0015/2469/5117/products/Galaxy_A205_Blue_03babd59-0c14-4668-b60b-c7dbc0cb8ea1_grande.png?v=1625989867"}
,           new Device{name="Surface Duo", brand="Microsoft", price=370000, type=DeviceType.Smartphone, image="https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RWFOAx?ver=e635"},
            new Device{name="Mi 13 Ultra", brand="Xiaomi", price=470000, type=DeviceType.Smartphone, image="https://www.giztop.com/media/catalog/product/cache/dc206057cdd42d7e34b9d36e347785ca/p/m/pms_1670745756.94572133.png"}


        });

        public static List<Device> GetDevices()
        {
            return Devices;
        }

        public static void SetDevice(Device device)
        {
            Devices.Add(device);
        }

        public static void DeleteDevice(Device device)
        {
            Devices.Remove(device);
        }
    }
}
