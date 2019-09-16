using ClassLibrary.Models;
using ClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.Areas.Admin.Models;

namespace www.Areas.Admin.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        IMenuServices _services;
        public MenuController(IMenuServices services)
        {
            this._services = services;
        }
        public ActionResult Index(string _searchKey, int? _parentId, int? _pageIndex)
        {
            MenuView result;
            int _languageId = 1;
            result = _services.GetAll(_searchKey, _parentId, _languageId, false, _pageIndex, 20);
            int totalPage = result?.Total ?? 0;
            ViewBag.TotalPage = totalPage;
            ViewBag.PageIndex = _pageIndex ?? 1;
            ViewBag.SearchKey = string.IsNullOrWhiteSpace(_searchKey) ? string.Empty : _searchKey;
            var category = _services.Dropdownlist(0, null, _languageId);
            ViewBag._parentId = category.Select(x => new SelectListItem { Text = x.Text, Value = x.Value.ToString() });
            if (result != null && result.ViewMenus.Count() > 0)
            {
                var model = result.ViewMenus.Select(x => new ModelDanhMuc
                {
                    isSort = x.isSort,
                    isTrash = x.isTrash,
                    Id = x.menuId,
                    Name = x.menuName,
                    Link = x.menuLink,
                    ParentId = x.menuParentId
                });
                return View(model);
            }
            else
            {
                var model = new List<ModelDanhMuc>();
                return View(model);
            }
        }

        public ActionResult ThemMoi()
        {
            int _languageId = 1;

            var model = new ModelDanhMuc
            {
                Id = 0
            };
            var category = _services.Dropdownlist(0, model.Id, _languageId);
            ViewBag.ParentId = category.Select(x => new SelectListItem { Text = x.Text, Value = x.Value.ToString() });
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult ThemMoi(ModelDanhMuc model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Menu();
                entity.menuName = model.Name;
                entity.menuLink = model.Link;
                entity.menuParentId = model.ParentId;
                entity.isSort = model.isSort;
                entity.isTrash = false;
                entity.languageId = 1;
                _services.Add(entity, User.Identity.Name);
                _services.Save();
                return RedirectToAction("Index", new { _parentId = entity.menuParentId });
            }
            var category = _services.Dropdownlist(0, model.Id, 1);
            ViewBag.ParentId = category.Select(x => new SelectListItem { Text = x.Text, Value = x.Value.ToString() });
            return View(model);
        }

        [HttpGet]
        public ActionResult CapNhat(int Id)
        {
            int _languageId = 1;
            var entity = _services.GetById(Id);
            var model = new ModelDanhMuc
            {
                Id = entity.menuId,
                isSort = entity.isSort,
                isTrash = entity.isTrash,
                Link = entity.menuLink,
                Name = entity.menuName,
                ParentId = entity.menuParentId
            };
            var category = _services.Dropdownlist(0, model.Id, _languageId);
            ViewBag.ParentId = category.Select(x => new SelectListItem { Text = x.Text, Value = x.Value.ToString() });
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhat(ModelDanhMuc model)
        {
            if (ModelState.IsValid)
            {
                var entity = _services.GetById(model.Id);
                entity.menuName = model.Name;
                entity.menuLink = model.Link;
                entity.menuParentId = model.ParentId;
                entity.isSort = model.isSort;
                _services.Update(entity, User.Identity.Name);
                _services.Save();
                return RedirectToAction("Index", new { _parentId = entity.menuParentId });
            }
            var category = _services.Dropdownlist(0, model.Id, 1);
            ViewBag.ParentId = category.Select(x => new SelectListItem { Text = x.Text, Value = x.Value.ToString() });
            return View(model);
        }

        public string getNameById(long? Id)
        {
            if (Id.HasValue)
            {
                var entity = _services.GetById((int)Id);
                if (entity != null)
                    return entity.menuName;
                else
                    return null;
            }
            else
                return null;

        }
    }
}