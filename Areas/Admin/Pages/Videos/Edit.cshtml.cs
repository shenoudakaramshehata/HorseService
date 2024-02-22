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
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace HorseService.Areas.Admin.Pages.Videos
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Video video { get; set; }
        private HorseServiceContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public EditModel(HorseServiceContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

        }
        public IActionResult OnGet(int id)
        {
            try
            {
                video = _context.Videos.Where(c => c.VideoId == id).FirstOrDefault();
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

        public async Task<IActionResult> OnPost(int id, IFormFile file)
        {

            try
            {
                var videouloaded = _context.Videos.Find(id);
                if (videouloaded == null)
                {
                    return Redirect("../NotFound");
                }
                if (ModelState.IsValid)
                {
                   
                    if (file != null)
                    {
                        if (videouloaded.VideoUrl != null)
                        {
                            var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, videouloaded.VideoUrl);
                            if (System.IO.File.Exists(ImagePath))
                            {
                                System.IO.File.Delete(ImagePath);
                            }
                        }

                        string folder = "Videos/Employee/";
                        videouloaded.VideoUrl = await UploadImage(folder, file);
                    }
                    if (video.EmployeeId == 0)
                    {
                        _toastNotification.AddErrorToastMessage("Video Not Edited,must select Employee ");
                        return Page();
                    }
                    videouloaded.EmployeeId = video.EmployeeId;
                    videouloaded.Caption = video.Caption;
                    var UpdatedVideo = _context.Videos.Attach(videouloaded);
                    UpdatedVideo.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    _context.SaveChanges();
                    _toastNotification.AddSuccessToastMessage("Video Edited successfully");
                    return RedirectToPage("/Videos/Index");
                }

                
            }
                catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");

            }
            return Page();

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
