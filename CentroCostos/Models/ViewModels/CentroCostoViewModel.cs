using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentroCostos.Models.ViewModels
{
    public class CentroCostoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo codigo es requerido")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El campo descripcion es requerido")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Es necesario elegir una categoria")]
        public int DepartamentoId { get; set; }
        public IEnumerable<SelectListItem> Departamentos { get; set; }
    }
}