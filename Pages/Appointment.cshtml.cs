using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using HorseService.Data;
using HorseService.Models;

namespace HorseService.Pages
{
    public class AppointmentModel : PageModel
    {
        private HorseServiceContext _context;
        public Appointments appointments { get; set; }

        public PaymentMethod paymentMethod { get; set; }
        public HttpClient httpClient { get; set; }

        public AppointmentModel(HorseServiceContext context)
        {
            _context = context;
            httpClient = new HttpClient();
        }
        public async Task<IActionResult> OnGetAsync(string Values)
        {
            if (Values != null)
            {
                var model = JsonConvert.DeserializeObject<Appointments>(Values);

                if (model.CustomerId == 0)
                {
                    return RedirectToPage("SomethingwentError");

                }
                if (model.NumberofHorses == 0)
                {
                    return RedirectToPage("SomethingwentError"); 

                }
                if (model.EmployeeId == 0)
                {
                    return RedirectToPage("SomethingwentError");
                }
                if (model.PaymentMethodId == 0)
                {
                    return RedirectToPage("SomethingwentError"); 
                }
                if (model.AppointmentStatusId == 0)
                {
                    return RedirectToPage("SomethingwentError");
                }

                model.ispaid = false;
                try
                {
                    _context.Appointments.Add(model);
                    _context.SaveChanges();

                }
                catch (Exception)
                {
                    return RedirectToPage("SomethingwentError");
                }
                var Customer = _context.Customers.Find(model.CustomerId);

                //if (model.PaymentMethodId == 2)
                //{
                //    var requesturl = "https://api.upayments.com/test-payment";
                //    var fields = new
                //    {
                //        merchant_id = "1201",
                //        username = "test",
                //        password = "test",
                //        order_id = model.AppointmentsId,
                //        total_price = model.Cost,
                //        test_mode = 0,
                //        CstFName = Customer.CustomerNameEn,
                //        CstEmail = Customer.CustomerEmail,
                //        CstMobile = Customer.CustomerPhone,
                //        api_key = "jtest123",
                //        //success_url = "https://localhost:44354/success",
                //        success_url = "http://codewarenet-001-site15.dtempurl.com/success",
                //        //error_url = "https://localhost:44354/failed"
                //        error_url = "http://codewarenet-001-site15.dtempurl.com/failed"

                //    };
                //    var content = new StringContent(JsonConvert.SerializeObject(fields), Encoding.UTF8, "application/json");
                //    var task = httpClient.PostAsync(requesturl, content);
                //    var result = await task.Result.Content.ReadAsStringAsync();
                //    var paymenturl = JsonConvert.DeserializeObject<paymenturl>(result);
                //    if (paymenturl.status == "success")

                //    {
                //        return Redirect(paymenturl.paymentURL);
                //    }
                //    else
                //    {
                        
                //        return RedirectToPage("SomethingwentError", new { Message = paymenturl.error_msg });
                //    }
                //}
                if (model.PaymentMethodId == 1)
                {
                    return RedirectToPage("Thankyou");
                }
            }

            return RedirectToPage("SomethingwentError");
        }
    }
}



