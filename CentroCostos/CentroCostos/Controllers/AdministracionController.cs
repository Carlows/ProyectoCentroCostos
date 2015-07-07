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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public AdministracionController(IUnitOfWork uow, ILineaRepository lineasRepository)
        {
            _uow = uow;
            _lineasDb = lineasRepository;
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
    }
}