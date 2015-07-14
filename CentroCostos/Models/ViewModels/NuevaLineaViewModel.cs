using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CentroCostos.Models.ViewModels
{
    public class NuevaLineaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Este campo es requerido")]
        public string Codigo { get; set; }
    }
}