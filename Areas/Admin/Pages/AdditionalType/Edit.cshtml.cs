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

namespace HorseService.Areas.Admin.Pages.AdditionalType
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
        public Models.AdditionalType AdditionalType { get; set; }
        public  IActionResult OnGet(int id)
        {
            try
            {
                if (id == 0)
                {
                    return Redirect("../NotFound");

                }
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
        public IActionResult OnPost(int id)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                AdditionalType.AdditionalTypeId = id;
                _context.Attach(AdditionalType).State = EntityState.Modified;
                 _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Additional Type Edited successfully");
            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
                return Page();
            }

            return Redirect("./Index");

        }

    }
}
