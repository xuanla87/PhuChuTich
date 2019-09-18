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
        private HttpCookie languagecode;
        public HomeController(IContentServices services, IMenuServices menuServices, IConfigSystemServices configSystemServices, IContactServices contactServices)
        {
            this._services = services;
            this._menuServices = menuServices;
            this._configSystemServices = configSystemServices;
            this._contactServices = contactServices;
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
                var entity = _services.GetAll(searchKey, null, null, null, "TinTuc", _laguageId, false, _pageIndex, 20, null, true);
                _totalRecord = entity.TotalRecord;
                ViewBag.TotalRecord = _totalRecord.ToString();
                ViewBag.TotalPage = entity.Total;
                ViewBag.PageIndex = _pageIndex ?? 1;
                ViewBag.SearchKey = searchKey;
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
            return PartialView();
        }

        public ActionResult TuLieuVideo()
        {
            return PartialView();
        }

        public ActionResult Box1()
        {
            int Id = 0;
            int.TryParse(_configSystemServices.GetValueByKey("Box1"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            var entity = _services.GetById(Id);
            ViewBag.Url = entity.alias;
            return PartialView(model.ViewContents.OrderBy(x => x.ngayDang).Take(5));
        }
        public ActionResult Box2()
        {
            int Id = 0;
            int.TryParse(_configSystemServices.GetValueByKey("Box2"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            var entity = _services.GetById(Id);
            ViewBag.Url = entity.alias;
            return PartialView(model.ViewContents.OrderBy(x => x.ngayDang).Take(10));
        }

        public ActionResult Box3()
        {
            int Id = 0;
            int.TryParse(_configSystemServices.GetValueByKey("Box3"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            var entity = _services.GetById(Id);
            ViewBag.Url = entity.alias;
            return PartialView(model.ViewContents.OrderBy(x => x.ngayDang).Take(5));
        }
        public ActionResult Box4()
        {
            int Id = 0;
            int.TryParse(_configSystemServices.GetValueByKey("Box4"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            var entity = _services.GetById(Id);
            ViewBag.Url = entity.alias;
            return PartialView(model.ViewContents.OrderBy(x => x.ngayDang).Take(5));
        }
        public ActionResult Box5()
        {
            int Id = 0;
            int.TryParse(_configSystemServices.GetValueByKey("Box5"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            var entity = _services.GetById(Id);
            ViewBag.Url = entity.alias;
            return PartialView(model.ViewContents.OrderBy(x => x.ngayDang).Take(5));
        }
        public ActionResult Box6()
        {
            int Id = 0;
            int.TryParse(_configSystemServices.GetValueByKey("Box6"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            var entity = _services.GetById(Id);
            ViewBag.Url = entity.alias;
            return PartialView(model.ViewContents.OrderBy(x => x.ngayDang).Take(5));
        }
        public ActionResult Box7()
        {
            int Id = 0;
            int.TryParse(_configSystemServices.GetValueByKey("Box7"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            var entity = _services.GetById(Id);
            ViewBag.Url = entity.alias;
            return PartialView(model.ViewContents.OrderBy(x => x.ngayDang).Take(5));
        }
        public ActionResult Box8()
        {
            int Id = 0;
            int.TryParse(_configSystemServices.GetValueByKey("Box8"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            var entity = _services.GetById(Id);
            ViewBag.Url = entity.alias;
            return PartialView(model.ViewContents.OrderBy(x => x.ngayDang).Take(5));
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
            return PartialView(entity.ViewContents.OrderByDescending(x => x.isView).Take(10));
        }

        public ActionResult TuTuongDaoDuc()
        {
            int Id = 0;
            int.TryParse(_configSystemServices.GetValueByKey("Box1"), out Id);
            var model = _services.GetAll(null, null, null, Id, "TinTuc", 1, false, null, null, null, true);
            var entity = _services.GetById(Id);
            ViewBag.Url = entity.alias;
            return PartialView(model.ViewContents.OrderBy(x => x.ngayDang).Take(8));
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
                    _stTime = "Thứ 2";
                else if (_time.DayOfWeek == DayOfWeek.Tuesday)
                    _stTime = "Thứ 3";
                else if (_time.DayOfWeek == DayOfWeek.Wednesday)
                    _stTime = "Thứ 4";
                else if (_time.DayOfWeek == DayOfWeek.Thursday)
                    _stTime = "Thứ 5";
                else if (_time.DayOfWeek == DayOfWeek.Friday)
                    _stTime = "Thứ 6";
                else if (_time.DayOfWeek == DayOfWeek.Saturday)
                    _stTime = "Thứ 7";
                else if (_time.DayOfWeek == DayOfWeek.Sunday)
                    _stTime = "Chủ nhật";
            }
            _stTime += _time.ToString("dd | MM | yyyy");
            ViewBag.TimePage = _stTime;
            return PartialView();
        }
    }
}