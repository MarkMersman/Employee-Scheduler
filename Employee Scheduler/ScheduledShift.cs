using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Scheduler
{
    class ScheduledShift
    {
        private int sshiftId;
        private string empName;
        private DateTime scheduledDate;
        private string startTime;
        private string endTime;


        public int SShiftId { get; set; }
        public string EmpName { get; set; }
        public DateTime ScheduledDate
        {
            get { return scheduledDate; }

            set
            {

                scheduledDate = value.Date;
            }
        }

        public string Starttime { get; set; }
        public string Endtime { get; set; }

        public int EmpId { get; set; }

        public int ShiftId { get; set; }
    }
}
