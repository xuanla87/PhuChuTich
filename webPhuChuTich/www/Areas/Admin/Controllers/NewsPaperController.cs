﻿using ClassLibrary.Models;
using ClassLibrary.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace www.Areas.Admin.Controllers
{
    [Authorize]
    public class NewsPaperController : Controller
    {
        private IContentServices _services;
        private IOptionServices _optionServices;
        www.Models.ModelPCT _db;
        public NewsPaperController(IContentServices services, IOptionServices optionServices)
        {
            _services = services;
            _optionServices = optionServices;
            _db = new www.Models.ModelPCT();
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public ActionResult Index(string _searchKey, int? _parentId, DateTime? _fromDate, DateTime? _toDate, int? _pageIndex)
        {
            var _user = _db.UserLogins.Find(User.Identity.Name);
            if (_user != null && _user.sessionId != HttpContext.Session.SessionID)
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            int _languageId = 1;
            string _userName = null;
            if (User.IsInRole("Admin"))
                _userName = null;
            else
                _userName = User.Identity.Name;
            var _list = new List<int>();
            IQueryable<www.Models.Content> _listChuyenMuc = null;
            if (_parentId > 0)
                _listChuyenMuc = _db.Contents.Where(x => x.contentKey == "ChuyenMucTinTuc" && x.languageId == 1 && x.isTrash == false && x.parentId == _parentId);
            if (_listChuyenMuc != null && _listChuyenMuc.Count() > 0)
            {
                foreach (var item in _listChuyenMuc)
                {
                    _list.Add(item.contentId);
                    _list = getById(item.contentId, _list);
                }
            }
            var _listContent = new List<www.Models.Content>();
            if (_parentId > 0)
                _list.Add(_parentId.Value);
            if (_list.Count > 0)
            {
                foreach (var item in _list)
                {
                    var model = _db.Contents.Where(x => x.parentId == item && x.contentKey == "TinTuc" && x.languageId == 1 && x.isTrash == false);
                    _listContent.AddRange(model.ToList());
                }
            }
            else
                _listContent = _db.Contents.Where(x => x.contentKey == "TinTuc" && x.languageId == 1 && x.isTrash == false).ToList();
            _listContent = _listContent.OrderByDescending(x => x.ngayDang).ToList();
            if (!string.IsNullOrEmpty(_searchKey))
                _listContent = _listContent.Where(x => x.name.Contains(_searchKey)).ToList();
            if (_fromDate.HasValue)
                _listContent = _listContent.Where(x => x.ngayDang >= _fromDate.Value.Date).ToList();
            if (_toDate.HasValue)
                _listContent = _listContent.Where(x => x.ngayDang <= _toDate.Value.Date.AddDays(1).AddSeconds(-1)).ToList();
            int _totalRecord = _listContent.Count;
            if (_pageIndex != null)
                _listContent = _listContent.Skip((_pageIndex.Value - 1) * 15).ToList();
            var totalPage = 0;
            totalPage = (int)Math.Ceiling(1.0 * _totalRecord / 15);
            _listContent = _listContent.Take(15).ToList();
            ViewBag.TotalRecord = _totalRecord.ToString();
            ViewBag.TotalPage = totalPage;
            ViewBag.PageIndex = _pageIndex ?? 1;
            ViewBag.SearchKey = string.IsNullOrWhiteSpace(_searchKey) ? string.Empty : _searchKey;
            ViewBag.FromDate = _fromDate?.ToString("dd/MM/yyyy") ?? null;
            ViewBag.ToDate = _toDate?.ToString("dd/MM/yyyy") ?? null;
            IEnumerable<DropdownModel> category = _services.Dropdownlist(0, null, "ChuyenMucTinTuc", _languageId);
            ViewBag._parentId = category.Select(x => new SelectListItem { Text = x.Text, Value = x.Value.ToString() });
            return View(_listContent);
        }

        private List<int> getById(int Id, List<int> _list)
        {
            var _listChuyenMuc = _db.Contents.Where(x => x.contentKey == "ChuyenMucTinTuc" && x.languageId == 1 && x.isTrash == false && x.parentId == Id);
            foreach (var item in _listChuyenMuc)
            {
                _list.Add(item.contentId);
                _list = getById(item.contentId, _list);
            }
            return _list;
        }

        [HttpGet]
        public ActionResult Detail(int? Id)
        {
            var _user = _db.UserLogins.Find(User.Identity.Name);
            if (_user != null && _user.sessionId != HttpContext.Session.SessionID)
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            int _languageId = 1;
            Content model = null;
            if (Id.HasValue && Id > 0)
            {
                model = _services.GetById(Id.Value);
                ViewBag.NgayViet = model.ngayDang.ToString("dd/MM/yyyy");
                ViewBag.Title = "Cập nhật bài viết";
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
                    contentKey = "TinTuc"
                };
                ViewBag.Title = "Thêm mới bài viết";
            }
            IEnumerable<DropdownModel> category = _services.Dropdownlist(0, null, "ChuyenMucTinTuc", _languageId);
            ViewBag.parentId = category.Select(x => new SelectListItem { Text = x.Text, Value = x.Value.ToString() });
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Detail(Content entity, string NgayViet)
        {
            DateTime _ngayDang = DateTime.Now;
            if (!string.IsNullOrEmpty(NgayViet))
                _ngayDang = DateTime.ParseExact(NgayViet, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (ModelState.IsValid)
            {
                if (entity.contentId > 0)
                {
                    Content model = _services.GetById((int)entity.contentId);
                    if (entity.alias.Contains("-" + entity.contentId))
                        model.alias = entity.alias;
                    else
                        model.alias = entity.alias + "-" + entity.contentId;
                    model.contentMain = entity.contentMain;
                    model.description = entity.description;
                    model.contentId = entity.contentId;
                    model.thumbnail = entity.thumbnail;
                    model.modifiedTime = DateTime.Now;
                    model.parentId = entity.parentId;
                    model.name = entity.name;
                    model.ngayDang = _ngayDang;
                    model.authorName = entity.authorName;
                    model.isFeature = entity.isFeature;
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
                        ngayDang = _ngayDang,
                        allowComment = false,
                        approved = false,
                        approvedTime = null,
                        approvedUser = null,
                        authorName = entity.authorName,
                        contentKey = "TinTuc",
                        contentMain = entity.contentMain,
                        createUser = User.Identity.Name,
                        isFeature = entity.isFeature,
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
            IEnumerable<DropdownModel> category = _services.Dropdownlist(0, (int)entity.contentId, "ChuyenMucTinTuc", entity.languageId);
            ViewBag.contentParentId = category.Select(x => new SelectListItem
            {
                Text = x.Text,
                Value = x.Value.ToString()
            });
            return View(entity);
        }


        public ActionResult ChuyenMuc(string _searchKey, int? _parentId, int? _pageIndex)
        {
            var _user = _db.UserLogins.Find(User.Identity.Name);
            if (_user != null && _user.sessionId != HttpContext.Session.SessionID)
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            ContentView result;
            int _languageId = 1;
            result = _services.GetAllAdmin(_searchKey, null, null, _parentId, "ChuyenMucTinTuc", _languageId, false, _pageIndex, 20);
            int totalPage = result?.Total ?? 0;
            ViewBag.TotalPage = totalPage;
            ViewBag.PageIndex = _pageIndex ?? 1;
            ViewBag.SearchKey = string.IsNullOrWhiteSpace(_searchKey) ? string.Empty : _searchKey;
            IEnumerable<DropdownModel> category = _services.Dropdownlist(_parentId.GetValueOrDefault(), null, "ChuyenMucTinTuc", 1);
            ViewBag._parentId = category.Select(x => new SelectListItem { Text = x.Text, Value = x.Value.ToString() });
            return View(result.ViewContents);
        }

        [HttpGet]
        public ActionResult DetailChuyenMuc(int? Id)
        {
            var _user = _db.UserLogins.Find(User.Identity.Name);
            if (_user != null && _user.sessionId != HttpContext.Session.SessionID)
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
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
                    contentKey = "ChuyenMucTinTuc"
                };
                ViewBag.Title = "Thêm mới chuyên mục";
            }
            IEnumerable<DropdownModel> category = _services.Dropdownlist(0, null, "ChuyenMucTinTuc", _languageId);
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
                    model.showCount = entity.showCount;
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
                        approvedTime = null,
                        approvedUser = null,
                        authorName = null,
                        contentKey = "ChuyenMucTinTuc",
                        contentMain = entity.contentMain,
                        createUser = User.Identity.Name,
                        showCount = entity.showCount,
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
                return RedirectToAction("ChuyenMuc", new { _parentId = entity.parentId });
            }
            IEnumerable<DropdownModel> category = _services.Dropdownlist(0, (int)entity.contentId, "ChuyenMucTinTuc", entity.languageId);
            ViewBag.contentParentId = category.Select(x => new SelectListItem
            {
                Text = x.Text,
                Value = x.Value.ToString()
            });
            return View(entity);
        }

        [HttpGet]
        public ActionResult AddImg(int Id)
        {
            IEnumerable<Option> model = null;
            if (Id > 0)
            {
                var _content = _services.GetById(Id);
                ViewBag.Name = _content.name;
                model = _optionServices.GetByContentId(Id);
            }
            ViewBag.ContentId = Id;
            return View(model);
        }

        public ActionResult Option(int Id, string thumbnail)
        {
            if (Id > 0 && !string.IsNullOrEmpty(thumbnail))
            {
                var model = _optionServices.GetByContentId(Id);
                var entity = new Option
                {
                    contentId = Id,
                    isSort = model.Count(),
                    thumbnail = thumbnail,
                    videoClip = null
                };
                _optionServices.Add(entity, User.Identity.Name);
                _optionServices.Save();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveOption(int Id)
        {
            var entity = _optionServices.GetById(Id);
            var model = _optionServices.GetByContentId(entity.contentId).Where(x => x.isSort > entity.isSort).ToList();
            foreach (var item in model)
            {
                item.isSort = item.isSort - 1;
                _optionServices.Update(item, User.Identity.Name);
                _optionServices.Save();
            }

            _optionServices.Delete(Id, User.Identity.Name);
            _optionServices.Save();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UnApproval(int id)
        {
            _services.UnApproval(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Approval(int id)
        {
            _services.Approval(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Trash(int id)
        {
            _services.Trash(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Up(int id, int _sort)
        {
            var entity = _optionServices.GetById(id);
            if (entity != null)
            {
                entity.isSort = _sort - 1;
                _optionServices.Update(entity, User.Identity.Name);
                _optionServices.Save();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Down(int id, int _sort)
        {
            var entity = _optionServices.GetById(id);
            if (entity != null)
            {
                entity.isSort = _sort + 1;
                _optionServices.Update(entity, User.Identity.Name);
                _optionServices.Save();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public string getName(int? Id)
        {
            if (Id.HasValue && Id > 0)
            {
                var entity = _services.GetById(Id.Value);
                return entity.name;
            }
            else
                return null;
        }
    }
}