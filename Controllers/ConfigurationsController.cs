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
    public class ConfigurationsController : Controller
    {
        private HorseServiceContext _context;

        public ConfigurationsController(HorseServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var configurations = _context.Configurations.Select(i => new {
                i.ConfigurationId,
                i.Cost
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ConfigurationId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(configurations, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Configuration();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Configurations.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ConfigurationId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Configurations.FirstOrDefaultAsync(item => item.ConfigurationId == key);
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
            var model = await _context.Configurations.FirstOrDefaultAsync(item => item.ConfigurationId == key);

            _context.Configurations.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Configuration model, IDictionary values) {
            string CONFIGURATION_ID = nameof(Configuration.ConfigurationId);
            string COST = nameof(Configuration.Cost);

            if(values.Contains(CONFIGURATION_ID)) {
                model.ConfigurationId = Convert.ToInt32(values[CONFIGURATION_ID]);
            }

            if(values.Contains(COST)) {
                model.Cost = Convert.ToDouble(values[COST], CultureInfo.InvariantCulture);
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