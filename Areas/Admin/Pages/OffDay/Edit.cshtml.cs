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
using Microsoft.EntityFrameworkCore;
namespace HorseService.Areas.Admin.Pages.OffDay
{
    public class EditModel : PageModel
    {
        public OffDays offDays { set; get; }
        private HorseServiceContext _context;
        private readonly IToastNotification _toastNotification;

        public EditModel(HorseServiceContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
            offDays = new OffDays();

        }
       
        public IActionResult OnGet(int id)
        {
            try
            {
                offDays = _context.OffDays.Where(c => c.OffDaysId == id).FirstOrDefault();
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
        
        public IActionResult OnPost( OffDays offDays)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                if (offDays == null)
                {
                    return Redirect("../NotFound");
                }
                if (offDays.To < offDays.From)
                {
                    ModelState.AddModelError("DateError", "Date To must be greater than Date From");
                    return Page();
                }
                if (offDays.EmployeeId == 0)
                {
                    ModelState.AddModelError("EmployeeError", "Employee Is Required..");
                    return Page();
                }
                if (offDays.breaktypesId == 0)
                {
                    ModelState.AddModelError("BreakTypeError", "Break Type Is Required..");
                    return Page();
                }
                else
                {
                    if (offDays.breaktypesId == 1)
                    {
                        if (offDays.From == null || offDays.To == null)
                        {
                            ModelState.AddModelError("FromandToError", "From Date & To Date is Required");
                            return Page();
                        }
                        if (offDays.From.Value.Date != offDays.To.Value.Date)
                        {
                            ModelState.AddModelError("DateError", "From and To Date must be the same");
                            return Page();
                        }
                        var dayStart = new DateTime(offDays.From.Value.Year, offDays.From.Value.Month, offDays.From.Value.Day, 9, 0, 0);
                        var dayEnd = new DateTime(offDays.From.Value.Year, offDays.From.Value.Month, offDays.From.Value.Day, 22, 0, 0);
                        if (!(offDays.From > dayStart && offDays.From < dayEnd))
                        {
                            ModelState.AddModelError("DateError", "From and To Date must be in the range of 9am to 10pm");
                            return Page();
                        }
                        if (!(offDays.To > dayStart && offDays.To < dayEnd))
                        {
                            ModelState.AddModelError("DateError", "From and To Date must be in the range of 9am to 10pm");
                            return Page();
                        }

                        if (offDays.To < offDays.From)
                        {
                            ModelState.AddModelError("DateError", "Date To must be greater than Date From");
                            return Page();
                        }

                        offDays.Onday = null;
                        var appointment = _context.Appointments.Where(e => e.Date.Date == offDays.From.Value.Date
                         && (offDays.From >= e.TimeFrom && offDays.From < e.TimeTowill) || (offDays.To > e.TimeFrom && offDays.To <= e.TimeTowill)).FirstOrDefault();
                        if (appointment != null)
                        {
                            ModelState.AddModelError("PeriodError", "This Period has been reserved");
                            return Page();
                        }
                        //    var appointment = _context.Appointments.Where(e => e.Date.Date == offDays.From.Value.Date && e.Date.Date == offDays.To.Value.Date).OrderBy(e=>e.TimeFrom).ToList();
                        //    for (int i = 0; i < appointment.Count; i++)
                        //    {
                        //        if (i == appointment.Count - 1)
                        //        {

                        //            if (offDays.From < appointment[i].TimeTowill)
                        //            {
                        //                ModelState.AddModelError("PeriodError", "This Period has been reserved");
                        //                return Page();
                        //            }
                        //        }                               
                        //                if (offDays.From < appointment[i].TimeTowill)
                        //                {
                        //                    ModelState.AddModelError("PeriodError", "This Period has been reserved");
                        //                    return Page();
                        //                }
                    }


                    //}
                    else if (offDays.breaktypesId == 2)
                    {

                        offDays.From = new DateTime(offDays.Onday.Value.Year, offDays.Onday.Value.Month, offDays.Onday.Value.Day, 12, 1, 0);
                        offDays.To = new DateTime(offDays.Onday.Value.Year, offDays.Onday.Value.Month, offDays.Onday.Value.Day, 23, 59, 0);
                        if (offDays.Onday == null)
                        {
                            ModelState.AddModelError("Onday", "On Date is Required");
                            return Page();
                        }
                        var appointment = _context.Appointments.Where(e => e.Date.Date == offDays.Onday.Value.Date).ToList();
                        if (appointment != null && appointment.Count != 0)
                        {
                            ModelState.AddModelError("PeriodError", "This day already has Appointments");
                            return Page();
                        }
                    }
                    else if (offDays.breaktypesId == 3)
                    {
                        if (offDays.To < offDays.From)
                        {
                            ModelState.AddModelError("DateError", "Date To must be greater than Date From");
                            return Page();
                        }
                        offDays.Onday = null;
                        if (offDays.From == null || offDays.To == null)
                        {
                            ModelState.AddModelError("FromandToError", "From Date & To Date is Required");
                            return Page();
                        }
                        var appointments = _context.Appointments.Where(e => e.Date.Date >= offDays.From.Value.Date && e.Date.Date <= offDays.To.Value.Date).ToList();
                        if (appointments != null && appointments.Count != 0)
                        {
                            ModelState.AddModelError("PeriodError", "This Period is not suitable!,Please Check Employee Appointments to Find suitable period to be break!");
                            return Page();
                        }
                    }
                }
                var UpdatedOffDay = _context.OffDays.Attach(offDays);
                UpdatedOffDay.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                 _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("OffDay Edited successfully");
            }

            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("./Index");


        }
    }
           
            
    }
