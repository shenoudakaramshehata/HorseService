using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using HorseService.Data;
using HorseService.Models;

namespace HorseService.Areas.Admin.Pages.Brands
{
    public class DetailsModel : PageModel
    {

        private HorseServiceContext _context;


        private readonly IToastNotification _toastNotification;
        public DetailsModel(HorseServiceContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }
        [BindProperty]
        public Employee employee { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
           
            try
            {
                employee = await _context.Employees.FirstOrDefaultAsync(m => m.EmployeeId == id);
                
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
