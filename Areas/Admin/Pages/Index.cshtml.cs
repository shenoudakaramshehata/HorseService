using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HorseService.Data;

namespace HorseService.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HorseServiceContext _context;
        public int revenues { get; set; }
        public int AppointmentCount { get; set; }
        public double AppointmentCost { get; set; }
        public int HorseCount { get; set; }
        public int ActiveEmployeeCount { get; set; }
        public int DailyCustomerCount { get; set; }

        public IndexModel(HorseServiceContext context)
        {
            _context = context;
        }
        
        public void OnGet()
        {
            AppointmentCount = _context.Appointments.Where(e=>e.Date.Date==DateTime.Now.Date).Count();
            AppointmentCost = _context.Appointments.Where(e => e.Date.Date == DateTime.Now.Date).Sum(e => e.Cost);
            HorseCount = _context.Appointments.Where(e => e.Date.Date == DateTime.Now.Date).Sum(e => e.NumberofHorses);
            ActiveEmployeeCount = _context.Employees.Where(e => e.IsActive == true).Count();
            DailyCustomerCount = _context.Appointments.Where(e => e.Date.Date == DateTime.Now.Date).GroupBy(e => e.CustomerId)
                .Count();

        }
    }
}
