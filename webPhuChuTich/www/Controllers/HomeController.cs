using ClassLibrary.Models;
using ClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using www.Models;

namespace www.Controllers
{

    public class HomeController : Controller
    {
        IContentServices _services;
        IMenuServices _menuServices;
        IConfigSystemServices _configSystemServices;
        IContactServices _contactServices;
        ILienKetWebServices _lienKetWebServices;
        IOptionServices _optionService;
        private HttpCookie languagecode;
        public HomeController(IContentServices services, IMenuServices menuServices, IConfigSystemServices configSystemServices, IContactServices contactServices, ILienKetWebServices lienKetWebServices, IOptionServices optionService)
        {
            this._services = services;
            this._menuServices = menuServices;
            this._configSystemServices = configSystemServices;
            this._contactServices = contactServices;
            this._lienKetWebServices = lienKetWebServices;
            this._optionService = optionService;
        }

        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Detail(string pageUrl)
        {
            var entity = _services.GetByAlias(pageUrl);
            return View(entity);
        }

        [ValidateInput(false)]
        public ActionResult Display(string pageUrl, int? _pageIndex)
        {
            var entity = _services.GetByAlias(pageUrl);
            if (entity != null)
            {
                ViewBag.Title = entity.name;
                _services.UpdateView(entity);
            }
            else
                ViewBag.Title = _configSystemServices.GetValueByKey("SiteTitle");
            return View(entity);
        }

        public ActionResult getMenuMain()
        {
            int _languageId = 1;
            int Id = 0;
            if (_languageId == 1)
                int.TryParse(_configSystemServices.GetValueByKey("DanhMucChinh"), out Id);
            List<Menu> eMenus = _menuServices.GetByParent(Id).Where(x => x.isTrash == false).OrderBy(x => x.isSort).ToList();
            return PartialView(eMenus);
        }

        public ActionResult _HitCounter()
        {

            HitCounterEntity db = new HitCounterEntity();
            int TotalOnline = 0;
            int TotalYesterday = 0;
            int TotalMonth = 0;
            int Total = 0;
            try
            {
                var _hit = db.HitCounters.ToList();
                TotalOnline = (int)HttpContext.Application["Totaluser"];
                TotalYesterday = _hit.Where(x => x.visitTime <= DateTime.Now && x.visitTime >= DateTime.Now.Date).Count();
                TotalMonth = _hit.Where(x => x.visitTime.Year == DateTime.Now.Year && x.visitTime.Month == DateTime.Now.Month).Count();
                Total = _hit.Count();
            }
            catch
            {
            }
            ViewBag.TotalOnline = TotalOnline.ToString("N0");
            ViewBag.TotalYesterday = TotalYesterday.ToString("N0"); ;
            ViewBag.TotalMonth = TotalMonth.ToString("N0");
            ViewBag.Total = Total.ToString("N0");
            return PartialView();
        }

        public ActionResult Search(string searchKey, int? _pageIndex)
        {
            int _laguageId = 1;
            if (string.IsNullOrEmpty(searchKey))
            {
                var entity = new ContentView();
                return View(entity.ViewContents);
            }
            else
            {
                int _totalRecord = 0;
                _pageIndex = _pageIndex ?? 1;
                var entity = _services.GetAll(searchKey, null, null, null, "TinTuc", _laguageId, false, _pageIndex, 9, null, true);
                _totalRecord = entity.TotalRecord;
                ViewBag.TotalRecord = _totalRecord.ToString();
                ViewBag.TotalPage = entity.Total;
                ViewBag.PageIndex = _pageIndex ?? 1;
                ViewBag.SearchKey = searchKey;
                ViewBag.CurentUrl = "tim-kiem?searchKey=" + searchKey;
                return View(entity.ViewContents.OrderByDescending(x => x.createTime));
            }

        }

        public ActionResult LienHe()
        {
            Session["CAPTCHA"] = GetRandomText();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult LienHe(Contact entity, string txtCaptcha)
        {
            if (ModelState.IsValid)
            {
                string text = Session["CAPTCHA"].ToString();
                if (txtCaptcha.ToLower() == text.ToLower())
                {
                    var model = new Contact();
                    model.contactFullName = entity.contactFullName;
                    model.contactTitle = entity.contactTitle;
                    model.contactEmail = entity.contactEmail;
                    model.contactBody = entity.contactBody;
                    model.createTime = DateTime.Now;
                    model.isTrash = false;
                    _contactServices.Add(model);
                    _contactServices.Save();
                    return Redirect("/");
                }
                return View(entity);
            }
            return View(entity);
        }

        private string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
            Random r = new Random();
            for (int j = 0; j < 4; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            return randomText.ToString();
        }

        public FileResult GetCaptchaImage()
        {
            string text = Session["CAPTCHA"].ToString();
            MemoryStream ms = new MemoryStream();
            RandomImage _captcha = new RandomImage(text, 220, 50);
            _captcha.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            _captcha.Dispose();
            return File(ms.ToArray(), "image/png");
        }

        public ActionResult TuLieuAnh()
        {
            var model = _services.GetAll(null, null, null, null, "Gallery", 1, false, null, null, null, null);
            return PartialView(model.ViewContents.OrderBy(x => x.isSort).Take(15));
        }

        public ActionResult TuLieuVideo()
        {
            var model = _services.GetAll(null, null, null, null, "Video", 1, false, null, null, null, null);
            return PartialView(model.ViewContents.OrderBy(x => x.isSort).Take(5));
        }

        public ActionResult TuLieuVideoL(int? _pageIndex)
        {
            int _laguageId = 1;
            int _totalRecord = 0;
            _pageIndex = _pageIndex ?? 1;
            var entity = _services.GetAll(null, null, null, null, "Video", _laguageId, false, _pageIndex, 20, null, null);
            _totalRecord = entity.TotalRecord;
            ViewBag.TotalRecord = _totalRecord.ToString();
            ViewBag.TotalPage = entity.Total;
            ViewBag.PageIndex = _pageIndex ?? 1;
            return View(entity.ViewContents.OrderByDescending(x => x.createTime));
        }

        public ActionResult TuLieuHinhAnhL(int? _pageIndex)
        {
            int _laguageId = 1;
            int _totalRecord = 0;
            _pageIndex = _pageIndex ?? 1;
            var entity = _services.GetAll(null, null, null, null, "Gallery", _laguageId, false, _pageIndex, 20, null, null);
            _totalRecord = entity.TotalRecord;
            ViewBag.TotalRecord = _totalRecord.ToString();
            ViewBag.TotalPage = entity.Total;
            ViewBag.PageIndex = _pageIndex ?? 1;
            return View(entity.ViewContents.OrderByDescending(x => x.createTime));
        }

        public ActionResult Box1()
        {
            int Id = 0;

            int.TryParse(_configSystemServices.GetValueByKey("Box1"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            if (Id > 0)
            {
                var entity = _services.GetById(Id);
                ViewBag.Url = entity.alias;
            }
            return PartialView(model.ViewContents.OrderByDescending(x => x.ngayDang).Take(5));
        }

        public ActionResult Box2()
        {
            int Id = 0;
            int.TryParse(_configSystemServices.GetValueByKey("Box2"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            if (Id > 0)
            {
                var entity = _services.GetById(Id);
                ViewBag.Url = entity.alias;
            }
            return PartialView(model.ViewContents.OrderByDescending(x => x.ngayDang).Take(10));
        }

        public ActionResult Box3()
        {
            int Id = 0;
            int.TryParse(_configSystemServices.GetValueByKey("Box3"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            if (Id > 0)
            {
                var entity = _services.GetById(Id);
                ViewBag.Url = entity.alias;
            }
            return PartialView(model.ViewContents.OrderByDescending(x => x.ngayDang).Take(5));
        }

        public ActionResult Box4()
        {
            int Id = 0;
            int.TryParse(_configSystemServices.GetValueByKey("Box4"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            if (Id > 0)
            {
                var entity = _services.GetById(Id);
                ViewBag.Url = entity.alias;
            }
            return PartialView(model.ViewContents.OrderByDescending(x => x.ngayDang).Take(5));
        }

        public ActionResult Box5()
        {
            int Id = 0;
            int.TryParse(_configSystemServices.GetValueByKey("Box5"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            if (Id > 0)
            {
                var entity = _services.GetById(Id);
                ViewBag.Url = entity.alias;
            }
            return PartialView(model.ViewContents.OrderByDescending(x => x.ngayDang).Take(5));
        }

        public ActionResult Box6()
        {
            int Id = 0;
            int.TryParse(_configSystemServices.GetValueByKey("Box6"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            if (Id > 0)
            {
                var entity = _services.GetById(Id);
                ViewBag.Url = entity.alias;
            }
            return PartialView(model.ViewContents.OrderByDescending(x => x.ngayDang).Take(4));
        }

        public ActionResult Box7()
        {
            int Id = 0;
            int.TryParse(_configSystemServices.GetValueByKey("Box7"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            if (Id > 0)
            {
                var entity = _services.GetById(Id);
                ViewBag.Url = entity.alias;
            }
            return PartialView(model.ViewContents.OrderByDescending(x => x.ngayDang).Take(5));
        }

        public ActionResult Box8()
        {
            int Id = 0;
            int.TryParse(_configSystemServices.GetValueByKey("Box8"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            if (Id > 0)
            {
                var entity = _services.GetById(Id);
                ViewBag.Url = entity.alias;
            }
            return PartialView(model.ViewContents.OrderByDescending(x => x.ngayDang).Take(5));
        }

        public ActionResult ThongTinHoatDong()
        {
            return PartialView();
        }

        public ActionResult getChildDisplay(int Id, string _url, int? _pageIndex)
        {
            int _totalRecord = 0;
            _pageIndex = _pageIndex ?? 1;
            var entity = _services.GetAll(null, null, null, Id, null, 1, false, _pageIndex, 10, null, true);
            _totalRecord = entity.TotalRecord;
            ViewBag.TotalRecord = _totalRecord.ToString();
            ViewBag.TotalPage = entity.Total;
            ViewBag.PageIndex = _pageIndex ?? 1;
            ViewBag.CurentUrl = _url;
            return PartialView(entity.ViewContents.OrderByDescending(x => x.ngayDang));
        }

        public ActionResult getBreadcrumb(string pageUrl)
        {
            var entity = _services.GetByAlias(pageUrl);
            if (entity != null && entity.parentId.HasValue)
            {
                var model = _services.GetById(entity.parentId.Value);
                if (model != null)
                {
                    ViewBag.PTitle = "<a href=\"/\">Trang chủ</a> - " + getParent(model.parentId) + "<a href=\"" + model.alias + "\">" + model.name + "</a>";
                }
                else
                {
                    ViewBag.PTitle = "<a href=\"/\">Trang chủ</a> - " + entity.name;
                }
            }
            else if (entity != null)
            {
                ViewBag.PTitle = "<a href=\"/\">Trang chủ</a> - " + entity.name;
            }
            return PartialView();
        }

        public string getParent(int? Id)
        {
            string _outHtml = "";
            if (Id.HasValue && Id.Value > 0)
            {
                var model = _services.GetById((int)Id.Value);
                _outHtml += getParent(model.parentId);
                if (!string.IsNullOrEmpty(_outHtml))
                    _outHtml += " - ";
                _outHtml += "<a href=\"" + model.alias + "\">" + model.name + "</a> >> ";
                return _outHtml;
            }
            else
            {
                return "";
            }
        }

        public ActionResult BaiVietXemNhieu()
        {
            var entity = _services.GetAll(null, null, null, null, "TinTuc", 1, false, null, null, null, true);
            return PartialView(entity.ViewContents.OrderByDescending(x => x.isView).Take(8));
        }

        public ActionResult TuTuongDaoDuc()
        {
            int Id = 0;
            int.TryParse(_configSystemServices.GetValueByKey("Box1"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            var entity = _services.GetById(Id);
            if (entity != null)
                ViewBag.Url = entity.alias;
            return PartialView(model.ViewContents.OrderByDescending(x => x.ngayDang).Take(8));
        }

        public ActionResult NgayThang()
        {
            DateTime _time = DateTime.Now;
            string _stTime = null;
            languagecode = HttpContext.Request.Cookies["languagecode"];
            if (languagecode != null && languagecode.Value == "en")
            {
                _stTime = _time.DayOfWeek.ToString();
            }
            else
            {
                if (_time.DayOfWeek == DayOfWeek.Monday)
                    _stTime = "Thứ hai";
                else if (_time.DayOfWeek == DayOfWeek.Tuesday)
                    _stTime = "Thứ ba";
                else if (_time.DayOfWeek == DayOfWeek.Wednesday)
                    _stTime = "Thứ tư";
                else if (_time.DayOfWeek == DayOfWeek.Thursday)
                    _stTime = "Thứ năm";
                else if (_time.DayOfWeek == DayOfWeek.Friday)
                    _stTime = "Thứ sáu";
                else if (_time.DayOfWeek == DayOfWeek.Saturday)
                    _stTime = "Thứ bẩy";
                else if (_time.DayOfWeek == DayOfWeek.Sunday)
                    _stTime = "Chủ nhật";
            }
            _stTime += ", " + _time.ToString("dd/MM/yyyy");
            ViewBag.TimePage = _stTime;
            return PartialView();
        }

        public ActionResult BaiVietNoiBat()
        {
            var model = _services.GetTinTucChung(null, null, null, null, "TinTuc", 1, false, true, null, null);
            return PartialView(model.ViewContents.Take(6));
        }

        public string MainMenu2()
        {
            int _languageId = 1;
            int Id = 0;
            if (_languageId == 1)
                int.TryParse(_configSystemServices.GetValueByKey("DanhMucChinh"), out Id);
            List<Menu> eMenus = _menuServices.GetByParent(Id).Where(x => x.isTrash == false).OrderBy(x => x.isSort).ToList();
            string _html = "";
            _html += "<ul class=\"main-nav nav navbar-nav\">";
            foreach (var item in eMenus)
            {
                if (item.menuLink == "/" || item.menuLink == "")
                {
                    _html += " <li>";
                    _html += " <a href=\"" + item.menuLink + "\">";
                    _html += item.menuName;
                    _html += "</a>";
                    _html += SubMenu3("sub-menu nav", item.menuId);
                    _html += "</li>";
                }
                else
                {
                    _html += " <li>";
                    _html += " <a href=\"" + item.menuLink + "\">";
                    _html += item.menuName;
                    _html += "</a>";
                    _html += SubMenu3("sub-menu nav", item.menuId);
                    _html += "</li>";
                }
            }
            _html += "</ul>";
            return _html;
        }

        public string SubMenu(string _css, string _html, string _link)
        {
            var entity = _services.GetByAlias(_link);
            if (entity != null)
            {
                var model = _services.GetAll(null, null, null, (int)entity.contentId, entity.contentKey, 1, false, null, null, null, null);
                var List = model.ViewContents.OrderBy(x => x.isSort);
                _html += " <ul class=\"" + _css + "\">";
                foreach (var item in List)
                {
                    _html += " <li>";
                    _html += " <a href=\"" + item.alias + "\">";
                    _html += item.name;
                    _html += "</a>";
                    _html += SubMenu2(_css, (int)item.contentId, item.contentKey);
                    _html += "</li>";
                }
                _html += "</ul>";
            }
            return _html;
        }
        public string SubMenu2(string _css, int _id, string _key)
        {
            string _html = "";
            var model = _services.GetByParent(_id, _key);
            if (model.Count() > 0)
            {
                _html += " <ul class=\"" + _css + "\">";
                foreach (var item in model)
                {
                    _html += " <li>";
                    _html += " <a href=\"" + item.alias + "\">";
                    _html += item.name;
                    _html += "</a>";
                    _html += SubMenu2(_css, (int)item.contentId, item.contentKey);
                    _html += "</li>";
                }
                _html += "</ul>";
            }

            return _html;
        }
        public string SubMenu3(string _css, int? _id)
        {
            string _html = "";
            if (_id.HasValue && _id > 0)
            {
                var model = _menuServices.GetByParent(_id).Where(x => x.isTrash == false);
                if (model.Count() > 0)
                {
                    model = model.OrderBy(x => x.isSort);
                    _html += " <ul class=\"" + _css + "\">";
                    foreach (var item in model)
                    {
                        _html += " <li>";
                        _html += " <a href=\"" + item.menuLink + "\">";
                        _html += item.menuName;
                        _html += "</a>";
                        _html += SubMenu3(_css, item.menuId);
                        _html += "</li>";
                    }
                    _html += "</ul>";
                }
            }
            return _html;
        }
        public ActionResult BaiVietMoi()
        {
            var entity = _services.GetAll(null, null, null, null, "TinTuc", 1, false, null, null, null, true);
            return PartialView(entity.ViewContents.OrderByDescending(x => x.createTime).Take(10));
        }

        public ActionResult LienKetWeb()
        {
            var model = _lienKetWebServices.All();
            return PartialView(model);
        }

        public string getOption(int Id)
        {
            var model = _optionService.GetByContentId(Id);
            string _html = "";
            if (model.Count() > 0)
            {
                _html += "<div class=\"lightbox-main\">";
                _html += "<div class=\"row\">";
                foreach (var item in model)
                {
                    _html += "<div class=\"col-md-4\">";
                    _html += "<div class=\"item\">";
                    _html += "<a href=\"" + item.thumbnail + "\" data-lightbox=\"roadtrip\"><img src=\"" + item.thumbnail + "\" alt=\"\" /></a>";
                    _html += "</div>";
                    _html += "</div>";
                }
                _html += "</div>";
                _html += "</div>";
            }
            return _html;
        }

        public string getOption2(int Id)
        {
            var model = _optionService.GetByContentId(Id);
            string _html = "";
            if (model.Count() > 0)
            {
                _html += "<div class=\"lightbox-main\">";
                _html += "<div class=\"row\">";
                foreach (var item in model)
                {
                    _html += "<div class=\"col-md-4\">";
                    _html += "<div class=\"item\">";
                    _html += "<a href=\"#modal-" + Id + "\" data-target=\"#modal-" + Id + "\" data-toggle=\"modal\" ><img src=\"" + item.thumbnail + "\" alt=\"\" /></a>";
                    _html += "</div>";
                    _html += "</div>";
                }
                _html += "</div>";
                _html += "<div class=\"modal\" id=\"modal-" + Id + "\" role=\"dialog\">";
                _html += "<div class=\"modal-dialog modal-lg\" role=\"document\">";
                _html += "<div class=\"modal-content\">";
                _html += "<div class=\"modal-header\">";
                _html += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\">";
                _html += "<span aria-hidden=\"true\">&times;</span>";
                _html += "</button>";
                _html += "</div>";
                _html += "<div class=\"modal-body\">";
                _html += "<div class=\"wrapper-img-slider\">";
                _html += "<div class=\"img-slider owl-carousel owl-theme\">";
                foreach (var item in model)
                {
                    _html += "<div class=\"item\">";
                    _html += "<a href=\"" + item.thumbnail + "\"><img src=\"" + item.thumbnail + "\" alt=\"\" /></a>";
                    _html += "</div>";
                }
                _html += "</div>";
                _html += "</div>";
                _html += "</div>";
                _html += "</div>";
                _html += "</div>";
                _html += "</div>";
                _html += "</div>";
            }
            return _html;
        }
    }
}