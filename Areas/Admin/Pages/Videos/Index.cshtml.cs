using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorseService.Data;
using HorseService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace HorseService.Areas.Admin.Pages.Videos
{
    public class IndexModel : PageModel
    {
        public List<Video> employeeVideos { get; set; }
        private HorseServiceContext _context;


        private readonly IToastNotification _toastNotification;
        public IndexModel(HorseServiceContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }
        public void OnGet(int? id)
        {
            if (id != null)
            {
                employeeVideos = _context.Videos.Where(e => e.EmployeeId == id).ToList();
            }
            else
            {
                employeeVideos = _context.Videos.ToList();
            }
        }
    }
}
