using System.Text.Json.Serialization;

namespace HorseService.Models
{
    public class AppointmentDetails
    {
        public int AppointmentDetailsId { get; set; }
        public int AppointmentsId { get; set; }
        [JsonIgnore]
        public virtual Appointments Appointments { get; set; }
        public int? ServiceId { get; set; }
        [JsonIgnore]
        public virtual Service Service { get; set; }
        public double Cost { get; set; }
        public string AdditionalTypes { get; set; }
        public double TotalAdditionalCost { get; set; }
        public int NumberOfHorses { get; set; }


    }
}
