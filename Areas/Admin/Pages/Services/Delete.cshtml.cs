using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HorseService.Data;
using HorseService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace HorseService.Areas.Admin.Pages.Services
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
        public Service Service { get; set; }


        public  IActionResult OnGetAsync(int id)
        {
            try
            {
                Service =  _context.Services.FirstOrDefault(m => m.ServiceId == id);
                if (Service == null)
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

                Service = await _context.Services.FindAsync(id);
                if (Service != null)
                {
                    _context.Services.Remove(Service);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("Service Deleted successfully");
                   
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
