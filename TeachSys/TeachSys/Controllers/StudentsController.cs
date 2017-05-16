using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeachSys.Controllers
{
    public class StudentsController : Controller
    {
        Models.TeachDBMEntities tdb = App_Start.Helper.tdb;
        //
        // GET: /Students/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetStudents(int classid)
        {
            var sts = tdb.Students.Where(t => t.ClassID == classid);
            return Json(sts,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult AddStudent(Models.Students stu)
        {
            try
            {
                stu.Password = "123";
                stu.Stauts = 0;
                stu.IsLogin = 1;
                tdb.Students.Add(stu);
                tdb.SaveChanges();
                return Content("ok");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult Edit(int id)
        {
            var stu = tdb.Students.First(t => t.ID == id);
            return View(stu);
        }
        public ActionResult EditStudent(Models.Students stu)
        {
            try
            {
                var stu1 = tdb.Students.First(t => t.ID == stu.ID);
                stu1.Name = stu.Name;
                stu1.StudentNo = stu.StudentNo;
                stu1.TelNum = stu.TelNum;
                stu1.QQ = stu.QQ;
                stu1.WeChat = stu.WeChat;
                stu1.PTelNo1 = stu.PTelNo1;
                stu1.PTelNo2 = stu.PTelNo2;
                stu1.Memo = stu.Memo;
                tdb.SaveChanges();
                return Content("ok");
            }
            catch
            {
                return Content("err");
            }

        }
        public ActionResult Delete(int id)
        {
            try
            {
                var st = tdb.Students.First(t => t.ID == id);
                tdb.Students.Remove(st);
                tdb.SaveChanges();
                return Content("ok");
            }
            catch
            {
                return Content("err");
            }
        }
    }
}
