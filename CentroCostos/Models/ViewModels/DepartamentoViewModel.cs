using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CentroCostos.Models.ViewModels
{
    public class DepartamentoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Es necesario darle un nombre al departamento")]
        public string Nombre_Departamento { get; set; }

        public bool esDeProduccion { get; set; }
    }
}