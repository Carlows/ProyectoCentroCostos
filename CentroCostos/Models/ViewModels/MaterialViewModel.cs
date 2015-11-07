using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentroCostos.Models.ViewModels
{
    public class MaterialViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage="El campo codigo es requerido")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El campo descripcion es requerido")]
        public string Descripcion_Material { get; set; }
        [Required(ErrorMessage = "El campo unidad de medida es requerido")]
        public string Unidad_Medida { get; set; }
        [Required(ErrorMessage = "El campo costo unitario es requerido")]
        public decimal Costo_Unitario { get; set; }
        [Required(ErrorMessage = "El campo consumo por par es requerido")]
        public decimal Consumo_Par { get; set; }

        public bool esMaterialDirecto { get; set; }

        [Required(ErrorMessage="Es necesario elegir una categoria")]
        public int CategoriaId { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set; }

        public int DepartamentoId { get; set; }
        public IEnumerable<SelectListItem> Departamentos { get; set; }
    }
}