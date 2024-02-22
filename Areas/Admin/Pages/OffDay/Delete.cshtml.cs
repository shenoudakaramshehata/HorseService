using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NToastNotify;
using HorseService.Data;
using HorseService.Models;

namespace HorseService.Areas.Admin.Pages.OffDay
{
    public class DeleteModel : PageModel
    {
        private HorseServiceContext _context;
        private readonly IToastNotification _toastNotification;

        public DeleteModel(HorseServiceContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;

        }
        [BindProperty]
        public OffDays offDays { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                offDays = await _context.OffDays.Include(c => c.Employee).Include(c => c.BreakTypes).FirstOrDefaultAsync(m => m.OffDaysId == id);
                if (offDays == null)
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




        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {

                offDays = await _context.OffDays.FindAsync(id);
                if (offDays != null)
                {
                    if (_context.OffDays.Any(c => c.EmployeeId == id))
                    {
                        _toastNotification.AddErrorToastMessage("You cannot delete this Employee");
                        return Page();

                    }
                    if (_context.BreakTypes.Any(c => c.breaktypesId == id))
                    {
                        _toastNotification.AddErrorToastMessage("You cannot delete this Break Type");
                        return Page();
                    }
                    _context.OffDays.Remove(offDays);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("OffDay Deleted successfully");
                }
            }
            catch (Exception)

            {
                _toastNotification.AddErrorToastMessage("Something went wrong");

                return Page();

            }
            return RedirectToPage("./Index");
        }

    }
}
