using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers {
    public class HomeController : Controller {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public HomeController() {
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager) {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager {
            get {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        // GET: /Home
        public ActionResult Index(string returnUrl) {

            if(User.Identity.IsAuthenticated) {
                return RedirectToAction("RedirectUser", "Home");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Home/Index
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginViewModel model, string returnUrl) {
            if(!ModelState.IsValid) {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch(result) {
                case SignInStatus.Success:
                    //return RedirectToLocal(returnUrl);
                    return RedirectToAction("RedirectUser", "Home");

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        
        // Redirect Users based on roles
        [Route("Home/RedirectUser/{returnUrl}")]
        public ActionResult RedirectUser(string returnUrl) {

            if(Url.IsLocalUrl(returnUrl)) {
                return Redirect(returnUrl);
            }

            if(User.IsInRole("Admin")) {

                return RedirectToAction("Admin", "Dash");
            } else if(User.IsInRole("Student")) {

                return RedirectToAction("Student", "Dash");
            } else {

                return RedirectToAction("Index", "Home");
            }
        }

        protected override void Dispose(bool disposing) {
            if(disposing) {
                if(_userManager != null) {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if(_signInManager != null) {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }

    [Authorize]
    public class DashController : Controller {
        // GET: Dash/Admin
        public ActionResult Admin() {
            return View();
        }

        // GET: Dash/Student
        public ActionResult Student() {
            return View();
        }
    }
}
