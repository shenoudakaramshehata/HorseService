using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorseService.Models
{
    public class AppointmentStatus
    {
        public int AppointmentStatusId { get; set; }
        [Required]
        public string AppointmentStatusTitle { get; set; }
       
    }
}
