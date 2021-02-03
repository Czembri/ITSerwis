using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItSerwisAPI.Controllers
{
    public class DocumentsController : Controller
    {
        // GET: Documents
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string SelectByID(string ID)
        {
            if (!String.IsNullOrEmpty(ID))
                //TODO: Save the data in database  
                return $"Document with ['ID':{ID}] generated. ";
            else
                return "Please complete the form.";
        }
    }
}