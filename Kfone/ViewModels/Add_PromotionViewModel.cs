using System;
using Kfone.Core.Models;
using Kfone.Core.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Kfone.ViewModels
{
    public class Add_PromotionViewModel : ObservableObject
    {
        public Add_PromotionViewModel()
        {
        }

        public void AddPromotion(Promotion promotion)
        {
            PromotionService.SetPromotion(promotion);
        }
    }
}
