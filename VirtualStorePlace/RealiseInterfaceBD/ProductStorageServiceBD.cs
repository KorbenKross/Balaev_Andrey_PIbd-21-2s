using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.UserViewModel;
using VirtualStore;
using VirtualStorePlace.LogicInterface;


namespace VirtualStorePlace.RealiseInterfaceBD
{
    public class ProductStorageServiceBD : IProductStorageService
    {
        private AbstractDbContext context;

        public ProductStorageServiceBD(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<ProductStorageUserViewModel> GetList()
        {
            List<ProductStorageUserViewModel> result = context.ProductStorages
                .Select(rec => new ProductStorageUserViewModel
                {
                    Id = rec.Id,
                    ProductStorageName = rec.ProductStorageName,
                    ProductStorageElements = context.ProductStorageElements
                            .Where(recPC => recPC.ProductStorageId == rec.Id)
                            .Select(recPC => new ProductStorageElementViewModel
                            {
                                Id = recPC.Id,
                                ProductStorageId = recPC.ProductStorageId,
                                ElementId = recPC.ElementId,
                                ElementName = recPC.Element.ElementName,
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public ProductStorageUserViewModel GetElement(int id)
        {
            ProductStorage element = context.ProductStorages.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ProductStorageUserViewModel
                {
                    Id = element.Id,
                    ProductStorageName = element.ProductStorageName,
                    ProductStorageElements = context.ProductStorageElements
                            .Where(recPC => recPC.ProductStorageId == element.Id)
                            .Select(recPC => new ProductStorageElementViewModel
                            {
                                Id = recPC.Id,
                                ProductStorageId = recPC.ProductStorageId,
                                ElementId = recPC.ElementId,
                                ElementName = recPC.Element.ElementName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ProductStorageConnectingModel model)
        {
            ProductStorage element = context.ProductStorages.FirstOrDefault(rec => rec.ProductStorageName == model.StockName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            context.ProductStorages.Add(new ProductStorage
            {
                ProductStorageName = model.StockName
            });
            context.SaveChanges();
        }

        public void UpdElement(ProductStorageConnectingModel model)
        {
            ProductStorage element = context.ProductStorages.FirstOrDefault(rec =>
                                        rec.ProductStorageName == model.StockName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = context.ProductStorages.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ProductStorageName = model.StockName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    ProductStorage element = context.ProductStorages.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // при удалении удаляем все записи о компонентах на удаляемом складе
                        context.ProductStorageElements.RemoveRange(
                                            context.ProductStorageElements.Where(rec => rec.ProductStorageId == id));
                        context.ProductStorages.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
