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
        public IActionResult OnPost(Service model)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                _context.Services.Add(model);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Service Added successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }

            return Redirect("./Index");

        }

    }
}
