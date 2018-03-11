using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.UserViewModel;
using VirtualStore;

namespace VirtualStorePlace.RealiseInterface
{
    public class GeneralSelectionList : IGeneralSelection
    {
        private BaseListSingleton source;

        public GeneralSelectionList()
        {
            source = BaseListSingleton.GetInstance();
        }

        public List<CustomerSelectionUserViewModel> GetList()
        {
            List<CustomerSelectionUserViewModel> result = source.CustomerSelections
                .Select(rec => new CustomerSelectionUserViewModel
                {
                    Id = rec.Id,
                    BuyerId = rec.BuyerId,
                    IngredientId = rec.IngredientId,
                    KitchinerId = rec.KitchenerId,
                    DateCreate = rec.DateCreate.ToLongDateString(),
                    DateCook = rec.DateImplement?.ToLongDateString(),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Sum = rec.Sum,
                    BuyerFIO = source.Buyers
                                    .FirstOrDefault(recC => recC.Id == rec.BuyerId)?.BuyerFIO,
                    IngredientName = source.Ingredients
                                    .FirstOrDefault(recP => recP.Id == rec.IngredientId)?.IngredientName,
                    KitchinerName = source.Kitcheners
                                    .FirstOrDefault(recI => recI.Id == rec.KitchenerId)?.KitchenerFIO
                })
                .ToList();
            return result;
        }

        public void CreateOrder(CustomerSelectionModel model)
        {
            int maxId = source.CustomerSelections.Count > 0 ? source.CustomerSelections.Max(rec => rec.Id) : 0;
            source.CustomerSelections.Add(new CustomerSelection
            {
                Id = maxId + 1,
                BuyerId = model.BuyerId,
                IngredientId = model.IngredientId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = CustomerSelectionCondition.Принят
            });
        }

        public void TakeOrderInWork(CustomerSelectionModel model)
        {
            CustomerSelection element = source.CustomerSelections.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            // смотрим по количеству компонентов на складах
            var ingredientElements = source.IngredientElements.Where(rec => rec.IngredientId == element.IngredientId);
            foreach (var ingredientElement in ingredientElements)
            {
                int countOnStocks = source.ProductStorageElement
                                            .Where(rec => rec.ElementId == ingredientElement.ElementId)
                                            .Sum(rec => rec.Count);
                if (countOnStocks < ingredientElement.Count * element.Count)
                {
                    var componentName = source.Elements
                                    .FirstOrDefault(rec => rec.Id == ingredientElement.ElementId);
                    throw new Exception("Не достаточно компонента " + componentName?.ElementName +
                        " требуется " + ingredientElement.Count + ", в наличии " + countOnStocks);
                }
            }
            // списываем
            foreach (var ingredientElement in ingredientElements)
            {
                int countOnStocks = ingredientElement.Count * element.Count;
                var productStorageElements = source.ProductStorageElement
                                            .Where(rec => rec.ElementId == ingredientElement.ElementId);
                foreach (var productStorageElement in productStorageElements)
                {
                    // компонентов на одном слкаде может не хватать
                    if (productStorageElement.Count >= countOnStocks)
                    {
                        productStorageElement.Count -= countOnStocks;
                        break;
                    }
                    else
                    {
                        countOnStocks -= productStorageElement.Count;
                        productStorageElement.Count = 0;
                    }
                }
            }
            element.KitchenerId = model.KitchenerId;
            element.DateImplement = DateTime.Now;
            element.Status = CustomerSelectionCondition.Готовиться;
        }

        public void FinishOrder(int id)
        {
            int index = -1;
            for (int i = 0; i < source.CustomerSelections.Count; ++i)
            {
                if (source.Buyers[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.CustomerSelections[index].Status = CustomerSelectionCondition.Готов;
        }

        public void PayOrder(int id)
        {
            int index = -1;
            for (int i = 0; i < source.CustomerSelections.Count; ++i)
            {
                if (source.Buyers[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.CustomerSelections[index].Status = CustomerSelectionCondition.Оплачен;
        }

        public void PutComponentOnStock(ProductStorageElementConnectingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.ProductStorageElement.Count; ++i)
            {
                if (source.ProductStorageElement[i].ProductStorageId == model.ProductStorageId &&
                    source.ProductStorageElement[i].ElementId == model.ElementId)
                {
                    source.ProductStorageElement[i].Count += model.Count;
                    return;
                }
                if (source.ProductStorageElement[i].Id > maxId)
                {
                    maxId = source.ProductStorageElement[i].Id;
                }
            }
            source.ProductStorageElement.Add(new ProductStorageElement
            {
                Id = ++maxId,
                ProductStorageId = model.ProductStorageId,
                ElementId = model.ElementId,
                Count = model.Count
            });
        }
    }
}
