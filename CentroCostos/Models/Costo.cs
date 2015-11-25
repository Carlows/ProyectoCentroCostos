using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentroCostos.Models
{
    // Representara un costo directo o indirecto
    // los directos serían por ejemplo de materia prima, por lo tanto tendria que asignarsele un material a este costo
    // los indirectos serian como reparacion, depreciacion de la maquinaria, etc.
    [Table("Costo")]
    public class Costo
    {
        public int Id { get; set; }

        public bool esCostoDirecto { get; set; }
        public string Descripcion { get; set; }
        public string Comentario { get; set; }
        
        public virtual DepartamentoProduccion Departamento { get; set; }
        // Solo se asignará si este costo es directo y por materia prima
        public virtual Material Material_Directo { get; set; }
    }
}