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
    public class DocumentController : Controller
    {
        private IContentServices _services;
        public DocumentController(IContentServices services)
        {
            _services = services;
        }
        public ActionResult Index(string _searchKey, int? _parentId, DateTime? _fromDate, DateTime? _toDate, int? _pageIndex)
        {
            ContentView result;
            int _languageId = 1;
            string _userName = null;
            if (User.IsInRole("Admin"))
                _userName = null;
            else
                _userName = User.Identity.Name;
            result = _services.GetAll(_searchKey, _fromDate, _toDate, _parentId, "Document", _languageId, false, _pageIndex, 20, _userName, null);
            int totalPage = result?.Total ?? 0;
            ViewBag.TotalPage = totalPage;
            ViewBag.PageIndex = _pageIndex ?? 1;
            ViewBag.SearchKey = string.IsNullOrWhiteSpace(_searchKey) ? string.Empty : _searchKey;
            ViewBag.FromDate = _fromDate?.ToString("dd/MM/yyyy") ?? null;
            ViewBag.ToDate = _toDate?.ToString("dd/MM/yyyy") ?? null;
            IEnumerable<DropdownModel> category = _services.Dropdownlist(0, null, "ChuyenMucDocument", _languageId);
            ViewBag._parentId = category.Select(x => new SelectListItem { Text = x.Text, Value = x.Value.ToString() });
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
                ViewBag.Title = "Cập nhật tài liệu";
            }
            else
            {
                model = new Content
                {
                    contentId = 0,
                    languageId = _languageId,
                    createTime = DateTime.Now,
                    approvedTime = DateTime.Now,
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
                    contentKey = "Document"
                };
                ViewBag.Title = "Thêm mới tài liệu";
            }
            IEnumerable<DropdownModel> category = _services.Dropdownlist(0, null, "ChuyenMucDocument", _languageId);
            ViewBag.parentId = category.Select(x => new SelectListItem { Text = x.Text, Value = x.Value.ToString() });
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
                    model.alias = entity.alias;
                    model.contentMain = entity.contentMain;
                    model.description = entity.description;
                    model.contentId = entity.contentId;
                    model.thumbnail = entity.thumbnail;
                    model.modifiedTime = DateTime.Now;
                    model.parentId = entity.parentId;
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
                        allowComment = false,
                        approved = false,
                        approvedTime = DateTime.Now,
                        approvedUser = null,
                        authorName = entity.authorName,
                        contentKey = "Document",
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
                return RedirectToAction("Index", new { _parentId = entity.parentId });
            }
            IEnumerable<DropdownModel> category = _services.Dropdownlist(0, (int)entity.contentId, "ChuyenMucDocument", entity.languageId);
            ViewBag.contentParentId = category.Select(x => new SelectListItem
            {
                Text = x.Text,
                Value = x.Value.ToString()
            });
            return View(entity);
        }


        public ActionResult ChuyenMuc(string _searchKey, int? _parentId, int? _pageIndex)
        {
            ContentView result;
            int _languageId = 1;
            result = _services.GetAllAdmin(_searchKey, null, null, _parentId, "ChuyenMucDocument", _languageId, false, _pageIndex, 20);
            int totalPage = result?.Total ?? 0;
            ViewBag.TotalPage = totalPage;
            ViewBag.PageIndex = _pageIndex ?? 1;
            ViewBag.SearchKey = string.IsNullOrWhiteSpace(_searchKey) ? string.Empty : _searchKey;
            IEnumerable<DropdownModel> category = _services.Dropdownlist(_parentId.GetValueOrDefault(), null, "ChuyenMucDocument", 1);
            ViewBag._parentId = category.Select(x => new SelectListItem { Text = x.Text, Value = x.Value.ToString() });
            return View(result.ViewContents);
        }

        [HttpGet]
        public ActionResult DetailChuyenMuc(int? Id)
        {
            int _languageId = 1;
            Content model = null;
            if (Id.HasValue && Id > 0)
            {
                model = _services.GetById(Id.Value);
                ViewBag.Title = "Cập nhật chuyên mục";
            }
            else
            {
                model = new Content
                {
                    contentId = 0,
                    languageId = _languageId,
                    createTime = DateTime.Now,
                    approvedTime = DateTime.Now,
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
                    contentKey = "ChuyenMucDocument"
                };
                ViewBag.Title = "Thêm mới chuyên mục";
            }
            IEnumerable<DropdownModel> category = _services.Dropdownlist(0, null, "ChuyenMucDocument", _languageId);
            ViewBag.parentId = category.Select(x => new SelectListItem { Text = x.Text, Value = x.Value.ToString() });
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult DetailChuyenMuc(Content entity)
        {
            if (ModelState.IsValid)
            {
                if (entity.contentId > 0)
                {
                    Content model = _services.GetById((int)entity.contentId);
                    model.alias = entity.alias;
                    model.contentMain = entity.contentMain;
                    model.description = entity.description;
                    model.contentId = entity.contentId;
                    model.thumbnail = entity.thumbnail;
                    model.modifiedTime = DateTime.Now;
                    model.parentId = entity.parentId;
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
                        allowComment = false,
                        approved = false,
                        approvedTime = DateTime.Now,
                        approvedUser = null,
                        authorName = null,
                        contentKey = "ChuyenMucDocument",
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
                return RedirectToAction("Index", new { _parentId = entity.parentId });
            }
            IEnumerable<DropdownModel> category = _services.Dropdownlist(0, (int)entity.contentId, "ChuyenMucDocument", entity.languageId);
            ViewBag.contentParentId = category.Select(x => new SelectListItem
            {
                Text = x.Text,
                Value = x.Value.ToString()
            });
            return View(entity);
        }
    }
}