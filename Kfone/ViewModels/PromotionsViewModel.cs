using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Kfone.Core.Models;
using Kfone.Core.Services;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Uwp.UI.Controls;

namespace Kfone.ViewModels
{
    public class PromotionsViewModel : ObservableObject
    {
        private Promotion _selected;

        public Promotion Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }

        public ObservableCollection<Promotion> SampleItems { get; private set; } = new ObservableCollection<Promotion>();

        public PromotionsViewModel()
        {
        }

        public async Task LoadDataAsync(ListDetailsViewState viewState)
        {
            SampleItems.Clear();

            var data = PromotionService.GetPromotions();

            foreach (var item in data)
            {
                SampleItems.Add(item);
            }

            if (viewState == ListDetailsViewState.Both)
            {
                Selected = SampleItems.First();
            }
        }
    }
}
