using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using HorseService.Data;
using HorseService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorseService.Pages
{
    public class IndexModel : PageModel


    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HorseServiceContext _context;

        public IndexModel(HorseServiceContext context, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _context = context;
        }
       
        [BindProperty]
        public ContactUs contactUs { get; set; }

        public void OnGet()
        {

        }

        //public IActionResult OnPostAddNewsletter()

        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Redirect("./Index");
        //    }
        //    try
        //    {
        //        newsletter.Date = DateTime.Now;
        //        _context.Newsletters.Add(newsletter);
        //        _context.SaveChanges();
        //    }
        //    catch (Exception)
        //    {

        //        return Redirect("./Index");
        //    }

        //    return Redirect("./Index");

        //}
        //public IActionResult OnPostAddContactUs()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Redirect("./Index");
        //    }
        //    try
        //    {
               
        //        _context.ContactUs.Add(contactUs);
        //        _context.SaveChanges();
        //    }
        //    catch (Exception)
        //    {

        //        return Redirect("./Index");

        //    }

        //    return Redirect("./Index");

        //}
  
    }
}
