using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;


namespace Employee_Scheduler
{
    /// <summary>
    /// Interaction logic for ViewSchedule.xaml
    /// </summary>
    public partial class ViewSchedule : Window
    {
        WeeklySchedule weekSched;
        DataView dv;
        public ViewSchedule()
        {
            InitializeComponent();

            weekSched = new WeeklySchedule();
           
            weekSched.PullData();
            weekSched.DisplaySchedule(ScheduleList);
            
        }

       

        private void ScheduleList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yyyy";
        }

        private void WeekCreateButton_Click(object sender, RoutedEventArgs e)
        {
            weekSched.Build_Schedule();
            weekSched.PullData();
            weekSched.DisplaySchedule(ScheduleList);
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            
            weekSched.PullData();
            weekSched.DisplaySchedule(ScheduleList);
        }

        private void DeleteCurrent_Click(object sender, RoutedEventArgs e)
        {
            weekSched.DeleteCurrent();

            weekSched.PullData();
           

            ScheduleList.ItemsSource = null;

            System.Windows.MessageBox.Show("Schedule Deleted!");
        }

        private void SchedSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
          
            weekSched.FilterSchedule(ScheduleList, SchedSearchBox.Text);
        }
    }
}
