using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using HorseService.Data;
using HorseService.Entities;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using NToastNotify;

namespace HorseService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SettingController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        public SettingController(SignInManager<ApplicationUser> signInManager, IToastNotification toastNotification, ILogger<LogoutModel> logger, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _toastNotification = toastNotification;
        }
        [HttpGet]
        public IActionResult ChangeLanguage(string culture ,string url)
           
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
              CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
              new CookieOptions { Expires = DateTimeOffset.UtcNow.AddMonths(1) }
              );
            return Redirect("~" + url);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
           
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return Redirect("~/identity/account/login");
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] HorseService.Entities.ResetPasswordModel resetPasswordModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new { Message = "model not Valid" });
                if (resetPasswordModel.ConfirmPassword != resetPasswordModel.NewPassword)
                {
                    return Ok(new { Message = "Confirm Password and New Password not matched" });
                }
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userid);
                var Result = await _userManager.ChangePasswordAsync(user, resetPasswordModel.CurrentPassword, resetPasswordModel.NewPassword);
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
    }
}
