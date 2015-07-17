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

        // Pertenece este departamento al proceso productivo?
        // Si sí pertenece, este departamento aparecerá en las fichas técnicas
        public bool esDeProduccion { get; set; }

        public virtual IList<Costo> Costos_Departamento { get; set; }
    }
}