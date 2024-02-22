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
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]/[action]")]
    public class AdditionalTypesController : Controller
    {
        private HorseServiceContext _context;

        public AdditionalTypesController(HorseServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var additionaltypes = _context.AdditionalTypes.Select(i => new {
                i.AdditionalTypeId,
                i.Title,
                i.Cost
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "AdditionalTypeId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(additionaltypes, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new AdditionalType();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.AdditionalTypes.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.AdditionalTypeId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.AdditionalTypes.FirstOrDefaultAsync(item => item.AdditionalTypeId == key);
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
            var model = await _context.AdditionalTypes.FirstOrDefaultAsync(item => item.AdditionalTypeId == key);

            _context.AdditionalTypes.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(AdditionalType model, IDictionary values) {
            string ADDITIONAL_TYPE_ID = nameof(AdditionalType.AdditionalTypeId);
            string TITLE = nameof(AdditionalType.Title);
            string COST = nameof(AdditionalType.Cost);

            if(values.Contains(ADDITIONAL_TYPE_ID)) {
                model.AdditionalTypeId = Convert.ToInt32(values[ADDITIONAL_TYPE_ID]);
            }

            if(values.Contains(TITLE)) {
                model.Title = Convert.ToString(values[TITLE]);
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