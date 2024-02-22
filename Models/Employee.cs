using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorseService.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public ICollection <Appointments> Appointments { get; set; }
        public ICollection<Video>Videos { get; set; }
    }

}
