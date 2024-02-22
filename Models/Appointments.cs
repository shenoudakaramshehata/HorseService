using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorseService.Models
{
    public class Appointments
    {
        [Key]

        public int AppointmentsId { get; set; }
      
        public int NumberofHorses { get; set; }
       
        public DateTime Date { get; set; }
       
        public DateTime TimeFrom { get; set; }
     
        public DateTime TimeTowill { get; set; }
        public string Remarks { get; set; }
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public Double Cost { get; set; }
        
        public string Lat { get; set; }
        public string Lng { get; set; }
        
        public string CustomerAddress { get; set; }
        public int? PaymentMethodId { get; set; }
        public bool ispaid { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public int? AppointmentStatusId { get; set; }
        public virtual AppointmentStatus AppointmentStatus { get; set; }
        public string payment_type { get; set; }
        public string PaymentID { get; set; }
        public string OrderSerialNumber { get; set; }
        public virtual ICollection<AppointmentDetails> AppointmentDetails { get; set; }
        public virtual ICollection<AppoimentsDate> AppoimentsDates { get; set; }
        

    }
}
