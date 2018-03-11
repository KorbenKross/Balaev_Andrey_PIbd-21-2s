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
            List<IngredientUserViewModel> result = source.Ingredients
                .Select(rec => new IngredientUserViewModel
                {
                    Id = rec.Id,
                    IngredientName = rec.IngredientName,
                    Price = rec.Cost,
                    IngredientElement = source.IngredientElements
                            .Where(recPC => recPC.IngredientId == rec.Id)
                            .Select(recPC => new IngredientElementUserViewModel
                            {
                                Id = recPC.Id,
                                IngredientId = recPC.IngredientId,
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

        public IngredientUserViewModel GetElement(int id)
        {
            Ingredient component = source.Ingredients.FirstOrDefault(rec => rec.Id == id);
            if (component != null)
            {
                return new IngredientUserViewModel
                {
                    Id = component.Id,
                    IngredientName = component.IngredientName,
                    Price = component.Cost,
                    IngredientElement = source.IngredientElements
                            .Where(recPC => recPC.IngredientId == component.Id)
                            .Select(recPC => new IngredientElementUserViewModel
                            {
                                Id = recPC.Id,
                                IngredientId = recPC.IngredientId,
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

        public void AddElement(IngredientConnectingModel model)
        {
            Ingredient component = source.Ingredients.FirstOrDefault(rec => rec.IngredientName == model.IngredientName);
            if (component != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.Ingredients.Count > 0 ? source.Ingredients.Max(rec => rec.Id) : 0;
            source.Ingredients.Add(new Ingredient
            {
                Id = maxId + 1,
                IngredientName = model.IngredientName,
                Cost = model.Value
            });
            // компоненты для изделия
            int maxPCId = source.IngredientElements.Count > 0 ?
                                    source.IngredientElements.Max(rec => rec.Id) : 0;
            // убираем дубли по компонентам
            var groupComponents = model.IngredientElement
                                        .GroupBy(rec => rec.ElementId)
                                        .Select(rec => new
                                        {
                                            ElementId = rec.Key,
                                            Count = rec.Sum(r => r.Count)
                                        });
            // добавляем компоненты
            foreach (var groupComponent in groupComponents)
            {
                source.IngredientElements.Add(new IngredientElement
                {
                    Id = ++maxPCId,
                    IngredientId = maxId + 1,
                    ElementId = groupComponent.ElementId,
                    Count = groupComponent.Count
                });
            }
        }

        public void UpdElement(IngredientConnectingModel model)
        {
            Ingredient component = source.Ingredients.FirstOrDefault(rec =>
                                        rec.IngredientName == model.IngredientName && rec.Id != model.Id);
            if (component != null)
            {
                throw new Exception("Уже есть суши с таким названием");
            }
            component = source.Ingredients.FirstOrDefault(rec => rec.Id == model.Id);
            if (component == null)
            {
                throw new Exception("Компонент не найден");
            }
            component.IngredientName = model.IngredientName;
            component.Cost = model.Value;

            int maxPCId = source.IngredientElements.Count > 0 ? source.IngredientElements.Max(rec => rec.Id) : 0;
            // обновляем существуюущие компоненты
            var compIds = model.IngredientElement.Select(rec => rec.ElementId).Distinct();
            var updateComponents = source.IngredientElements
                                            .Where(rec => rec.IngredientId == model.Id &&
                                           compIds.Contains(rec.ElementId));
            foreach (var updateComponent in updateComponents)
            {
                updateComponent.Count = model.IngredientElement
                                                .FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
            }
            source.IngredientElements.RemoveAll(rec => rec.IngredientId == model.Id &&
                                       !compIds.Contains(rec.ElementId));
            // новые записи
            var groupComponents = model.IngredientElement
                                        .Where(rec => rec.Id == 0)
                                        .GroupBy(rec => rec.ElementId)
                                        .Select(rec => new
                                        {
                                            ElementId = rec.Key,
                                            Count = rec.Sum(r => r.Count)
                                        });
            foreach (var groupComponent in groupComponents)
            {
                IngredientElement elementPC = source.IngredientElements
                                        .FirstOrDefault(rec => rec.IngredientId == model.Id &&
                                                        rec.ElementId == groupComponent.ElementId);
                if (elementPC != null)
                {
                    elementPC.Count += groupComponent.Count;
                }
                else
                {
                    source.IngredientElements.Add(new IngredientElement
                    {
                        Id = ++maxPCId,
                        IngredientId = model.Id,
                        ElementId = groupComponent.ElementId,
                        Count = groupComponent.Count
                    });
                }
            }
        }

        public void DelElement(int id)
        {
            Ingredient component = source.Ingredients.FirstOrDefault(rec => rec.Id == id);
            if (component != null)
            {
                // удаяем записи по компонентам при удалении изделия
                source.IngredientElements.RemoveAll(rec => rec.IngredientId == id);
                source.Ingredients.Remove(component);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
