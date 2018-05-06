using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.RealiseInterface;

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
            currentContainer.RegisterType<IBuyerCustomer, BuyerSelectionList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IElementService, ElementSelectionList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IKitchenerService, KitchenerSelectionList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IIngredientService, IngredientSelectionList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProductStorageService, ProductStorageSelectionList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGeneralSelection, GeneralSelectionList>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
