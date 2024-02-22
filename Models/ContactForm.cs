using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorseService.Models
{
    public class ContactForm
    {
        public int ContactFormId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = " Accept Number Only")]
        public string Mobile { get; set; }
        [Required]
        public string Message { get; set; }

    }
}
