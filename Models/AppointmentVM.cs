using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorseService.Models
{
    public class AppointmentVM
    {
        public int NumberofHorses { get; set; }
       
        public string Remarks { get; set; }
        public int? EmployeeId { get; set; }
        public int CustomerId { get; set; }
        public Double Cost { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string CustomerAddress { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? AppointmentStatusId { get; set; }
        public bool ispaid { get; set; }
        public List<AppointmentDetails> AppointmentDetails { get; set; }

    }
}
