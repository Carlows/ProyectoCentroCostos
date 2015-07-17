using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentroCostos.Models
{
    // Representa un departamento en la empresa 
    // (de produccion como corte, montura, etc. o de ventas o administracion)
    [Table("Departamento")]
    public class Departamento
    {
        public int Id { get; set; }

        public string Nombre_Departamento { get; set; }

        public virtual IList<Costo> Costos_Departamento { get; set; }
    }
}