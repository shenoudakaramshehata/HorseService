using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using HorseService.Data;
using HorseService.Entities;
using HorseService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HorseService.Controllers
{
   
    [Route("api/[Controller]/[action]")]
    public class HorseServiceAPIsController : Controller
    {
        private readonly HorseServiceContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db ;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IEmailSender _emailSender;
        private  int count = 0;
        private  bool FromElse = false;
         
        List<AppResultVm> MoreAppo = new List<AppResultVm>();
        List<AppResultVm> MoreAppoNotStatic = new List<AppResultVm>();

        public HorseServiceAPIsController(HorseServiceContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            ApplicationDbContext db, RoleManager<IdentityRole> roleManager, IWebHostEnvironment hostEnvironment, IEmailSender emailSender)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;
            _roleManager = roleManager;
            _hostEnvironment = hostEnvironment;
            _emailSender = emailSender;

        }



        [HttpGet]
        public async Task<IActionResult> getEmployees()
        {
            try
            {
                var EmployeesList = await _context.Employees.Where(e => e.IsActive == true).ToListAsync();
                return Ok(new { EmployeesList });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Something went Error" });
            }
        }
        [HttpGet]
        public async Task<IActionResult> getEmployeebyId(int employeeId)
        {
            try
            {
                var employee = await _context.Employees.Where(i => i.EmployeeId == employeeId && i.IsActive == true).FirstOrDefaultAsync();
                return Ok(new { employee });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Something went Error" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> getEmployeeAppoitmnents(int employeeId)
        {
            try
            {
                var appoitments = await _context.Appointments.Where(i => i.EmployeeId == employeeId).ToListAsync();
                return Ok(new { appoitments });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Something went Error" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> getCustomerAppoitmnents(int customerId)
        {
            try
            {
                var appoitments = await _context.Appointments.Where(i => i.CustomerId == customerId).ToListAsync();
                return Ok(new { appoitments });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Something went Error" });
            }
        }
        [HttpGet]
        public async Task<IActionResult> getEmployeeVideos(int employeeId)
        {
            try
            {
                var video = await _context.Videos.Where(i => i.EmployeeId == employeeId).ToListAsync();
                return Ok(new { video });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Something went Error" });
            }
        }
        [HttpGet]
        public async Task<IActionResult> PaymentMethod()
        {
            try
            {
                var payment = await _context.PaymentMethods.ToListAsync();
                return Ok(new { payment });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Something went Error" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ContactInformation()
        {
            try
            {
                var contactInformation = await _context.ContactUs.ToListAsync();
                return Ok(new { contactInformation });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Something went Error" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomer(int customerId)
        {
            try
            {
                var customer = _context.Customers.FirstOrDefault(e => e.CustomerId == customerId);
                return Ok(new { customer });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Something went Error" });
            }

        }
        [HttpPost]
        public async Task<IActionResult> ContactForm(ContactForm contactForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Model State is not valid" });
            }
            try
            {
                _context.ContactForms.Add(contactForm);
                _context.SaveChanges();
                return Ok(new { Message = "Thank You for your Message!" });

            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Something went Error" });
            }

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomerAccount(int customerId)
        {
            var user = _db.Users.Where(e => e.EntityId == customerId).FirstOrDefault();
            try
            {
                if (user == null)
                {
                    return Ok(new { Status=false,Message = "Customer Not Found" });
                }
               
                    _db.Users.Remove(user);
                    _db.SaveChanges();
            }
            catch(Exception e)
            {
                return Ok(new { Status = false, Message =e.Message });

            }
            return Ok(new { Status = true, Message = "Customer Account Deleted Successfully" });
        }
        //[HttpGet]
        //public IActionResult GetAppointmentDate(int employeeId,int NumberofHorses, int customerId)
        //{

        //    try
        //    {

        //        DateTime timeFrom = new DateTime();
        //        DateTime timetoWill = new DateTime();
        //        DateTime date = new DateTime();
        //        Appointments appointment = new Appointments();
        //        var employee = _context.Employees.Find(employeeId);
        //        var offdays = _context.OffDays.Where(e => e.EmployeeId == employeeId).OrderByDescending(e => e.OffDaysId).FirstOrDefault();
        //        var lastAppointment = _context.Appointments.Where(e => e.EmployeeId == employeeId).OrderByDescending(e => e.AppointmentsId).FirstOrDefault();
        //        if (!employee.IsActive)
        //        {
        //            return Ok(new { Message="Employee not Active!"});
        //        }
        //        if (lastAppointment != null)
        //        {

        //            if (lastAppointment.Date != DateTime.Now)
        //            {


        //                date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 0, 0, 0);
        //                if (lastAppointment.Date != date)
        //                {

        //                    timeFrom = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0);
        //                    timetoWill = new DateTime(date.Year, date.Month, date.Day, 9 + NumberofHorses, 0, 0);
        //                }
        //                else
        //                {
        //                    var newTime = lastAppointment.TimeTowill.AddMinutes(35);
        //                    DateTime DayEnd = new DateTime(lastAppointment.Date.Year, lastAppointment.Date.Month, lastAppointment.Date.Day, 22, 0, 0);

        //                    if (newTime.Hour < DayEnd.Hour && newTime.Hour + NumberofHorses <= DayEnd.Hour)
        //                    {
        //                        timeFrom = new DateTime(lastAppointment.Date.Year, lastAppointment.Date.Month, lastAppointment.Date.Day, newTime.Hour, newTime.Minute, newTime.Second);
        //                        timetoWill = new DateTime(lastAppointment.Date.Year, lastAppointment.Date.Month, lastAppointment.Date.Day, timeFrom.Hour + NumberofHorses, newTime.Minute, newTime.Second);
        //                        appointment.Date = lastAppointment.Date;
        //                        appointment.TimeFrom = timeFrom;
        //                        appointment.TimeTowill = timetoWill;
        //                        appointment.EmployeeId = employeeId;
        //                        appointment.NumberofHorses = NumberofHorses;
        //                        appointment.Cost = NumberofHorses * 100;
        //                        appointment.CustomerId = customerId;
        //                        appointment.PaymentMethodId = 1;
        //                        appointment.AppointmentStatusId = 1;
        //                        _context.Appointments.Add(appointment);
        //                        _context.SaveChanges();
        //                    }
        //                    else
        //                    {
        //                        var newDay = lastAppointment.Date.AddDays(1);
        //                        timeFrom = new DateTime(newDay.Year, newDay.Month, newDay.Day, 9, 0, 0);
        //                        timetoWill = new DateTime(newDay.Year, newDay.Month, newDay.Day, 9 + NumberofHorses, 0, 0);
        //                        appointment.Date = newDay;
        //                        appointment.TimeFrom = timeFrom;
        //                        appointment.TimeTowill = timetoWill;
        //                        appointment.EmployeeId = employeeId;
        //                        appointment.NumberofHorses = NumberofHorses;
        //                        appointment.Cost = NumberofHorses * 100;
        //                        appointment.CustomerId = customerId;
        //                        appointment.PaymentMethodId = 1;
        //                        appointment.AppointmentStatusId = 1;
        //                        _context.Appointments.Add(appointment);
        //                        _context.SaveChanges();
        //                    }
        //                }

        //            }
        //        }

        //        if (offdays != null && offdays.To < DateTime.Now)
        //        {

        //            if (lastAppointment != null)
        //            {
        //                date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 0, 0, 0);
        //                if (lastAppointment.Date != date)
        //                {

        //                    timeFrom = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0);
        //                    timetoWill = new DateTime(date.Year, date.Month, date.Day, 9 + NumberofHorses, 0, 0);
        //                }
        //                else
        //                {
        //                    var newTime = lastAppointment.TimeTowill.AddMinutes(35);
        //                    DateTime DayEnd = new DateTime(lastAppointment.Date.Year, lastAppointment.Date.Month, lastAppointment.Date.Day, 22, 0, 0);

        //                    if (newTime.Hour < DayEnd.Hour && newTime.Hour + NumberofHorses <= DayEnd.Hour)
        //                    {
        //                        timeFrom = new DateTime(lastAppointment.Date.Year, lastAppointment.Date.Month, lastAppointment.Date.Day, newTime.Hour, newTime.Minute, newTime.Second);
        //                        timetoWill = new DateTime(lastAppointment.Date.Year, lastAppointment.Date.Month, lastAppointment.Date.Day, timeFrom.Hour + NumberofHorses, newTime.Minute, newTime.Second);
        //                        appointment.Date = lastAppointment.Date;
        //                        appointment.TimeFrom = timeFrom;
        //                        appointment.TimeTowill = timetoWill;
        //                        appointment.EmployeeId = employeeId;
        //                        appointment.NumberofHorses = NumberofHorses;
        //                        appointment.Cost = NumberofHorses * 100;
        //                        appointment.CustomerId = customerId;
        //                        appointment.PaymentMethodId = 1;
        //                        appointment.AppointmentStatusId = 1;
        //                        _context.Appointments.Add(appointment);
        //                        _context.SaveChanges();
        //                    }
        //                    else
        //                    {
        //                        var newDay = lastAppointment.Date.AddDays(1);
        //                        timeFrom = new DateTime(newDay.Year, newDay.Month, newDay.Day, 9, 0, 0);
        //                        timetoWill = new DateTime(newDay.Year, newDay.Month, newDay.Day, 9 + NumberofHorses, 0, 0);
        //                        appointment.Date = newDay;
        //                        appointment.TimeFrom = timeFrom;
        //                        appointment.TimeTowill = timetoWill;
        //                        appointment.EmployeeId = employeeId;
        //                        appointment.NumberofHorses = NumberofHorses;
        //                        appointment.Cost = NumberofHorses * 100;
        //                        appointment.CustomerId = customerId;
        //                        appointment.PaymentMethodId = 1;
        //                        appointment.AppointmentStatusId = 1;
        //                        _context.Appointments.Add(appointment);
        //                        _context.SaveChanges();
        //                    }
        //                }

        //            }
        //            else
        //            {

        //                var newDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 0, 0, 0);
        //                timeFrom = new DateTime(newDay.Year, newDay.Month, newDay.Day, 9, 0, 0);
        //                timetoWill = new DateTime(newDay.Year, newDay.Month, newDay.Day, 9 + NumberofHorses, 0, 0);
        //                appointment.Date = newDay;
        //                appointment.TimeFrom = timeFrom;
        //                appointment.TimeTowill = timetoWill;
        //                appointment.EmployeeId = employeeId;
        //                appointment.NumberofHorses = NumberofHorses;
        //                appointment.Cost = NumberofHorses * 100;
        //                appointment.CustomerId = customerId;
        //                appointment.PaymentMethodId = 1;
        //                appointment.AppointmentStatusId = 1;
        //                _context.Appointments.Add(appointment);
        //                _context.SaveChanges();

        //            }

        //        }
        //        else
        //        {
        //            if (lastAppointment != null)
        //            {
        //                var newTime = lastAppointment.TimeTowill.AddMinutes(35);
        //                DateTime DayEnd = new DateTime(lastAppointment.Date.Year, lastAppointment.Date.Month, lastAppointment.Date.Day, 22, 0, 0);

        //                if (newTime.Hour < DayEnd.Hour && newTime.Hour + NumberofHorses <= DayEnd.Hour)
        //                {
        //                    timeFrom = new DateTime(lastAppointment.Date.Year, lastAppointment.Date.Month, lastAppointment.Date.Day, newTime.Hour, newTime.Minute, newTime.Second);
        //                    timetoWill = new DateTime(lastAppointment.Date.Year, lastAppointment.Date.Month, lastAppointment.Date.Day, timeFrom.Hour + NumberofHorses, newTime.Minute, newTime.Second);
        //                    appointment.Date = lastAppointment.Date;
        //                    appointment.TimeFrom = timeFrom;
        //                    appointment.TimeTowill = timetoWill;
        //                    appointment.EmployeeId = employeeId;
        //                    appointment.NumberofHorses = NumberofHorses;
        //                    appointment.Cost = NumberofHorses * 100;
        //                    appointment.CustomerId = customerId;
        //                    appointment.PaymentMethodId = 1;
        //                    appointment.AppointmentStatusId = 1;
        //                    _context.Appointments.Add(appointment);
        //                    _context.SaveChanges();
        //                }
        //                else
        //                {
        //                    var newDay = lastAppointment.Date.AddDays(1);
        //                    timeFrom = new DateTime(newDay.Year, newDay.Month, newDay.Day, 9, 0, 0);
        //                    timetoWill = new DateTime(newDay.Year, newDay.Month, newDay.Day, 9 + NumberofHorses, 0, 0);
        //                    appointment.Date = newDay;
        //                    appointment.TimeFrom = timeFrom;
        //                    appointment.TimeTowill = timetoWill;
        //                    appointment.EmployeeId = employeeId;
        //                    appointment.NumberofHorses = NumberofHorses;
        //                    appointment.Cost = NumberofHorses * 100;
        //                    appointment.CustomerId = customerId;
        //                    appointment.PaymentMethodId = 1;
        //                    appointment.AppointmentStatusId = 1;
        //                    _context.Appointments.Add(appointment);
        //                    _context.SaveChanges();
        //                }
        //            }
        //            else
        //            {

        //                var newDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 0, 0, 0);
        //                timeFrom = new DateTime(newDay.Year, newDay.Month, newDay.Day, 9, 0, 0);
        //                timetoWill = new DateTime(newDay.Year, newDay.Month, newDay.Day, 9 + NumberofHorses, 0, 0);
        //                appointment.Date = newDay;
        //                appointment.TimeFrom = timeFrom;
        //                appointment.TimeTowill = timetoWill;
        //                appointment.EmployeeId = employeeId;
        //                appointment.NumberofHorses = NumberofHorses;
        //                appointment.Cost = NumberofHorses * 100;
        //                appointment.CustomerId = customerId;
        //                appointment.PaymentMethodId = 1;
        //                appointment.AppointmentStatusId = 1;
        //                _context.Appointments.Add(appointment);
        //                _context.SaveChanges();

        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest(new { Message = "Something went Error" });
        //    }
        //    return Ok(new { Message = "Employee not Active!" });

        //}




        [ApiExplorerSettings(IgnoreApi = true)]
        public List<AppResultVm> GetAppointment(int NumberofHorser, int employeeId, int customerId)
        {
            
            bool IsReturn = true;
            var day = DateTime.Now;
            

            do
            {
                var result = GetFreeTime(day.AddDays(count), employeeId);
                var reservationDate = day.AddDays(count);
                if (result != null && result.Count != 0)
                {
                    
                    
                    foreach (var item in result)
                    {
                        var timeDifference = item.To - item.From;
                        if (timeDifference.Hours >= NumberofHorser)
                        {
                            //if (DateTime.Now < item.From)
                            //{
                                #region OldLogic
                                //Appointments appointments = new Appointments()
                                //{
                                //    NumberofHorses = NumberofHorser,
                                //    Date = new DateTime(reservationDate.Year, reservationDate.Month, reservationDate.Day, 0, 0, 0),
                                //    EmployeeId = employeeId,
                                //    TimeFrom = item.From,
                                //    TimeTowill = item.From.AddHours(NumberofHorser),
                                //    CustomerId = customerId
                                //};
                                //_context.Appointments.Add(appointments);
                                //_context.SaveChanges();
                                #endregion
                                var appResultVm = new AppResultVm()
                                {
                                    From = item.From,
                                    To = item.From.AddHours(NumberofHorser),
                                    Date = reservationDate
                                };
                                //List<AppResultVm> MoreAppo = new List<AppResultVm>();
                                var ObjResult=new AppResultVm() { From = item.From, To = item.From.AddHours(NumberofHorser), Date = reservationDate,NumberOfHourses= NumberofHorser };
                                MoreAppo.Add(ObjResult);
                                FromElse = true;
                                break;
                            //}
                        }
                        else
                        {
                            var ObjResult = new AppResultVm() { From = item.From, To = item.From.AddHours(timeDifference.Hours), Date = reservationDate,NumberOfHourses= timeDifference.Hours };
                            MoreAppo.Add(ObjResult);
                            var remainingHourses = NumberofHorser - timeDifference.Hours;
                            if (remainingHourses <= 0)
                            {
                                break;
                            }
                            count++;
                            GetAppointment(remainingHourses, employeeId,customerId);
                            #region TestNewLogic
                            // DateTime dayStart = new DateTime(day.Year, day.Month, day.AddDays(count + 1).Day, 6, 30, 0);
                            //List<AppResultVm> MoreAppo = new List<AppResultVm>()
                            //{
                            //    new AppResultVm() { From = item.From, To = item.From.AddHours(timeDifference.Hours), Date = reservationDate },
                            //    new AppResultVm() { From = dayStart, To = dayStart.AddHours(remainingHourses), Date = day.AddDays(count+1) }

                            //};

                            //return Ok(new { Status = true, AppList = MoreAppo });

                            #endregion
                        }
                    }
                }
                
                IsReturn = false;
                count++;
                if (FromElse == true)
                {
                    break;
                }
                

            }
            while (IsReturn == false);

            return MoreAppo;
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public List<TimeSlots> GetFreeTime(DateTime day, int employeeId)
        {
           
            var DateName = day.DayOfWeek.ToString();
            var daybreaks = _context.OffDays.Where(e => e.EmployeeId == employeeId && e.From.Value.Date <= day.Date && e.To.Value.Date >= day.Date).ToList();
            //var daybreaks = _context.OffDays.Where(e => e.EmployeeId == employeeId && e.From.Value.Date == day.Date && e.To.Value.Date == day.Date).ToList();
            DateTime dayStart=new DateTime(day.Year, day.Month, day.Day, 6, 30, 0);
            DateTime dayEnd = new DateTime(day.Year, day.Month, day.Day, 17, 30, 0); 
            if (DateName== "Saturday")
            {
                dayStart = new DateTime(day.Year, day.Month, day.Day, 6, 30, 0);
                dayEnd = new DateTime(day.Year, day.Month, day.Day, 13, 30, 0);
            }
            //int DateResult = DateTime.Compare(day, dayEnd);
            List<TimeSlots> timeSlots = new List<TimeSlots>();
            List<TimeSlots> Gaps = new List<TimeSlots>();
            var Lastoffdays = _context.OffDays.Where(e => e.EmployeeId == employeeId).OrderByDescending(e => e.OffDaysId).FirstOrDefault();
           // var dayAppointmentsNew = _context.Appointments.Where(e => e.EmployeeId == employeeId).OrderByDescending(e => e.AppointmentsId).FirstOrDefault();
            List<AppoimentsDate> dayAppointments = new List<AppoimentsDate>();
            //if (dayAppointmentsNew != null)
            //{
                dayAppointments = _context.AppoimentsDates.Where(e => e.Date.Date == day.Date).ToList();

           // }
            List<OffDays> offDays = new List<OffDays>();

            if (daybreaks != null && daybreaks.Count != 0)
            {
                foreach (var item in daybreaks)
                {
                    if (item.breaktypesId == 1)
                    {
                        timeSlots.Add(new TimeSlots() { From = item.From.Value, To = item.To.Value, TimeSlotType = "Break" });
                    }
                    else if (item.breaktypesId == 2)
                    {
                        return Gaps;
                    }
                    else if (item.breaktypesId == 3)
                    {
                        return Gaps;

                    }
                }
            }
            if (dayAppointments != null && dayAppointments.Count != 0)
            {
                foreach (var item in dayAppointments)
                {
                    timeSlots.Add(new TimeSlots() { From = item.TimeFrom, To = item.TimeTowill, TimeSlotType = "Appointment" });
                }
            }
            if (timeSlots != null && timeSlots.Count != 0)
            {
                var list = timeSlots.OrderBy(x => x.From).ToList();

                if (list[0].From > dayStart)
                {
                    var difference = list[0].From.Subtract(day);
                    if (difference.Hours >= 1)
                        if (day <= dayEnd)
                            Gaps.Add(new TimeSlots() { From = day, To = list[0].From });
                }

                for (int i = 0; i < list.Count; i++)
                {

                    if (list[i].TimeSlotType == "Appointment")
                    {

                        if (list[i].To == dayEnd.Date)
                        {
                            break;
                        }
                        if (i == list.Count - 1)
                        {
                            var difference = dayEnd.Subtract(list[i].To.AddMinutes(35));
                            if (difference.Hours >= 1)
                                if (list[i].To.AddMinutes(35) <=dayEnd)
                                    Gaps.Add(new TimeSlots() { From = list[i].To.AddMinutes(35), To = dayEnd });
                        }
                        else
                        {
                            var difference = list[i + 1].From.Subtract(list[i].To.AddMinutes(35));
                            if (difference.Hours >= 1)
                            {
                                if (list[i].To.AddMinutes(35) <= dayEnd)
                                    Gaps.Add(new TimeSlots() { From = list[i].To.AddMinutes(35), To = list[i + 1].From });
                            }

                        }
                    }
                    else if (list[i].TimeSlotType == "Break")
                    {
                        if (list[i].To == dayEnd.Date)
                        {
                            break;
                        }
                        if (i == list.Count - 1)
                        {
                            var difference = dayEnd.Subtract(list[i].To);
                            if (difference.Hours >= 1)
                                
                                    Gaps.Add(new TimeSlots() { From = list[i].To, To = dayEnd });
                        }
                        else
                        {
                            var difference = list[i + 1].From.Subtract(list[i].To);
                            if (difference.Hours >= 1)
                                
                                    Gaps.Add(new TimeSlots() { From = list[i].To, To = list[i + 1].From });

                        }
                    }
                }
            }
            else
            {
                //int DateResultCheck = DateTime.Compare(day, DateTime.Now);
                if (day>DateTime.Now)
                    Gaps.Add(new TimeSlots() { From = dayStart, To = dayEnd });
                else if (day == DateTime.Now)
                {
                    Gaps.Add(new TimeSlots() { From = DateTime.Now, To = dayEnd });

                }
                else 
                {
                   
                }
            }

            //     if (Lastoffdays!=null && Lastoffdays.breaktypesId == 2 && Lastoffdays.Onday.Value.Date == day.Date)
            //     {
            //     GetFreeTime(new DateTime(day.Year,day.Month,day.Day+1,9,0,0), employeeId);
            //     }

            //     else if(Lastoffdays != null && Lastoffdays.breaktypesId == 3 && Lastoffdays.From.Value.Date >= day.Date && Lastoffdays.To.Value.Date <= day.Date)
            //     {
            //     GetFreeTime(new DateTime(Lastoffdays.To.Value.Year, Lastoffdays.To.Value.Month, Lastoffdays.To.Value.Day + 1, 9, 0, 0), employeeId);
            //     }
            //  else if (Lastoffdays != null && Lastoffdays.breaktypesId == 1 && Lastoffdays.From.Value.Date >= day.Date && Lastoffdays.To.Value.Date == day.Date)
            //  {

            //      if (daybreaks != null && daybreaks.Count != 0)
            //      {

            //          foreach (var item in daybreaks)
            //          {
            //              timeSlots.Add(new TimeSlots() { From = item.From.Value, To = item.To.Value,TimeSlotType= "Break" });
            //          }
            //      }
            //      if (dayAppointments != null && dayAppointments.Count != 0)
            //      {
            //          foreach (var item in dayAppointments)
            //          {
            //              timeSlots.Add(new TimeSlots() { From = item.TimeFrom, To = item.TimeTowill,TimeSlotType= "Appointment" });
            //          }
            //      }
            //      if(timeSlots!=null && timeSlots.Count != 0)
            //      {
            //          var list = timeSlots.OrderBy(x => x.From).ToList();

            //          if (list[0].From > dayStart)
            //          {
            //              var difference = list[0].From.Subtract(DateTime.Now);
            //              if (difference.Hours >= 1)
            //                  Gaps.Add(new TimeSlots() { From = DateTime.Now, To = list[0].From });
            //          }

            //          for (int i = 0; i < list.Count; i++)
            //          {

            //              if (list[i].TimeSlotType == "Appointment")
            //              {

            //                  if (list[i].To == dayEnd)
            //                  {
            //                      break;
            //                  }
            //                  if (i == list.Count - 1)
            //                  {
            //                      var difference = dayEnd.Subtract(list[i].To.AddMinutes(35)) ;
            //                      if (difference.Hours >= 1)
            //                          Gaps.Add(new TimeSlots() { From = list[i].To.AddMinutes(35), To = dayEnd });
            //                  }
            //                  else
            //                  { var difference = list[i + 1].From.Subtract(list[i].To.AddMinutes(35));
            //                      if (difference.Hours >= 1)
            //                      {
            //                          Gaps.Add(new TimeSlots() { From = list[i].To.AddMinutes(35), To = list[i + 1].From });
            //                      }

            //                  }
            //              }
            //              else if (list[i].TimeSlotType == "Break")
            //              {
            //                  if (list[i].To == dayEnd)
            //                  {
            //                      break;
            //                  }
            //                  if (i == list.Count - 1)
            //                  {
            //                      var difference = dayEnd.Subtract(list[i].To) ;
            //                      if (difference.Hours >= 1)
            //                          Gaps.Add(new TimeSlots() { From = list[i].To, To = dayEnd });
            //                  }
            //                  else
            //                  {
            //                      var difference = list[i + 1].From.Subtract(list[i].To);
            //                      if (difference.Hours >= 1)
            //                          Gaps.Add(new TimeSlots() { From = list[i].To, To = list[i + 1].From });

            //                  }
            //              }
            //          }
            //      }
            //  }
            //  else if (dayAppointments != null && dayAppointments.Count!=0 && (daybreaks==null || daybreaks.Count==0))
            //  {
            //      foreach (var item in dayAppointments)
            //      {
            //          timeSlots.Add(new TimeSlots() { From = item.TimeFrom, To = item.TimeTowill, TimeSlotType = "Appointment"});
            //      }
            //      if (timeSlots != null && timeSlots.Count != 0)
            //      {
            //          var list = timeSlots.OrderBy(x => x.From).ToList();

            //          if (list[0].From > dayStart)
            //          {
            //              var difference = list[0].From.Subtract(DateTime.Now);
            //              if (difference.Hours >= 1)
            //                  Gaps.Add(new TimeSlots() { From = DateTime.Now, To = list[0].From });
            //          }
            //          for (int i = 0; i < list.Count; i++)
            //          {

            //              if (list[i].TimeSlotType == "Appointment")
            //              {

            //                  if (list[i].To == dayEnd)
            //                  {
            //                      break;
            //                  }
            //                  if (i == list.Count - 1)
            //                  {
            //                      var difference = dayEnd.Subtract(list[i].To.AddMinutes(35));
            //                      if (difference.Hours >= 1)
            //                          Gaps.Add(new TimeSlots() { From = list[i].To.AddMinutes(35), To = dayEnd });
            //                  }
            //                  else
            //                  {
            //                      var difference = list[i + 1].From.Subtract(list[i].To.AddMinutes(35));
            //                      if (difference.Hours >= 1)
            //                      {
            //                          Gaps.Add(new TimeSlots() { From = list[i].To.AddMinutes(35), To = list[i + 1].From });
            //                      }
            //                  }
            //                  }

            //          }
            //      }
            //  }
            //else  if ((dayAppointments == null || dayAppointments.Count == 0)&&(daybreaks==null || daybreaks.Count==0))
            //  {
            //      Gaps.Add(new TimeSlots() { From = dayStart, To = dayEnd });
            //  }
            return Gaps;
        }


        
        [HttpPost]
        public IActionResult MakeAppointment([FromBody]AppointmentVM appointment)
        {
            Random r = new Random();

            string RandomEmail = r.Next().ToString();
            if (appointment == null)
            {
                return Ok(new { Status = false, Message = "Object Equal Null,Please Send Objct Again.." });
            }
             if (appointment.EmployeeId == 0||appointment.EmployeeId ==null)
            {
                return Ok(new { Status = false, Message = "Employee Id Not Valid" });
            }
              if (appointment.CustomerId == 0||appointment.CustomerId ==null)
            {
                return Ok(new { Status = false, Message = "Customer Id Not Valid" });
            }
             if (appointment.Cost == 0)
            {
                return Ok(new { Status = false, Message = "Total Cost Must Be Large Than 0" });
            }
             if (appointment.NumberofHorses == 0)
            {
                return Ok(new { Status = false, Message = "Number of Hourses Must Be Large Than 0" });
            }
               if (appointment.AppointmentStatusId == 0)
            {
                return Ok(new { Status = false, Message = "Appointment Status  Id Not Valid" });
            }
            var ListOfDates = GetAppointment(appointment.NumberofHorses, appointment.EmployeeId.Value, appointment.CustomerId);
            var appointmentObj = new Appointments()
            {
                PaymentMethodId = 1,
                Cost = appointment.Cost,
                EmployeeId = appointment.EmployeeId,
                CustomerId = appointment.CustomerId,
                //Date = appointment.Date,
                ispaid = false,
                Lat = appointment.Lat,
                Lng = appointment.Lng,
                NumberofHorses = appointment.NumberofHorses,
                OrderSerialNumber = RandomEmail,
                //TimeTowill = appointment.TimeTowill,
                //TimeFrom = appointment.TimeFrom,
                Remarks = appointment.Remarks,
                CustomerAddress = appointment.CustomerAddress,
                AppointmentStatusId = appointment.AppointmentStatusId,
                AppointmentDetails= appointment.AppointmentDetails
            };
            try
            {
                _context.Appointments.Add(appointmentObj);
                _context.SaveChanges();
                List<AppoimentsDate> appoimentsDatesList = new List<AppoimentsDate>();
                foreach (var item in ListOfDates)
                {
                    AppoimentsDate AppimentDate = new AppoimentsDate()
                    { 
                        Date=item.Date,
                        TimeFrom=item.From,
                        TimeTowill=item.To,
                        AppointmentsId= appointmentObj.AppointmentsId

                    };
                    appoimentsDatesList.Add(AppimentDate);
                }
                _context.AppoimentsDates.AddRange(appoimentsDatesList);
                _context.SaveChanges();
                Appointments UpdateAppoiments = UpdateAppo(ListOfDates, appointmentObj.AppointmentsId);
                return Ok(new { Status = true, Message = "Appointment Added Successfully", Appoiment = UpdateAppoiments, ListOfDates=ListOfDates });
            }
            catch (Exception ex)
            {
               return Ok(new { Status = false, Message = ex.Message });

            }

            //var GuidRandm = 
            // Random r = new Random();

            //string RandomSerialNumber = Guid.NewGuid().ToString();
            //int count = 0;
            //try
            //{
            //    List<Appointments> appointmentsList = new List<Appointments>();
                //foreach (AppointmentVM appointment in ListModel)
                //{
                //    if (appointment.CustomerId == 0)
                //{
                //    return Ok(new { Statut=false,Message = $"There is no Customer for item{count}" });

                //}
                //if (appointment.NumberofHorses == 0)
                //{
                //    return Ok(new { Statut = false, Message = $"Please Enter No. of Horses for item{count}" });

                //}
                //if(appointment.EmployeeId == 0)
                //{
                //    return Ok(new { Statut = false, Message = $"There is no Employee for item{count}" });
                //}
                //if (appointment.Cost == 0)
                //{
                //    return Ok(new { Statut = false, Message = $"Cost Mst be more than 0 for item{count}" });
                //}
                //if (appointment.ServiceId == 0)
                //{
                //    return Ok(new { Statut = false, Message = $"There is no Service for item{count}" });
                //}
                //if (appointment.AppointmentStatusId == 0)
                //{
                //    return Ok(new { Statut = false,Message = $"There is no AppointmentStatus for item{count}" });
                //}
                //var servicecost = _context.Services.Find(appointment.ServiceId).Cost;
                //if (servicecost==0)
                //{
                //    return Ok(new { Statut = false,Message = $"There is no service cost for item{count}" });

                //}

               
                //    var appointmentObj = new Appointments()
                //    {
                //        PaymentMethodId = 1,
                //        Cost = appointment.Cost,
                //        EmployeeId = appointment.EmployeeId,
                //        CustomerId = appointment.CustomerId,
                //        Date = appointment.Date,
                //        ispaid = false,
                //        Lat = appointment.Lat,
                //        Lng = appointment.Lng,
                //        NumberofHorses = appointment.NumberofHorses,
                //        TimeTowill = appointment.TimeTowill,
                //        TimeFrom = appointment.TimeFrom,
                //        Remarks = appointment.Remarks,
                //        CustomerAddress = appointment.CustomerAddress,
                //        AppointmentStatusId = appointment.AppointmentStatusId,
                //       // ServiceId = appointment.ServiceId,
                //        OrderSerialNumber= RandomSerialNumber


                //    };
                //   // appointmentObj.AppointmentAdditionalTypes = appointment.AppointmentAdditionalTypes;
                //    appointmentsList.Add(appointmentObj);
                //    count++;
                //}
            //    _context.Appointments.AddRange(appointmentsList);
            //    _context.SaveChanges();
            //    return Ok(new { status=true, AppointmentList = appointmentsList });
            //}
            //catch (Exception ex)
            //{
            //    return Ok(new { status = false,Message = ex.Message });
            //}
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public Appointments UpdateAppo(List<AppResultVm> appResultVms,int AppoimentId)
        {
            var Appoiment = _context.Appointments.Where(e => e.AppointmentsId == AppoimentId).FirstOrDefault();
            Appoiment.TimeFrom = appResultVms[0].From;
            Appoiment.TimeTowill = appResultVms[appResultVms.Count - 1].To;
             _context.Entry(Appoiment).State = EntityState.Modified;
            _context.SaveChanges();
            return Appoiment;
        }
        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            try
            {
            var services = await _context.Services.ToListAsync();
            return Ok(new {status=true, services= services });
            }
            catch (Exception)
            {
                return Ok(new { status = false, Message = "Something went Error.." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAdditionalTypes()
        {
           
            
            try
            {
                var AdditionalTypesList = await _context.AdditionalTypes.ToListAsync();
                return Ok(new { status = true, AdditionalTypesList = AdditionalTypesList });
            }
            catch (Exception e)
            {
                return Ok(new { status = false, Message = e.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationModel Model)
        {
            try
            {
                var userExists= _db.Users.Where(e => e.PhoneNumber == Model.CustomerPhone).FirstOrDefault();
                if (userExists != null)
                {
                    return Ok( new { Status = false, Message = "User already exists!,Please Enter Another Phone" });
                }
                var customer = new Customer();
                //if (Model.CustomerImage != string.Empty)
                //{
                //    var bytes = Convert.FromBase64String(Model.CustomerImage);
                //    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images/Customer");
                //    string uniqePictureName = Guid.NewGuid().ToString("N") + ".jpeg";
                //    string uploadedImagePath = Path.Combine(uploadFolder, uniqePictureName);
                //    using (var imageFile = new FileStream(uploadedImagePath, FileMode.Create))
                //    {
                //        imageFile.Write(bytes, 0, bytes.Length);
                //        imageFile.Flush();
                //    }
                //    customer.CustomerImage = uniqePictureName;
                //}
                
                customer.CustomerNameEn = Model.CustomerNameEn;
                customer.CustomerPhone = Model.CustomerPhone;
                //customer.CustomerRemarks = Model.CustomerRemarks;
                //customer.CustomerEmail = RandomEmail;
                _context.Customers.Add(customer);
                _context.SaveChanges();
                Random r = new Random();

                string RandomEmail = "Midan" + r.Next() + "@gmail.com";
                var user = new ApplicationUser
                {
                    UserName = RandomEmail,
                    Email = RandomEmail,
                    PhoneNumber = Model.CustomerPhone,
                    EntityId = customer.CustomerId,
                    EntityName = "Customer"
                };

                var result = await _userManager.CreateAsync(user, Model.Password);

                if (!result.Succeeded)
                {
                    _context.Customers.Remove(customer);
                    _context.SaveChanges();
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });

                }
                await _userManager.AddToRoleAsync(user, "Customer");
                return Ok(new { Status = "Success", Message = "User created successfully!", user, customer });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Something went Error" });
            }

        }

        [HttpGet]
        public async Task<ActionResult<ApplicationUser>> Login([FromQuery] string Phone, [FromQuery] string Password)
        {
            try
            {
                var user = _db.Users.Where(e => e.PhoneNumber== Phone).FirstOrDefault();

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, Password, true);
                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles != null && roles.FirstOrDefault() == "Customer")
                        {
                            var customer = await _context.Customers.FindAsync(user.EntityId);

                            return Ok(new { Status = "Success", Message = "User Login successfully!", user, customer });
                        }
                    }
                }
                var invalidResponse = new { status = false };
                return Ok(invalidResponse);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Something went Error" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new { Message = "model not Valid" });
                if (resetPasswordModel.ConfirmPassword != resetPasswordModel.NewPassword)
                {
                    return Ok(new { Message = "Confirm Password and New Password not matched" });
                }
                var curUser = _userManager.Users.Where(c => c.EntityId == resetPasswordModel.CustomerId).FirstOrDefault();
                if (curUser == null)
                {
                    return StatusCode(409, "Customer not found");
                }
                var Result = await _userManager.ChangePasswordAsync(curUser, resetPasswordModel.CurrentPassword, resetPasswordModel.NewPassword);
                if (!Result.Succeeded)
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.TryAddModelError(error.Code, error.Description);
                    }
                    return Ok(new { Message = ModelState });

                }

                return Ok(new { Message = "Password Changed" });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Something went Error" });
            }
        }
        [HttpPut]
        public async Task<IActionResult> EditCustomer([FromBody] RegistrationModel Model)
        {
            try
            {
                Customer customer = _context.Customers.FirstOrDefault(e => e.CustomerId == Model.CustomerId);
                if (customer == null)
                {
                    return Ok(new { Message = "Customer Not Found" });
                }

                if (Model.CustomerImage != string.Empty)
                {
                    var wwwroot = _hostEnvironment.WebRootPath;
                    var ImagePath = Path.Combine(wwwroot, "Images/Customer/" + customer.CustomerImage);
                    if (System.IO.File.Exists(ImagePath))
                    {
                        System.IO.File.Delete(ImagePath);
                    }
                    var bytes = Convert.FromBase64String(Model.CustomerImage);
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images/Customer");
                    string uniqePictureName = Guid.NewGuid().ToString("N") + ".jpeg";
                    string uploadedImagePath = Path.Combine(uploadFolder, uniqePictureName);
                    using (var imageFile = new FileStream(uploadedImagePath, FileMode.Create))
                    {
                        imageFile.Write(bytes, 0, bytes.Length);
                        imageFile.Flush();
                    }
                    customer.CustomerImage = uniqePictureName;
                }

                if (Model.CustomerNameEn != string.Empty)
                {
                    customer.CustomerNameEn = Model.CustomerNameEn;
                }
                if (Model.CustomerPhone != string.Empty)
                {
                    customer.CustomerPhone = Model.CustomerPhone;
                }
                 
                _context.Attach(customer).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                
                var user = _db.Users.Where(e => e.EntityId == customer.CustomerId).FirstOrDefault();
                user.PhoneNumber = customer.CustomerPhone;
                _db.Attach(user).State = EntityState.Modified;
                 _db.SaveChanges();
                return Ok(new { Status = true, customer= customer });

            }
            catch (Exception ex)
            {

                return Ok(new {Status=false, Message = ex.Message });
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetHomeSlider()
        {

            //var sliderList = await _context.HomeSliders.ToListAsync();
            var sliderList = await _context.HomeSliders.Select(h => new { h.HomeSliderPic }).ToListAsync();

            return Ok(new { sliderList });
        }

        [HttpGet]
        public async Task<IActionResult> GetContactUs()
        {


            var Contactus = await _context.ContactUs.ToListAsync();

            return Ok(new { Contactus });
        }

        [HttpGet]
        public async Task<IActionResult> getUserById(int id)
        {
            var user = await _context.Customers.FindAsync(id);


            return Ok(new { user });
        }
        [HttpGet]
        public async Task<IActionResult> GetPageContent()
        {
            var SystemConfiguration = await _context.PageContents.ToListAsync();
            return Ok(new { SystemConfiguration });
        }

        [HttpGet]
        public async Task<IActionResult> GetCost()
        {
            try
            {
                var cost = await _context.Configurations.Select(i => i.Cost).FirstOrDefaultAsync();
                return Ok(cost);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

        }

        //[HttpPost]
        //public async Task<ActionResult<ApplicationUser>> forgetPassword([FromQuery] string UserName)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByNameAsync(UserName);
        //        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        //        {
        //            // Don't reveal that the user does not exist or is not confirmed
        //            return RedirectToPage("./ForgotPasswordConfirmation");
        //        }

        //        // For more information on how to enable account confirmation and password reset please 
        //        // visit https://go.microsoft.com/fwlink/?LinkID=532713
        //        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        //        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        //        var callbackUrl = Url.Page(
        //            "/Account/ResetPassword",
        //            pageHandler: null,
        //            values: new { area = "Identity", code },
        //            protocol: Request.Scheme);

        //        await _emailSender.SendEmailAsync(
        //            user.Email,
        //            "Reset Password",
        //            $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        //        return RedirectToPage("./ForgotPasswordConfirmation");
        //    }

        //    return View();
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetCategoriesList()
        //{
        //    var categoryList = await _context.Categories.Where(e=>e.IsActive==true).ToListAsync();
        //    return Ok(new { categoryList });
        //}
        //[HttpGet]
        //public async Task<IActionResult> getItemsImageList()
        //{
        //    var itemsImageList = await _context.ItemImages.Include(e=>e.Item).Where(e=>e.Item.IsActive==true).ToListAsync();
        //    return Ok(new { itemsImageList });
        //}
        //[HttpGet]
        //public async Task<IActionResult> getItemsList()
        //{
        //    var ItemsList = await _context.Items.Where(e=>e.IsActive==true&&e.Category.IsActive==true).ToListAsync();
        //    return Ok(new { ItemsList });
        //}
        //[HttpGet]
        //public async Task<IActionResult> getItembyId(int id)
        //{
        //    var Item = await _context.Items.Where(i=>i.ItemId==id).FirstOrDefaultAsync();
        //    return Ok(new { Item });
        //}

        //[HttpGet]
        //public IActionResult getitemsbycategoryId(int? CategoryId)
        //{
        //    if (CategoryId != null)
        //    {
        //        var items=  _context.Items.Where(i => i.CategoryId == CategoryId && i.IsActive==true&&i.Category.IsActive==true);
        //        return Ok(new { items });
        //    }
        //    else
        //    {
        //        return Ok(new { Message="Category Id can not be null" });
        //    }
        //}
        ////[HttpGet]
        ////public async Task<IActionResult> categorybysectionid(int id)
        ////{
        ////    var categoryList = await _context.Categories.Where(x => x.SectionId == id).ToListAsync();
        ////    return Ok(new { categoryList });
        ////}

        //[HttpGet]
        //public async Task<IActionResult> Search([FromQuery] string SearchText, [FromQuery] int CustomerId = 0)

        //{
        //    if (CustomerId == 0)
        //    {

        //        var items = await _context.Items.Where(c =>c.IsActive==true&&c.Category.IsActive==true &&(c.ItemTlAr.Contains(SearchText) || c.ItemTlEn.Contains(SearchText) || c.Category.CategoryTlAr.Contains(SearchText) || c.Category.CategoryTlEn.Contains(SearchText) || c.Description.Contains(SearchText) || c.Brand.BrandTlAr.Contains(SearchText) || c.Brand.BrandTlEn.Contains(SearchText))).Select(i => new
        //        {
        //            i.ItemId,
        //            i.ItemTlAr,
        //            i.ItemTlEn,
        //            i.ItemPic,
        //            i.Price,
        //            i.BrandId,
        //            i.Brand.BrandTlAr,
        //            i.Brand.BrandTlEn,
        //            i.CategoryId,
        //            i.Description,
        //            i.Remarks,
        //            i.IsActive,
        //            IsFav = false
        //        }).ToListAsync();
        //        return Ok(new { items });
        //    }
        //    else
        //    {
        //        var items = await _context.Items.Where(c =>c.IsActive==true&&c.Category.IsActive==true && (c.ItemTlAr.Contains(SearchText) || c.ItemTlEn.Contains(SearchText) || c.Category.CategoryTlAr.Contains(SearchText) || c.Category.CategoryTlEn.Contains(SearchText) || c.Description.Contains(SearchText) || c.Brand.BrandTlAr.Contains(SearchText) || c.Brand.BrandTlEn.Contains(SearchText))).Select(i => new
        //        {
        //            i.ItemId,
        //            i.ItemTlAr,
        //            i.ItemTlEn,
        //            i.ItemPic,
        //            i.Price,
        //            i.BrandId,
        //            i.Brand.BrandTlAr,
        //            i.Brand.BrandTlEn,
        //            i.CategoryId,
        //            i.Description,
        //            i.Remarks,
        //            i.IsActive,
        //            IsFav = _context.CustomerFav.Any(c => c.CustomerId == CustomerId && c.ItemId == i.ItemId)
        //        }).ToListAsync();
        //        return Ok(new { items });
        //    }
        //}



        //[HttpGet]
        //public async Task<IActionResult> GetBrands()
        //{
        //    var BrandList = await _context.Brands.ToListAsync();
        //    return Ok(new { BrandList });
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetCustomerInfo([FromQuery] int CustomerId)
        //{
        //    var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == CustomerId);
        //    if (customer == null)
        //    {
        //        return Ok(new { Message = "customer Not Found" });
        //    }
        //    return Ok(new { customer });
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddToCart([FromQuery] int CustomerId, int ItemId,int QTY)
        //{
        //    Item item = _context.Items.Find(ItemId);
        //        Cart model = new Cart();
        //        model.CartDate = DateTime.Now;
        //        model.CustomerId = CustomerId;
        //        model.ItemId = ItemId;
        //        model.UnitPrice = item.Price;
        //        model.QTY = QTY;
        //        model.Total = item.Price * QTY;
        //        _context.Carts.Add(model);
        //        await _context.SaveChangesAsync();

        //    return Ok(new { Message = " Item Added To Cart" });
        //}

        //[HttpPost]
        //public async Task<IActionResult> deleteCartItem(int id)
        //{
        //    Cart cart = await _context.Carts.FindAsync(id);
        //    _context.Carts.Remove(cart);
        //    await _context.SaveChangesAsync();
        //    return Ok(new { Message = " Item Deleted" });
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetPaymentList()
        //{
        //    List<PaymentMethod> payments = await _context.PaymentMethods.ToListAsync();

        //    return Ok(new { payments });
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetOrderList(int userId)
        //{

        //    var user = await _context.Customers.FindAsync(userId);
        //    if(user !=null)
        //    {
        //        var OrerLst = _context.Orders.Where(c => c.CustomerId == userId).Select(i => new
        //        {
        //            i.Addrerss,
        //            i.Closed,
        //            i.CustomerId,
        //            i.OrderDate,
        //            i.OrderId,
        //            i.OrderSerial,
        //            i.PaymentMethodId,
        //            i.Remarks,
        //            i.Total,
        //            OrderItem = i.OrderItem,
        //            ItemDetails = i.OrderItem.Select(j => new
        //            {
        //                j.Item.ItemPic,
        //                j.Item.ItemTlAr,
        //                j.Item.ItemTlEn,
        //                j.Item.Description
        //            }
        //              )

        //        });

        //        return Ok(new { OrerLst });
        //    }
        //    else
        //        return Ok(new { Message = " user Not found" });
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetNotificationslist(int userId)
        //{
        //    List<Notifications> lstNotifications = await _context.Notifications.Where(n => n.CustomerId == userId).ToListAsync();

        //    return Ok(new { lstNotifications });
        //}

        //[HttpGet]
        //public async Task<IActionResult> allOrderList()
        //{
        //    List<Order> lstOrders = new List<Order>();
        //    lstOrders = await _context.Orders.ToListAsync();
        //    return Ok(new { lstOrders });
        //}
        //[HttpGet]
        //public IActionResult Top5OrderedItems()
        //{
        //    var topItemsIds = _context.OrderItems.Include(e=>e.Item).Where(i=>i.Item.IsActive==true && i.Item.Category.IsActive==true).GroupBy(x => x.ItemId).OrderByDescending(g => g.Count()).Take(5).Select(x => x.Key).ToList();
        //    var topItems = _context.Items.Where(x => topItemsIds.Contains(x.ItemId));
        //    return Ok(new {topItems});
        //}


        //[HttpPost]
        //public async Task<IActionResult> UpdateCart([FromQuery] int cartId,int customerId,int qty)
        //{
        //    var cartModel = await _context.Carts.Where(c => c.CartId == cartId & c.CustomerId== customerId).FirstOrDefaultAsync();
        //    if (cartModel == null)
        //    {
        //        return Ok(new { Message = "  Item Not Found" });
        //    }
        //    cartModel.QTY = qty;
        //    await _context.SaveChangesAsync();
        //    return Ok(new { Message = "Item Updated" });

        //}

        //[HttpPost]
        //public async Task<IActionResult> ReadNotification([FromQuery] int NotificationId, [FromQuery] int CustomerId)
        //{

        //    Notifications NotificationsModel = await _context.Notifications.FirstOrDefaultAsync(c => c.Id == NotificationId & c.CustomerId == CustomerId);

        //    if (NotificationsModel == null)
        //    {
        //        return Ok(new { Message = "  Item Not Found" });
        //    }

        //    //set new values

        //    NotificationsModel.IsReaded = true;


        //    _context.Entry(NotificationsModel).State = EntityState.Modified;

        //    await _context.SaveChangesAsync();

        //    return Ok(new { Message = "Item Updated" });


        //}

        //[HttpGet]
        //public async Task<IActionResult> FaqsList()
        //{
        //    List<FAQ> lstFaqs = new List<FAQ>();
        //    lstFaqs = await _context.FAQs.ToListAsync();
        //    return Ok(new { lstFaqs });
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetUserPaymentMethod([FromQuery] int userId)
        //{
        //    Order order = await _context.Orders.Where(o => o.CustomerId == userId).FirstOrDefaultAsync();
        //    if(order == null)
        //    {
        //        return Ok(new { Message = "Order Not Found" });
        //    }
        //    PaymentMethod paymentMethod = await _context.PaymentMethods.FindAsync(order.PaymentMethodId);
        //    if(paymentMethod == null)
        //    {
        //        return Ok(new { Message = "Payment Method Not Found" });
        //    }
        //    return Ok(new { paymentMethod });
        //}

        ///* [HttpDelete]
        // public async Task ClearUserNotification(int key)
        // {
        //     var model = await _context.PageContent.FirstOrDefaultAsync(item => item.PageContentId == key);

        //     _context.PageContent.Remove(model);
        //     await _context.SaveChangesAsync();
        // }*/

        //[HttpGet]
        //public async Task<IActionResult> userCart(int id)
        //{

        //    //var sliderList = await _context.HomeSliders.ToListAsync();
        //    var userCart = await _context.Carts.Where(c=>c.CustomerId==id).ToListAsync();
        //    return Ok(new { userCart });
        //}
        ////[HttpGet]
        ////public async Task<IActionResult> ChangePassword(string password)
        ////{


        ////    //var sliderList = await _context.HomeSliders.ToListAsync();
        ////    var userCart = await _context.Carts.Where(c => c.CustomerId == id).ToListAsync();

        ////    return Ok(new { userCart });
        ////}





    }


}