using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using HorseService.Data;
using HorseService.Models;
using Microsoft.AspNetCore.Http;

namespace HorseService.Areas.Admin.Pages.HomeSliders
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
        public HomeSlider homeSlider { get; set; }
      
        
        public IActionResult OnGetAsync(int id)
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
        public async Task<IActionResult> OnPost(int id, IFormFile file)
        {

            try
            {
                var Pictureloaded = _context.HomeSliders.Find(id);
                if (Pictureloaded == null)
                {
                    return Redirect("../NotFound");
                }
               

                    if (file != null)
                    {
                        if (Pictureloaded.HomeSliderPic != null)
                        {
                            var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, Pictureloaded.HomeSliderPic);
                            if (System.IO.File.Exists(ImagePath))
                            {
                                System.IO.File.Delete(ImagePath);
                            }
                        }

                        string folder = "Images/Slider/";
                        Pictureloaded.HomeSliderPic = await UploadImage(folder, file);
                    }
                   
                    var UpdatedVideo = _context.HomeSliders.Attach(Pictureloaded);
                    UpdatedVideo.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    _context.SaveChanges();
                    _toastNotification.AddSuccessToastMessage("Picture Edited successfully");
                    return RedirectToPage("/HomeSliders/Index");
                


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
