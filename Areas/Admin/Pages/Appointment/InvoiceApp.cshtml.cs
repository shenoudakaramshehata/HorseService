using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorseService.Data;
using HorseService.Models;
using HorseService.ReportModels;
using HorseService.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HorseService.Areas.Admin.Pages.Appointment
{
    public class InvoiceAppModel : PageModel
    {
        public HorseServiceContext _context { get; }
        public InvoiceAppModel(HorseServiceContext context)
        {
            _context = context;
        }

        public rptInvoice Report { get; set; }
        [BindProperty]
        public FilterModel filterModel { get; set; }

        public IActionResult OnGet(int id)
        {
            Report = new rptInvoice();
            var Invoice = _context.AppointmentDetails.Include(a=>a.Appointments).ThenInclude(a=>a.Customer).Where(e => e.AppointmentsId == id).Select(a=> new InvoiceVm { 
           CustomerNameEn= a.Appointments.Customer.CustomerNameEn,
           CustomerPhone= a.Appointments.Customer.CustomerPhone,
           AdditionalTypes=a.AdditionalTypes,
           Date=_context.AppoimentsDates.FirstOrDefault(e=>e.AppointmentsId==id).Date,
           ispaid= a.Appointments.ispaid,
           NumberofHorses=a.Appointments.NumberofHorses,
           OrderSerialNumber= a.Appointments.OrderSerialNumber,
           ServiceCost=a.Service.Cost,
           ServiceTitle= a.Service.Title,
           TimeFrom= _context.AppoimentsDates.FirstOrDefault(e => e.AppointmentsId == id).TimeFrom,
           TimeTowill= _context.AppoimentsDates.FirstOrDefault(e => e.AppointmentsId == id).TimeTowill,
           TotalAdditionalCost =a.TotalAdditionalCost,
           TotalCost=a.Appointments.Cost,
           TotalNumberofHorses= a.NumberOfHorses,

           }).ToList();
            Report.DataSource = Invoice;
            return Page();
        }
    }
}
