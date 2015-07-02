using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCostos.Models
{
    // Representa un departamento en la empresa 
    // (de produccion como corte, montura, etc. o de ventas o administracion)
    public class Departamento
    {
        public int Id { get; set; }

        public string Nombre_Departamento { get; set; }

        public virtual IEnumerable<Costo> Costos_Departamento { get; set; }
    }
}