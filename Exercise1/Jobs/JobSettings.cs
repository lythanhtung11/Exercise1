using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1.Jobs
{
    public class JobSettings
    {
        //Config a time to run job count by minutes
        public int ScheduleTimeByMinutes { get; set; }

        //Config a month that password will be expired
        public int MonthToPwdExpired { get; set; }
    }
}
