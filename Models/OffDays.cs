using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorseService.Models
{
    public class OffDays
    {
        public int OffDaysId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public DateTime? Onday { get; set; }
        public int breaktypesId { get; set; }
        public virtual BreakTypes BreakTypes { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
