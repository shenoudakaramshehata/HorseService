using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorseService.Entities
{
    public class RegistrationModel
    {
        public int? CustomerId { get; set; }
        public string CustomerNameEn { get; set; }
        //public string CustomerAddress { get; set; }
        //public string Lat { get; set; }
        //public string Lng { get; set; }
        public string CustomerPhone { get; set; }
        // public string CustomerEmail { get; set; }
        public string CustomerImage { get; set; }
        //public string CustomerRemarks { get; set; }
        //public string Mobile { get; set; }

        //public string UserName { get; set; }
        public string Password { get; set; }
    }
}
