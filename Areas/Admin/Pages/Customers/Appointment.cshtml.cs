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
    public class AppointmentModel : PageModel
    {
        private HorseServiceContext _context;
        private readonly IToastNotification _toastNotification;
        public AppointmentModel(HorseServiceContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }

        
        public Customer customer { get; set; }



        public async Task<IActionResult> OnGetAsync(int id)
        {

            try
            {
                customer = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
                if (customer == null)
                {
                    return Redirect("../NotFound");

                }

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");

            }


            return Page();


        }
    }
}
