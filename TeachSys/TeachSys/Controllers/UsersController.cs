using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeachSys.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        ///  返回所有用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUsers()
        {
            List<book> bs = new List<book>();
            bs.Add(new book { no = 1, name = "abc阿斯顿发斯蒂芬" });
            bs.Add(new book { no = 2, name = "sdfsdfsf" });
            return Json(bs, JsonRequestBehavior.AllowGet);//Json方法会把传入的对象封装成JsonResult对象，return JsonResult对象，会进一步将JsonResult对象转换成Json串。
        }
    }
    class book
    {
        public int no { get; set; }
        public string name { get; set; }
    }
}
