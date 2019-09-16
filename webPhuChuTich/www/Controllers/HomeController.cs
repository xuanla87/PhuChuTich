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
            return PartialView();
        }
        public ActionResult Box2()
        {
            return PartialView();
        }

        public ActionResult Box3()
        {
            return PartialView();
        }
        public ActionResult Box4()
        {
            return PartialView();
        }
        public ActionResult Box5()
        {
            return PartialView();
        }
        public ActionResult Box6()
        {
            return PartialView();
        }
        public ActionResult Box7()
        {
            return PartialView();
        }
        public ActionResult Box8()
        {
            return PartialView();
        }
        public ActionResult ThongTinHoatDong()
        {
            return PartialView();
        }
    }
}