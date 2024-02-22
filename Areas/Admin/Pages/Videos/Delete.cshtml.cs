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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NToastNotify;

namespace HorseService.Areas.Admin.Pages.Videos
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
        public Video video { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                video = _context.Videos.Include(c => c.Employee).Where(c => c.VideoId == id).FirstOrDefault();
                if (video == null)
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
                video = await _context.Videos.Include(c => c.Employee).FirstOrDefaultAsync(m => m.VideoId == id);

                if (video != null)
                {
                    _context.Videos.Remove(video);
                    await _context.SaveChangesAsync();
                }
                if (video.VideoUrl != null)
                {
                    var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, video.VideoUrl);
                    if (System.IO.File.Exists(ImagePath))
                    {
                        System.IO.File.Delete(ImagePath);
                    }
                }

         _toastNotification.AddSuccessToastMessage("Video Deleted successfully");
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
