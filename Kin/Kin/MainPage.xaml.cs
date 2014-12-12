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



namespace Kin
{

    public sealed partial class MainPage : Page
    {
        MainPageViewModel viewModel = new MainPageViewModel();

        public MainPage()
        {
            this.InitializeComponent();
            InitializeComponent();
            Binding();
        }

        private async void Binding()
        {
            viewModel = new MainPageViewModel
            {
                CurrentConditions = await MainPageViewModel.FetchCurrentConditions(),
                Forecast = await MainPageViewModel.FetchTenDayForecast(),
                ScheduledTasksList = await MainPageViewModel.FetchTasks()
            };

            DataContext = viewModel;


            try
            {
                var temp = viewModel.CurrentConditions.ElementAt(0);

                this.txtBlk_Weather.Text = "℃";
                this.txtBlk_Place.Text = temp.name + ", " + temp.sys.country; ;
                this.txtBlk_DayName.Text = DateTime.Today.DayOfWeek.ToString();
                this.txtBlk_Date.Text = DateTime.Now.ToString("MMMM d");
            }
            catch{}
        }

        private void CreateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string value = btn.CommandParameter.ToString();
            int index;
            bool result = Int32.TryParse(value, out index);
            if (result)
            {
                this.Frame.Navigate(typeof(CreateTask), viewModel.Forecast.ElementAt(index));
            }
        }
    }
}
