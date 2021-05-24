using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WearAndGoS2_WPF.Models
{
    class UserCartContent
    {
        public static JArray CartContent = new JArray();
        public static JObject MainUser = new JObject();
        public static JArray ClientList = new JArray();
    }
}
