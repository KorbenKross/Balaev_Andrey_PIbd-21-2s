using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.UserViewModel;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace;
using VirtualStore;

namespace VirtualStorePlace.RealiseInterface
{ 
    public class ProductStorageSelectionList : IProductStorageService
    {
        private BaseListSingleton source;

        public ProductStorageSelectionList()
        {
            source = BaseListSingleton.GetInstance();
        }

        public List<ProductStorageUserViewModel> GetList()
        {
            List<ProductStorageUserViewModel> result = source.ProductStorages
                .Select(rec => new ProductStorageUserViewModel
                {
                    Id = rec.Id,
                    ProductStorageName = rec.ProductStorageName,
                    ProductStorageElements = source.ProductStorageElement
                            .Where(recPC => recPC.ProductStorageId == rec.Id)
                            .Select(recPC => new ProductStorageElementViewModel
                            {
                                Id = recPC.Id,
                                ProductStorageId = recPC.ProductStorageId,
                                ElementId = recPC.ElementId,
                                ElementName = source.Elements
                                    .FirstOrDefault(recC => recC.Id == recPC.ElementId)?.ElementName,
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }


        public ProductStorageUserViewModel GetElement(int id)
        {
            ProductStorage component = source.ProductStorages.FirstOrDefault(rec => rec.Id == id);
            if (component != null)
            {
                return new ProductStorageUserViewModel
                {
                    Id = component.Id,
                    ProductStorageName = component.ProductStorageName,
                    ProductStorageElements = source.ProductStorageElement
                            .Where(recPC => recPC.ProductStorageId == component.Id)
                            .Select(recPC => new ProductStorageElementViewModel
                            {
                                Id = recPC.Id,
                                ProductStorageId = recPC.ProductStorageId,
                                ElementId = recPC.ElementId,
                                ElementName = source.Elements
                                    .FirstOrDefault(recC => recC.Id == recPC.ElementId)?.ElementName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }


        public void AddElement(ProductStorageConnectingModel model)
        {
            ProductStorage compinent = source.ProductStorages.FirstOrDefault(rec => rec.ProductStorageName == model.StockName);
            if (compinent != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.ProductStorages.Count > 0 ? source.ProductStorages.Max(rec => rec.Id) : 0;
            source.ProductStorages.Add(new ProductStorage
            {
                Id = maxId + 1,
                ProductStorageName = model.StockName
            });
        }

        public void UpdElement(ProductStorageConnectingModel model)
        {
            ProductStorage component = source.ProductStorages.FirstOrDefault(rec =>
                                        rec.ProductStorageName == model.StockName && rec.Id != model.Id);
            if (component != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            component = source.ProductStorages.FirstOrDefault(rec => rec.Id == model.Id);
            if (component == null)
            {
                throw new Exception("Элемент не найден");
            }
            component.ProductStorageName = model.StockName;
        }

        public void DelElement(int id)
        {
            ProductStorage component = source.ProductStorages.FirstOrDefault(rec => rec.Id == id);
            if (component != null)
            {
                // при удалении удаляем все записи о компонентах на удаляемом складе
                source.ProductStorageElement.RemoveAll(rec => rec.ProductStorageId == id);
                source.ProductStorages.Remove(component);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
