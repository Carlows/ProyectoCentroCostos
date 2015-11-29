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

        public Material Find(string material)
        {
            return DbContext.Materiales.Where(l => l.Codigo.Equals(material)).Single();
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
                        Unidad_Medida = row["Unidad"].ToString(),
                        Costo_Unitario = decimal.Parse(row["Costo"].ToString()),
                        esMaterialDirecto = true,
                        Categoria_Material = categoria
                    };

                materiales.Add(material);
            }

            this.CreateMultiple(materiales);
        }
    }
}