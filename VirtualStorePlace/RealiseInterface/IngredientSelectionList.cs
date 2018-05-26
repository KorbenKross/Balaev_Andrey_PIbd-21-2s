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
    public class IngredientSelectionList : IIngredientService
    {
        private BaseListSingleton source;

        public IngredientSelectionList()
        {
            source = BaseListSingleton.GetInstance();
        }

        public List<IngredientUserViewModel> GetList()
        {
            List<IngredientUserViewModel> result = new List<IngredientUserViewModel>();
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<IngredientElementUserViewModel> ingredientElements = new List<IngredientElementUserViewModel>();
                //////////////////////////////////////////////////////////
                for (int j = 0; j < source.IngredientElements.Count; ++j)
                {
                    if (source.IngredientElements[j].IngredientId == source.Ingredients[i].Id)
                    {
                        string componentName = string.Empty;
                        for (int k = 0; k < source.Elements.Count; ++k)
                        {
                            if (source.IngredientElements[j].ElementId == source.Elements[k].Id)
                            {
                                componentName = source.Elements[k].ElementName;
                                break;
                            }
                        }
                        ingredientElements.Add(new IngredientElementUserViewModel
                        {
                            Id = source.IngredientElements[j].Id,
                            IngredientId = source.IngredientElements[j].IngredientId,
                            ElementId = source.IngredientElements[j].ElementId,
                            ElementName = componentName,
                            Count = source.IngredientElements[j].Count
                        });
                    }
                }
                result.Add(new IngredientUserViewModel
                {
                    Id = source.Ingredients[i].Id,
                    IngredientName = source.Ingredients[i].IngredientName,
                    Price = source.Ingredients[i].Cost,
                    IngredientElement = ingredientElements
                });
            }
            return result;
        }
        public void AddElement(IngredientConnectingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                if (source.Ingredients[i].Id > maxId)
                {
                    maxId = source.Ingredients[i].Id;
                }
                if (source.Ingredients[i].IngredientName == model.IngredientName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Ingredients.Add(new Ingredient
            {
                Id = maxId + 1,
                IngredientName = model.IngredientName,
                Cost = model.Value
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.IngredientElements.Count; ++i)
            {
                if (source.IngredientElements[i].Id > maxPCId)
                {
                    maxPCId = source.IngredientElements[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.IngredientElement.Count; ++i)
            {
                for (int j = 1; j < model.IngredientElement.Count; ++j)
                {
                    if (model.IngredientElement[i].ElementId ==
                        model.IngredientElement[j].ElementId)
                    {
                        model.IngredientElement[i].Count +=
                            model.IngredientElement[j].Count;
                        model.IngredientElement.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.IngredientElement.Count; ++i)
            {
                source.IngredientElements.Add(new IngredientElement
                {
                    Id = ++maxPCId,
                    IngredientId = maxId + 1,
                    ElementId = model.IngredientElement[i].ElementId,
                    Count = model.IngredientElement[i].Count
                });
            }
        }

        public void UpdElement(IngredientConnectingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                if (source.Ingredients[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Ingredients[i].IngredientName == model.IngredientName &&
                    source.Ingredients[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Ingredients[index].IngredientName = model.IngredientName;
            source.Ingredients[index].Cost = model.Value;
            int maxPCId = 0;
            for (int i = 0; i < source.IngredientElements.Count; ++i)
            {
                if (source.IngredientElements[i].Id > maxPCId)
                {
                    maxPCId = source.IngredientElements[i].Id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.IngredientElements.Count; ++i)
            {
                if (source.IngredientElements[i].IngredientId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.IngredientElement.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.IngredientElements[i].Id == model.IngredientElement[j].Id)
                        {
                            source.IngredientElements[i].Count = model.IngredientElement[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.IngredientElements.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.IngredientElement.Count; ++i)
            {
                if (model.IngredientElement[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.IngredientElements.Count; ++j)
                    {
                        if (source.IngredientElements[j].IngredientId == model.Id &&
                            source.IngredientElements[j].ElementId == model.IngredientElement[i].ElementId)
                        {
                            source.IngredientElements[j].Count += model.IngredientElement[i].Count;
                            model.IngredientElement[i].Id = source.IngredientElements[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.IngredientElement[i].Id == 0)
                    {
                        source.IngredientElements.Add(new IngredientElement
                        {
                            Id = ++maxPCId,
                            IngredientId = model.Id,
                            ElementId = model.IngredientElement[i].ElementId,
                            Count = model.IngredientElement[i].Count
                        });
                    }
                }
            }
        }

        public IngredientUserViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<IngredientElementUserViewModel> ingredientElements = new List<IngredientElementUserViewModel>();
                for (int j = 0; j < source.IngredientElements.Count; ++j)
                {
                    if (source.IngredientElements[j].IngredientId == source.Ingredients[i].Id)
                    {
                        string elementName = string.Empty;
                        for (int k = 0; k < source.Elements.Count; ++k)
                        {
                            if (source.IngredientElements[j].IngredientId == source.Elements[k].Id)
                            {
                                elementName = source.Elements[k].ElementName;
                                break;
                            }
                        }
                        ingredientElements.Add(new IngredientElementUserViewModel
                        {
                            Id = source.IngredientElements[j].Id,
                            IngredientId = source.IngredientElements[j].IngredientId,
                            ElementId = source.IngredientElements[j].IngredientId,
                            ElementName = elementName,
                            Count = source.IngredientElements[j].Count
                        });
                    }
                }
                if (source.Ingredients[i].Id == id)
                {
                    return new IngredientUserViewModel
                    {
                        Id = source.Ingredients[i].Id,
                        IngredientName = source.Ingredients[i].IngredientName,
                        Price = source.Ingredients[i].Cost,
                        IngredientElement = ingredientElements
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.IngredientElements.Count; ++i)
            {
                if (source.IngredientElements[i].IngredientId == id)
                {
                    source.IngredientElements.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                if (source.Ingredients[i].Id == id)
                {
                    source.Ingredients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
