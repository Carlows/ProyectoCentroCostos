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

        public LineaProduccionController(IUnitOfWork uow, ILineaRepository lineasRepository, IModeloRepository modelosRepository)
        {
            _uow = uow;
            _lineasDb = lineasRepository;
            _modelosDb = modelosRepository;
        }

        // GET: LineaProduccion
        public ActionResult Index(LineaIndexViewModel model)
        {
            LineaIndexViewModel newModel = new LineaIndexViewModel();

            newModel.Lineas = _lineasDb.FindAll().Select(l => new SelectListItem() { Text = l.Nombre_Linea, Value = l.Id.ToString() }).ToList();
            newModel.LineaID = model.LineaID;
            newModel.ModeloID = model.ModeloID;

            if (model.ModeloID == null && model.LineaID != null)
            {
                newModel.ModelosLinea = ObtenerModelos(model);
            }
            else if(model.ModeloID != null)
            {
                newModel.ModelosLinea = ObtenerModelos(model);
                newModel.Modelo = _modelosDb.GetById((int)model.ModeloID);
            }

            return View(newModel);
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
    }
}