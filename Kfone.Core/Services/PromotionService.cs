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
            new Promotion{name="Fifty Fifty", code="5050", item=DeviceService.GetDevices()[0], percent=50, image="https://th.bing.com/th/id/R.08d804452e7de2d4fc2867063e1e5331?rik=WtfakmjRRP0ieQ&pid=ImgRaw&r=0"}
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
