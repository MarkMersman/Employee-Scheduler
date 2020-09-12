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
    /// Interaction logic for Shifts.xaml
    /// </summary>
    public partial class Shifts : Window
    {
        Shift selectedShift;

        List<Shift> allShifts;

        DataView dv;

        public Shifts()
        {
            InitializeComponent();

            allShifts = new List<Shift>();
            selectedShift = new Shift();                        
            PopShiftList();
            
        }

        public void PopShiftList()
        {
            
            EmployeeScheduleDatabaseDataSetTableAdapters.ShiftTableTableAdapter sAdapter =
                new EmployeeScheduleDatabaseDataSetTableAdapters.ShiftTableTableAdapter();

            EmployeeScheduleDatabaseDataSet.ShiftTableDataTable shiftData = sAdapter.GetData();

            

            foreach (EmployeeScheduleDatabaseDataSet.ShiftTableRow row in shiftData)
            {
                Shift tempShift = new Shift();
                tempShift.ShiftId = row.ShiftId;
                tempShift.ShiftName = row.ShiftName;
                tempShift.StartTime = row.StartTime.ToString();
                tempShift.EndTime = row.EndTime.ToString();
                allShifts.Add(tempShift);
            }

            ShiftList.ItemsSource = allShifts;
            dv = new DataView(shiftData);
        }

        private void CreateShift_Click(object sender, RoutedEventArgs e)
        {
            CreateEditShift CES = new CreateEditShift();

            CES.Show();

            this.Close();
            
        }

        private void EditShift_Click(object sender, RoutedEventArgs e)
        {
            

            CreateEditShift CES = new CreateEditShift(selectedShift);

            CES.Show();

            this.Close();

            
        }

        private void ShiftList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ShiftList.ItemsSource == dv)
            {
                ShiftList.ItemsSource = allShifts;
            }

            try
            {
                selectedShift = (Shift)ShiftList.SelectedItem;
             }
            catch
            {

            }

        }

        private void DeleteShift_Click(object sender, RoutedEventArgs e)
        {
            EmployeeScheduleDatabaseDataSetTableAdapters.ShiftTableTableAdapter sAdapter =
                new EmployeeScheduleDatabaseDataSetTableAdapters.ShiftTableTableAdapter();

            EmployeeScheduleDatabaseDataSet.ShiftTableDataTable shiftData = sAdapter.GetData();

            try
            {
                EmployeeScheduleDatabaseDataSet.ShiftTableRow delRow =
                shiftData.FindByShiftId(selectedShift.ShiftId);

                delRow.Delete();

                sAdapter.Update(delRow);

                allShifts.Remove(selectedShift);
                ShiftList.ItemsSource = null;
                ShiftList.ItemsSource = allShifts;
                System.Windows.MessageBox.Show("Shift Deleted!");
                PopShiftList();
            }
            catch
            {
                System.Windows.MessageBox.Show("whoops");
            }
        }

        private void ShiftSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dv.RowFilter = string.Format("ShiftName like '%{0}%'", ShiftSearchBox.Text);

            ShiftList.ItemsSource = dv;

            if (ShiftSearchBox.Text == null)
            {
                ShiftList.ItemsSource = allShifts;
            }
           
        }
    }
}
