using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;

namespace ItSerwisAPI.Controllers

{
    public class ItemsController : ApiController
    {


        [HttpGet]
        public object SelectByID()
        {
            return "Items";

        }

    }
}
