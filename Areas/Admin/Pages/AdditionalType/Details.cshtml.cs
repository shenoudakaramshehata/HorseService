using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorseService.Data;
using HorseService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace HorseService.Areas.Admin.Pages.AdditionalType
{
    public class DetailsModel : PageModel
    {
        private HorseServiceContext _context;


        private readonly IToastNotification _toastNotification;
        public DetailsModel(HorseServiceContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }
        [BindProperty]
        public Models.AdditionalType additionalType { get; set; }


        public  IActionResult OnGetAsync(int id)
        {

            try
            {
                additionalType =  _context.AdditionalTypes.FirstOrDefault(m => m.AdditionalTypeId == id);

                if (additionalType == null)
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
