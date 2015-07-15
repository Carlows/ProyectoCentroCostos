using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CentroCostos.Models.ViewModels
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Es necesario especificar el nombre de la categoria")]
        public string Categoria { get; set; }
    }
}