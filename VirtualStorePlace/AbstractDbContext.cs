using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using VirtualStore;

namespace VirtualStorePlace
{
    public class AbstractDbContext : DbContext
    {
        public AbstractDbContext() : base("AbstractDatabaseWeb")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Buyer> Buyers { get; set; }

        public virtual DbSet<Element> Elements { get; set; }

        public virtual DbSet<Kitchener> Kitcheners { get; set; }

        public virtual DbSet<CustomerSelection> CustomerSelections { get; set; }

        public virtual DbSet<Ingredient> Ingredients { get; set; }

        public virtual DbSet<IngredientElement> IngredientElements { get; set; }

        public virtual DbSet<ProductStorage> ProductStorages { get; set; }

        public virtual DbSet<ProductStorageElement> ProductStorageElements { get; set; }
    }
}
