using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HorseService.Models
{
    public class AdditionalType
    {
        public int AdditionalTypeId { get; set; }
        public string Title { get; set; }
        public double Cost { get; set; }
        [JsonIgnore]
        public virtual ICollection<AppointmentAdditionalTypes> AppointmentAdditionalTypes { get; set; }
    }
}
