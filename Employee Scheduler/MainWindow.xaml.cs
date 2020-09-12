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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;


namespace Employee_Scheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void viewSchedule_Click(object sender, RoutedEventArgs e)
        {
            ViewSchedule VS = new ViewSchedule();
            VS.Show();
        }

        private void viewEmployees_Click(object sender, RoutedEventArgs e)
        {
            Employees emp = new Employees();
            emp.Show();
        }

        private void viewshifts_Click(object sender, RoutedEventArgs e)
        {
            Shifts SP = new Shifts();
            SP.Show();
        }

        private void createSchedule_Click(object sender, RoutedEventArgs e)
        {
            //WeeklySchedule Sched = new WeeklySchedule();
            //Sched.PullData();
        }
    }
}
