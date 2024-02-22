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

    public class VideosController : Controller
    {
        private HorseServiceContext _context;

        public VideosController(HorseServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var videos = _context.Videos.Select(i => new {
                i.VideoId,
                i.VideoUrl,
                i.EmployeeId
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "VideoId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(videos, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Video();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Videos.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.VideoId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Videos.FirstOrDefaultAsync(item => item.VideoId == key);
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
            var model = await _context.Videos.FirstOrDefaultAsync(item => item.VideoId == key);

            _context.Videos.Remove(model);
            await _context.SaveChangesAsync();
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

        private void PopulateModel(Video model, IDictionary values) {
            string VIDEO_ID = nameof(Video.VideoId);
            string VIDEO_URL = nameof(Video.VideoUrl);
            string EMPLOYEE_ID = nameof(Video.EmployeeId);

            if(values.Contains(VIDEO_ID)) {
                model.VideoId = Convert.ToInt32(values[VIDEO_ID]);
            }

            if(values.Contains(VIDEO_URL)) {
                model.VideoUrl = Convert.ToString(values[VIDEO_URL]);
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