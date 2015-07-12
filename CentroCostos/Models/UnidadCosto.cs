using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentroCostos.Models
{
    // Aqui se almacena un costo, por ahora lo imagino almacenando costos indirectos
    // ya que los directos pueden calcularse directamente con las ordenes de producción
    [Table("UnidadCosto")]
    public class UnidadCosto
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }
        public decimal Valor_Costo { get; set; }

        public virtual Costo Costo { get; set; }
    }
}