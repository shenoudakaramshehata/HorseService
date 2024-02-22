using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorseService.Data;
using HorseService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace HorseService.Areas.Admin.Pages.OffDay
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


        public OffDays offDays { get; set; }



        public IActionResult OnGet(int id)
        {
            try
            {
                offDays = _context.OffDays.Include(e => e.BreakTypes).Include(e=>e.Employee).Where(c => c.OffDaysId == id).FirstOrDefault();
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
    }
}
