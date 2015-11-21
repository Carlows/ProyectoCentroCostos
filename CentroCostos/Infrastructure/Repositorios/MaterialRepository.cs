using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CentroCostos.Infrastructure.Repositorios
{
    public class MaterialRepository : BaseRepository<Material, int>, IMaterialRepository
    {
        public MaterialRepository(ApplicationContext dbContext)
            : base(dbContext)
        {
        }

        public IList<Material> FindMateriales(string query)
        {
            if (String.IsNullOrEmpty(query))
                return DbContext.Materiales.ToList();
            else
                return DbContext.Materiales.Where(m => m.Codigo.Contains(query)).ToList();
        }

        public void CreateMultipleMateriales(IEnumerable<DataRow> data)
        {
            IList<Material> materiales = new List<Material>();

            foreach (DataRow row in data)
            {
                string nombreCategoria = row["Categoria"].ToString();

                var categoria = DbContext.Categorias.Where(c => c.NombreCategoria == nombreCategoria).SingleOrDefault();

                if (categoria == null)
                {
                    throw new ArgumentException(String.Format("La categoria {0} no existe", nombreCategoria));
                    return;
                }

                var material = new Material()
                    {
                        Codigo = row["Codigo"].ToString(),
                        Descripcion_Material = row["Descripcion"].ToString(),
                        // Especificar medida en la documentación (puede causar confusión)
                        Unidad_Medida = row["Medida"].ToString(),
                        Costo_Unitario = decimal.Parse(row["Costo"].ToString()),
                        esMaterialDirecto = esMaterialDirecto(row),
                        Categoria_Material = categoria
                    };

                materiales.Add(material);
            }

            this.CreateMultiple(materiales);
        }

        private bool esMaterialDirecto(DataRow row)
        {
            if (row["Directo"].ToString() == "directo")
                return true;
            else if (row["Directo"].ToString() == "indirecto")
                return false;
            else
                throw new ArgumentException("El material debe ser 'directo' o 'indirecto'");
        }
    }
}