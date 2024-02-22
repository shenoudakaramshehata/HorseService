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

namespace HorseService.Areas.Admin.Pages.Brands
{
    public class DeleteModel : PageModel
    {
        private HorseServiceContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public DeleteModel(HorseServiceContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
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




        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
               
                employee = await _context.Employees.FindAsync(id);
                if (employee != null)
                {
                    if (_context.Appointments.Any(c => c.EmployeeId == id))
                    {
                        _toastNotification.AddErrorToastMessage("You cannot delete this Employee");
                        return Page();

                    }
                    if (_context.Videos.Any(c => c.EmployeeId == id))
                    {
                        _toastNotification.AddErrorToastMessage("You cannot delete this Employee");
                        return Page();
                    }
                    _context.Employees.Remove(employee);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("Employee Deleted successfully");
                    var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images/Employee/" + employee.Image);
                    if (System.IO.File.Exists(ImagePath))
                    {
                        System.IO.File.Delete(ImagePath);
                    }
                }
                else
                    return Redirect("../Error");
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
