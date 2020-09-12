using System;
using System.Collections.Generic;
using System.Data;
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

namespace Employee_Scheduler
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeScheduleDatabaseDataSetTableAdapters.LoginTableTableAdapter loginadapter =
                new EmployeeScheduleDatabaseDataSetTableAdapters.LoginTableTableAdapter();

            EmployeeScheduleDatabaseDataSet.LoginTableDataTable logindata = loginadapter.GetData();
            DataTableReader rdr = logindata.CreateDataReader();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        if ((rdr[i].ToString().ToUpper() == UsernameTextBox.Text.ToUpper()) && (rdr[i + 1].ToString() == PasswordTextBox.Text))
                        {
                            MainWindow mw = new MainWindow();
                            mw.Show();
                            this.Close();
                            break;
                            
                        }
                        else if (i == rdr.FieldCount - 1)
                        {
                            MessageBox.Show("Wrong Username or Password!");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No Rows in Reader");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
