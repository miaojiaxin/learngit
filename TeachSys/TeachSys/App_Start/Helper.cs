using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Security;
namespace TeachSys.App_Start
{
    public class Helper
    {
        public static Models.TeachDBMEntities tdb = new Models.TeachDBMEntities();
        public static FormsAuthenticationTicket GetTicket(HttpContextBase context)
        {
            var t = context.Request.Cookies[FormsAuthentication.FormsCookieName].Value;
            var ticket = FormsAuthentication.Decrypt(t);
            return ticket;
        }
        /// <summary>
        /// 获取当前登录用户的部门ID
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int GetDepartmentID(HttpContextBase context)
        {

            var ticket = GetTicket(context);
            string[] userdata = ticket.UserData.Split(';');
            return int.Parse(userdata[0]);
        }
        /// <summary>
        /// 获取当前登录用户的ID
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int GetTeacherID(HttpContextBase context)
        {
            var ticket = GetTicket(context);
            string[] userdata = ticket.UserData.Split(';');
            return int.Parse(userdata[1]);
        }
    }
}