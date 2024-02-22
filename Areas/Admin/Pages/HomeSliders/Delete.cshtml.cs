using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NToastNotify;
using HorseService.Data;
using HorseService.Models;
using HorseService;

namespace HorseService.Areas.Admin.Pages.HomeSliders
{
    public class DeleteModel : PageModel
    {

        private HorseServiceContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IStringLocalizer<SharedResource> _sharedResource;
        private readonly IToastNotification _toastNotification;

        public DeleteModel(HorseServiceContext context, IWebHostEnvironment hostEnvironment, IStringLocalizer<SharedResource> sharedResource, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _sharedResource = sharedResource;
            _toastNotification = toastNotification;

        }


        [BindProperty]
        public HomeSlider homeSlider { get; set; }

        [BindProperty]
        public string EntityNameEn { get; set; }
        [BindProperty]
        public string EntityNameAr { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                homeSlider = _context.HomeSliders.Where(c => c.HomeSliderId == id).FirstOrDefault();
                if (homeSlider == null)
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
                homeSlider = await _context.HomeSliders.FirstOrDefaultAsync(m => m.HomeSliderId == id);

                if (homeSlider != null)
                {
                    _context.HomeSliders.Remove(homeSlider);
                    await _context.SaveChangesAsync();
                }
                if (homeSlider.HomeSliderPic != null)
                {
                    var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, homeSlider.HomeSliderPic);
                    if (System.IO.File.Exists(ImagePath))
                    {
                        System.IO.File.Delete(ImagePath);
                    }
                }

                _toastNotification.AddSuccessToastMessage("Picture Deleted successfully");
                return RedirectToPage("./Index");
            }
            catch (Exception)

            {
                _toastNotification.AddErrorToastMessage("Something went wrong");

                return Page();

            }


        }


    }
    
}
