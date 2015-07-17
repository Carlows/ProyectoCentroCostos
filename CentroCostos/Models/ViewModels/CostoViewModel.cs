using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CentroCostos.Models.ViewModels
{
    public class CostoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Es necesaria una descripcion")]
        public string Descripcion { get; set; }
        public string Comentario { get; set; }

        public string Departamento { get; set; }

        public bool esCostoDirecto { get; set; }
    }
}