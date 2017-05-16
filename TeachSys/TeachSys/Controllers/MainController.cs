using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TeachSys.Controllers
{
    public class MainController : Controller
    {
        Models.TeachDBMEntities tdb = new Models.TeachDBMEntities();
        //
        // GET: /Main/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(string username,string password,string role)
        {

            if (role == "t") //教师
            {
                try
                {
                    var teacher = tdb.Teachers.Single(t => t.TeacherNo == username && t.Password == password);
                    //admin/123
                    if (teacher != null)
                    {
                        //FormsAuthentication.SetAuthCookie(teacher.Departments + ";;" + teacher.DeptID, false);
                        
                        FormsAuthenticationTicket t = new FormsAuthenticationTicket(1,teacher.Name, DateTime.Now, DateTime.Now.AddMinutes(30), false, teacher.DeptID + ";" + teacher.ID);
                        var ticket = FormsAuthentication.Encrypt(t);
                        HttpCookie c = new HttpCookie(FormsAuthentication.FormsCookieName,ticket);
                        HttpContext.Response.Cookies.Add(c);
                        return Content("ok");
                    }
                        
                    else
                        return Content("err");
                }
                catch
                {
                    return Content("err");
                }
            }
            else if (role == "s") //学生
            {
                //TODO:学生登录未完成
                return Content("err");
            }
            else
                return Content("err");
        }
        public ActionResult Main()
        {
            string t = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value; //取到的是加密数据  -- ticket
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(t);
            ViewBag.teacherName = ticket.Name;
            return View();
        }
       
        
    }
   
}
