using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCostos.Models.ViewModels
{
    public class ModelosLineaViewModel
    {
        public Linea Linea { get; set; }
        public IList<Modelo> Modelos { get; set; }
    }
}