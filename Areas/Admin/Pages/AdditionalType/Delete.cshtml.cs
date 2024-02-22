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

namespace HorseService.Areas.Admin.Pages.AdditionalType
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
        public Models.AdditionalType AdditionalType { get; set; }


        public  IActionResult OnGetAsync(int id)
        {
            try
            {
                AdditionalType =  _context.AdditionalTypes.FirstOrDefault(m => m.AdditionalTypeId == id);
                if (AdditionalType == null)
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

                AdditionalType = await _context.AdditionalTypes.FindAsync(id);
                if (AdditionalType != null)
                {
                    _context.AdditionalTypes.Remove(AdditionalType);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("Additional Type Deleted successfully");
                   
                }
                else
                    return Redirect("../NotFound");
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
