using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorseService.Data;
using HorseService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace HorseService.Areas.Admin.Pages.Appointment
{
    public class AppointmentDetailsModel : PageModel
    {
        private HorseServiceContext _context;
        private readonly IToastNotification _toastNotification;

        public AppointmentDetailsModel(HorseServiceContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
           
        }
        [BindProperty]
        public Appointments appointments { get; set; }
        public PaymentMethod paymentMethod { get; set; }
       
        public AppointmentStatus appointmentStatus { get; set; }

        static int AppoimentId = 0;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            AppoimentId = id;
            try
            {
                if (!_context.Appointments.Any(c => c.AppointmentsId == id))
                {
                    return Redirect("../NotFound");
                }
                appointments = await _context.Appointments
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                //.Include(o=>o.Service)
                .FirstOrDefaultAsync(m => m.AppointmentsId == id);
                paymentMethod = _context.PaymentMethods.FirstOrDefault(c => c.PaymentMethodId == appointments.PaymentMethodId);
                appointmentStatus = _context.AppointmentStatuses.FirstOrDefault(c => c.AppointmentStatusId == appointments.AppointmentStatusId);

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Page();

        }
        public async Task<IActionResult> OnPostAsync(bool isPaid)
        {
            try
            {
                if (!_context.Appointments.Any(c => c.AppointmentsId == AppoimentId))
                {
                    return Redirect("../NotFound");
                }
                appointments = await _context.Appointments
                .Include(o => o.Customer)
                .Include(o => o.Employee)
               
                .FirstOrDefaultAsync(m => m.AppointmentsId == AppoimentId);
                paymentMethod = _context.PaymentMethods.FirstOrDefault(c => c.PaymentMethodId == appointments.PaymentMethodId);
                appointmentStatus = _context.AppointmentStatuses.FirstOrDefault(c => c.AppointmentStatusId == appointments.AppointmentStatusId);
                appointments.ispaid = isPaid;
                _context.Entry(appointments).State = EntityState.Modified;
                _context.SaveChanges();

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return RedirectToPage("Index");
        }
    }
}
