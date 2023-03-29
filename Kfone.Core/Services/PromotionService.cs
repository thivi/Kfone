using Kfone.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kfone.Core.Services
{
    public static class PromotionService
    {
        public static List<Promotion> promotions = new List<Promotion>(new Promotion[]
       {
            new Promotion{name="Fifty Fifty", code="5050", item=DeviceService.GetDevices()[0], percent=50, image="https://www.mpa-pro.fr/resize/360x360/zc/3/f/0/src/sites/mpapro/files/products/d11242.png"},
            new Promotion{name="New Year Sale", code="NWY", item=DeviceService.GetDevices()[0], percent=30, image="https://dbdzm869oupei.cloudfront.net/img/vinylrugs/preview/47413.png"},
            new Promotion{name="April Fiesta", code="APF", item=DeviceService.GetDevices()[0], percent=70, image="https://creazilla-store.fra1.digitaloceanspaces.com/cliparts/1723058/april-clipart-xl.png"},
            new Promotion{name="Stock Clearance", code="SALEMAR", item=DeviceService.GetDevices()[0], percent=40, image="https://png.pngtree.com/png-vector/20220912/ourmid/pngtree-clearance-sale-promotion-banner-png-image_6172550.png"},
            new Promotion{name="Mi Magic", code="MiMGK", item=DeviceService.GetDevices()[0], percent=20, image="https://upload.wikimedia.org/wikipedia/commons/thumb/a/ae/Xiaomi_logo_%282021-%29.svg/512px-Xiaomi_logo_%282021-%29.svg.png"}
       });

        public static List<Promotion> GetPromotions()
        {
            return promotions;
        }

        public static void SetPromotion(Promotion promotion)
        {
            promotions.Add(promotion);
        }

        public static void DeletePromotion(Promotion promotion)
        {
            promotions.Remove(promotion);
        }
    }
}
