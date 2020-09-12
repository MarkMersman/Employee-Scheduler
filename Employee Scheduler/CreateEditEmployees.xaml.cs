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

namespace Employee_Scheduler
{
    
    public partial class CreateEditEmployees : Window
    {

        public Employee updateEmp;
        public CreateEditEmployees()
        {
            InitializeComponent();
        }

        public CreateEditEmployees(Employee selectedEmp)
        {
            InitializeComponent();

            EmpFNameTextBox.Text = selectedEmp.FName;
            EmpLNameTextBox.Text = selectedEmp.LName;

            updateEmp = selectedEmp;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EmployeeScheduleDatabaseDataSetTableAdapters.EmployeesTableAdapter empAdapter =
                new EmployeeScheduleDatabaseDataSetTableAdapters.EmployeesTableAdapter();

            EmployeeScheduleDatabaseDataSet.EmployeesDataTable empdata = empAdapter.GetData();

            if (updateEmp != null)
            {

                EmployeeScheduleDatabaseDataSet.EmployeesRow editRow =
                    empdata.FindByempId(updateEmp.EmpId);


                editRow.firstName = EmpFNameTextBox.Text;
                editRow.lastName = EmpLNameTextBox.Text;





                try
                {
                    empAdapter.Update(editRow);

                    Employees empWindow = new Employees();
                    empWindow.Show();
                    this.Close();
                }
                catch
                {
                    System.Windows.MessageBox.Show("whoops");
                }
            }
            else
            {
                EmployeeScheduleDatabaseDataSet.EmployeesRow newRow =
                    empdata.NewEmployeesRow();

                newRow.empId = empdata.Last().empId + 1;
                newRow.firstName = EmpFNameTextBox.Text;
                newRow.lastName = EmpLNameTextBox.Text;


                try
                {
                    empAdapter.Insert(newRow.empId, newRow.firstName, newRow.lastName);

                    Employees empWindow = new Employees();
                    empWindow.Show();
                    this.Close();
                }
                catch
                {
                    System.Windows.MessageBox.Show("whoops");
                }
            }
        }
    }
}
