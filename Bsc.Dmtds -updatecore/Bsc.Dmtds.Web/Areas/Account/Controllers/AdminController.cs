using System.Web.Mvc;
using Bsc.Dmtds.Service;

namespace Bsc.Dmtds.Web.Areas.Account.Controllers
{
//    [Authorize]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {

            _userService = userService;
        }

        // GET: Admin/AManage
//        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCourse()
        {
            return View();
        }
//        public ActionResult Info()
//        {
//            var user = _userService.FindById(User.Identity.GetUserId());
//            //BasicInfo info=new BasicInfo{CurIp = user.CurIp, ImgUrl = user.ImgUrl, LastIp = user.LastIp};
//            //AdminModel admin = new AdminModel {Name = user.UserName, ImgUrl = user.ImgUrl};
//            return View();
//        }
//        
//        public ActionResult UList()
//        {
//            return View();
//        }
    }
}