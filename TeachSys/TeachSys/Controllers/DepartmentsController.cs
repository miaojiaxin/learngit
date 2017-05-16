using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeachSys.Controllers
{
    public class DepartmentsController : Controller
    {
        //.net   Entity framework  -- 操作数据库，它是对ADO.NET的一个高级封装
        Models.TeachDBMEntities tdb = new Models.TeachDBMEntities();

        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult EditIndex(int id)
        {
            //在控制器中获取到的name，如何送到view
            //string name = tdb.Departments.First(t => t.ID == id).Name;
            //ViewBag.DepartmentName = name;  //ViewBag对象是mvc里的内置对象，可以灵活的添加动态属性
            //return View();

            //使用model传递数据：把数据库中得到的对象，完成的传递给视图
            Models.Departments dep = tdb.Departments.First(t => t.ID == id);
            return View(dep);
        }
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDepartments()
        {
            var deps = from dep in tdb.Departments
                       select new { ID = dep.ID, Name = dep.Name, Status = dep.Status };//new创建了一个匿名对象
            return Json(deps, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据部门Id来删除部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteDepartment(int id)
        {
            try
            {
                var dep = tdb.Departments.First(t => t.ID == id);  //找到要删除的实体（记录）
                tdb.Departments.Remove(dep);//把实体（数据库中的记录）标记为deleted删除状态
                tdb.SaveChanges();//所有被标记的状态将被更行到数据库中
                return Content("ok");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult GetDepartmentById(int id)   {
            try{
                var dep1 = from dep in tdb.Departments
                          where dep.ID == id
                           select new { ID = dep.ID, Name = dep.Name, Status = dep.Status };
                
                return Json(dep1,JsonRequestBehavior.AllowGet);
            }
            catch{ return Content("[]");  }
        }
        public ActionResult EditDepartment(int id, string name, int status)   {
            try{
                var dep = tdb.Departments.First(t => t.ID == id);  //找到要修改的实体（记录）
                dep.Name = name;
                dep.Status = status;
                tdb.SaveChanges();
                return Content("ok");
            }
            catch{   return Content("err");   }
        }
        public ActionResult AddDepartment(string name, int status)  {
            try{
                var dep = new Models.Departments();
                dep.Name = name;
                dep.Status = status;
                tdb.Departments.Add(dep);
                tdb.SaveChanges();//所有被标记的状态将被更行到数据库中
                return Content("ok");
            }
            catch {     return Content("err");   }
        }
    }
  
}
