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
    public class EmployeesController : Controller
    {
        private HorseServiceContext _context;

        public EmployeesController(HorseServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var employees = _context.Employees.Select(i => new {
                i.EmployeeId,
                i.FullName,
                i.Image,
                i.Description,
                i.IsActive
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "EmployeeId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(employees, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Employee();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Employees.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.EmployeeId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Employees.FirstOrDefaultAsync(item => item.EmployeeId == key);
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
            var model = await _context.Employees.FirstOrDefaultAsync(item => item.EmployeeId == key);

            _context.Employees.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Employee model, IDictionary values) {
            string EMPLOYEE_ID = nameof(Employee.EmployeeId);
            string FULL_NAME = nameof(Employee.FullName);
            string IMAGE = nameof(Employee.Image);
            string DESCRIPTION = nameof(Employee.Description);
            string IS_ACTIVE = nameof(Employee.IsActive);

            if(values.Contains(EMPLOYEE_ID)) {
                model.EmployeeId = Convert.ToInt32(values[EMPLOYEE_ID]);
            }

            if(values.Contains(FULL_NAME)) {
                model.FullName = Convert.ToString(values[FULL_NAME]);
            }

            if(values.Contains(IMAGE)) {
                model.Image = Convert.ToString(values[IMAGE]);
            }

            if(values.Contains(DESCRIPTION)) {
                model.Description = Convert.ToString(values[DESCRIPTION]);
            }

            if(values.Contains(IS_ACTIVE)) {
                model.IsActive = Convert.ToBoolean(values[IS_ACTIVE]);
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