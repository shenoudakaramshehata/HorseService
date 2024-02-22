using HorseService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorseService.ReportModels
{
    public class EmployeeAppointmentM
    {
        public int AppointmentsId { get; set; }

        public int NumberofHorses { get; set; }

        public DateTime Date { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTowill { get; set; }
       
        public int? EmployeeId { get; set; }
        public string AppointmentStatus { get; set; }
        public string FullName { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string ispaid  { get; set; }
        public string Image { get; set; }
        public Double Cost { get; set; }
        public string SerialNo { get; set; }



    }
}
