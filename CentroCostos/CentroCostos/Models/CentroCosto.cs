using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentroCostos.Models
{
    // Representa un Centro de Costo
    // tendrá una relacion con un departamento
    [Table("CentroCosto")]
    public class CentroCosto
    {
        public int Id { get; set; }

        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        
        [Required]
        public virtual Departamento Departamento_CentroCosto { get; set; }
    }
}