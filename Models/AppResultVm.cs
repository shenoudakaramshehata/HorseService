using System;

namespace HorseService.Models
{
    public class AppResultVm
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfHourses { get; set; }
    }
}
