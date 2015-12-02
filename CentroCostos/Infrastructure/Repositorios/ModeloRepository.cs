using CentroCostos.Helpers;
using CentroCostos.Models;
using CentroCostos.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace CentroCostos.Infrastructure.Repositorios
{
    public class ModeloRepository : BaseRepository<Modelo, int>, IModeloRepository
    {
        private IImageManager _imageManager;

        public ModeloRepository(ApplicationContext dbContext, ILineaRepository lineasRepo, IDepartamentoRepository depsRepo, IImageManager imageManager)
            : base(dbContext)
        {
            _lineasDb = lineasRepo;
            _imageManager = imageManager;
            _departamentosDb = depsRepo;
        }

        private readonly ILineaRepository _lineasDb;
        private readonly IDepartamentoRepository _departamentosDb;

        public Modelo Find(string codigo)
        {
            return DbContext.Modelos.Where(m => m.Codigo.StartsWith(codigo)).Single();
        }

        // devuelve la URL de la imagen para ser guardada en la BD
        public string UploadImageWithUniqueId(string codigo, HttpPostedFileBase file, string serverPath)
        {
            if (file == null || file.ContentLength == 0)
            {
                throw new ArgumentNullException("file");
            }

            string pathToCreate = Path.Combine(serverPath, "Imgs", codigo);
            string fileName = string.Format("{0}{1}", codigo, Guid.NewGuid().ToString());
            string storagePath = Path.Combine("Imgs", codigo, fileName);

            string pathUploaded = _imageManager.UploadImageToServer(file, pathToCreate, serverPath, storagePath, fileName);

            return pathUploaded;
        }


        public void CreateMultipleModelos(IEnumerable<DataRow> rows)
        {
            IList<Modelo> modelos = new List<Modelo>();
            foreach (DataRow row in rows)
            {
                string nombreLinea = row["Linea"].ToString();
                var linea = DbContext.Lineas.Where(l => l.Nombre_Linea == nombreLinea).SingleOrDefault();

                if (linea == null)
                {
                    throw new ArgumentException("Linea no existe");
                    return;
                }
                else
                {
                    modelos.Add(new Modelo()
                    {
                        Codigo = row["Codigo"].ToString(),
                        Horma = row["Horma"].ToString(),
                        Planta = row["Planta"].ToString(),
                        Tipo_Suela = row["Suela"].ToString(),
                        Numeracion = new Numeracion
                        {
                            Menor = byte.Parse(row["NumeracionMenor"].ToString()),
                            Mayor = byte.Parse(row["NumeracionMayor"].ToString())
                        },
                        Pieza = int.Parse(row["Pieza"].ToString()),
                        Color = row["Color"].ToString(),
                        Linea = linea
                    });
                }
            }

            this.CreateMultiple(modelos);
        }


        public void CreateMultipleModelos(IEnumerable<DataRow> rows, int lineaId)
        {
            IList<Modelo> modelos = new List<Modelo>();
            var linea = _lineasDb.GetById(lineaId);

            if (linea == null)
            {
                throw new ArgumentException("Linea no existe");
            }
            else
            {
                foreach (DataRow row in rows)
                {
                    modelos.Add(new Modelo()
                    {
                        Codigo = row["Codigo"].ToString(),
                        Horma = row["Horma"].ToString(),
                        Planta = row["Planta"].ToString(),
                        Tipo_Suela = row["Suela"].ToString(),
                        Numeracion = new Numeracion
                        {
                            Menor = byte.Parse(row["NumeracionMenor"].ToString()),
                            Mayor = byte.Parse(row["NumeracionMayor"].ToString())
                        },
                        Pieza = int.Parse(row["Pieza"].ToString()),
                        Color = row["Color"].ToString(),
                        Linea = linea,
                        Ficha = CrearFichaTecnicaVacia()
                    });
                }

                this.CreateMultiple(modelos);
            }
        }

        public void CreateSingleModelo(NuevoModeloViewModel model, string serverPath)
        {
            var linea = _lineasDb.GetById(model.IdLinea);

            var modeloNuevo = new Modelo
            {
                Codigo = model.Codigo,
                Horma = model.Horma,
                Planta = model.Planta,
                Tipo_Suela = model.Tipo_Suela,
                Numeracion = model.Numeracion,
                Pieza = model.Pieza,
                Color = model.Color,
                Linea = linea
            };

            modeloNuevo.URL_Imagen = CheckAndUploadImage(model, serverPath);

            modeloNuevo.Ficha = CrearFichaTecnicaVacia();

            this.Create(modeloNuevo);
        }

        private FichaTecnica CrearFichaTecnicaVacia()
        {
            FichaTecnica ficha = new FichaTecnica();
            var departamentos = _departamentosDb.FindAll();
            ficha.MaterialesDepartamento = InicializarFichaVaciaConDepartamentos(departamentos);

            return ficha;
        }

        private string CheckAndUploadImage(NuevoModeloViewModel model, string serverPath)
        {
            if (model.Imagen != null)
            {
                return UploadImageWithUniqueId(model.Codigo, model.Imagen, serverPath);
            }

            return null;
        }

        private List<MaterialesDepartamentoProduccion> InicializarFichaVaciaConDepartamentos(IList<DepartamentoProduccion> departamentos)
        {
            List<MaterialesDepartamentoProduccion> materialesDepartamentos = new List<MaterialesDepartamentoProduccion>();
            foreach (DepartamentoProduccion departamento in departamentos)
            {
                MaterialesDepartamentoProduccion departamentoMaterialVacio = new MaterialesDepartamentoProduccion();
                departamentoMaterialVacio.Departamento = departamento;

                materialesDepartamentos.Add(departamentoMaterialVacio);
            }

            return materialesDepartamentos;
        }
    }
}