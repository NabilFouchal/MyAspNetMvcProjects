using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStoreApp.Utils.ExtensionMethods
{
    public static class ControllerExtensions
    {
        public static string RemoveControllerSuffix<T>() where T : Controller
        {
            string controllerName = typeof(T).Name;
            if(controllerName.EndsWith("Controller"))
            {
                controllerName = controllerName.Replace("Controller", "");
            }
            return controllerName;
        }
    }
}