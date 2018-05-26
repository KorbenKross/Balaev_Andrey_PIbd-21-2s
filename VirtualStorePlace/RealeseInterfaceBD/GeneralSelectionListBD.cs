using System;
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
using System.Net.Mail;
using System.Net;
using System.Configuration;

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
            var order = new CustomerSelection
            {
                BuyerId = model.BuyerId,
                IngredientId = model.IngredientId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = CustomerSelectionCondition.Принят
            };
            context.CustomerSelections.Add(order);
            context.SaveChanges();
            var client = context.Buyers.FirstOrDefault(x => x.Id == model.BuyerId);
            SendEmail(client.Mail, "Оповещение по заказам",
                string.Format("Заказ №{0} от {1} создан успешно", order.Id,
                order.DateCreate.ToShortDateString()));
        }

        public void TakeOrderInWork(CustomerSelectionModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    CustomerSelection element = context.CustomerSelections.Include(rec => rec.Buyer).FirstOrDefault(rec => rec.Id == model.Id);
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
                    SendEmail(element.Buyer.Mail, "Оповещение по заказам",
                        string.Format("Заказ №{0} от {1} передеан в работу", element.Id, element.DateCreate.ToShortDateString()));
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
            CustomerSelection element = context.CustomerSelections.Include(rec => rec.Buyer).FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = CustomerSelectionCondition.Готов;
            context.SaveChanges();
            SendEmail(element.Buyer.Mail, "Оповещение по заказам",
                string.Format("Заказ №{0} от {1} передан на оплату", element.Id,
                element.DateCreate.ToShortDateString()));
        }

        public void PayOrder(int id)
        {
            CustomerSelection element = context.CustomerSelections.Include(rec => rec.Buyer).FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = CustomerSelectionCondition.Оплачен;
            context.SaveChanges();
            SendEmail(element.Buyer.Mail, "Оповещение по заказам",
                string.Format("Заказ №{0} от {1} оплачен успешно", element.Id, element.DateCreate.ToShortDateString()));
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

        private void SendEmail(string mailAddress, string subject, string text)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;

            try
            {
                objMailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;

                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailLogin"],
                    ConfigurationManager.AppSettings["MailPassword"]);

                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }
    }
}
