using CentroCostos.Infrastructure;
using CentroCostos.Infrastructure.Repositorios;
using CentroCostos.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public LineaProduccionController(IUnitOfWork uow, ILineaRepository lineasRepository, IModeloRepository modelosRepository, IMaterialRepository materialesRepository)
        {
            _uow = uow;
            _lineasDb = lineasRepository;
            _modelosDb = modelosRepository;
            _materialesDb = materialesRepository;
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
    }
}