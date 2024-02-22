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
    [Route("api/[controller]/[action]")]
    public class LookupsController : Controller
    {
        private HorseServiceContext _context;

        public LookupsController(HorseServiceContext context) {
            _context = context;
        }

       
        [HttpGet]
        public async Task<IActionResult> EmployeesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Employees
                         orderby i.FullName
                         select new {
                             Value = i.EmployeeId,
                             Text = i.FullName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> AppointmentStatusesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.AppointmentStatuses
                         orderby i.AppointmentStatusTitle
                         select new {
                             Value = i.AppointmentStatusId,
                             Text = i.AppointmentStatusTitle
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
        [HttpGet]
        public async Task<IActionResult> CustomersLookup(DataSourceLoadOptions loadOptions)
        {
         
            
                var lookupEn = from i in _context.Customers
                               orderby i.CustomerNameEn
                               select new
                               {
                                   Value = i.CustomerId,
                                   Text = i.CustomerNameEn
                               };
            
           
            return Json(await DataSourceLoader.LoadAsync(lookupEn, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> PaymentMethodLookup(DataSourceLoadOptions loadOptions)
        {

           
                var lookupEn = from i in _context.PaymentMethods
                               orderby i.PaymentMethodTitle
                               select new
                               {
                                   Value = i.PaymentMethodId,
                                   Text = i.PaymentMethodTitle
                               };
                return Json(await DataSourceLoader.LoadAsync(lookupEn, loadOptions));
            }

        [HttpGet]
        public async Task<IActionResult> BreakTypesLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.BreakTypes
                         orderby i.breaktypesId
                         select new
                         {
                             Value = i.breaktypesId,
                             Text = i.Title
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
         [HttpGet]
        public async Task<IActionResult>ServicesLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Services
                         orderby i.ServiceId
                         select new
                         {
                             Value = i.ServiceId,
                             Text = i.Title
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }


    }
}