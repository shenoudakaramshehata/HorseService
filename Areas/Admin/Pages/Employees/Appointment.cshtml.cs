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

namespace HorseService.Areas.Admin.Pages.Employees
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
       
        
        
        public Employee employee { get; set; }



        public async Task<IActionResult> OnGetAsync(int id)
        {

            try
            {
               employee = _context.Employees.FirstOrDefault(c => c.EmployeeId == id);
                if (employee == null)
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
