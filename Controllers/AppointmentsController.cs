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
    public class AppointmentsController : Controller
    {
        private HorseServiceContext _context;
        public  static List<Appointments> Appoints  { get; set; }
        public AppointmentsController(HorseServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var appointments = _context.Appointments.Select(i => new {
                i.AppointmentsId,
                i.NumberofHorses,
                i.Date,
                i.TimeFrom,
                i.TimeTowill,
                i.Remarks,
                i.EmployeeId,
                i.CustomerId,
                i.Cost,
                i.PaymentMethodId,
                i.AppointmentStatusId,
                i.CustomerAddress,
                i.Lat,
                i.Lng,
                i.ispaid
            }).OrderByDescending(e=>e.Date.Date);

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "AppointmentsId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(appointments, loadOptions));
        }
        [HttpGet]
        public async Task<IActionResult> GetAdditionalTypes(DataSourceLoadOptions loadOptions,int AppointmentsId)
        {
            var appointments = _context.AppointmentAdditionalTypes.Include(e=>e.AdditionalType).Where(a=>a.AppointmentsId== AppointmentsId).Select(i => new {
                i.AdditionalType,
                i.AdditionalType.Title,
                i.AdditionalType.Cost,
               i.AppointmentAdditionalTypesId,
               i.AdditionalTypeId,
               i.AppointmentsId

     
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "AppointmentsId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(appointments, loadOptions));
        }
        [HttpGet]
        public async Task<IActionResult> GetAppointmentDetails(DataSourceLoadOptions loadOptions, int AppointmentsId)
        {
            var appointmentDetailsList = _context.AppointmentDetails.Where(a => a.AppointmentsId == AppointmentsId).Select(i => new {
                i.Service,
                i.NumberOfHorses,
                i.Cost,
                i.TotalAdditionalCost,
                i.AdditionalTypes,
                i.AppointmentDetailsId


            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "AppointmentsId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(appointmentDetailsList, loadOptions));
        }
        [HttpGet]
        public async Task<IActionResult> GetAppointmentDates(DataSourceLoadOptions loadOptions, int AppointmentsId)
        {
            var AppointmentDatesList = _context.AppoimentsDates.Where(a => a.AppointmentsId == AppointmentsId).Select(i => new {
                i.Date,
                i.TimeFrom,
                i.TimeTowill,
                i.AppoimentsDateId

                


            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "AppointmentsId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(AppointmentDatesList, loadOptions));
        }
        [HttpPut]
        public IActionResult PutNotPaid(int key, string values)
        {
            var employee = _context.Appointments.First(a => a.AppointmentsId == key);
            JsonConvert.PopulateObject(values, employee);

            //if (!TryValidateModel(employee))
            //    return BadRequest(ModelState.GetFullErrorMessage());

            _context.SaveChanges();

            return Ok();
        }
        [HttpPut]
        public IActionResult PutPaidAppo(int key, string values)
        {
            var appointments = _context.Appointments.First(a => a.AppointmentsId == key);
            JsonConvert.PopulateObject(values, appointments);

            //if (!TryValidateModel(employee))
            //    return BadRequest(ModelState.GetFullErrorMessage());

            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetNotPaid(DataSourceLoadOptions loadOptions)
        {
            var appointments = _context.Appointments.Where(a=>a.ispaid==false).Select(i => new {
                i.AppointmentsId,
                i.NumberofHorses,
                i.Date,
                i.TimeFrom,
                i.TimeTowill,
                i.Remarks,
                i.EmployeeId,
                i.CustomerId,
                i.Cost,
                i.PaymentMethodId,
                i.AppointmentStatusId,
                i.CustomerAddress,
                i.Lat,
                i.Lng,
                i.ispaid
            });
            //Appoints = appointments.ToList();

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "AppointmentsId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(appointments, loadOptions));
        }
        [HttpGet]
        public async Task<IActionResult> GetPaid(DataSourceLoadOptions loadOptions)
        {
            var appointments = _context.Appointments.Where(a => a.ispaid == true).Select(i => new {
                i.AppointmentsId,
                i.NumberofHorses,
                i.Date,
                i.TimeFrom,
                i.TimeTowill,
                i.Remarks,
                i.EmployeeId,
                i.CustomerId,
                i.Cost,
                i.PaymentMethodId,
                i.AppointmentStatusId,
                i.CustomerAddress,
                i.Lat,
                i.Lng,
                i.ispaid
            });
    

            return Json(await DataSourceLoader.LoadAsync(appointments, loadOptions));
        }
        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Appointments();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Appointments.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.AppointmentsId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {
            var model = await _context.Appointments.FirstOrDefaultAsync(item => item.AppointmentsId == key);
            if (model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Appointments.FirstOrDefaultAsync(item => item.AppointmentsId == key);

            _context.Appointments.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> GetEmployeeAppointments(int employeeId, DataSourceLoadOptions loadOptions)
        {
            var employess = _context.Appointments.Where(c => c.EmployeeId == employeeId).Select(i => new {
                i.AppointmentsId,
                i.EmployeeId,
                i.NumberofHorses,
                i.Date,
                i.TimeFrom,
                i.TimeTowill,
                i.Remarks,
                i.CustomerId,
                i.AppointmentStatusId,
                i.Cost,
                i.PaymentMethodId

            });

            return Json(await DataSourceLoader.LoadAsync(employess, loadOptions));
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerAppointments(int customerId, DataSourceLoadOptions loadOptions)
        {
            var customerAppointment = _context.Appointments.Where(c => c.CustomerId == customerId).Select(i => new {
                i.AppointmentsId,
                i.EmployeeId,
                i.NumberofHorses,
                i.Date,
                i.TimeFrom,
                i.TimeTowill,
                i.Remarks,
                i.CustomerId,
                i.AppointmentStatusId,
                i.Cost,
                i.PaymentMethodId

            });

            return Json(await DataSourceLoader.LoadAsync(customerAppointment, loadOptions));
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
          


        private void PopulateModel(Appointments model, IDictionary values) {
            string APPOINTMENTS_ID = nameof(Appointments.AppointmentsId);
            string NUMBEROF_HORSES = nameof(Appointments.NumberofHorses);
            string DATE = nameof(Appointments.Date);
            string TIME_FROM = nameof(Appointments.TimeFrom);
            string TIME_TOWILL = nameof(Appointments.TimeTowill);
            string REMARKS = nameof(Appointments.Remarks);
            string EMPLOYEE_ID = nameof(Appointments.EmployeeId);
            string CUSTOMER_ID = nameof(Appointments.CustomerId);
            string COST = nameof(Appointments.Cost);
            string PAYMENT_METHOD_ID = nameof(Appointments.PaymentMethodId);
            string APPOINTMENT_STATUS_ID = nameof(Appointments.AppointmentStatusId);

            if(values.Contains(APPOINTMENTS_ID)) {
                model.AppointmentsId = Convert.ToInt32(values[APPOINTMENTS_ID]);
            }

            if(values.Contains(NUMBEROF_HORSES)) {
                model.NumberofHorses = Convert.ToInt32(values[NUMBEROF_HORSES]);
            }

            if(values.Contains(DATE)) {
                model.Date = Convert.ToDateTime(values[DATE]);
            }

            if(values.Contains(TIME_FROM)) {
                model.TimeFrom = Convert.ToDateTime(values[TIME_FROM]);
            }

            if(values.Contains(TIME_TOWILL)) {
                model.TimeTowill = Convert.ToDateTime(values[TIME_TOWILL]);
            }

            if(values.Contains(REMARKS)) {
                model.Remarks = Convert.ToString(values[REMARKS]);
            }

            if(values.Contains(EMPLOYEE_ID)) {
                model.EmployeeId = Convert.ToInt32(values[EMPLOYEE_ID]);
            }

            if(values.Contains(CUSTOMER_ID)) {
                model.CustomerId = Convert.ToInt32(values[CUSTOMER_ID]);
            }

            if(values.Contains(COST)) {
                model.Cost = Convert.ToDouble(values[COST], CultureInfo.InvariantCulture);
            }

            if(values.Contains(PAYMENT_METHOD_ID)) {
                model.PaymentMethodId = Convert.ToInt32(values[PAYMENT_METHOD_ID]);
            }

            if(values.Contains(APPOINTMENT_STATUS_ID)) {
                model.AppointmentStatusId = Convert.ToInt32(values[APPOINTMENT_STATUS_ID]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}