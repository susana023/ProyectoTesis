using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.DAL
{
    public static class GlobalVariables
    {
        // readonly variable
        public static double Igv
        {
            get
            {
                return 0.18;
            }
        }

        // read-write variable
        public static string CurrentUserID
        {
            get
            {
                return HttpContext.Current.Application["CurrentUserID"] as string;
            }
            set
            {
                HttpContext.Current.Application["CurrentUserID"] = value;
            }
        }
        public static string CurrentUser
        {
            get
            {
                if (HttpContext.Current.Application["CurrentUser"] == null) return "";
                else return HttpContext.Current.Application["CurrentUser"] as string;
            }
            set
            {
                HttpContext.Current.Application["CurrentUser"] = value;
            }
        }
    }
}