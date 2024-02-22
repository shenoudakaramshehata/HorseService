using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HorseService.Data;
using HorseService.Models;

namespace HorseService.Controllers
{
    public class SchedulerController : Controller
    {
        private HorseServiceContext _context;

        public SchedulerController(HorseServiceContext context)
        {
            _context = context;
        }

        public ActionResult BasicViews()
        {
            var Appointments = _context.Appointments.Where(e => e.EmployeeId == 4).Select(i => new
            {
                i.AppointmentsId,
                i.TimeFrom,
                i.TimeTowill
            });
            return View(Appointments);
        }
    }
}