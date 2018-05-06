using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using System.Data.Entity;
using Unity.Lifetime;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.RealiseInterface;
using VirtualStorePlace.RealeseInterfaceBD;
using VirtualStorePlace;

namespace VirtualStoreView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormGeneral>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, AbstractDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IBuyerCustomer, BuyerSelectionListBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IElementService, ElementSelectionListBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IKitchenerService, KitchenerSelectionListBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IIngredientService, IngredientSelectionListBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProductStorageService, ProductStorageSelectionListBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGeneralSelection, GeneralSelectionListBD>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
