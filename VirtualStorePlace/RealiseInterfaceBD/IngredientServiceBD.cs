using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.UserViewModel;
using VirtualStore;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.UserViewModel;
using VirtualStore;
using VirtualStorePlace.LogicInterface;

namespace VirtualStorePlace.RealiseInterfaceBD
{
    public class IngredientServiceBD : IIngredientService
    {
        private AbstractDbContext context;

        public IngredientServiceBD(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<IngredientUserViewModel> GetList()
        {
            List<IngredientUserViewModel> result = context.Ingredients
                .Select(rec => new IngredientUserViewModel
                {
                    Id = rec.Id,
                    IngredientName = rec.IngredientName,
                    Price = rec.Cost,
                    IngredientElement = context.IngredientElements
                            .Where(recPC => recPC.IngredientId == rec.Id)
                            .Select(recPC => new IngredientElementUserViewModel
                            {
                                Id = recPC.Id,
                                IngredientId = recPC.IngredientId,
                                ElementId = recPC.ElementId,
                                ElementName = recPC.Element.ElementName,
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public IngredientUserViewModel GetElement(int id)
        {
            Ingredient element = context.Ingredients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new IngredientUserViewModel
                {
                    Id = element.Id,
                    IngredientName = element.IngredientName,
                    Price = element.Cost,
                    IngredientElement = context.IngredientElements
                            .Where(recPC => recPC.IngredientId == element.Id)
                            .Select(recPC => new IngredientElementUserViewModel
                            {
                                Id = recPC.Id,
                                IngredientId = recPC.IngredientId,
                                ElementId = recPC.ElementId,
                                ElementName = recPC.Element.ElementName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(IngredientConnectingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Ingredient element = context.Ingredients.FirstOrDefault(rec => rec.IngredientName == model.IngredientName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new Ingredient
                    {
                        IngredientName = model.IngredientName,
                        Cost = model.Value
                    };
                    context.Ingredients.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupComponents = model.IngredientElement
                                                .GroupBy(rec => rec.ElementId)
                                                .Select(rec => new
                                                {
                                                    ComponentId = rec.Key,
                                                    Count = rec.Sum(r => r.Count)
                                                });
                    // добавляем компоненты
                    foreach (var groupComponent in groupComponents)
                    {
                        context.IngredientElements.Add(new IngredientElement
                        {
                            IngredientId = element.Id,
                            ElementId = groupComponent.ComponentId,
                            Count = groupComponent.Count
                        });
                        context.SaveChanges();
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

        public void UpdElement(IngredientConnectingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Ingredient element = context.Ingredients.FirstOrDefault(rec =>
                                        rec.IngredientName == model.IngredientName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Ingredients.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.IngredientName = model.IngredientName;
                    element.Cost = model.Value;
                    context.SaveChanges();

                    // обновляем существуюущие компоненты
                    var compIds = model.IngredientElement.Select(rec => rec.ElementId).Distinct();
                    var updateComponents = context.IngredientElements
                                                    .Where(rec => rec.IngredientId == model.Id &&
                                                        compIds.Contains(rec.ElementId));
                    foreach (var updateComponent in updateComponents)
                    {
                        updateComponent.Count = model.IngredientElement
                                                        .FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
                    }
                    context.SaveChanges();
                    context.IngredientElements.RemoveRange(
                                        context.IngredientElements.Where(rec => rec.IngredientId == model.Id &&
                                                                            !compIds.Contains(rec.ElementId)));
                    context.SaveChanges();
                    // новые записи
                    var groupComponents = model.IngredientElement
                                                .Where(rec => rec.Id == 0)
                                                .GroupBy(rec => rec.ElementId)
                                                .Select(rec => new
                                                {
                                                    ComponentId = rec.Key,
                                                    Count = rec.Sum(r => r.Count)
                                                });
                    foreach (var groupComponent in groupComponents)
                    {
                        IngredientElement elementPC = context.IngredientElements
                                                .FirstOrDefault(rec => rec.IngredientId == model.Id &&
                                                                rec.ElementId == groupComponent.ComponentId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupComponent.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.IngredientElements.Add(new IngredientElement
                            {
                                IngredientId = model.Id,
                                ElementId = groupComponent.ComponentId,
                                Count = groupComponent.Count
                            });
                            context.SaveChanges();
                        }
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

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Ingredient element = context.Ingredients.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.IngredientElements.RemoveRange(
                                            context.IngredientElements.Where(rec => rec.IngredientId == id));
                        context.Ingredients.Remove(element);
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
