using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Web.Services;
using Anlis.Core;

namespace Anlis.Web.Controllers
{
    public class DemoController : Controller
    {
        public ActionResult Index()
        {
            var statement = RandomPhraseService.GetRandomPhraseFromResource();
            return View("Index", statement as object);//throw new System.NotImplementedException();
        }        
    }
}