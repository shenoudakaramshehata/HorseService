using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorseService.Data;
using HorseService.Models;
using HorseService.ReportModels;
using HorseService.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HorseService.Areas.Admin.Pages.ReportsManagement
{
    public class EmployeeAppoitmentModel : PageModel
    {
        public HorseServiceContext _context { get; }
        public EmployeeAppoitmentModel(HorseServiceContext context)
        {
            _context = context;
        }
      
        public rptAppointmentEmployee Report { get; set; }
        [BindProperty]
        public FilterModel filterModel { get; set; }

        public void OnGet()
        {
            
        }
        public IActionResult OnPost()
        {

            List<EmployeeAppointmentM> ds = _context.Appointments.Include(a => a.Employee).Include(a => a.Customer).Include(a => a.AppointmentStatus).Select(i => new EmployeeAppointmentM
            {
                FullName = i.Employee.FullName,
                NumberofHorses=i.NumberofHorses,
                Image=i.Employee.Image,
                CustomerName=i.Customer.CustomerNameEn,
                Cost=i.Cost,
                Date=i.Date,
                 AppointmentStatus=i.AppointmentStatus.AppointmentStatusTitle,
                 TimeFrom=i.TimeFrom,
                 TimeTowill=i.TimeTowill,
                 EmployeeId = i.EmployeeId,
                 Address=i.CustomerAddress,
                 Lat=i.Lat,
                 Lng=i.Lng,
                 ispaid=i.ispaid==true?"Paid":"Not Paid"
                //SerialNo=i.OrderSerialNumber

            }).ToList();

            if (filterModel.EmployeeId != null)
            {
                ds = ds.Where(i => i.EmployeeId == filterModel.EmployeeId).ToList();
            }
            if (filterModel.radiobtn != null)
            {
                if(filterModel.radiobtn== "في يوم")
                {
                    if(filterModel.OnDay != null)
                    {
                        ds = ds.Where(i => i.Date.Date == filterModel.OnDay.Value.Date).ToList();
                    }
                    else
                    {
                        ds = null;
                    }
                }
               else if(filterModel.radiobtn == "فترة")
                {
                    if (filterModel.FromDate != null && filterModel.ToDate == null)
                    {
                        ds = null;
                    }
                    if (filterModel.FromDate == null && filterModel.ToDate != null)
                    {
                        ds = null;
                    }
                    if (filterModel.FromDate != null && filterModel.ToDate != null)

                    {
                        ds = ds.Where(i => i.Date.Date >= filterModel.FromDate.Value.Date && i.Date.Date <= filterModel.ToDate.Value.Date).ToList();
                    }
                }
            }
            if (filterModel.radiobtn == null && (filterModel.OnDay != null || filterModel.FromDate != null || filterModel.ToDate != null))
            {
                ds = null;
            }

            if (filterModel.EmployeeId == null && filterModel.FromDate == null && filterModel.ToDate == null&& filterModel.radiobtn==null)
            {
                ds = null;
            }

            Report = new rptAppointmentEmployee();
            Report.DataSource = ds;
            return Page();

        }
    }
}
