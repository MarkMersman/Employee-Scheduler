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
    /// Interaction logic for Employees.xaml
    /// </summary>
    public partial class Employees : Window
    {
        Employee selectedEmp;

        List<Employee> allEmps;

        DataView dv;
        public Employees()
        {
            InitializeComponent();

            allEmps = new List<Employee>();
            selectedEmp = new Employee();
            PopEmpList();
        }

        public void PopEmpList()
        {
            EmployeeScheduleDatabaseDataSetTableAdapters.EmployeesTableAdapter empAdapter =
                new EmployeeScheduleDatabaseDataSetTableAdapters.EmployeesTableAdapter();

            EmployeeScheduleDatabaseDataSet.EmployeesDataTable empdata = empAdapter.GetData();

            foreach (EmployeeScheduleDatabaseDataSet.EmployeesRow row in empdata)
            {
                Employee tempEmp = new Employee();
                tempEmp.EmpId = row.empId;
                tempEmp.FName = row.firstName;
                tempEmp.LName = row.lastName;
                allEmps.Add(tempEmp);
            }

            EmployeeList.ItemsSource = allEmps;
            dv = new DataView(empdata);

            
        }

        private void CreateEmployee_Click(object sender, RoutedEventArgs e)
        {
            CreateEditEmployees CEE = new CreateEditEmployees();

            CEE.Show();

            this.Close();

        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            CreateEditEmployees CEE = new CreateEditEmployees(selectedEmp);

            CEE.Show();

            this.Close();
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeScheduleDatabaseDataSetTableAdapters.EmployeesTableAdapter empAdapter =
                new EmployeeScheduleDatabaseDataSetTableAdapters.EmployeesTableAdapter();

            EmployeeScheduleDatabaseDataSet.EmployeesDataTable empdata = empAdapter.GetData();

            try
            {
                EmployeeScheduleDatabaseDataSet.EmployeesRow delRow =
                empdata.FindByempId(selectedEmp.EmpId);

                delRow.Delete();

                empAdapter.Update(delRow);

                allEmps.Remove(selectedEmp);
                EmployeeList.ItemsSource = null;
                EmployeeList.ItemsSource = allEmps;
                System.Windows.MessageBox.Show("Employee Deleted!");
               
            }
            catch
            {
                System.Windows.MessageBox.Show("whoops");
            }
        }

        private void EmpSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dv.RowFilter = string.Format("firstName like '%{0}%' OR lastName like '%{0}%'", EmpSearchBox.Text);

            EmployeeList.ItemsSource = dv;

            if (EmpSearchBox.Text == null)
            {
                EmployeeList.ItemsSource = allEmps;
            }
        }

        private void EmployeeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeeList.ItemsSource == dv)
            {
                EmployeeList.ItemsSource = allEmps;
            }

            try
            {
                selectedEmp = (Employee)EmployeeList.SelectedItem;
            }
            catch
            {

            }
        }
    }
}
