using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorseService.Models
{
    public class Video
    {
        public int VideoId { get; set; }
        public string VideoUrl { get; set; }
        [Required]
        public string Caption { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
