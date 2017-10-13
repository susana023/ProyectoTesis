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

        public static string Correlative
        {
            get
            {
                int correlative = 0;
                if (HttpContext.Current.Application["Correlative"] != null)
                {                    
                    int.TryParse(HttpContext.Current.Application["Correlative"] as string, out correlative);  
                }
                Correlative = (correlative + 1).ToString();
                return (correlative + 1).ToString();
            }
            set
            {
                HttpContext.Current.Application["Correlative"] = value;
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

        public static string SerialNumber
        {
            get
            {
                if (HttpContext.Current.Application["SerialNumber"] == null) return "01";
                else return HttpContext.Current.Application["SerialNumber"] as string;
            }
            set
            {
                HttpContext.Current.Application["SerialNumber"] = value;
            }
        }
    }
}