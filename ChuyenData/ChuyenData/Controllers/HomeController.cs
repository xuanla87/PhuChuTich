using ChuyenData.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChuyenData.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [ValidateInput(false)]
        public ActionResult ChuyenTinTuc()
        {
            ModelV1 _db1 = new ModelV1();
            ModelV2 _db2 = new ModelV2();
            var post = _db1.Posts.Where(x => x.PostTypeId == 0);
            foreach (var item in post)
            {
                var model = new Content();
                model.alias = "";
                model.allowComment = false;
                model.approved = true;
                model.approvedTime = item.ApprovedDate;
                model.approvedUser = "admin";
                model.authorName = "admin";
                model.contentKey = "TinTuc";
                model.contentMain = item.Content;
                model.createTime = item.CreatedDate;
                model.createUser = "admin";
                model.description = "";
                model.isFeature = false;
                model.isHome = false;
                model.isNew = false;
                model.isSort = 0;
                model.isTrash = false;
                model.isView = item.Views;
                model.languageId = 1;
                model.metaDescription = "";
                model.metaKeyword = "";
                model.metaTitle = "";
                model.modifiedTime = item.ModifiedDate;
                model.modifiedUser = "admin";
                model.name = item.Title;
                model.ngayDang = item.CreatedDate;
                model.oldId = (int)item.ID;
                model.parentId = 0;
                model.thumbnail = item.Thumbnail;
                _db2.Contents.Add(model);
                _db2.SaveChanges();
            }
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ChuyenChuyenMuc()
        {
            ModelV2 _db2 = new ModelV2();
            var model = _db2.Contents.ToList();
            foreach (var item in model)
            {
                item.alias = RemoveSign4VietnameseString(item.name).ToLower().Replace("--","-") + "-" + item.contentId;
                _db2.Contents.Attach(item);
                _db2.Entry(item).State = EntityState.Modified;
                _db2.SaveChanges();
            }
            return View();
        }

        private string chuyen(string _input)
        {
            return _input;
        }

        private static readonly string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY-",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ",
            " /?(){}[]:;''|<>,.*&^%$#@!~`+=\"\"\\–“”"
        };



        public static string RemoveSign4VietnameseString(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                {
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
                }
            }
            return str;
        }
    }
}