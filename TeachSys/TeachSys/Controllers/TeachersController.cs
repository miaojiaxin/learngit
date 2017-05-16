using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TeachSys.Controllers
{
    public class TeachersController : Controller
    {
        Models.TeachDBMEntities tdb = new Models.TeachDBMEntities();
        //
        // GET: /Teachers/

        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult GetTeachers() {
        //    var teachers = from t in tdb.Teachers
        //                  join d in tdb.Departments on t.DeptID equals d.ID
        //                  select new
        //                  {
        //                      ID = t.ID,
        //                      departmentName = d.Name,
        //                      Name = t.Name,
        //                      TeacherNo=t.TeacherNo,
        //                      IsLogin = t.IsLogin
        //                  };
        //    return Json(teachers);
        //}

        /// <summary>
        /// 根据用户传来的页号（1开始），和每页的行数，来获取对应页的记录
        /// 返回给客户端的数据有一个格式：{total:num,rows:[{},{}]}
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetTeachers(int page,int rows)  //2,10
        {
            var depid = App_Start.Helper.GetDepartmentID(HttpContext);
            int nums = tdb.Teachers.Count(t=>t.DeptID == depid); //总记录数量

            var teachers = (from t in tdb.Teachers
                            join d in tdb.Departments on t.DeptID equals d.ID
                            where t.DeptID == depid
                            orderby t.ID descending
                            select new
                            {
                                ID = t.ID,
                                departmentName = d.Name,
                                Name = t.Name,
                                TeacherNo = t.TeacherNo,
                                IsLogin = t.IsLogin
                            }
                             ).Skip((page-1) * rows).Take(rows);
                          ;
            var obj = new {total=nums,rows=teachers};

            return Json(obj,JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTeachersList()
        {
            FormsAuthenticationTicket ti = TeachSys.App_Start.Helper.GetTicket(HttpContext);
            int deptid = App_Start.Helper.GetDepartmentID(HttpContext);
            var teachers = from t in tdb.Teachers
                            where t.DeptID == deptid
                            select new
                            {
                                ID = t.ID,
                                Name = t.Name
                            };
            ;

            return Json(teachers, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add()
        {
            int depid = App_Start.Helper.GetDepartmentID(HttpContext);
            ViewBag.DepartmentID = depid;
            return View();
        }
        public ActionResult AddTeacher(Models.Teachers teacher)
        {
            try
            {
                //TODO:默认密码，后续需要从配置文件中读取
                teacher.Password = "123";
                tdb.Teachers.Add(teacher);
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
            var teacher = tdb.Teachers.Single(t => t.ID == id);
            
            return View(teacher);
        }
        public ActionResult EditTeacher(TeachSys.Models.Teachers teacher)
        {
            try
            {
                var t = tdb.Teachers.Single(tt => tt.ID == teacher.ID);
                t.Name = teacher.Name;
                t.TeacherNo = teacher.TeacherNo;
                t.IsLogin = teacher.IsLogin;
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

                var te = tdb.Teachers.Single(t => t.ID == id);
                tdb.Teachers.Remove(te);
                tdb.SaveChanges();
                return Content("ok");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult ResetPass(int id)
        {
            try
            {
                var teacher = tdb.Teachers.Single(t => t.ID == id);
                teacher.Password = "123";
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

