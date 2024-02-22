using System;
using System.Collections.Generic;
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

namespace HorseService.Areas.Admin.Pages.HomeSliders
{
    public class DetailsModel : PageModel
    {
        private HorseServiceContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public DetailsModel(HorseServiceContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
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



    }
}
