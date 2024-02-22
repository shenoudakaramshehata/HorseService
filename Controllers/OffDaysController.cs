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
    public class OffDaysController : Controller
    {
        private HorseServiceContext _context;

        public OffDaysController(HorseServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var offdays = _context.OffDays.Select(i => new {
                i.OffDaysId,
                i.From,
                i.To,
                i.Onday,
                i.breaktypesId,
                i.EmployeeId
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "OffDaysId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(offdays, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new OffDays();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.OffDays.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.OffDaysId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.OffDays.FirstOrDefaultAsync(item => item.OffDaysId == key);
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
            var model = await _context.OffDays.FirstOrDefaultAsync(item => item.OffDaysId == key);

            _context.OffDays.Remove(model);
            await _context.SaveChangesAsync();
        }

        private void PopulateModel(OffDays model, IDictionary values) {
            string OFF_DAYS_ID = nameof(OffDays.OffDaysId);
            string FROM = nameof(OffDays.From);
            string TO = nameof(OffDays.To);
            string ONDAY = nameof(OffDays.Onday);
            string BREAKTYPES_ID = nameof(OffDays.breaktypesId);
            string EMPLOYEE_ID = nameof(OffDays.EmployeeId);

            if(values.Contains(OFF_DAYS_ID)) {
                model.OffDaysId = Convert.ToInt32(values[OFF_DAYS_ID]);
            }

            if(values.Contains(FROM)) {
                model.From = values[FROM] != null ? Convert.ToDateTime(values[FROM]) : (DateTime?)null;
            }

            if(values.Contains(TO)) {
                model.To = values[TO] != null ? Convert.ToDateTime(values[TO]) : (DateTime?)null;
            }

            if(values.Contains(ONDAY)) {
                model.Onday = values[ONDAY] != null ? Convert.ToDateTime(values[ONDAY]) : (DateTime?)null;
            }

            if(values.Contains(BREAKTYPES_ID)) {
                model.breaktypesId = Convert.ToInt32(values[BREAKTYPES_ID]);
            }

            if(values.Contains(EMPLOYEE_ID)) {
                model.EmployeeId = Convert.ToInt32(values[EMPLOYEE_ID]);
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