using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Scheduler
{
    public class Shift
    {
        private string startTime;
        private string endTime;
        private int shiftId;

        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string StartTime {
         get { return startTime ; }

            set {
                
                startTime = DateTime.Parse(value).ToString(@"hh\:mm\:ss tt");
            }
        }
        public string EndTime
        {
            get { return endTime; }

            set
            {

                endTime = DateTime.Parse(value).ToString(@"hh\:mm\:ss tt");
            }
        }



    }
}
