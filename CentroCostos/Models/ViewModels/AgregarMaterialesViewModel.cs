using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Models.ViewModels
{
    public class AgregarMaterialesViewModel
    {
        public IList<CostoMaterial> materialesAgregados { get; set; }
        public IList<Material> listaMateriales { get; set; }
    }
}
