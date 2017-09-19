using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMusicStoreApp.Models
{
    public class DraftClass
    {
        public static bool ClientValidationEnabled { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class MyAttribute : Attribute
    {
        public string MandatoryProperty { get;}
        public string OptionalProperty { get; set; }

        public MyAttribute(string mandatoryParam)
        {
            MandatoryProperty = mandatoryParam;
        }
    }

}