using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Controls;

namespace Employee_Scheduler
{
    class Schedule
    {

       public List<ScheduledShift> allSShifts;

       public List<ScheduledShift> orderedList;
        public Schedule()
        {
            allSShifts = new List<ScheduledShift>();
            //orderedList = new List<ScheduledShift>();
           // PullData();
        }
        public virtual void Build_Schedule()
        {

        }

        public virtual void DisplaySchedule(DataGrid dataGrid)
        {
            orderedList = allSShifts.OrderBy(o => o.ScheduledDate).ToList();

            dataGrid.ItemsSource = orderedList;

        }

        public void DeleteCurrent()
        {
            EmployeeScheduleDatabaseDataSetTableAdapters.ScheduledShiftTableAdapter SSAdapter =
                new EmployeeScheduleDatabaseDataSetTableAdapters.ScheduledShiftTableAdapter();

            EmployeeScheduleDatabaseDataSet.ScheduledShiftDataTable sshiftdata = SSAdapter.GetData();

            foreach (EmployeeScheduleDatabaseDataSet.ScheduledShiftRow row in sshiftdata)
            {
                if (row.Date >= DateTime.Now)
                {
                    try
                    {
                        EmployeeScheduleDatabaseDataSet.ScheduledShiftRow delRow =
                        sshiftdata.FindBySShiftId(row.SShiftId);

                        delRow.Delete();





                    }
                    catch
                    {
                        System.Windows.MessageBox.Show("whoops");
                    }
                }
            }

            SSAdapter.Update(sshiftdata);
            
        }

        public void PullData()
        {
            if (allSShifts != null)
            {
                allSShifts.Clear();
            }

            EmployeeScheduleDatabaseDataSetTableAdapters.ScheduledShiftTableAdapter SSAdapter =
                new EmployeeScheduleDatabaseDataSetTableAdapters.ScheduledShiftTableAdapter();

            EmployeeScheduleDatabaseDataSet.ScheduledShiftDataTable sshiftdata = SSAdapter.GetData();

            EmployeeScheduleDatabaseDataSetTableAdapters.ShiftTableTableAdapter SAdapter =
                new EmployeeScheduleDatabaseDataSetTableAdapters.ShiftTableTableAdapter();

            EmployeeScheduleDatabaseDataSet.ShiftTableDataTable shiftdata = SAdapter.GetData();

            EmployeeScheduleDatabaseDataSetTableAdapters.EmployeesTableAdapter empAdapter =
                new EmployeeScheduleDatabaseDataSetTableAdapters.EmployeesTableAdapter();

            EmployeeScheduleDatabaseDataSet.EmployeesDataTable empdata = empAdapter.GetData();

            foreach (EmployeeScheduleDatabaseDataSet.ScheduledShiftRow row in sshiftdata)
            {
                ScheduledShift tempSShift = new ScheduledShift();
                tempSShift.SShiftId = row.SShiftId;
                tempSShift.ScheduledDate = row.Date;
                tempSShift.Starttime = shiftdata.FindByShiftId(row.ShiftId).StartTime;
                tempSShift.Endtime = shiftdata.FindByShiftId(row.ShiftId).EndTime;
                tempSShift.EmpName = empdata.FindByempId(row.EmpId).firstName + " " + empdata.FindByempId(row.EmpId).lastName;
               

                allSShifts.Add(tempSShift);
                
                
            }

        }
    }
}
