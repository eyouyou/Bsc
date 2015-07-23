using System.Threading.Tasks;
using System.Web.Mvc;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Dto;
using Bsc.Dmtds.Service;
using Bsc.Dmtds.Sites;


namespace Bsc.Dmtds.Web.Areas.Account.Controllers
{
    public class LogOnController : AreaControllerBase
    {
        // GET: Account/LogOn
        private readonly IUserService _userService;
        //[Ninject.Inject]
        //private IUserService _userService { get; set; }
        public LogOnController(IUserService userService)
        {
            this._userService = userService;
        }
  
        public ActionResult Index()
        {
            return View();
        }
//        [AllowAnonymous]
//        public ActionResult Register()
//        {
//            return View();
//        }
//        [HttpPost]
//        public ActionResult Register(RegisterModel model)
//        {
//            return View();
//        }
//        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }
//
//            // 这不会计入到为执行帐户锁定而统计的登录失败次数中
//            // 若要在多次输入错误密码的情况下触发帐户锁定，请更改为 shouldLockout: true
//            var user = new UserDto { UserName = model.Email, Password = model.Password };
//            var result = await _signInService.PasswordSignIn(user, model.RememberMe, shouldLockout: false);
//            switch (result)
//            {
//                case SignInStatus.Success:
//                    return Redirect(returnUrl);
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
    }
}