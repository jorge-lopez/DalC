using Kin.Model;
using Kin.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace Kin
{
    public sealed partial class CreateTask : Page
    {
        DateTime Day = new DateTime();

        public CreateTask()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ForecastTenDays viewModel = e.Parameter as ForecastTenDays;
            Binding(viewModel);
        }
        private async void Binding(ForecastTenDays loadedData)
        {
            Day = loadedData.Dt;
            List<ForecastTenDays> temp = new List<ForecastTenDays>();
            temp.Add(loadedData);

            var viewModel = new CreateTaskViewModel
            {
                Forecast = temp,
                SuggestionsList = await CreateTaskViewModel.FetchSuggestion(loadedData.DayOfTheWeek, loadedData.Description[0].main)
            };

            DataContext = viewModel;



            this.txtBlk_TempMax.Text = "℃";
            this.txtBlk_TempMin.Text = "℃";
            this.txtBlk_Place.Text = loadedData.City + ", " + loadedData.Country;
            this.txtBlk_DayName.Text = loadedData.Dt.DayOfWeek.ToString();
            this.txtBlk_Date.Text = loadedData.Dt.ToString("MMMM d");
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewTask();
        }

        private async void AddNewTask()
        {
            DateTime tempDue = new DateTime(Day.Year, Day.Month, Day.Day,
                time_DueDate.Time.Hours, time_DueDate.Time.Minutes, time_DueDate.Time.Seconds);
            
            DateTime tempReminder = new DateTime();
            
            if (cmbBox_Reminder.SelectedIndex == 0)
                tempReminder = new DateTime(Day.Year, Day.Month, Day.Day,
                    time_DueDate.Time.Hours, time_DueDate.Time.Minutes - 10, time_DueDate.Time.Seconds);

            else if (cmbBox_Reminder.SelectedIndex == 1)
                tempReminder = new DateTime(Day.Year, Day.Month, Day.Day,
                    time_DueDate.Time.Hours, time_DueDate.Time.Minutes -30, time_DueDate.Time.Seconds);
            
            else if (cmbBox_Reminder.SelectedIndex == 2)
                tempReminder = new DateTime(Day.Year, Day.Month, Day.Day,
                    time_DueDate.Time.Hours - 1, time_DueDate.Time.Minutes, time_DueDate.Time.Seconds);
            
            else if (cmbBox_Reminder.SelectedIndex == 3)
                tempReminder = new DateTime(Day.Year, Day.Month, Day.Day,
                    time_DueDate.Time.Hours - 2, time_DueDate.Time.Minutes, time_DueDate.Time.Seconds);

            bool b = await CreateTaskViewModel.AddNewTask(txtBox_Title.Text, txtBox_Description.Text, tempDue, tempReminder);

            string response = b ? "Operation Succesful" : "Operation Unsuccesful";
            var messageDialog = new MessageDialog( response);

            
            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();

        }

        private void chk_Reminder_Checked(object sender, RoutedEventArgs e)
        {
            cmbBox_Reminder.IsEnabled = false;
        }

        private void chk_Reminder_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbBox_Reminder.IsEnabled = true;
        }
    }
}
