[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(KMGames.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(KMGames.Web.App_Start.NinjectWebCommon), "Stop")]

namespace KMGames.Web.App_Start
{
    using System;
    using System.Web;
    using KMGames.Data;
    using KMGames.Data.Interfaces;
    using KMGames.Data.Repositories;
    using KMGames.Services.Interfaces;
    using KMGames.Services.Services;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
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
            kernel.Bind<ICountryService>().To<CountryService>().InRequestScope();
            kernel.Bind<ICountryRepository>().To<CountryRepository>().InRequestScope();

            kernel.Bind<ICategoryService>().To<CategoryService>().InRequestScope();
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>().InRequestScope();

            kernel.Bind<IDeveloperService>().To<DeveloperService>().InRequestScope();
            kernel.Bind<IDeveloperRepository>().To<DeveloperRepository>().InRequestScope();

            kernel.Bind<ICityService>().To<CityService>().InRequestScope();
            kernel.Bind<ICityRepository>().To<CityRepository>().InRequestScope();

            kernel.Bind<IPLayerTypeService>().To<PlayerTypeService>().InRequestScope();
            kernel.Bind<IPlayerTypeRepository>().To<PlayerTypeRepository>().InRequestScope();

            kernel.Bind<IGameService>().To<GameService>().InRequestScope();
            kernel.Bind<IGameRepository>().To<GameRepository>().InRequestScope();

            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IUserRepository>().To<UserRepository>().InRequestScope();

            kernel.Bind<ISaleService>().To<SaleService>().InRequestScope();
            kernel.Bind<ISaleRepository>().To<SaleRepository>().InRequestScope();

            kernel.Bind<KmGamesDBContext>().ToSelf().InRequestScope();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
        }
    }
}