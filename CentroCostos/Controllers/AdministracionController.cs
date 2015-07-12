using CentroCostos.Infrastructure;
using CentroCostos.Infrastructure.Repositorios;
using CentroCostos.Models;
using CentroCostos.Models.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentroCostos.Controllers
{
    public class AdministracionController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly ILineaRepository _lineasDb;
        private readonly IModeloRepository _modelosDb;
        private readonly IMaterialRepository _materialesDb;
        private readonly ICategoriaRepository _categoriasDb;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public AdministracionController(IUnitOfWork uow, ILineaRepository lineasRepository, 
            IModeloRepository modelosRepository, IMaterialRepository materialRepository, ICategoriaRepository categoriaRepository)
        {
            _uow = uow;
            _lineasDb = lineasRepository;
            _modelosDb = modelosRepository;
            _materialesDb = materialRepository;
            _categoriasDb = categoriaRepository;
        }

        // GET: Administracion
        public ActionResult Index()
        {
            return View();
        }

        // GET: LineasProduccion
        public ActionResult LineasProduccion()
        {
            var model = new LineasProduccionAdmViewModel()
            {
                Lineas = _lineasDb.FindAll()
            };

            return View(model);
        }

        // GET: NuevaLinea
        public ActionResult NuevaLinea()
        {
            return View();
        }

        // POST: NuevaLinea
        [HttpPost]
        public ActionResult NuevaLinea(NuevaLineaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var nuevaLinea = new Linea
                {
                    Nombre_Linea = model.Codigo
                };

                try
                {
                    _lineasDb.Create(nuevaLinea);
                    _uow.SaveChanges();

                    TempData["message"] = "La linea ha sido creada correctamente";

                    return RedirectToAction("LineasProduccion");
                }
                catch (Exception e)
                {
                    logger.Error(e, "Error al crear una linea nueva");
                    ModelState.AddModelError("", "Se produjo un error al intentar crear la linea nueva");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        // GET: ModelosLinea
        public ActionResult ModelosLinea(int id)
        {
            var linea = _lineasDb.GetById(id);

            var model = new ModelosLineaViewModel
            {
                Linea = linea,
                Modelos = linea.Modelos_Linea
            };

            return View(model);
        }

        // GET: NuevoModelo
        public ActionResult NuevoModelo(int id)
        {
            var model = new NuevoModeloViewModel
            {
                IdLinea = id
            };

            return View(model);
        }

        // POST: NuevoModelo
        [HttpPost]
        public ActionResult NuevoModelo(NuevoModeloViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
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

                    modeloNuevo.URL_Imagen = CheckAndUploadImage(model);

                    _modelosDb.Create(modeloNuevo);
                    _uow.SaveChanges();

                    return RedirectToAction("ModelosLinea", new { id = model.IdLinea });
                }
                catch(Exception e)
                {
                    logger.Error(e, "Error al agregar nuevo modelo");
                    ModelState.AddModelError("", "Se produjo un error al agregar el modelo");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        // GET: EditarModelo
        public ActionResult EditarModelo(int idLinea, int id)
        {
            var linea = _lineasDb.GetById(idLinea);
            var modelo = _modelosDb.GetById(id);

            if (modelo == null || linea == null)
            {
                logger.Warn("No se pudo encontrar el modelo o la linea con id {0}", id);
                return RedirectToAction("ModelosLinea", new { id = idLinea });
            }

            var viewModel = new NuevoModeloViewModel
            {
                IdLinea = idLinea,
                id = modelo.Id,
                Horma = modelo.Horma,
                Tipo_Suela = modelo.Tipo_Suela,
                Codigo = modelo.Codigo,
                Color = modelo.Color,
                Numeracion = modelo.Numeracion,
                Pieza = modelo.Pieza,
                Planta = modelo.Planta
            };

            ViewBag.URLImagen = modelo.URL_Imagen;

            return View(viewModel);
        }

        // POST: EditarModelo
        [HttpPost]
        public ActionResult EditarModelo(NuevoModeloViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var curModel = _modelosDb.GetById(model.id);
                    var linea = _lineasDb.GetById(model.IdLinea);

                    curModel.Horma = model.Horma;
                    curModel.Codigo = model.Codigo;
                    curModel.Color = model.Color;
                    curModel.Numeracion = model.Numeracion;
                    curModel.Pieza = model.Pieza;
                    curModel.Planta = model.Planta;
                    curModel.Tipo_Suela = model.Tipo_Suela;
                    curModel.Linea = linea;
                    curModel.Fecha_Ultima_Modificacion = DateTime.Now;

                    curModel.URL_Imagen = CheckAndUploadImage(model) ?? curModel.URL_Imagen;

                    _modelosDb.Update(curModel);
                    _uow.SaveChanges();

                    return RedirectToAction("ModelosLinea", new { id = model.IdLinea });
                }
                catch(Exception e)
                {
                    logger.Error(e, "Error al editar modelo");

                    ModelState.AddModelError("", "Se produjo un error al editar el modelo");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        // GET: Materiales
        public ActionResult Materiales()
        {
            var model = new MaterialesAdmViewModel
            {
                Materiales = _materialesDb.FindAll(),
                Categorias = _categoriasDb.FindAll()
            };

            return View(model);
        }

        // GET: NuevoMaterial
        public ActionResult NuevoMaterial()
        {
            return View();
        }

        // POST: NuevoMaterial
        [HttpPost]
        public ActionResult NuevoMaterial(MaterialViewModel model)
        {
            if(ModelState.IsValid)
            {
                var material = new Material
                {
                    Codigo = model.Codigo,
                    Descripcion_Material = model.Descripcion_Material,
                    s
                }
            }

            return View(model);
        }

        // GET: NuevaCategoria
        public ActionResult NuevaCategoria()
        {
            return View();
        }

        // POST: NuevaCategoria
        [HttpPost]
        public ActionResult NuevaCategoria(CategoriaViewModel model)
        {
            if(ModelState.IsValid)
            {
                CategoriaMaterial categoria = new CategoriaMaterial
                {
                    Categoria = model.Categoria
                };

                try
                {
                    _categoriasDb.Create(categoria);
                    _uow.SaveChanges();
                    return RedirectToAction("Materiales");
                }
                catch(Exception e)
                {
                    logger.Error(e, "Se produjo un error al agregar una categoria");
                    ModelState.AddModelError("", "Se produjo un error al intentar agregar la categoria");
                    return View(model);
                }
            }

            return View(model);
        }


        private string CheckAndUploadImage(NuevoModeloViewModel model)
        {
            if (model.Imagen != null)
            {
                string serverPath = Server.MapPath("~/Content");
                return _modelosDb.UploadImage(model.Codigo, model.Imagen, serverPath);
            }

            return null;
        }
    }
}