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
            List<ProductStorageUserViewModel> result = new List<ProductStorageUserViewModel>();
            for (int i = 0; i < source.ProductStorages.Count; ++i)
            {
                // требуется дополнительно получить список компонентов на складе и их количество
                List<ProductStorageElementViewModel> ProductStorageElements = new List<ProductStorageElementViewModel>();
                for (int j = 0; j < source.ProductStorageElement.Count; ++j)
                {
                    if (source.ProductStorageElement[j].ProductStorageId == source.ProductStorages[i].Id)
                    {
                        string elementName = string.Empty;
                        for (int k = 0; k < source.Elements.Count; ++k)
                        {
                            if (source.ProductStorageElement[j].ElementId == source.Elements[k].Id)
                            {
                                elementName = source.Elements[k].ElementName;
                                break;
                            }
                        }
                        ProductStorageElements.Add(new ProductStorageElementViewModel
                        {
                            Id = source.ProductStorageElement[j].Id,
                            ProductStorageId = source.ProductStorageElement[j].ProductStorageId,
                            ElementId = source.ProductStorageElement[j].ElementId,
                            ElementName = elementName,
                            Count = source.ProductStorageElement[j].Count
                        });
                    }
                }
                result.Add(new ProductStorageUserViewModel
                {
                    Id = source.ProductStorages[i].Id,
                    ProductStorageName = source.ProductStorages[i].ProductStorageName,
                    ProductStorageElements = ProductStorageElements
                });
            }
            return result;
        }

        public ProductStorageUserViewModel GetElement(int id)
        {
            for (int i = 0; i < source.ProductStorages.Count; ++i)
            {
                // требуется дополнительно получить список компонентов на складе и их количество
                List<ProductStorageElementViewModel> ProductStorageElement = new List<ProductStorageElementViewModel>();
                for (int j = 0; j < source.ProductStorageElement.Count; ++j)
                {
                    if (source.ProductStorageElement[j].ProductStorageId == source.ProductStorages[i].Id)
                    {
                        string componentName = string.Empty;
                        for (int k = 0; k < source.Elements.Count; ++k)
                        {
                            if (source.ProductStorageElement[j].ElementId == source.Elements[k].Id)
                            {
                                componentName = source.Elements[k].ElementName;
                                break;
                            }
                        }
                        ProductStorageElement.Add(new ProductStorageElementViewModel
                        {
                            Id = source.ProductStorageElement[j].Id,
                            ProductStorageId = source.ProductStorageElement[j].ProductStorageId,
                            ElementId = source.ProductStorageElement[j].ElementId,
                            ElementName = componentName,
                            Count = source.ProductStorageElement[j].Count
                        });
                    }
                }
                if (source.ProductStorages[i].Id == id)
                {
                    return new ProductStorageUserViewModel
                    {
                        Id = source.ProductStorages[i].Id,
                        ProductStorageName = source.ProductStorages[i].ProductStorageName,
                        ProductStorageElements = ProductStorageElement
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ProductStorageConnectingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.ProductStorages.Count; ++i)
            {
                if (source.ProductStorages[i].Id > maxId)
                {
                    maxId = source.ProductStorages[i].Id;
                }
                if (source.ProductStorages[i].ProductStorageName == model.StockName)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            source.ProductStorages.Add(new ProductStorage
            {
                Id = maxId + 1,
                ProductStorageName = model.StockName
            });
        }

        public void UpdElement(ProductStorageConnectingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.ProductStorages.Count; ++i)
            {
                if (source.ProductStorages[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.ProductStorages[i].ProductStorageName == model.StockName &&
                    source.ProductStorages[i].Id != model.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.ProductStorages[index].ProductStorageName = model.StockName;
        }

        public void DelElement(int id)
        {
            // при удалении удаляем все записи о компонентах на удаляемом складе
            for (int i = 0; i < source.ProductStorageElement.Count; ++i)
            {
                if (source.ProductStorageElement[i].ProductStorageId == id)
                {
                    source.ProductStorageElement.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.ProductStorages.Count; ++i)
            {
                if (source.ProductStorages[i].Id == id)
                {
                    source.ProductStorages.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
