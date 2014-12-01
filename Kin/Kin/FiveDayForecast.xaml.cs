using Kin.Model;
using Kin.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Kin
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FiveDayForecast : Page
    {
        public FiveDayForecast()
        {
            this.InitializeComponent();

            cmbBox_WeatherConditions.ItemsSource = FiveDayForecastViewModel.WeatherConditions;
            cmbBox_WeatherConditions.SelectedIndex = 0;
        }
        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            Result();   
        }
        private async void Result()
        {
            int selected = cmbBox_WeatherConditions.SelectedIndex;
            string s = cmbBox_WeatherConditions.SelectedItem.ToString();
            var viewModel = new FiveDayForecastViewModel { BestFitted = await FiveDayForecastViewModel.SearchBestFitted(s) };
        }
    }
}
