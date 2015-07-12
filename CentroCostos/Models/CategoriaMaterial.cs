using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCostos.Models
{
    // Pieles, telas, tejidos, etc.
    public class CategoriaMaterial
    {
        public int Id { get; set; }

        public string Categoria { get; set; }

        public virtual IList<Material> Materiales_Categoria { get; set; }
    }
}