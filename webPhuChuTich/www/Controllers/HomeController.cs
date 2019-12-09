using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        ModelPCT _db;
        private HttpCookie languagecode;
        public HomeController()
        {
            this._db = new ModelPCT();
        }

        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Detail(string pageUrl)
        {
            var entity = GetByAlias(pageUrl);
            return View(entity);
        }

        [ValidateInput(false)]
        public ActionResult Display(string pageUrl, int? _pageIndex)
        {
            var entity = GetByAlias(pageUrl);
            if (entity != null)
            {
                ViewBag.Title = entity.name;
                entity.isView = entity.isView + 1;
                _db.Contents.Attach(entity);
                _db.Entry(entity).State = EntityState.Modified;
                _db.SaveChanges();
            }
            else
                ViewBag.Title = GetValueByKey("SiteTitle");
            return View(entity);
        }

        private Content GetByAlias(string _pageUrl)
        {
            var entity = _db.Contents.FirstOrDefault(x => x.alias == _pageUrl);
            return entity;
        }
        private Content GetById(int Id)
        {
            var entity = _db.Contents.Find(Id);
            return entity;
        }

        private IEnumerable<Content> GetByParent(int? _id, string _key)
        {
            var entity = _db.Contents.Where(x => x.parentId == _id && x.contentKey == _key);
            return entity;
        }
        private string GetValueByKey(string _key)
        {
            var model = _db.ConfigSytems.FirstOrDefault(x => x.configKey == _key);
            if (model != null)
                return model.configValue;
            else
                return null;
        }

        public ActionResult getMenuMain()
        {
            int _languageId = 1;
            int Id = 0;
            if (_languageId == 1)
                int.TryParse(GetValueByKey("DanhMucChinh"), out Id);
            List<Menu> eMenus = _db.Menus.Where(x => x.menuParentId == Id && x.isTrash == false).OrderBy(x => x.isSort).ToList();
            return PartialView(eMenus);
        }

        public ActionResult _HitCounter()
        {

            int TotalOnline = 0;
            int TotalYesterday = 0;
            int TotalMonth = 0;
            int Total = 0;
            try
            {
                var _hit = _db.HitCounters.ToList();
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
                var entity = new Content();
                return View(entity);
            }
            else
            {
                int _totalRecord = 0;
                _pageIndex = _pageIndex ?? 1;
                var entity = _db.Contents.Where(x => x.contentKey == "TinTuc" && x.isTrash == false && x.approved == true && x.languageId == _laguageId);
                if (!string.IsNullOrEmpty(searchKey))
                    entity = entity.Where(x => x.name.Contains(searchKey.Trim()));
                entity = entity.OrderByDescending(x => x.ngayDang);
                _totalRecord = entity.Count();
                if (_pageIndex != null)
                    entity = entity.Skip((_pageIndex.Value - 1) * 9);
                var totalPage = 0;
                totalPage = (int)Math.Ceiling(1.0 * _totalRecord / 9);
                entity = entity.Take(9);
                ViewBag.TotalRecord = _totalRecord.ToString();
                ViewBag.TotalPage = totalPage;
                ViewBag.PageIndex = _pageIndex ?? 1;
                ViewBag.SearchKey = searchKey;
                return View(entity);
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
                    _db.Contacts.Add(model);
                    _db.SaveChanges();
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
            var model = _db.Contents.Where(x => x.contentKey == "Gallery" && x.languageId == 1 && x.isTrash == false);
            model = model.OrderByDescending(x => x.isSort);
            return PartialView(model.Take(15));

        }

        public ActionResult TuLieuVideo()
        {
            var model = _db.Contents.Where(x => x.contentKey == "Video" && x.languageId == 1 && x.isTrash == false);
            model = model.OrderByDescending(x => x.isSort);
            return PartialView(model.Take(5));
        }

        public ActionResult TuLieuVideoL(int? _pageIndex)
        {
            int _laguageId = 1;
            int _totalRecord = 0;
            _pageIndex = _pageIndex ?? 1;
            var entity = _db.Contents.Where(x => x.contentKey == "Video" && x.languageId == _laguageId && x.isTrash == false);
            entity = entity.OrderByDescending(x => x.createTime);
            _totalRecord = entity.Count();
            if (_pageIndex != null)
                entity = entity.Skip((_pageIndex.Value - 1) * 20);
            var totalPage = 0;
            totalPage = (int)Math.Ceiling(1.0 * _totalRecord / 20);
            entity = entity.Take(20);
            ViewBag.TotalRecord = _totalRecord.ToString();
            ViewBag.TotalPage = totalPage;
            ViewBag.PageIndex = _pageIndex ?? 1;
            return View(entity);
        }

        public ActionResult TuLieuHinhAnhL(int? _pageIndex)
        {
            int _laguageId = 1;
            int _totalRecord = 0;
            _pageIndex = _pageIndex ?? 1;
            var entity = _db.Contents.Where(x => x.contentKey == "Gallery" && x.languageId == _laguageId && x.isTrash == false);
            entity = entity.OrderByDescending(x => x.createTime);
            _totalRecord = entity.Count();
            if (_pageIndex != null)
                entity = entity.Skip((_pageIndex.Value - 1) * 10);
            var totalPage = 0;
            totalPage = (int)Math.Ceiling(1.0 * _totalRecord / 10);
            entity = entity.Take(10);
            ViewBag.TotalRecord = _totalRecord.ToString();
            ViewBag.TotalPage = totalPage;
            ViewBag.PageIndex = _pageIndex ?? 1;
            return View(entity);
        }

        public ActionResult Box1()
        {
            int Id = 0;

            int.TryParse(GetValueByKey("Box1"), out Id);
            var model = _db.Contents.Where(x => x.parentId == Id && x.contentKey == "TinTuc" && x.languageId == 1 && x.isTrash == false && x.approved == true);
            model = model.OrderByDescending(x => x.ngayDang);
            if (Id > 0)
            {
                var entity = GetById(Id);
                ViewBag.Url = entity.alias;
            }
            return PartialView(model.Take(5).OrderByDescending(x => x.createTime));
        }

        public ActionResult Box2()
        {
            int Id = 0;
            int.TryParse(GetValueByKey("Box2"), out Id);
            var model = _db.Contents.Where(x => x.parentId == Id && x.contentKey == "TinTuc" && x.languageId == 1 && x.isTrash == false && x.approved == true);
            model = model.OrderByDescending(x => x.ngayDang);
            if (Id > 0)
            {
                var entity = GetById(Id);
                ViewBag.Url = entity.alias;
            }
            return PartialView(model.Take(5).OrderByDescending(x => x.createTime));
        }

        public ActionResult Box3()
        {
            int Id = 0;
            int.TryParse(GetValueByKey("Box3"), out Id);
            var model = _db.Contents.Where(x => x.parentId == Id && x.contentKey == "TinTuc" && x.languageId == 1 && x.isTrash == false && x.approved == true);
            model = model.OrderByDescending(x => x.ngayDang);
            if (Id > 0)
            {
                var entity = GetById(Id);
                ViewBag.Url = entity.alias;
            }
            return PartialView(model.Take(5).OrderByDescending(x => x.createTime));
        }

        public ActionResult Box4()
        {
            int Id = 0;
            int.TryParse(GetValueByKey("Box4"), out Id);
            var model = _db.Contents.Where(x => x.parentId == Id && x.contentKey == "TinTuc" && x.languageId == 1 && x.isTrash == false && x.approved == true);
            model = model.OrderByDescending(x => x.ngayDang);
            if (Id > 0)
            {
                var entity = GetById(Id);
                ViewBag.Url = entity.alias;
            }
            return PartialView(model.Take(5).OrderByDescending(x => x.createTime));
        }

        public ActionResult Box5()
        {
            int Id = 0;
            int.TryParse(GetValueByKey("Box5"), out Id);
            var model = _db.Contents.Where(x => x.parentId == Id && x.contentKey == "TinTuc" && x.languageId == 1 && x.isTrash == false && x.approved == true);
            model = model.OrderByDescending(x => x.ngayDang);
            if (Id > 0)
            {
                var entity = GetById(Id);
                ViewBag.Url = entity.alias;
            }
            return PartialView(model.Take(5).OrderByDescending(x => x.createTime));
        }

        public ActionResult Box6()
        {
            int Id = 0;
            int.TryParse(GetValueByKey("Box6"), out Id);
            var model = _db.Contents.Where(x => x.parentId == Id && x.contentKey == "TinTuc" && x.languageId == 1 && x.isTrash == false && x.approved == true);
            model = model.OrderByDescending(x => x.ngayDang);
            if (Id > 0)
            {
                var entity = GetById(Id);
                ViewBag.Url = entity.alias;
            }
            return PartialView(model.Take(4).OrderByDescending(x => x.createTime));
        }

        public ActionResult Box7()
        {
            int Id = 0;
            int.TryParse(GetValueByKey("Box7"), out Id);
            var model = _db.Contents.Where(x => x.parentId == Id && x.contentKey == "TinTuc" && x.languageId == 1 && x.isTrash == false && x.approved == true);
            model = model.OrderByDescending(x => x.ngayDang);
            if (Id > 0)
            {
                var entity = GetById(Id);
                ViewBag.Url = entity.alias;
            }
            return PartialView(model.Take(5).OrderByDescending(x => x.createTime));
        }

        public ActionResult Box8()
        {
            int Id = 0;
            int.TryParse(GetValueByKey("Box8"), out Id);
            var model = _db.Contents.Where(x => x.parentId == Id && x.contentKey == "TinTuc" && x.languageId == 1 && x.isTrash == false && x.approved == true);
            model = model.OrderByDescending(x => x.ngayDang);
            if (Id > 0)
            {
                var entity = GetById(Id);
                ViewBag.Url = entity.alias;
            }
            return PartialView(model.Take(5).OrderByDescending(x => x.createTime));
        }

        public ActionResult ThongTinHoatDong()
        {
            return PartialView();
        }

        public ActionResult getChildDisplay(int Id, string _url, int? _pageIndex)
        {
            int _totalRecord = 0;
            _pageIndex = _pageIndex ?? 1;
            var entity = _db.Contents.Where(x => x.parentId == Id && x.languageId == 1 && x.isTrash == false && x.approved == true);
            entity = entity.OrderByDescending(x => x.ngayDang);
            _totalRecord = entity.Count();
            if (_pageIndex != null)
                entity = entity.Skip((_pageIndex.Value - 1) * 10);
            var totalPage = 0;
            totalPage = (int)Math.Ceiling(1.0 * _totalRecord / 10);
            entity = entity.Take(10);
            ViewBag.TotalRecord = _totalRecord.ToString();
            ViewBag.TotalPage = totalPage;
            ViewBag.PageIndex = _pageIndex ?? 1;
            ViewBag.CurentUrl = _url;
            return PartialView(entity.OrderByDescending(x => x.createTime));
        }

        public ActionResult getBreadcrumb(string pageUrl)
        {
            var entity = GetByAlias(pageUrl);
            if (entity != null && entity.parentId.HasValue)
            {
                var model = GetById(entity.parentId.Value);
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
                var model = GetById((int)Id.Value);
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
            var model = _db.Contents.Where(x => x.contentKey == "TinTuc" && x.languageId == 1 && x.isTrash == false && x.approved == true);
            model = model.OrderByDescending(x => x.isView);
            return PartialView(model.Take(8).OrderByDescending(x => x.ngayDang));
        }

        public ActionResult TuTuongDaoDuc()
        {
            int Id = 0;
            int.TryParse(GetValueByKey("Box1"), out Id);
            var model = _db.Contents.Where(x => x.contentKey == "TinTuc" && x.parentId == Id && x.languageId == 1 && x.isTrash == false && x.approved == true);
            model = model.OrderByDescending(x => x.ngayDang);
            var entity = GetById(Id);
            if (entity != null)
                ViewBag.Url = entity.alias;
            return PartialView(model.Take(8).OrderByDescending(x => x.createTime));
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
            var model = _db.Contents.Where(x => x.contentKey == "TinTuc" && x.languageId == 1 && x.isTrash == false && x.approved == true && x.isFeature == true);
            model = model.OrderByDescending(x => x.ngayDang);
            return PartialView(model.Take(6).OrderByDescending(x => x.createTime));
        }

        public string MainMenu2()
        {
            int _languageId = 1;
            int Id = 0;
            if (_languageId == 1)
                int.TryParse(GetValueByKey("DanhMucChinh"), out Id);
            List<Menu> eMenus = _db.Menus.Where(x => x.menuParentId == Id && x.isTrash == false).OrderBy(x => x.isSort).ToList();
            string _html = "";
            _html += "<ul class=\"main-nav nav navbar-nav\">";
            _html += "<li>";
            _html += "<a class=\"home-icon\" href=\"/\"><i class=\"fa fa-home\"></i>";
            _html += "</a>";
            _html += "</li>";
            foreach (var item in eMenus)
            {
                _html += "<li>";
                _html += "<a href=\"" + item.menuLink + "\">";
                _html += item.menuName;
                _html += "</a>";
                _html += SubMenu3("sub-menu nav", item.menuId);
                _html += "</li>";
            }
            _html += "</ul>";
            return _html;
        }

        public string SubMenu(string _css, string _html, string _link)
        {
            var entity = GetByAlias(_link);
            if (entity != null)
            {
                var model = _db.Contents.Where(x => x.parentId == entity.contentId && x.contentKey == entity.contentKey && x.languageId == 1 && x.isTrash == false && x.approved == true);
                var List = model.OrderBy(x => x.isSort);
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
            var model = GetByParent(_id, _key);
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
                var model = _db.Menus.Where(x => x.menuParentId == _id && x.isTrash == false);
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
            var entity = _db.Contents.Where(x => x.contentKey == "TinTuc" && x.languageId == 1 && x.isTrash == false && x.approved == true);
            entity = entity.OrderByDescending(x => x.ngayDang);
            return PartialView(entity.OrderByDescending(x => x.createTime).Take(10));
        }

        public ActionResult LienKetWeb()
        {
            var model = _db.LienKetWebs.OrderBy(x => x.isSort);
            return PartialView(model);
        }

        public string getOption(int Id)
        {
            var model = _db.Options.Where(x => x.contentId == Id).OrderBy(x => x.isSort);
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
            var model = _db.Options.Where(x => x.contentId == Id).OrderBy(x => x.isSort);
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