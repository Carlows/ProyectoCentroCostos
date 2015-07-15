[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CentroCostos.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(CentroCostos.App_Start.NinjectWebCommon), "Stop")]

namespace CentroCostos.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using CentroCostos.Infrastructure;
    using CentroCostos.Helpers;
    using CentroCostos.Infrastructure.Repositorios;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ApplicationContext>().ToSelf().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<IExcelData>().To<ExcelData>();
            kernel.Bind<IExcelDataExport>().To<ExcelDataExport>();
            kernel.Bind<ILineaRepository>().To<LineaRepository>();
            kernel.Bind<IModeloRepository>().To<ModeloRepository>();
            kernel.Bind<ICentroCostoRepository>().To<CentroCostoRepository>();
            kernel.Bind<IDepartamentoRepository>().To<DepartamentoRepository>();
            kernel.Bind<IOrdenRepository>().To<OrdenRepository>();
            kernel.Bind<IUnidadCostoRepository>().To<UnidadCostoRepository>();
            kernel.Bind<IMaterialRepository>().To<MaterialRepository>();
            kernel.Bind<ICategoriaRepository>().To<CategoriaRepository>();
            kernel.Bind<ICostoRepository>().To<CostoRepository>();
        }        
    }
}
