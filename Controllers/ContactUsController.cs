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
    public class ContactUsController : Controller
    {
        private HorseServiceContext _context;

        public ContactUsController(HorseServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var contactus = _context.ContactUs.Select(i => new {
                i.ContactUsId,
                i.CompanyName,
                i.Tele,
                i.Fax,
                i.Mobile,
                i.Email,
                i.Address,
                i.Instgram,
                i.LinkedIn,
                i.Twitter,
                i.WhatsApp,
                i.Facebook
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ContactUsId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(contactus, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new ContactUs();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.ContactUs.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ContactUsId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.ContactUs.FirstOrDefaultAsync(item => item.ContactUsId == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.ContactUs.FirstOrDefaultAsync(item => item.ContactUsId == key);

            _context.ContactUs.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(ContactUs model, IDictionary values) {
            string CONTACT_US_ID = nameof(ContactUs.ContactUsId);
            string COMPANY_NAME = nameof(ContactUs.CompanyName);
            string TELE = nameof(ContactUs.Tele);
            string FAX = nameof(ContactUs.Fax);
            string MOBILE = nameof(ContactUs.Mobile);
            string EMAIL = nameof(ContactUs.Email);
            string ADDRESS = nameof(ContactUs.Address);
            string FACEBOOK = nameof(ContactUs.Facebook);
            string TWITTER = nameof(ContactUs.Twitter);
            string LINKDIN = nameof(ContactUs.LinkedIn);
            string WHATSAPP = nameof(ContactUs.WhatsApp);
            string INSTGRAM = nameof(ContactUs.Instgram);


            if (values.Contains(CONTACT_US_ID)) {
                model.ContactUsId = Convert.ToInt32(values[CONTACT_US_ID]);
            }

            if(values.Contains(COMPANY_NAME)) {
                model.CompanyName = Convert.ToString(values[COMPANY_NAME]);
            }
            if (values.Contains(FACEBOOK))
            {
                model.Facebook = Convert.ToString(values[FACEBOOK]);
            }
            if (values.Contains(TWITTER))
            {
                model.Twitter = Convert.ToString(values[TWITTER]);
            }
            if (values.Contains(LINKDIN))
            {
                model.LinkedIn = Convert.ToString(values[LINKDIN]);
            }
            if (values.Contains(WHATSAPP))
            {
                model.WhatsApp = Convert.ToString(values[WHATSAPP]);
            }
            if (values.Contains(INSTGRAM))
            {
                model.Instgram = Convert.ToString(values[INSTGRAM]);
            }

            if (values.Contains(TELE)) {
                model.Tele = Convert.ToString(values[TELE]);
            }

            if(values.Contains(FAX)) {
                model.Fax = Convert.ToString(values[FAX]);
            }

            if(values.Contains(MOBILE)) {
                model.Mobile = Convert.ToString(values[MOBILE]);
            }

            if(values.Contains(EMAIL)) {
                model.Email = Convert.ToString(values[EMAIL]);
            }

            if(values.Contains(ADDRESS)) {
                model.Address = Convert.ToString(values[ADDRESS]);
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