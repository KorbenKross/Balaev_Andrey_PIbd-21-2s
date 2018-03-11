using VirtualStore;
using System.Collections.Generic;

namespace VirtualStorePlace
{
    public class BaseListSingleton
    {
        private static BaseListSingleton instance;

        public List<Buyer> Buyers { get; set; }

        public List<Element> Elements { get; set; }

        public List<Kitchener> Kitcheners { get; set; }

        public List<CustomerSelection> CustomerSelections { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public List<IngredientElement> IngredientElements { get; set; }

        public List<ProductStorage> ProductStorages { get; set; }

        public List<ProductStorageElement> ProductStorageElements { get; set; }

        private BaseListSingleton()
        {
            Buyers = new List<Buyer>();
            Elements = new List<Element>();
            Kitcheners = new List<Kitchener>();
            CustomerSelections = new List<CustomerSelection>();
            Ingredients = new List<Ingredient>();
            IngredientElements = new List<IngredientElement>();
            ProductStorages = new List<ProductStorage>();
            ProductStorageElements = new List<ProductStorageElement>();
        }

        public static BaseListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new BaseListSingleton();
            }

            return instance;
        }
    }
}