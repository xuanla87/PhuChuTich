using ClassLibrary.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using www.Models;

namespace www.Areas.Admin.Controllers
{
    //[Authorize]
    public class ThanhVienController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IContentServices _servicesContent;
        public ThanhVienController(IContentServices servicesContent)
        {
            _servicesContent = servicesContent;
        }
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var users = db.Users.ToList();
            return View(users);
        }

        public ActionResult ThemMoi()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ThemMoi(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, FullName = model.FullName, BirthDay = DateTime.Now };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.RoleName))
                    {
                        var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                        var idResult = um.AddToRole(user.Id, model.RoleName);
                    }
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var _role = rm.Roles;
            ViewBag.RoleName = _role.Select(x => new SelectListItem { Text = x.Name, Value = x.Name });
            return View(model);
        }
        [HttpGet]
        public ActionResult ChangePass(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ApplicationDbContext db = new ApplicationDbContext();
                var user = db.Users.Find(id);
                ChangePasswordViewModel model = new ChangePasswordViewModel { UserId = id };
                return View(model);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePass(ChangePasswordViewModel model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (ModelState.IsValid)
            {
                var result = await UserManager.RemovePasswordAsync(model.UserId);
                if (result.Succeeded)
                {
                    var _out = await UserManager.AddPasswordAsync(model.UserId, model.ConfirmPassword);
                    if (_out.Succeeded)
                        return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public string getRoleByUserId(string Id)
        {
            var model = UserManager.GetRoles(Id).FirstOrDefault();
            if (model != null)
                return model;
            else
                return null;
        }

        public ActionResult DeleteUser(string Id)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var model = UserManager.FindById(Id);
            if (model != null)
            {
                //Xoa role
                var currentRoles = new List<IdentityUserRole>();
                currentRoles.AddRange(model.Roles);
                foreach (var role in currentRoles)
                {
                    um.RemoveFromRole(Id, role.RoleId);
                }
                UserManager.Delete(model);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Redirect("/Admin");
        }
    }
}