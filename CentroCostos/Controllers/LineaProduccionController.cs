using CentroCostos.Infrastructure;
using CentroCostos.Infrastructure.Repositorios;
using CentroCostos.Models;
using CentroCostos.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CentroCostos.Controllers
{
    // Este controlador va a llevar el control de las lineas, los modelos y los materiales
    [Authorize]
    public class LineaProduccionController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly ILineaRepository _lineasDb;
        private readonly IModeloRepository _modelosDb;
        private readonly IMaterialRepository _materialesDb;
        private readonly IMaterialesDepartamentoRepository _materialesDepartamentoDb;
        private readonly ICostoMaterialRepository _costoMaterialDb;

        public LineaProduccionController(IUnitOfWork uow, ILineaRepository lineasRepository, IModeloRepository modelosRepository, IMaterialRepository materialesRepository,
                                         IMaterialesDepartamentoRepository materialesDepartamentoRepo, ICostoMaterialRepository costoMaterialRepo)
        {
            _uow = uow;
            _lineasDb = lineasRepository;
            _modelosDb = modelosRepository;
            _materialesDb = materialesRepository;
            _materialesDepartamentoDb = materialesDepartamentoRepo;
            _costoMaterialDb = costoMaterialRepo;
        }

        // GET: LineaProduccion
        public ActionResult Index(LineaIndexViewModel model)
        {
            LineaIndexViewModel newModel = new LineaIndexViewModel();

            newModel.Lineas = _lineasDb.FindAll().Select(l => new SelectListItem() { Text = l.Nombre_Linea, Value = l.Id.ToString() }).ToList();
            newModel.LineaID = model.LineaID;
            newModel.ModeloID = model.ModeloID;

            if (isOnlyLineaSelected(model))
            {
                newModel.ModelosLinea = ObtenerModelos(model);
            }
            else if (isModelSelected(model))
            {
                newModel.ModelosLinea = ObtenerModelos(model);
                newModel.Modelo = _modelosDb.GetById((int)model.ModeloID);
            }

            return View(newModel);
        }

        private bool isModelSelected(LineaIndexViewModel model)
        {
            return model.ModeloID != null;
        }

        private bool isOnlyLineaSelected(LineaIndexViewModel model)
        {
            return model.ModeloID == null && model.LineaID != null;
        }

        private IList<SelectListItem> ObtenerModelos(LineaIndexViewModel model)
        {
            return _lineasDb.GetById((int)model.LineaID).Modelos_Linea
                .Select(m => new SelectListItem()
                {
                    Text = m.Codigo,
                    Value = m.Id.ToString()
                }).ToList();
        }

        public ActionResult AgregarMaterialDepartamento(int id, int departamentoId)
        {
            try
            {
                var modelo = _modelosDb.GetById(id);
                var departamentoMateriales = modelo.Ficha.MaterialesDepartamento.Where(d => d.Id == departamentoId).Single().Materiales;
                var materiales = _materialesDb.FindAll();

                ViewBag.departamentoMaterialId = departamentoId;

                var model = new AgregarMaterialesViewModel()
                {
                    materialesAgregados = departamentoMateriales,
                    listaMateriales = materiales
                };

                return View(model);
            }
            catch(Exception e)
            {
                // log here

                TempData["message"] = "Hubo un error al encontrar la información";
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult AgregarMaterialDepartamentoAJAX(AgregarMaterialDepartamentoViewModel model)
        {
            try
            {
                var departamentoMaterial = _materialesDepartamentoDb.GetById(model.departamentoId);
                var material = _materialesDb.Find(model.codigoMaterial);

                CostoMaterial costo = new CostoMaterial()
                {
                    Consumo_Par = model.consumoMaterial,                    
                    Material = material
                };

                departamentoMaterial.Materiales.Add(costo);

                // A problem with lazy loading, if you didn't refer to this property before, then it will not ever initialize
                departamentoMaterial.Departamento = departamentoMaterial.Departamento;

                _materialesDepartamentoDb.Update(departamentoMaterial);
                _uow.SaveChanges();

                return Json(new { success = true, message = "El material se agrego correctamente" });
            }
            catch(Exception e)
            {
                return Json(new { success = false, message = "Se produjo un error en el servidor" });
            }
        }

        public ActionResult EliminarCostoMaterial(int id)
        {
            ViewBag.idCosto = id;

            return View();
        }

        [HttpPost]
        public ActionResult EliminarCostoMaterialPost(int idMaterial)
        {
            ViewBag.idCosto = idMaterial;            
            try
            {
                var material = _costoMaterialDb.GetById(idMaterial);

                _costoMaterialDb.Delete(material);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }            
            catch(Exception e)
            {
                // log here

                ModelState.AddModelError("", "No se pudo eliminar el material");
                return View();
            }
        }
    }
}