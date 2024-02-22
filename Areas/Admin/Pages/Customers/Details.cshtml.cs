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

namespace HorseService.Areas.Admin.Pages.Customers
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
        public Customer customer  { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);

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
