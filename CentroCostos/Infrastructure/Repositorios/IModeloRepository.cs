using CentroCostos.Models;
using CentroCostos.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CentroCostos.Infrastructure.Repositorios
{
    public interface IModeloRepository : IRepository<Modelo, int>
    {
        Modelo Find(string codigo);
        string UploadImageWithUniqueId(string codigo, HttpPostedFileBase imagen, string serverPath);
        void CreateSingleModelo(NuevoModeloViewModel model, string serverPath);
        void CreateMultipleModelos(IEnumerable<DataRow> rows);
        void CreateMultipleModelos(IEnumerable<DataRow> rows, int lineaId);
    }
}
