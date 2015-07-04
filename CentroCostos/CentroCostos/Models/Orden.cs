using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentroCostos.Models
{
    // Representara una orden de produccion que sera ingresada al sistema
    [Table("Orden")]
    public class Orden
    {
        public int Id { get; set; }

        public int Cantidad_Total { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Modelo Modelo { get; set; }
    }
}