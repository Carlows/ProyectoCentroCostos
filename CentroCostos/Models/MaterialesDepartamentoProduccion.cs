using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Models
{
    [Table("MaterialesDepartamentoProduccion")]
    public class MaterialesDepartamentoProduccion
    {
        public MaterialesDepartamentoProduccion()
        {
            Materiales = new List<CostoMaterial>();
        }

        public int Id { get; set; }

        [Required]
        public virtual DepartamentoProduccion Departamento { get; set; }
        public virtual IList<CostoMaterial> Materiales { get; set; }
    }
}
