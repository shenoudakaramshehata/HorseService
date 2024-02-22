using DevExtreme.AspNet.Mvc;
using HorseService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorseService.Data;
using System.Globalization;


namespace HorseService.Controllers
{
    [Route("api/[controller]/[action]")]
    public class StatisticsController : Controller
    {
        private HorseServiceContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StatisticsController(HorseServiceContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public object GetDailyAppointment(DataSourceLoadOptions loadOptions)
        {
            var dailyAppointment = _context.Appointments.Include(e=>e.Employee)
                .Where(o => o.Date.Date == DateTime.Now.Date)
                .GroupBy(c => c.EmployeeId).
                Select(g => new
                {

                    employee =_context.Employees.Where(e=>e.EmployeeId== g.Key).FirstOrDefault().FullName,

                    Appointments = g.Count()

                }).OrderByDescending(r => r.Appointments);

            return dailyAppointment;
        }

        [HttpGet]
        public object GeCosttDailyAppointment(DataSourceLoadOptions loadOptions)
        {
            var CostdailyAppointment = _context.Appointments.Include(e => e.Employee)
                .Where(o => o.Date.Date == DateTime.Now.Date)
                .GroupBy(c => c.EmployeeId).


                Select(g => new
                {

                    employee = _context.Employees.Where(e => e.EmployeeId == g.Key).FirstOrDefault().FullName,

                    Cost = g.Sum(e=>e.Cost)

                }).OrderByDescending(r => r.Cost);

            return CostdailyAppointment;
        }

        [HttpGet]
        public object GetWeeklyAppointmentCount(DataSourceLoadOptions loadOptions)
        {
            var WeeklyAppointmentCount = _context.Appointments
                .Where(o => o.Date.Date >= DateTime.Now.Date && o.Date.Date <= DateTime.Now.Date.AddDays(6))
                .GroupBy(c =>c.Date.Date).

            Select(g => new
                {

                    OnDay =g.Key,

                    Count = g.Count()

                }).OrderByDescending(r => r.Count);

            return WeeklyAppointmentCount;
        }

        [HttpGet]
        public object GetWeeklyHorsesCount(DataSourceLoadOptions loadOptions)
        {
            var WeeklyHorsesCount = _context.Appointments
                .Where(o => o.Date.Date >= DateTime.Now.Date && o.Date.Date <= DateTime.Now.Date.AddDays(6))
                .GroupBy(c => c.Date.Date).


                Select(g => new
                {

                    OnDay = g.Key,

                    Count = g.Sum(e=>e.NumberofHorses),

                }).OrderByDescending(r => r.Count);

            return WeeklyHorsesCount;
        }

        //[HttpGet]
        //public object GetDailyOrdersRevenue(DataSourceLoadOptions loadOptions)
        //{
        //    var dailyOrder = _context.Orders
        //        .Where(o => o.OrderDate.Value.Year == DateTime.Now.Year)
        //        .GroupBy(c => c.OrderDate.Value.Date).

        //        Select(g => new
        //        {

        //            day = g.Key,

        //            sales = g.Sum(s => s.Total)

        //        }).OrderByDescending(r => r.day);

        //    return dailyOrder;


        //}

    }
}
