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
using Microsoft.AspNetCore.Localization;

namespace HorseService.Controllers
{
    [Route("api/[controller]/[action]")]
    public class HomeSlidersController : Controller
    {
        private HorseServiceContext _context;

        public HomeSlidersController(HorseServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var homesliders = _context.HomeSliders.Select(i => new {
                i.HomeSliderId,
                i.HomeSliderPic,
               
            });

            // If you work with a large amount of data, consider specifying the PaginateViaPrimaryKey and PrimaryKey properties.
            // In this case, keys and data are loaded in separate queries. This can make the SQL execution plan more efficient.
            // Refer to the topic https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "HomeSliderId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(homesliders, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new HomeSlider();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.HomeSliders.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.HomeSliderId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.HomeSliders.FirstOrDefaultAsync(item => item.HomeSliderId == key);
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
            var model = await _context.HomeSliders.FirstOrDefaultAsync(item => item.HomeSliderId == key);

            _context.HomeSliders.Remove(model);
            await _context.SaveChangesAsync();
        }

        private void PopulateModel(HomeSlider model, IDictionary values) {
            string HOME_SLIDER_ID = nameof(HomeSlider.HomeSliderId);
            string HOME_SLIDER_PIC = nameof(HomeSlider.HomeSliderPic);
          

            if(values.Contains(HOME_SLIDER_ID)) {
                model.HomeSliderId = Convert.ToInt32(values[HOME_SLIDER_ID]);
            }

            if(values.Contains(HOME_SLIDER_PIC)) {
                model.HomeSliderPic = Convert.ToString(values[HOME_SLIDER_PIC]);
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