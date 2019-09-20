using ClassLibrary.Models;
using ClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace www.Areas.Admin.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        private IContentServices _services;
        public GalleryController(IContentServices services)
        {
            _services = services;
        }
        public ActionResult Index(string _searchKey, DateTime? _fromDate, DateTime? _toDate, int? _pageIndex)
        {
            ContentView result;
            int _languageId = 1;
            string _userName = null;
            if (User.IsInRole("Admin"))
                _userName = null;
            else
                _userName = User.Identity.Name;
            result = _services.GetAll(_searchKey, _fromDate, _toDate, null, "Gallery", _languageId, false, _pageIndex, 20, _userName, null);
            int totalPage = result?.Total ?? 0;
            ViewBag.TotalPage = totalPage;
            ViewBag.PageIndex = _pageIndex ?? 1;
            ViewBag.SearchKey = string.IsNullOrWhiteSpace(_searchKey) ? string.Empty : _searchKey;
            ViewBag.FromDate = _fromDate?.ToString("dd/MM/yyyy") ?? null;
            ViewBag.ToDate = _toDate?.ToString("dd/MM/yyyy") ?? null;
            return View(result.ViewContents);
        }

        [HttpGet]
        public ActionResult Detail(int? Id)
        {
            int _languageId = 1;
            Content model = null;
            if (Id.HasValue && Id > 0)
            {
                model = _services.GetById(Id.Value);
                ViewBag.Title = "Cập nhật hình ảnh";
            }
            else
            {
                model = new Content
                {
                    contentId = 0,
                    languageId = _languageId,
                    createTime = DateTime.Now,
                    approvedTime = DateTime.Now,
                    ngayDang = DateTime.Now,
                    allowComment = false,
                    isHome = false,
                    isNew = false,
                    parentId = null,
                    approved = false,
                    approvedUser = User.Identity.Name,
                    createUser = User.Identity.Name,
                    isSort = 0,
                    isTrash = false,
                    isFeature = false,
                    isView = 0,
                    modifiedTime = DateTime.Now,
                    modifiedUser = User.Identity.Name,
                    contentKey = "Gallery"
                };
                ViewBag.Title = "Thêm mới hình ảnh";
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Detail(Content entity)
        {
            if (ModelState.IsValid)
            {
                if (entity.contentId > 0)
                {
                    Content model = _services.GetById((int)entity.contentId);
                    model.contentMain = entity.contentMain;
                    model.description = entity.description;
                    model.contentId = entity.contentId;
                    model.thumbnail = entity.thumbnail;
                    model.modifiedTime = DateTime.Now;
                    model.name = entity.name;
                    _services.Update(model);
                    _services.Save();
                }
                else
                {
                    Content model = new Content
                    {
                        alias = entity.alias,
                        name = entity.name,
                        description = entity.description,
                        thumbnail = entity.thumbnail,
                        createTime = DateTime.Now,
                        modifiedTime = DateTime.Now,
                        ngayDang = DateTime.Now,
                        allowComment = false,
                        approved = false,
                        approvedTime = DateTime.Now,
                        approvedUser = null,
                        authorName = entity.authorName,
                        contentKey = "Gallery",
                        contentMain = entity.contentMain,
                        createUser = User.Identity.Name,
                        isFeature = false,
                        isHome = false,
                        isNew = false,
                        isSort = 0,
                        isTrash = false,
                        isView = 0,
                        languageId = entity.languageId,
                        metaDescription = entity.metaDescription,
                        metaKeyword = entity.metaKeyword,
                        metaTitle = entity.metaTitle,
                        modifiedUser = User.Identity.Name,
                        oldId = null,
                        parentId = entity.parentId
                    };
                    _services.Add(model);
                    _services.Save();
                    model.alias = model.alias + "-" + model.contentId;
                    _services.Update(model);
                    _services.Save();
                }
                return RedirectToAction("Index");
            }
            return View(entity);
        }
    }
}