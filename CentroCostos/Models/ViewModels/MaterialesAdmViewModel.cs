using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCostos.Models.ViewModels
{
    public class MaterialesAdmViewModel
    {
        public IPagedList<Material> Materiales { get; set; }
        public IList<CategoriaMaterial> Categorias { get; set; }
    }
}