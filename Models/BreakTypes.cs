using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorseService.Models
{
    public class BreakTypes
    {
        [Key]
        public int breaktypesId { get; set; }
        public string Title { get; set; }
    }
}
