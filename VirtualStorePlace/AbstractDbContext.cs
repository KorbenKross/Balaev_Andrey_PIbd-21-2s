using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.UserViewModel;
using VirtualStore;

namespace VirtualStorePlace
{    
    public class AbstractDbContext : DbContext
    {   
        public AbstractDbContext() : base("AbstractDatabase")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<AbstractDbContext>(null);
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

        public virtual DbSet<MessageInfo> MessageInfos { get; set; }

        /// <summary>
        /// Перегружаем метод созранения изменений. Если возникла ошибка - очищаем все изменения
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception)
            {
                foreach (var entry in ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.Reload();
                            break;
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                    }
                }
                throw;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<AbstractDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }    
}
