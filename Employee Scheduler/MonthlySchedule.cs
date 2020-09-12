using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Controls;


namespace Employee_Scheduler
{
    class MonthlySchedule : WeeklySchedule
    {

        public override void Build_Schedule()
        {
            if (CheckForCurrentSched())
            {
               DeleteCurrent();
                
            }

           

        }

        public override void DisplaySchedule(DataGrid dataGrid)
        {
            IEnumerable<ScheduledShift> tempShiftList = new List<ScheduledShift>();
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            tempShiftList = allSShifts.Where(shift => shift.ScheduledDate <= end && shift.ScheduledDate >= start);
            orderedList = tempShiftList.OrderBy(o => o.ScheduledDate).ToList();


            dataGrid.ItemsSource = orderedList;

        }

        public override bool CheckForCurrentSched()
        {
            IEnumerable<ScheduledShift> tempShiftList = new List<ScheduledShift>();
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            tempShiftList = allSShifts.Where(shift => shift.ScheduledDate <= end && shift.ScheduledDate >= start);
            if (tempShiftList != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
