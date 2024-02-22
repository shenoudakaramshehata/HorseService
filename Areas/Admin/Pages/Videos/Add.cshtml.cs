using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HorseService.Data;
using HorseService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace HorseService.Areas.Admin.Pages.Videos
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
        public async Task<IActionResult> OnPost(IFormFile file, Video videos)
        {
            
            if (file != null)
            {
                string folder = "Videos/Employee/";
                videos.VideoUrl = await UploadImage(folder, file);
            }

            
                _context.Videos.Add(videos);

                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Video Added successfully");
                return RedirectToPage("/Videos/Index");
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_hostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return folderPath;
        }
    }
}
