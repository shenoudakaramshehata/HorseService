using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HorseService.Models
{
    public class AppointmentAdditionalTypes
    {
        [Key]
        public int AppointmentAdditionalTypesId { get; set; }
        public int AppointmentsId { get; set; }
        [JsonIgnore]
        public virtual Appointments Appointments { get; set; }
        public int AdditionalTypeId { get; set; }
        [JsonIgnore]
        public virtual AdditionalType AdditionalType { get; set; }



    }
}
