using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using HorseService.Data;
using HorseService.Models;
using Microsoft.AspNetCore.Http;

namespace HorseService.Areas.Admin.Pages.HomeSliders
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
        public async Task<IActionResult> OnPost(IFormFile file, HomeSlider model)
        {

            if (file != null)
            {
                string folder = "Images/Slider/";
                model.HomeSliderPic = await UploadImage(folder, file);
            }

            _context.HomeSliders.Add(model);

            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Picture Added successfully");
            return RedirectToPage("/HomeSliders/Index");
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_hostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return folderPath;
        }







        //public IActionResult OnPost(HomeSlider model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }
        //    try
        //    {
        //        if (model.HomeSliderId != 0)
        //        {
        //            var uniqeFileName = "";

        //            if (Response.HttpContext.Request.Form.Files.Count() > 0)
        //            {
        //                string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images/Slider");
        //                string ext = Path.GetExtension(Response.HttpContext.Request.Form.Files[0].FileName);
        //                uniqeFileName = Guid.NewGuid().ToString("N") + ext;
        //                string uploadedImagePath = Path.Combine(uploadFolder, uniqeFileName);
        //                using (FileStream fileStream = new FileStream(uploadedImagePath, FileMode.Create))
        //                {
        //                    Response.HttpContext.Request.Form.Files[0].CopyTo(fileStream);
        //                }
        //                model.HomeSliderPic = uniqeFileName;
        //            }
        //            _context.HomeSliders.Add(model);
        //            _context.SaveChanges();
        //            _toastNotification.AddSuccessToastMessage("Home Slider Added successfully");

        //        }
        //    }
        //    catch (Exception)
        //    {

        //        _toastNotification.AddErrorToastMessage("Something went wrong");
        //    }
          
        //    return Redirect("./Index");

        //}
    }
}
