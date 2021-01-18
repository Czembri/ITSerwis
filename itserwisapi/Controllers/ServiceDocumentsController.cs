using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ItSerwisAPI.Controllers
{
    public class ServiceDocumentsController : ApiController
    { 

        [HttpGet]
        public object SelectByID()
        {
            DbConnection dbConn = new DbConnection();
            var docs = dbConn.GetDocuments();
            return Classes.Utl.SerializeToXml(docs);

        }
    }
}
