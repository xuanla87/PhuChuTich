using ClassLibrary.Models;
using ClassLibrary.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace www.Areas.Admin.Controllers
{
    [Authorize]
    public class LienKetWebController : Controller
    {
        ILienKetWebServices _services;
        www.Models.ModelPCT _db;
        public LienKetWebController(ILienKetWebServices services)
        {
            this._services = services;
            _db = new www.Models.ModelPCT();
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public ActionResult Index()
        {
            var _user = _db.UserLogins.Find(User.Identity.Name);
            if (_user != null && _user.sessionId != HttpContext.Session.SessionID)
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            var result = _services.All();
            return View(result);
        }
        [HttpGet]
        public ActionResult Detail(int? Id)
        {
            var _user = _db.UserLogins.Find(User.Identity.Name);
            if (_user != null && _user.sessionId != HttpContext.Session.SessionID)
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            LienKetWeb model = null;
            if (Id.HasValue && Id > 0)
            {
                model = _services.GetById(Id.Value);
                ViewBag.Title = "Cập nhật liên kết";
            }
            else
            {
                model = new LienKetWeb
                {
                    isSort = 0,
                    lienKetWebId = 0
                };
                ViewBag.Title = "Thêm mới liên kết";
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Detail(LienKetWeb entity)
        {
            if (ModelState.IsValid)
            {
                if (entity.lienKetWebId > 0)
                {
                    LienKetWeb model = _services.GetById((int)entity.lienKetWebId);
                    model.lienKetWebName = entity.lienKetWebName;
                    model.lienKetWebLink = entity.lienKetWebLink;
                    model.isSort = entity.isSort;
                    _services.Update(model, User.Identity.Name);
                    _services.Save();
                }
                else
                {
                    LienKetWeb model = new LienKetWeb
                    {
                        isSort = entity.isSort,
                        lienKetWebLink = entity.lienKetWebLink,
                        lienKetWebName = entity.lienKetWebName
                    };
                    _services.Add(model, User.Identity.Name);
                    _services.Save();
                }
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public ActionResult Trash(int id)
        {
            _services.Delete(id, User.Identity.Name);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}