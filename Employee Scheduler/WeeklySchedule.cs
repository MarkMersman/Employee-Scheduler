using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Controls;



namespace Employee_Scheduler
{
    class WeeklySchedule : Schedule
    {
        public WeeklySchedule()
        {
            
        }
        public override void Build_Schedule()
        {
            if (CheckForCurrentSched())
            {
                DeleteCurrent();
                
            }

            EmployeeScheduleDatabaseDataSetTableAdapters.ScheduledShiftTableAdapter SSAdapter =
                new EmployeeScheduleDatabaseDataSetTableAdapters.ScheduledShiftTableAdapter();

            EmployeeScheduleDatabaseDataSet.ScheduledShiftDataTable sshiftdata = SSAdapter.GetData();

            //EmployeeScheduleDatabaseDataSet.ScheduledShiftDataTable newsched = SSAdapter.

            EmployeeScheduleDatabaseDataSetTableAdapters.ShiftTableTableAdapter SAdapter =
                new EmployeeScheduleDatabaseDataSetTableAdapters.ShiftTableTableAdapter();

            EmployeeScheduleDatabaseDataSet.ShiftTableDataTable shiftdata = SAdapter.GetData();

            EmployeeScheduleDatabaseDataSetTableAdapters.EmployeesTableAdapter empAdapter =
                new EmployeeScheduleDatabaseDataSetTableAdapters.EmployeesTableAdapter();

            EmployeeScheduleDatabaseDataSet.EmployeesDataTable empdata = empAdapter.GetData();

            int EmpsperShift = 3;
            int ShiftsperDay = shiftdata.Count;
            int daysleftinWeek = GetEndofWeek().DayOfWeek - DateTime.Today.AddDays(1).DayOfWeek;
            int neededShifts = (EmpsperShift * ShiftsperDay) * daysleftinWeek;
            double neededemps = Math.Ceiling((double)neededShifts / (double)empdata.Count);        
            List<ScheduledShift> tempList = new List<ScheduledShift>();
            List<Shift> shiftIdList = new List<Shift>();
            List<Employee> empIdList = new List<Employee>();
            //int tryEmp = empdata.First().empId;
            //int empCount = empdata.Count;
            int newSID = sshiftdata.Last().SShiftId + 1;
            int empsAssigned = 0;

            foreach (EmployeeScheduleDatabaseDataSet.ShiftTableRow row in shiftdata)
            {
                Shift tempShift = new Shift();
                tempShift.ShiftId = row.ShiftId;
                tempShift.ShiftName = row.ShiftName;
                tempShift.StartTime = row.StartTime.ToString();
                tempShift.EndTime = row.EndTime.ToString();

                shiftIdList.Add(tempShift);
            }

            foreach (EmployeeScheduleDatabaseDataSet.EmployeesRow row in empdata)
            {
                Employee tempEmp = new Employee();
                tempEmp.EmpId = row.empId;
                tempEmp.FName = row.firstName;
                tempEmp.LName = row.lastName;

                empIdList.Add(tempEmp);
            }



            for (int i = 0; i <= daysleftinWeek; i++)
            {
                //get the date for each day left in week
                DateTime adate = DateTime.Now.AddDays(i+1).Date;

                for (int j = 0; j < ShiftsperDay; j++)
                {
                    //for each date assign 3 shifts

                    int shiftID = shiftIdList.ElementAt(j).ShiftId;
                    empsAssigned = 0;
                    var shuffled = empIdList.OrderBy(x => Guid.NewGuid()).ToList();
                    foreach (Employee emp in shuffled)
                    {
                        // List<ScheduledShift> matchedDate = tempList.Where(x => x.ScheduledDate == adate).ToList();
                        List<ScheduledShift> matchedEmp = tempList.Where(x => x.EmpId == emp.EmpId).ToList();
                        List<ScheduledShift> matchedShift = tempList.Where(x => ((x.ScheduledDate == adate) && (x.EmpId == emp.EmpId))).ToList();

                        if (matchedShift.Count != 0)//((matchedDate.Count != 0) && (matchedEmp.Count != 0))
                        {
                            //Emp has been assigned a shift for this date, need to move to next emp in list
                            if  (matchedShift.ElementAt(0).EmpId == emp.EmpId)//((emp.EmpId == matchedEmp.ElementAt(0).EmpId) && (emp.EmpId == matchedDate.ElementAt(0).EmpId))
                            {

                            }


                        }
                        else
                        {
                            if (matchedEmp.Count > neededemps)
                            {

                            }

                            else if (empsAssigned < EmpsperShift)
                            {
                                // Emp is good to go, add it to the List
                                ScheduledShift tempSShift = new ScheduledShift();
                                tempSShift.ScheduledDate = adate;
                                tempSShift.SShiftId = newSID;
                                tempSShift.ShiftId = shiftID;
                                tempSShift.EmpId = emp.EmpId;

                                //Console.WriteLine(tempSShift.SShiftId + " " + tempSShift.ScheduledDate + " " + tempSShift.ShiftId + " " + tempSShift.EmpId);
                                tempList.Add(tempSShift);
                                newSID++;
                                empsAssigned++;
                            }
                            else
                            {
                                
                            }
                        }

                    }                    
                                      
                }
            }

            foreach (ScheduledShift s in tempList)
            {
                try
                {
                    SSAdapter.Insert(s.SShiftId, s.EmpId, s.ShiftId, s.ScheduledDate);
                }
                catch
                {
                    System.Windows.MessageBox.Show("Whoops");
                }
            }

        }

    

        public override void DisplaySchedule(DataGrid dataGrid)
        {
            IEnumerable<ScheduledShift> tempShiftList = new List<ScheduledShift>();
            DateTime start = GetStartofWeek();
            DateTime end = GetEndofWeek();
            tempShiftList = allSShifts.Where(shift => shift.ScheduledDate <= end && shift.ScheduledDate >= start);
            orderedList = tempShiftList.OrderBy(o => o.ScheduledDate).ToList();


            dataGrid.ItemsSource = orderedList;
            
        }

        public void FilterSchedule(DataGrid dg, string textbox)
        {
            IEnumerable<ScheduledShift> tempShiftList = new List<ScheduledShift>();
            DateTime start = GetStartofWeek();
            DateTime end = GetEndofWeek();
            tempShiftList = allSShifts.Where(shift => shift.EmpName.Contains(textbox) );
            orderedList = tempShiftList.OrderBy(o => o.ScheduledDate).ToList();


            dg.ItemsSource = orderedList;
        }
        public virtual bool CheckForCurrentSched()
        {            
            IEnumerable<ScheduledShift> tempShiftList = new List<ScheduledShift>();
            DateTime start = GetStartofWeek();
            DateTime end = GetEndofWeek();
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

       

        public DateTime GetStartofWeek()
        {
            DayOfWeek day = DateTime.Now.DayOfWeek;

            int days = day - DayOfWeek.Sunday;
            DateTime start = DateTime.Now.AddDays(-days);

            return start.Date;
        }

        public DateTime GetEndofWeek()
        {
            DateTime start = GetStartofWeek();
            DateTime end = start.AddDays(6);
            return end.Date;
        }
    }
}
