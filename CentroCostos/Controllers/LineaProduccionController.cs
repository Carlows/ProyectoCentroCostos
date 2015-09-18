using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentroCostos.Controllers
{
    // Este controlador va a llevar el control de las lineas, los modelos y los materiales
    [Authorize]
    public class LineaProduccionController : Controller
    {
        // GET: LineaProduccion
        public ActionResult Index()
        {
            return View();
        }
    }
}