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

namespace HorseService.Areas.Admin.Pages.Customers
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
        [BindProperty]
        public PaymentMethod paymentMethod { get; set; }
        [BindProperty]

        public AppointmentStatus AppointmentStatus { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                appointments = await _context.Appointments
               .Include(o => o.Employee)
               .Include(o => o.Customer)
               .FirstOrDefaultAsync(m => m.AppointmentsId == id);
                if (appointments == null)
                {
                    return Redirect("../NotFound");
                }
                paymentMethod = _context.PaymentMethods.FirstOrDefault(c => c.PaymentMethodId == appointments.PaymentMethodId);
                AppointmentStatus = _context.AppointmentStatuses.FirstOrDefault(c => c.AppointmentStatusId == appointments.AppointmentStatusId);

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");

            }


            return Page();
        }
    }
}
