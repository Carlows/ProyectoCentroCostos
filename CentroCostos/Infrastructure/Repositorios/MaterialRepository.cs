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

        public IOrderedQueryable<Material> FindMateriales()
        {
            return DbContext.Materiales;
        }

        public void CreateMultipleMateriales(IEnumerable<DataRow> data)
        {
            IList<Material> materiales = new List<Material>();

            foreach (DataRow row in data)
            {
                string nombreCategoria = row["Categoria"].ToString();
                string nombreDepartamento = row["Departamento"].ToString();

                var categoria = DbContext.Categorias.Where(c => c.Categoria == nombreCategoria).SingleOrDefault();
                var departamento = DbContext.Departamentos.Where(d => d.Nombre_Departamento == nombreDepartamento).SingleOrDefault();

                if (categoria == null)
                {
                    throw new ArgumentException(String.Format("La categoria {0} no existe", nombreCategoria));
                    return;
                }
                else if (departamento == null)
                {
                    throw new ArgumentException(String.Format("El departamento {0} no existe", nombreDepartamento));
                    return;
                }

                var material = new Material()
                    {
                        Codigo = row["Codigo"].ToString(),
                        Descripcion_Material = row["Descripcion"].ToString(),
                        // Especificar medida en la documentación (puede causar confusión)
                        Unidad_Medida = row["Medida"].ToString(),
                        Costo_Unitario = decimal.Parse(row["Costo"].ToString()),
                        Consumo_Par = decimal.Parse(row["Consumo"].ToString()),
                        Categoria_Material = categoria
                    };

                var costoMaterial = new Costo
                {
                    esCostoDirecto = true,
                    Comentario = material.Descripcion_Material,
                    Descripcion = "Material",
                    Departamento = departamento
                };

                material.Costo = costoMaterial;

                materiales.Add(material);
            }

            this.CreateMultiple(materiales);
        }
    }
}