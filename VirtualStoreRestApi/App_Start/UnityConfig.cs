using System;

using Unity;
using System.Data.Entity;
using Unity.Lifetime;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.RealiseInterface;
using VirtualStorePlace.RealeseInterfaceBD;
using VirtualStorePlace;

namespace VirtualStoreRestApi
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();
            container.RegisterType<DbContext, AbstractDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IBuyerCustomer, BuyerSelectionListBD>(new HierarchicalLifetimeManager());
            container.RegisterType<IElementService, ElementSelectionListBD>(new HierarchicalLifetimeManager());
            container.RegisterType<IKitchenerService, KitchenerSelectionListBD>(new HierarchicalLifetimeManager());
            container.RegisterType<IIngredientService, IngredientSelectionListBD>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductStorageService, ProductStorageSelectionListBD>(new HierarchicalLifetimeManager());
            container.RegisterType<IGeneralSelection, GeneralSelectionListBD>(new HierarchicalLifetimeManager());
            container.RegisterType<IReportService, ReportServiceBD>(new HierarchicalLifetimeManager());
            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
        }
    }
}