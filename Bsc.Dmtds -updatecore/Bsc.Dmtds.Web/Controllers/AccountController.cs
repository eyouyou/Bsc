using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Bsc.Dmtds.Dto;
using Bsc.Dmtds.Service;
using Bsc.Dmtds.Web.Areas.Account.Models;
using Bsc.Dmtds.Web.Filter;
using Bsc.Dmtds.Web.Models;


namespace Bsc.Dmtds.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            this._userService = userService;
        }

        //
        // GET: /Account/Login
//        [Auth]
//        [AllowAnonymous]
//        public ActionResult Login(string returnUrl)
//        {
//            ViewBag.ReturnUrl = returnUrl;
//            return View();
//        }
//
//        //
//        // POST: /Account/Login
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }
//
//            // 这不会计入到为执行帐户锁定而统计的登录失败次数中
//            // 若要在多次输入错误密码的情况下触发帐户锁定，请更改为 shouldLockout: true
//            var user = new UserDto { UserName=model.Email, Password=model.Password };
//            var result = await _signInService.PasswordSignIn(user, model.RememberMe, shouldLockout: false);
//            switch (result)
//            {
//                case SignInStatus.Success:
//                    return RedirectToLocal(returnUrl);
//                case SignInStatus.LockedOut:
//                    return View("Lockout");
//                case SignInStatus.RequiresVerification:
//                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
//                case SignInStatus.Failure:
//                default:
//                    ModelState.AddModelError("", "无效的登录尝试。");
//                    return View(model);
//            }
//        }
//        [AllowAnonymous]
//        public string Test()
//        {
//            try
//            {
//                if (_userService.Test())
//                    return "成功";
//                else return "失败";
//            }
//            catch (Exception ex)
//            {
//                return ex.ToString();
//            }
//        }
//        //
//        // GET: /Account/VerifyCode
//        [AllowAnonymous]
//        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
//        {
//            // 要求用户已通过使用用户名/密码或外部登录名登录
//            if (!await _signInService.HasBeenVerified())
//            {
//                return View("Error");
//            }
//            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
//        }
//
//        //
//        // POST: /Account/VerifyCode
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }
//
//            // 以下代码可以防范双重身份验证代码遭到暴力破解攻击。
//            // 如果用户输入错误代码的次数达到指定的次数，则会将
//            // 该用户帐户锁定指定的时间。
//            // 可以在 IdentityConfig 中配置帐户锁定设置
//            var result = await _signInService.TwoFactorSignIn(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
//            switch (result)
//            {
//                case SignInStatus.Success:
//                    return RedirectToLocal(model.ReturnUrl);
//                case SignInStatus.LockedOut:
//                    return View("Lockout");
//                case SignInStatus.Failure:
//                default:
//                    ModelState.AddModelError("", "代码无效。");
//                    return View(model);
//            }
//        }
//
//        //
//        // GET: /Account/Register
//        [AllowAnonymous]
//        public ActionResult Register()
//        {
//            return View();
//        }
//
//        //
//        // POST: /Account/Register
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Register(RegisterViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = new UserDto { UserName = model.Email, Email = model.Email };
//                var result = await _userService.Create(user,model.Password);
//                if (result.Succeeded)
//                {
//                    await _signInService.SignIn(user, isPersistent: false, rememberBrowser: false);
//                    
//                    // 有关如何启用帐户确认和密码重置的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=320771
//                    // 发送包含此链接的电子邮件
//                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
//                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
//                    // await UserManager.SendEmailAsync(user.Id, "确认你的帐户", "请通过单击 <a href=\"" + callbackUrl + "\">這裏</a>来确认你的帐户");
//
//                    return RedirectToAction("Index", "Home");
//                }
//                AddErrors(result);
//            }
//
//            // 如果我们进行到这一步时某个地方出错，则重新显示表单
//            return View(model);
//        }
//
//        //
//        // GET: /Account/ConfirmEmail
//        [AllowAnonymous]
//        public async Task<ActionResult> ConfirmEmail(string userId, string code)
//        {
//            if (userId == null || code == null)
//            {
//                return View("Error");
//            }
//            var result = await _userService.ConfirmEmail(userId, code);
//            return View(result.Succeeded ? "ConfirmEmail" : "Error");
//        }
//
//        //
//        // GET: /Account/ForgotPassword
//        [AllowAnonymous]
//        public ActionResult ForgotPassword()
//        {
//            return View();
//        }
//
//        //
//        // POST: /Account/ForgotPassword
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user =  _userService.FindByName(model.Email);
//                if (user == null || !(await _userService.IsEmailConfirmed(user.Id)))
//                {
//                    // 请不要显示该用户不存在或者未经确认
//                    return View("ForgotPasswordConfirmation");
//                }
//
//                // 有关如何启用帐户确认和密码重置的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=320771
//                // 发送包含此链接的电子邮件
//                 string code = await _userService.GeneratePasswordResetToken(user.Id);
//                 var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
//                 await _userService.SendEmail(user.Id, "重置密码", "请通过单击 <a href=\"" + callbackUrl + "\">此处</a>来重置你的密码");
//                 return RedirectToAction("ForgotPasswordConfirmation", "Account");
//            }
//
//            // 如果我们进行到这一步时某个地方出错，则重新显示表单
//            return View(model);
//        }
//
//        //
//        // GET: /Account/ForgotPasswordConfirmation
//        [AllowAnonymous]
//        public ActionResult ForgotPasswordConfirmation()
//        {
//            return View();
//        }
//
//        //
//        // GET: /Account/ResetPassword
//        [AllowAnonymous]
//        public ActionResult ResetPassword(string code)
//        {
//            return code == null ? View("Error") : View();
//        }
//
//        //
//        // POST: /Account/ResetPassword
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }
//            var user =  _userService.FindByName(model.Email);
//            if (user == null)
//            {
//                // 请不要显示该用户不存在
//                return RedirectToAction("ResetPasswordConfirmation", "Account");
//            }
//            var result = await _userService.ResetPassword(user.Id, model.Code, model.Password);
//            if (result.Succeeded)
//            {
//                return RedirectToAction("ResetPasswordConfirmation", "Account");
//            }
//            AddErrors(result);
//            return View();
//        }
//
//        //
//        // GET: /Account/ResetPasswordConfirmation
//        [AllowAnonymous]
//        public ActionResult ResetPasswordConfirmation()
//        {
//            return View();
//        }
//
//        //
//        // POST: /Account/ExternalLogin
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public ActionResult ExternalLogin(string provider, string returnUrl)
//        {
//            // 请求重定向到外部登录提供程序
//            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
//        }
//
//        //
//        // GET: /Account/SendCode
//        [AllowAnonymous]
//        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
//        {
//            var userId = await _signInService.GetVerifiedUserId();
//            if (userId == null)
//            {
//                return View("Error");
//            }
//            var userFactors = await _userService.GetValidTwoFactorProviders(userId);
//            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
//            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
//        }
//
//        //
//        // POST: /Account/SendCode
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> SendCode(SendCodeViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View();
//            }
//
//            // 生成令牌并发送该令牌
//            if (!await _signInService.SendTwoFactorCode(model.SelectedProvider))
//            {
//                return View("Error");
//            }
//            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
//        }
//
//        //
//        // GET: /Account/ExternalLoginCallback
//        [AllowAnonymous]
//        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
//        {
//            var loginInfo = await _authenticationManager.GetExternalLoginInfoAsync();
//            if (loginInfo == null)
//            {
//                return RedirectToAction("Login");
//            }
//
//            // 如果用户已具有登录名，则使用此外部登录提供程序将该用户登录
//            var result = await _signInService.ExternalSignIn(loginInfo, isPersistent: false);
//            switch (result)
//            {
//                case SignInStatus.Success:
//                    return RedirectToLocal(returnUrl);
//                case SignInStatus.LockedOut:
//                    return View("Lockout");
//                case SignInStatus.RequiresVerification:
//                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
//                case SignInStatus.Failure:
//                default:
//                    // 如果用户没有帐户，则提示该用户创建帐户
//                    ViewBag.ReturnUrl = returnUrl;
//                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
//                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
//            }
//        }
//
//        //
//        // POST: /Account/ExternalLoginConfirmation
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
//        {
//            if (User.Identity.IsAuthenticated)
//            {
//                return RedirectToAction("Index", "Manage");
//            }
//
//            if (ModelState.IsValid)
//            {
//                // 从外部登录提供程序获取有关用户的信息
//                var info = await _authenticationManager.GetExternalLoginInfoAsync();
//                if (info == null)
//                {
//                    return View("ExternalLoginFailure");
//                }
//                var user = new UserDto { UserName = model.Email, Email = model.Email };
//                var result = await _userService.Create(user);
//                if (result.Succeeded)
//                {
//                    result = await _userService.AddLogin(user.Id, info.Login);
//                    if (result.Succeeded)
//                    {
//                        await _signInService.SignIn(user, isPersistent: false, rememberBrowser: false);
//                        return RedirectToLocal(returnUrl);
//                    }
//                }
//                AddErrors(result);
//            }
//
//            ViewBag.ReturnUrl = returnUrl;
//            return View(model);
//        }
//
//        //
//        // POST: /Account/LogOff
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult LogOff()
//        {
//            _authenticationManager.SignOut();
//            return RedirectToAction("Index", "Home");
//        }
//
//        //
//        // GET: /Account/ExternalLoginFailure
//        [AllowAnonymous]
//        public ActionResult ExternalLoginFailure()
//        {
//            return View();
//        }
//
//        //protected override void Dispose(bool disposing)
//        //{
//        //    if (disposing)
//        //    {
//        //        if (_userManager != null)
//        //        {
//        //            _userManager.Dispose();
//        //            _userManager = null;
//        //        }
//
//        //        if (_signInManager != null)
//        //        {
//        //            _signInManager.Dispose();
//        //            _signInManager = null;
//        //        }
//        //    }
//
//        //    base.Dispose(disposing);
//        //}
//
//        #region 帮助程序
//        // 用于在添加外部登录名时提供 XSRF 保护
//        private const string XsrfKey = "XsrfId";
//
//
//
//        private void AddErrors(IdentityResult result)
//        {
//            foreach (var error in result.Errors)
//            {
//                ModelState.AddModelError("", error);
//            }
//        }
//
//        private ActionResult RedirectToLocal(string returnUrl)
//        {
//            if (Url.IsLocalUrl(returnUrl))
//            {
//                return Redirect(returnUrl);
//            }
//            return RedirectToAction("Index", "Home");
//        }
//
//        internal class ChallengeResult : HttpUnauthorizedResult
//        {
//            public ChallengeResult(string provider, string redirectUri)
//                : this(provider, redirectUri, null)
//            {
//            }
//
//            public ChallengeResult(string provider, string redirectUri, string userId)
//            {
//                LoginProvider = provider;
//                RedirectUri = redirectUri;
//                UserId = userId;
//            }
//
//            private string LoginProvider { get; set; }
//            private string RedirectUri { get; set; }
//            private string UserId { get; set; }
//
//            public override void ExecuteResult(ControllerContext context)
//            {
//                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
//                if (UserId != null)
//                {
//                    properties.Dictionary[XsrfKey] = UserId;
//                }
//                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
//            }
//        }
//        #endregion
    }
}