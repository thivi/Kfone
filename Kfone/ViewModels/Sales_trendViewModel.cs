﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Kfone.Core.Models;
using Kfone.Core.Services;

using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Kfone.ViewModels
{
    public class Sales_trendViewModel : ObservableObject
    {
        public ObservableCollection<DataPoint> Source { get; } = new ObservableCollection<DataPoint>();

        public Sales_trendViewModel()
        {
        }

        public async Task LoadDataAsync()
        {
            Source.Clear();

            // Replace this with your actual data
            var data = await SampleDataService.GetChartDataAsync();
            foreach (var item in data)
            {
                Source.Add(item);
            }
        }
    }
}
