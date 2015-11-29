using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Infrastructure.Repositorios
{
    public interface IMaterialRepository : IRepository<Material, int>
    {
        Material Find(string material);
        void CreateMultipleMateriales(IEnumerable<DataRow> data);
        IList<Material> FindMateriales(string query);
    }
}
