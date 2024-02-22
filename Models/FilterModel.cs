using HorseService.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HorseService.Models
{
    public class FilterModel
    {
        public DateTime? FromDate { set; get; }
        public DateTime? ToDate { set; get; }
        public DateTime? OnDay { set; get; }
        public string radiobtn { get; set; }
        public int? EmployeeId { get; set; }
        
    }
}
