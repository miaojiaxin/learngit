using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeachSys.Content
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request["type"];
            if (type == "图书")
            {
                string str = "[{'no':1,'name':'C#高级编程'},{'no':2,'name':'asp.net mvc教程'}]";
                str = str.Replace("'", "\"");
                context.Response.Write(str);
            }
            else
            {
                context.Response.Write("");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}