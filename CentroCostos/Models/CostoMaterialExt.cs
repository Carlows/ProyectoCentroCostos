using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Models
{
    public partial class CostoMaterial
    {
        public decimal Costo_Total
        {
            get
            {
                return this.Consumo_Par * this.Material.Costo_Unitario;
            }
        }
    }
}
