using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentroCostos.Models.ViewModels
{
    public class LineaIndexViewModel
    {
        public Modelo Modelo { get; set; }

        public int? LineaID { get; set; }
        public int? ModeloID { get; set; }

        public IList<SelectListItem> Lineas { get; set; }
        public IList<SelectListItem> ModelosLinea { get; set; }
    }
}