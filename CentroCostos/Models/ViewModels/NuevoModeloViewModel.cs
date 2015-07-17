using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CentroCostos.Models.ViewModels
{
    public class NuevoModeloViewModel
    {
        public int IdLinea { get; set; }

        public int id { get; set; }
        [Required(ErrorMessage="El código es requerido")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El campo horma es requerida")]
        public string Horma { get; set; }
        [Required(ErrorMessage = "El campo planta es requerido")]
        public string Planta { get; set; }
        [Required(ErrorMessage = "El código es requerido")]
        public string Tipo_Suela { get; set; }
        [Required(ErrorMessage = "El campo numeracion es requerido")]
        public Numeracion Numeracion { get; set; }
        public HttpPostedFileBase Imagen { get; set; }
        [Required(ErrorMessage = "El campo pieza es requerido")]
        public int Pieza { get; set; }
        [Required(ErrorMessage = "El campo color es requerido")]
        public string Color { get; set; }
    }
}