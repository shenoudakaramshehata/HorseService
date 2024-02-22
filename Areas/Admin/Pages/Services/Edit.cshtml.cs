using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using HorseService.Data;
using HorseService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace HorseService.Areas.Admin.Pages.Services
{
    public class EditModel : PageModel
    {

        private HorseServiceContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public EditModel(HorseServiceContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

        }
        [BindProperty]
        public Service Service { get; set; }
        public  IActionResult OnGet(int id)
        {
            try
            {
                if (id == 0)
                {
                    return Redirect("../NotFound");

                }
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
        public IActionResult OnPost(int id)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                Service.ServiceId = id;
                _context.Attach(Service).State = EntityState.Modified;
                 _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Service Edited successfully");
            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }

            return Redirect("./Index");

        }

    }
}
