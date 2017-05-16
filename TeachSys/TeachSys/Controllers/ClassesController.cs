using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeachSys.Controllers
{
    public class ClassesController : Controller
    {
        Models.TeachDBMEntities tdb = App_Start.Helper.tdb;
        //
        // GET: /Classes/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetClassesList()
        {
            var classes = from c in tdb.Classes
                          select new
                          {
                              ID = c.ID,
                              Name = c.Name
                          };
            return Json(classes,JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetClassesListByMajorId(int majorId)
        {
            var classes = from c in tdb.Classes
                          where c.MajorID == majorId
                          select new
                          {
                              ID = c.ID,
                              Name = c.Name
                          };
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取带班主任信息的班级列表
        /// </summary>
        /// <param name="majorId">专业的id，如果为-1表示所有专业</param>
        /// <returns></returns>
        public ActionResult GetClassesListByMajorIdWithDirector(int majorId)
        {
            var classes = from c in tdb.View_TeacherClasses
                          select new
                          {
                              ID = c.ID,
                              Name = c.Name,
                              MajorId = c.MajorID,
                              TeacherName = c.TeacherName,
                              TeacherNo = c.TeacherNo
                          };
            if(majorId != -1)
            {
                classes = classes.Where(t => t.MajorId == majorId);
            }
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddClass(Models.View_TeacherClasses classteacher)
        {
            try
            {
                tdb.AddClasses(classteacher.MajorID, classteacher.Name, classteacher.TeacherID);
                return Content("ok");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var c = tdb.View_TeacherClasses.Single(t => t.ID == id);
            return View(c);
        }
        public ActionResult EditClass(Models.View_TeacherClasses tc)
        {
            try
            {
                var c = tdb.Classes.Single(t => t.ID == tc.ID);
                var ttcc = tdb.TeacherClasses.Where(t => t.ClassID == tc.ID);
                if (ttcc.Count() == 0)
                {
                    tdb.TeacherClasses.Add(new Models.TeacherClasses() { TeacherID = tc.TeacherID, ClassID = tc.ID, Status = 0 });
                }
                else
                {
                    var teaclass = ttcc.ToList()[0];
                    teaclass.TeacherID = tc.TeacherID;
                    teaclass.Status = 0;
                    teaclass.ClassID = tc.ID;
                }
                c.Name = tc.Name;

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
                tdb.TeacherClasses.Remove(tdb.TeacherClasses.Single(t => t.ClassID == id));
                tdb.Classes.Remove(tdb.Classes.Single(t => t.ID == id));
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
