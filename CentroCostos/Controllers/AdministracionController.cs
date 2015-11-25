using CentroCostos.Helpers;
using CentroCostos.Infrastructure;
using CentroCostos.Infrastructure.Repositorios;
using CentroCostos.Models;
using CentroCostos.Models.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace CentroCostos.Controllers
{
    [Authorize]
    public class AdministracionController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly ILineaRepository _lineasDb;
        private readonly IModeloRepository _modelosDb;
        private readonly IMaterialRepository _materialesDb;
        private readonly ICategoriaRepository _categoriasDb;
        private readonly ICostoRepository _costosDb;
        private readonly IDepartamentoRepository _departamentosDb;
        private readonly ICentroCostoRepository _centrosDb;
        private readonly IExcelData _manager;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public AdministracionController(IUnitOfWork uow, ILineaRepository lineasRepository,
            IModeloRepository modelosRepository, IMaterialRepository materialRepository, ICategoriaRepository categoriaRepository,
            ICostoRepository costosRepository, IDepartamentoRepository departamentosRepository, ICentroCostoRepository centrosRepository,
            IExcelData manager)
        {
            _uow = uow;
            _lineasDb = lineasRepository;
            _modelosDb = modelosRepository;
            _materialesDb = materialRepository;
            _categoriasDb = categoriaRepository;
            _costosDb = costosRepository;
            _departamentosDb = departamentosRepository;
            _centrosDb = centrosRepository;
            _manager = manager;
        }

        // GET: Administracion
        public ActionResult Index()
        {
            return View();
        }

        #region Lineas de produccion
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

        // GET: EditarLinea
        public ActionResult EditarLinea(int id)
        {
            var linea = _lineasDb.GetById(id);

            if (linea == null)
            {
                TempData["message_error"] = "No se pudo encontrar el registro especificado";
                return RedirectToAction("LineasProduccion");
            }

            var model = new NuevaLineaViewModel
            {
                Id = linea.Id,
                Codigo = linea.Nombre_Linea
            };

            return View(model);
        }

        // POST: EditarLinea
        [HttpPost]
        public ActionResult EditarLinea(NuevaLineaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var linea = _lineasDb.GetById(model.Id);

                    linea.Nombre_Linea = model.Codigo;

                    _lineasDb.Update(linea);
                    _uow.SaveChanges();

                    TempData["message"] = "La linea fue modificada correctamente";
                    return RedirectToAction("ModelosLinea", new { id = model.Id });
                }
                catch (Exception e)
                {
                    logger.Error(e, "Error al editar linea");
                    ModelState.AddModelError("", "Se produjo un error al intentar modificar esta linea");
                }
            }

            return View(model);
        }

        // GET: ModelosLinea
        public ActionResult ModelosLinea(int id)
        {
            var linea = _lineasDb.GetById(id);

            if (linea == null)
            {
                TempData["message_error"] = "El registro especificado no se pudo encontrar";
                return RedirectToAction("LineasProduccion");
            }

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
                    _modelosDb.CreateSingleModelo(model, serverPath);
                    _uow.SaveChanges();

                    TempData["message"] = "El modelo fue creado correctamente";
                    return RedirectToAction("ModelosLinea", new { id = model.IdLinea });
                }
                catch (Exception e)
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
                TempData["message_error"] = "No se pudo encontrar el registro especificado";
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

                    TempData["message"] = "Registro modificado correctamente";
                    return RedirectToAction("ModelosLinea", new { id = model.IdLinea });
                }
                catch (Exception e)
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

        //GET: ImportarLineas
        public ActionResult ImportarLineas()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportarLineasExcel(HttpPostedFileBase file, string workSheetName)
        {
            string pathFile = UploadAndValidateExcelFile(file);

            if(String.IsNullOrEmpty(pathFile) || String.IsNullOrEmpty(workSheetName))
            {
                ModelState.AddModelError(String.Empty, "Ocurrió un error al importar el archivo");
                return RedirectToAction("ImportarLineas");
            }

            var data = ReadExcelFile(pathFile, workSheetName);
            _lineasDb.CreateMultipleLineas(data);
            _uow.SaveChanges();

            return RedirectToAction("LineasProduccion");
        }

        [HttpPost]
        public ActionResult ImportarModelosExcel(HttpPostedFileBase file, string workSheetName)
        {
            string pathFile = UploadAndValidateExcelFile(file);

            if(String.IsNullOrEmpty(pathFile) || string.IsNullOrEmpty(workSheetName))
            {
                ModelState.AddModelError(String.Empty, "Ocurrió un error al importar el archivo");
                return RedirectToAction("ImportarLineas");
            }

            var data = ReadExcelFile(pathFile, workSheetName);
            _modelosDb.CreateMultipleModelos(data);
            _uow.SaveChanges();

            return RedirectToAction("LineasProduccion");
        }

        public ActionResult ImportarModelosLinea(int lineaId)
        {
            ViewBag.lineaId = lineaId;

            return View();
        }

        [HttpPost]
        public ActionResult ImportarModelosLineaExcel(HttpPostedFileBase file, string workSheetName, int lineaId)
        {
            string pathFile = UploadAndValidateExcelFile(file);

            if (String.IsNullOrEmpty(pathFile) || string.IsNullOrEmpty(workSheetName))
            {
                ModelState.AddModelError(String.Empty, "Ocurrió un error al importar el archivo");
                return RedirectToAction("ImportarModelosLinea");
            }

            var data = ReadExcelFile(pathFile, workSheetName);
            _modelosDb.CreateMultipleModelos(data, lineaId);
            _uow.SaveChanges();

            return RedirectToAction("LineasProduccion");
        }

        #endregion

        #region Materiales y categorias
        // GET: Materiales
        public ActionResult Materiales(int? page, string query)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            
            var materiales = _materialesDb.FindMateriales(query);

            var materialesPaginated = materiales.ToPagedList(pageNumber, pageSize);

            var model = new MaterialesAdmViewModel
            {
                Materiales = materialesPaginated,
                Categorias = _categoriasDb.FindAll()
            };

            ViewBag.query = query;

            return View(model);
        }

        // GET: NuevoMaterial
        public ActionResult NuevoMaterial()
        {
            var model = new MaterialViewModel
            {
                Categorias = _categoriasDb.FindAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.NombreCategoria
                })
            };

            return View(model);
        }

        // POST: NuevoMaterial
        [HttpPost]
        public ActionResult NuevoMaterial(MaterialViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var categoriaMaterial = _categoriasDb.GetById(model.CategoriaId);

                    var material = new Material
                    {
                        Codigo = model.Codigo,
                        Descripcion_Material = model.Descripcion_Material,
                        Costo_Unitario = model.Costo_Unitario,
                        Unidad_Medida = model.Unidad_Medida,
                        esMaterialDirecto = model.esMaterialDirecto,
                        Categoria_Material = categoriaMaterial
                    };

                    _materialesDb.Create(material);
                    _uow.SaveChanges();
                    TempData["message"] = "El material se agregó correctamente";

                    return RedirectToAction("Materiales");
                }
                catch (Exception e)
                {
                    logger.Error(e, "Error al agregar material");
                    ModelState.AddModelError("", "Se produjo un error al intentar agregar este material");
                    return View(model);
                }
            }

            model.Categorias = _categoriasDb.FindAll()
                                .Select(c => new SelectListItem
                                {
                                    Value = c.Id.ToString(),
                                    Text = c.NombreCategoria
                                });

            return View(model);
        }

        // GET: EditarMaterial
        public ActionResult EditarMaterial(int id)
        {
            var material = _materialesDb.GetById(id);

            if (material == null)
            {
                TempData["message_error"] = "No se pudo encontrar el registro especificado";
                return RedirectToAction("Materiales");
            }

            var model = new MaterialViewModel
            {
                Id = material.Id,
                CategoriaId = material.Categoria_Material.Id,
                Codigo = material.Codigo,
                Descripcion_Material = material.Descripcion_Material,
                Costo_Unitario = material.Costo_Unitario,
                Unidad_Medida = material.Unidad_Medida,
                esMaterialDirecto = material.esMaterialDirecto,
                Categorias = _categoriasDb.FindAll()
                                .Select(c => new SelectListItem
                                {
                                    Value = c.Id.ToString(),
                                    Text = c.NombreCategoria
                                })
            };

            return View(model);
        }

        // POST: EditarMaterial
        [HttpPost]
        public ActionResult EditarMaterial(MaterialViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var material = _materialesDb.GetById(model.Id);
                    var categoria = _categoriasDb.GetById(model.CategoriaId);

                    material.Codigo = model.Codigo;
                    material.Costo_Unitario = model.Costo_Unitario;
                    material.Unidad_Medida = model.Unidad_Medida;
                    material.Descripcion_Material = model.Descripcion_Material;
                    material.Categoria_Material = categoria;
                    material.esMaterialDirecto = model.esMaterialDirecto;

                    _materialesDb.Update(material);
                    _uow.SaveChanges();

                    TempData["message"] = "Registro modificado correctamente";
                    return RedirectToAction("Materiales");
                }
                catch (Exception e)
                {
                    logger.Error(e, "Se produjo un error al editar un material");
                    ModelState.AddModelError("", "Se produjo un error al intentar editar el material");
                    return View(model);
                }
            }

            model.Categorias = _categoriasDb.FindAll()
                                .Select(c => new SelectListItem
                                {
                                    Value = c.Id.ToString(),
                                    Text = c.NombreCategoria
                                });
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
            if (ModelState.IsValid)
            {
                CategoriaMaterial categoria = new CategoriaMaterial
                {
                    NombreCategoria = model.Categoria
                };

                try
                {
                    _categoriasDb.Create(categoria);
                    _uow.SaveChanges();

                    TempData["message"] = "Registro agregado correctamente";
                    return RedirectToAction("Materiales");
                }
                catch (Exception e)
                {
                    logger.Error(e, "Se produjo un error al agregar una categoria");
                    ModelState.AddModelError("", "Se produjo un error al intentar agregar la categoria");
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: EditarCategoria
        public ActionResult EditarCategoria(int id)
        {
            var categoria = _categoriasDb.GetById(id);

            if (categoria == null)
            {
                TempData["message_error"] = "No se pudo encontrar el registro especificado";
                return RedirectToAction("Materiales");
            }

            var model = new CategoriaViewModel
            {
                Id = categoria.Id,
                Categoria = categoria.NombreCategoria
            };

            return View(model);
        }

        // POST: EditarCategoria
        [HttpPost]
        public ActionResult EditarCategoria(CategoriaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var categoria = _categoriasDb.GetById(model.Id);

                    categoria.NombreCategoria = model.Categoria;

                    _categoriasDb.Update(categoria);
                    _uow.SaveChanges();

                    TempData["message"] = "Registro modificado correctamente";
                    return RedirectToAction("Materiales");
                }
                catch (Exception e)
                {
                    logger.Error(e, "Error al editar una categoria");
                    ModelState.AddModelError("", "Se produjo un error al editar una categoria");
                }
            }

            return View(model);
        }

        // GET: ImportarMateriales
        public ActionResult ImportarMateriales()
        {
            return View();
        }

        // POST: ImportarMateriales
        [HttpPost]
        public ActionResult ImportarMaterialesExcel(HttpPostedFileBase file, string workSheetName)
        {
            string pathFile = UploadAndValidateExcelFile(file);

            if (String.IsNullOrEmpty(pathFile) || string.IsNullOrEmpty(workSheetName))
            {
                ModelState.AddModelError(String.Empty, "Ocurrió un error al importar el archivo");
                return RedirectToAction("ImportarMateriales");
            }

            var data = ReadExcelFile(pathFile, workSheetName);
            _materialesDb.CreateMultipleMateriales(data);
            _uow.SaveChanges();

            return RedirectToAction("Materiales");
        }
        #endregion

        #region Costos
        // GET: Costos
        public ActionResult Costos()
        {
            var model = new CostosAdmViewModel
            {
                Costos = _costosDb.FindAll().Select(c => new CostoViewModel
                {
                    Id = c.Id,
                    Comentario = c.Comentario,
                    Descripcion = c.Descripcion,
                    Departamento = c.Departamento != null ? c.Departamento.Nombre_Departamento : "",
                    esCostoDirecto = c.esCostoDirecto
                }).ToList()
            };

            return View(model);
        }

        // GET: NuevoCosto
        public ActionResult NuevoCosto()
        {
            var model = new CostoViewModel()
            {
                Departamentos = _departamentosDb.FindAll()
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Nombre_Departamento
                })
            };

            return View(model);
        }

        // POST: NuevoCosto
        [HttpPost]
        public ActionResult NuevoCosto(CostoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var departamentoCosto = _departamentosDb.GetById(model.DepartamentoId);
                    var costo = new Costo
                    {
                        Descripcion = model.Descripcion,
                        Comentario = model.Comentario,
                        esCostoDirecto = (bool)model.esCostoDirecto,
                        Departamento = departamentoCosto
                    };

                    _costosDb.Create(costo);
                    _uow.SaveChanges();

                    TempData["message"] = "Registro agregado correctamente";
                    return RedirectToAction("Costos");
                }
                catch (Exception e)
                {
                    logger.Error(e, "Error al agregar un costo");
                    ModelState.AddModelError("", "Se produjo un error al intentar agregar este costo");
                }
            }

            return View(model);
        }

        // GET: EditarCosto
        public ActionResult EditarCosto(int id)
        {
            var costo = _costosDb.GetById(id);

            if (costo == null)
            {
                TempData["message_error"] = "No se pudo encontrar el registro especificado";
                return RedirectToAction("Costos");
            }

            var model = new CostoViewModel
            {
                Id = costo.Id,
                Descripcion = costo.Descripcion,
                Comentario = costo.Comentario,
                esCostoDirecto = (bool)costo.esCostoDirecto,
                DepartamentoId = costo.Departamento.Id,
                Departamentos = _departamentosDb.FindAll()
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Nombre_Departamento
                })
            };

            return View(model);
        }

        // POST: EditarCosto
        [HttpPost]
        public ActionResult EditarCosto(CostoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var costo = _costosDb.GetById(model.Id);
                    var departamentoCosto = _departamentosDb.GetById(model.DepartamentoId);

                    costo.Descripcion = model.Descripcion;
                    costo.Comentario = model.Comentario;
                    costo.esCostoDirecto = (bool)model.esCostoDirecto;
                    costo.Departamento = departamentoCosto;

                    _costosDb.Update(costo);
                    _uow.SaveChanges();

                    TempData["message"] = "Registro modificado correctamente";
                    return RedirectToAction("Costos");
                }
                catch (Exception e)
                {
                    logger.Error(e, "Error al editar un costo");
                    ModelState.AddModelError("", "Se produjo un error al intentar editar este costo");
                }
            }

            return View(model);
        }
        #endregion

        #region Departamentos
        // GET: Departamentos
        public ActionResult Departamentos()
        {
            var model = new DepartamentosAdmViewModel
            {
                Departamentos = _departamentosDb.FindAll()
            };

            return View(model);
        }

        // GET: NuevoDepartamento
        public ActionResult NuevoDepartamento()
        {
            return View();
        }

        // POST: NuevoDepartamento
        [HttpPost]
        public ActionResult NuevoDepartamento(DepartamentoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var departamento = new DepartamentoProduccion
                    {
                        Nombre_Departamento = model.Nombre_Departamento
                    };

                    _departamentosDb.Create(departamento);
                    _uow.SaveChanges();

                    TempData["message"] = "El registro fue creado correctamente";
                    return RedirectToAction("Departamentos");
                }
                catch (Exception e)
                {
                    logger.Error(e, "Ocurrio un error al agregar un departamento");
                    ModelState.AddModelError(String.Empty, "Ocurrio un error al agregar el departamento");
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: EditarDepartamento
        public ActionResult EditarDepartamento(int id)
        {
            var departamento = _departamentosDb.GetById(id);

            if (departamento == null)
            {
                TempData["message_error"] = "No se pudo encontrar el registro especificado";
                return RedirectToAction("Departamentos");
            }

            var model = new DepartamentoViewModel
            {
                Id = departamento.Id,
                Nombre_Departamento = departamento.Nombre_Departamento
            };

            return View(model);
        }

        // POST: EditarDepartamento
        [HttpPost]
        public ActionResult EditarDepartamento(DepartamentoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var departamento = _departamentosDb.GetById(model.Id);

                    departamento.Nombre_Departamento = model.Nombre_Departamento;

                    _departamentosDb.Update(departamento);
                    _uow.SaveChanges();

                    TempData["message"] = "El registro fue modificado correctamente";
                    return RedirectToAction("Departamentos");
                }
                catch (Exception e)
                {
                    logger.Error(e, "Error al editar departamento");
                    ModelState.AddModelError(String.Empty, "Ocurrio un error al intentar editar el departamento");
                    return View(model);
                }
            }

            return View(model);
        }
        #endregion

        #region Centros de costo
        // GET: CentrosCosto
        public ActionResult CentrosCosto()
        {
            var centros = _centrosDb.FindAll();

            var model = new CentrosCostoAdmViewModel
            {
                CentrosdeCosto = centros
            };

            return View(model);
        }

        // GET: NuevoCentro
        public ActionResult NuevoCentro()
        {
            return View();
        }

        // POST: NuevoCentro
        [HttpPost]
        public ActionResult NuevoCentro(CentroCostoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var centro = new CentroCosto
                    {
                        Codigo = model.Codigo,
                        Descripcion = model.Descripcion
                    };

                    _centrosDb.Create(centro);
                    _uow.SaveChanges();

                    TempData["message"] = "Centro de costo creado correctamente";
                    return RedirectToAction("CentrosCosto");
                }
                catch (Exception e)
                {
                    logger.Error(e, "Error al agregar nuevo centro");
                    ModelState.AddModelError(String.Empty, "Se produjo un error al intentar agregar el centro de costo");
                }
            }

            return View(model);
        }

        // GET: EditarCentro
        public ActionResult EditarCentro(int id)
        {
            var centro = _centrosDb.GetById(id);

            if (centro == null)
            {
                TempData["message_error"] = "No se pudo encontrar el registro especificado";
                return RedirectToAction("Centros");
            }

            var model = new CentroCostoViewModel
            {
                Id = centro.Id,
                Codigo = centro.Codigo,
                Descripcion = centro.Descripcion
            };

            return View(model);
        }

        // POST: EditarCentro
        [HttpPost]
        public ActionResult EditarCentro(CentroCostoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var centro = _centrosDb.GetById(model.Id);

                    centro.Codigo = model.Codigo;
                    centro.Descripcion = model.Descripcion;

                    _centrosDb.Update(centro);
                    _uow.SaveChanges();

                    TempData["message"] = "El centro de costos se modificó correctamente";
                    return RedirectToAction("CentrosCosto");
                }
                catch (Exception e)
                {
                    logger.Error(e, "Error al editar centro de costos");
                    ModelState.AddModelError(String.Empty, "Se produjo un error al intentar editar el centro de costo");
                }
            }

            return View(model);
        }
        #endregion

        #region HelperMethods

        private string CheckAndUploadImage(NuevoModeloViewModel model)
        {
            if (model.Imagen != null)
            {
                return _modelosDb.UploadImageWithUniqueId(model.Codigo, model.Imagen, serverPath);
            }

            return null;
        }

        private string serverPath
        {
            get
            {
                return Server.MapPath("~/Content");
            }
        }

        // Returns the path to the uploaded file
        private string UploadAndValidateExcelFile(HttpPostedFileBase file)
        {
            string extension = Path.GetExtension(file.FileName);
            if (extension.Equals(".xls") || extension.Equals(".xlsx"))
            {
                try
                {
                    // upload the file
                    string fileName = string.Format("Excel-{0:dd-MM-yyyy-HH-mm-ss}{1}", DateTime.Now, extension);
                    string path = Path.Combine(Server.MapPath("~/ExcelTemp"), fileName);
                    file.SaveAs(path);

                    return path;                   
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error al cargar archivo la servidor");
                    return String.Empty;
                }
            }

            return String.Empty;
        }

        private IEnumerable<DataRow> ReadExcelFile(string path, string workSheetName)
        {
            _manager.setPath(path);
            var data = _manager.getData(workSheetName, true);

            // delete the file
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            return data;
        }
        #endregion
    }
}