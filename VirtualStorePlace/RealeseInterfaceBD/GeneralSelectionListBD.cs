﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.UserViewModel;
using VirtualStore;
using System.Data.Entity;

namespace VirtualStorePlace.RealeseInterfaceBD
{
    public class GeneralSelectionListBD : IGeneralSelection
    {
        private AbstractDbContext context;

        public GeneralSelectionListBD(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<CustomerSelectionUserViewModel> GetList()
        {
            List<CustomerSelectionUserViewModel> result = context.CustomerSelections
                .Select(rec => new CustomerSelectionUserViewModel
                {
                    Id = rec.Id,
                    BuyerId = rec.BuyerId,
                    IngredientId = rec.IngredientId,
                    KitchinerId = rec.KitchenerId,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.DateCreate),
                    DateCook = rec.DateImplement == null ? "" :
                                        SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " +
                                        SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " +
                                        SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Sum = rec.Sum,
                    BuyerFIO = rec.Buyer.BuyerFIO,
                    IngredientName = rec.Ingredient.IngredientName,
                    KitchinerName = rec.Kitchener.KitchenerFIO
                })
                .ToList();
            return result;
        }

        public void CreateOrder(CustomerSelectionModel model)
        {
            context.CustomerSelections.Add(new CustomerSelection
            {
                BuyerId = model.BuyerId,
                IngredientId = model.IngredientId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = CustomerSelectionCondition.Принят
            });
            context.SaveChanges();
        }

        public void TakeOrderInWork(CustomerSelectionModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    CustomerSelection element = context.CustomerSelections.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    var productComponents = context.IngredientElements
                                                .Include(rec => rec.Element)
                                                .Where(rec => rec.IngredientId == element.IngredientId);
                    // списываем
                    foreach (var productComponent in productComponents)
                    {
                        int countOnStocks = productComponent.Count * element.Count;
                        var stockComponents = context.ProductStorageElements
                                                    .Where(rec => rec.ElementId == productComponent.ElementId);
                        foreach (var stockComponent in stockComponents)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (stockComponent.Count >= countOnStocks)
                            {
                                stockComponent.Count -= countOnStocks;
                                countOnStocks = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStocks -= stockComponent.Count;
                                stockComponent.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStocks > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                                productComponent.Element.ElementName + " требуется " +
                                productComponent.Count + ", не хватает " + countOnStocks);
                        }
                    }
                    element.KitchenerId = model.KitchenerId;
                    element.DateImplement = DateTime.Now;
                    element.Status = CustomerSelectionCondition.Готовиться;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

 

        public void FinishOrder(int id)
        {
            CustomerSelection element = context.CustomerSelections.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = CustomerSelectionCondition.Готов;
            context.SaveChanges();
        }

        public void PayOrder(int id)
        {
            CustomerSelection element = context.CustomerSelections.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = CustomerSelectionCondition.Оплачен;
            context.SaveChanges();
        }

        public void PutComponentOnStock(ProductStorageElementConnectingModel model)
        {
            ProductStorageElement element = context.ProductStorageElements
                                                .FirstOrDefault(rec => rec.ProductStorageId == model.ProductStorageId &&
                                                                    rec.ElementId == model.ElementId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.ProductStorageElements.Add(new ProductStorageElement
                {
                    ProductStorageId = model.ProductStorageId,
                    ElementId = model.ElementId,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }
    }
}
