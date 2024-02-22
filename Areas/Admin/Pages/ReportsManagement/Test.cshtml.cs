using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorseService.Data;
using HorseService.Models;
using HorseService.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HorseService.Areas.Admin.Pages.ReportsManagement
{
    public class TestModel : PageModel
    {
        public HorseServiceContext _context { get; }
        public TestModel(HorseServiceContext context)
        {
            _context = context;
        }
        [BindProperty]
        public int AssetId { get; set; }
        [BindProperty]
        public Test Report { get; set; }

        public async Task<IActionResult> OnGet()
        {
            List<Employee> ds = _context.Employees.ToList();

            Report = new Test();
            Report.DataSource = ds;
            //Report.Parameters[0].Value = AssetId;
            //Report.RequestParameters = false;
            return Page();
        }
        //public async Task<IActionResult> OnPost()
        //{
        //    var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var user = await UserManger.FindByIdAsync(userid);
        //    tenant = _context.Tenants.Find(user.TenantId);
        //    List<AssetReportsModel> ds = _context.Assets.Where(e => e.TenantId == tenant.TenantId && e.AssetStatusId == 2).Include(a => a.AssetMovementDetails).ThenInclude(a => a.AssetMovement).ThenInclude(a => a.Department).Select(i => new AssetReportsModel
        //    {
        //        AssetCost = i.AssetCost,
        //        AssetSerialNo = i.AssetSerialNo,
        //        AssetTagId = i.AssetTagId,
        //        ItemTL = i.Item.ItemTitle,
        //        Photo = i.Photo,
        //        TransactionDate = _context.AssetMovementDetails.Where(a => a.AssetId == i.AssetId).OrderByDescending(a => a.AssetMovementDetailsId).FirstOrDefault() == null ? null : _context.AssetMovementDetails.Where(a => a.AssetId == i.AssetId).OrderByDescending(a => a.AssetMovementDetailsId).FirstOrDefault().AssetMovement.TransactionDate,
        //        DepartmentId = _context.AssetMovementDetails.Where(a => a.AssetId == i.AssetId).OrderByDescending(a => a.AssetMovementDetailsId).FirstOrDefault() == null ? 0 : _context.AssetMovementDetails.Where(a => a.AssetId == i.AssetId).OrderByDescending(a => a.AssetMovementDetailsId).FirstOrDefault().AssetMovement.Department.DepartmentId,
        //        DepartmentTL = _context.AssetMovementDetails.Where(a => a.AssetId == i.AssetId).OrderByDescending(a => a.AssetMovementDetailsId).FirstOrDefault() == null ? null : _context.AssetMovementDetails.Where(a => a.AssetId == i.AssetId).OrderByDescending(a => a.AssetMovementDetailsId).FirstOrDefault().AssetMovement.Department.DepartmentTitle

        //    }).ToList();

        //    if (filterModel.DepartmentId != null)
        //    {
        //        ds = ds.Where(i => i.DepartmentId == filterModel.DepartmentId).ToList();
        //    }
        //    if (filterModel.DepartmentId == null)
        //    {
        //        ds = new List<AssetReportsModel>();
        //    }

        //    Report = new rptDepartmentAssets(tenant);
        //    Report.DataSource = ds;
        //    return Page();

        //}
    }
}
