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
            List<CustomerSelectionUserViewModel> result = new List<CustomerSelectionUserViewModel>();
            for (int i = 0; i < source.CustomerSelections.Count; ++i)
            {
                string buyerFIO = string.Empty;
                for (int j = 0; j < source.Buyers.Count; ++j)
                {
                    if (source.Buyers[j].Id == source.CustomerSelections[i].BuyerId)
                    {
                        buyerFIO = source.Buyers[j].BuyerFIO;
                        break;
                    }
                }
                string ingredientName = string.Empty;
                for (int j = 0; j < source.Ingredients.Count; ++j)
                {
                    if (source.Ingredients[j].Id == source.CustomerSelections[i].IngredientId)
                    {
                        ingredientName = source.Ingredients[j].IngredientName;
                        break;
                    }
                }
                string kitchenerFIO = string.Empty;
                if (source.CustomerSelections[i].KitchenerId.HasValue)
                {
                    for (int j = 0; j < source.Kitcheners.Count; ++j)
                    {
                        if (source.Kitcheners[j].Id == source.CustomerSelections[i].KitchenerId.Value)
                        {
                            kitchenerFIO = source.Kitcheners[j].KitchenerFIO;
                            break;
                        }
                    }
                }
                result.Add(new CustomerSelectionUserViewModel
                {
                    Id = source.CustomerSelections[i].Id,
                    BuyerId = source.CustomerSelections[i].BuyerId,
                    BuyerFIO = buyerFIO,
                    IngredientId = source.CustomerSelections[i].IngredientId,
                    IngredientName = ingredientName,
                    KitchinerId = source.CustomerSelections[i].KitchenerId,
                    KitchinerName = kitchenerFIO,
                    Count = source.CustomerSelections[i].Count,
                    Sum = source.CustomerSelections[i].Sum,
                    DateCreate = source.CustomerSelections[i].DateCreate.ToLongDateString(),
                    DateCook = source.CustomerSelections[i].DateImplement?.ToLongDateString(),
                    Status = source.CustomerSelections[i].Status.ToString()
                });
            }
            return result;
        }

        public void CreateOrder(CustomerSelectionModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.CustomerSelections.Count; ++i)
            {
                if (source.CustomerSelections[i].Id > maxId)
                {
                    maxId = source.Buyers[i].Id;
                }
            }
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
            int index = -1;
            for (int i = 0; i < source.CustomerSelections.Count; ++i)
            {
                if (source.CustomerSelections[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            // смотрим по количеству компонентов на складах
            for (int i = 0; i < source.IngredientElements.Count; ++i)
            {
                if (source.IngredientElements[i].IngredientId == source.CustomerSelections[index].IngredientId)
                {
                    int countOnStocks = 0;
                    for (int j = 0; j < source.ProductStorageElement.Count; ++j)
                    {
                        if (source.ProductStorageElement[j].ElementId == source.IngredientElements[i].ElementId)
                        {
                            countOnStocks += source.ProductStorageElement[j].Count;
                        }
                    }
                    if (countOnStocks < source.IngredientElements[i].Count * source.CustomerSelections[index].Count)
                    {
                        for (int j = 0; j < source.Elements.Count; ++j)
                        {
                            if (source.Elements[j].Id == source.IngredientElements[i].ElementId)
                            {
                                throw new Exception("Не достаточно компонента " + source.Elements[j].ElementName +
                                    " требуется " + source.IngredientElements[i].Count + ", в наличии " + countOnStocks);
                            }
                        }
                    }
                }
            }
            // списываем
            for (int i = 0; i < source.IngredientElements.Count; ++i)
            {
                if (source.IngredientElements[i].IngredientId == source.CustomerSelections[index].IngredientId)
                {
                    int countOnStocks = source.IngredientElements[i].Count * source.CustomerSelections[index].Count;
                    for (int j = 0; j < source.ProductStorageElement.Count; ++j)
                    {
                        if (source.ProductStorageElement[j].ElementId == source.IngredientElements[i].ElementId)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (source.ProductStorageElement[j].Count >= countOnStocks)
                            {
                                source.ProductStorageElement[j].Count -= countOnStocks;
                                break;
                            }
                            else
                            {
                                countOnStocks -= source.ProductStorageElement[j].Count;
                                source.ProductStorageElement[j].Count = 0;
                            }
                        }
                    }
                }
            }
            source.CustomerSelections[index].KitchenerId = model.KitchenerId;
            source.CustomerSelections[index].DateImplement = DateTime.Now;
            source.CustomerSelections[index].Status = CustomerSelectionCondition.Готовиться;
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
