using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeachSys.Controllers
{
    public class MajorsController : Controller
    {
        Models.TeachDBMEntities tdb = new Models.TeachDBMEntities();
        //
        // GET: /Majars/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetMajors()
        {
            var depid = App_Start.Helper.GetDepartmentID(HttpContext);
            //1、LinQ的关联语句
            //2、在数据库中建视图
            var marjors = from m in tdb.Majors
                          join d in tdb.Departments on m.DepartmentID equals d.ID
                          where m.DepartmentID == depid
                          select new { 
                            ID=m.ID,
                            departmentName=d.Name,
                            Name=m.Name
                          };
            return Json(marjors);
        }
        public ActionResult GetMajorsByDepartmentId(int deptId)
        {
            //1、LinQ的关联语句
            //2、在数据库中建视图
            var marjors = (from m in tdb.Majors
                          where m.DepartmentID == deptId
                          select new
                          {
                              ID = m.ID,
                              Name = m.Name
                          }).ToList();
            marjors.Insert(0,new { ID = -1, Name = "----全部----" });
            return Json(marjors,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add()
        {
            ViewBag.DepartmentID = App_Start.Helper.GetDepartmentID(HttpContext);
            return View();
        }
        public ActionResult AddMajor(Models.Majors major)
        {
            try
            {
                tdb.Majors.Add(major);
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
            var major = tdb.Majors.Single(t => t.ID == id);
            return View(major);
        }
        public ActionResult EditMajor(Models.Majors m)
        {
            try
            {
                var mm = tdb.Majors.Single(t => t.ID == m.ID);
                mm.Name = m.Name;
                
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
                var m = tdb.Majors.Single(t => t.ID == id);
                tdb.Majors.Remove(m);
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
