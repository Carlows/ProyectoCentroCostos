using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Models
{
    [Table("FichaTecnica")]
    public class FichaTecnica
    {
        public FichaTecnica()
        {
            MaterialesDepartamento = new List<MaterialesDepartamentoProduccion>();
        }

        public int Id { get; set; }

        public virtual IList<MaterialesDepartamentoProduccion> MaterialesDepartamento { get; set; }
    }
}
