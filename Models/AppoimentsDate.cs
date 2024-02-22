using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HorseService.Models
{
    public class AppoimentsDate
    {
        [Key]
        public int AppoimentsDateId { get; set; }
        public DateTime Date { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTowill { get; set; }
    
        public int AppointmentsId { get; set; }
        [JsonIgnore]
        public virtual Appointments Appointments { get; set; }

    }
}
