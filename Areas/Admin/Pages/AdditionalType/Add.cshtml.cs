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
    public class AddModel : PageModel
    {
        private HorseServiceContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public AddModel(HorseServiceContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

        }

        public void OnGet()
        {



        }
        public IActionResult OnPost(Models.AdditionalType model)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                _context.AdditionalTypes.Add(model);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Additional Type Added successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }

            return Redirect("./Index");

        }

    }
}
