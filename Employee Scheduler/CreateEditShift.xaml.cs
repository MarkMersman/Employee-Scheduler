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
using Xceed.Wpf.Toolkit;
using System.Data.SqlClient;

namespace Employee_Scheduler
{
    /// <summary>
    /// Interaction logic for CreateEditShift.xaml
    /// </summary>
    public partial class CreateEditShift : Window
    {
        public Shift updateShift;
        public CreateEditShift()
        {
            InitializeComponent();

            
        }

        public CreateEditShift(Shift shift)
        {
            InitializeComponent();

            ShiftNameTextBox.Text = shift.ShiftName;

            StartTimePicker.Value = DateTime.Parse(shift.StartTime);

            EndTimePicker.Value = DateTime.Parse(shift.EndTime);

            updateShift = shift;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EmployeeScheduleDatabaseDataSetTableAdapters.ShiftTableTableAdapter sAdapter =
                    new EmployeeScheduleDatabaseDataSetTableAdapters.ShiftTableTableAdapter();

            EmployeeScheduleDatabaseDataSet.ShiftTableDataTable shiftData = sAdapter.GetData();
            if (StartTimePicker.Value > EndTimePicker.Value)
            {
                System.Windows.MessageBox.Show("Start time must be before end time.");
                return;
            }

            if (updateShift != null)
            {

                EmployeeScheduleDatabaseDataSet.ShiftTableRow editRow =
                    shiftData.FindByShiftId(updateShift.ShiftId);

                
                editRow.ShiftName = ShiftNameTextBox.Text;
                editRow.StartTime = DateTime.Parse(StartTimePicker.Value.ToString()).ToString(@"hh\:mm\:ss tt");
                editRow.EndTime = DateTime.Parse(EndTimePicker.Value.ToString()).ToString(@"hh\:mm\:ss tt");



                try
                {                    
                    sAdapter.Update(editRow);

                    Shifts shiftWindow = new Shifts();
                    shiftWindow.Show();
                    this.Close();
                }
                catch
                {
                    System.Windows.MessageBox.Show("whoops");
                }
            }
            else
            {
                EmployeeScheduleDatabaseDataSet.ShiftTableRow newRow =
                    shiftData.NewShiftTableRow();
                
                newRow.ShiftId = shiftData.Last().ShiftId + 1;
                newRow.ShiftName = ShiftNameTextBox.Text;
                newRow.StartTime = DateTime.Parse(StartTimePicker.Value.ToString()).ToString(@"hh\:mm\:ss tt");
                newRow.EndTime = DateTime.Parse(EndTimePicker.Value.ToString()).ToString(@"hh\:mm\:ss tt");

                
                try
                {
                    sAdapter.Insert(newRow.ShiftId, newRow.ShiftName, newRow.StartTime, newRow.EndTime);

                    Shifts shiftWindow = new Shifts();
                    shiftWindow.Show();
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
