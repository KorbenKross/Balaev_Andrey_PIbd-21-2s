using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.UserViewModel;
using VirtualStore;

namespace VirtualStorePlace.RealeseInterfaceBD
{
    public class BuyerSelectionListBD : IBuyerCustomer
    {
        private AbstractDbContext context;

        public BuyerSelectionListBD(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<BuyerUserViewModel> GetList()
        {
            List<BuyerUserViewModel> result = context.Buyers
                .Select(rec => new BuyerUserViewModel
                {
                    Id = rec.Id,
                    BuyerFIO = rec.BuyerFIO
                })
                .ToList();
            return result;
        }

        public BuyerUserViewModel GetElement(int id)
        {
            Buyer element = context.Buyers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new BuyerUserViewModel
                {
                    Id = element.Id,
                    BuyerFIO = element.BuyerFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(BuyerConnectingModel model)
        {
            Buyer element = context.Buyers.FirstOrDefault(rec => rec.BuyerFIO == model.BuyerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Buyers.Add(new Buyer
            {
                BuyerFIO = model.BuyerFIO
            });
            context.SaveChanges();
        }

        public void UpdElement(BuyerConnectingModel model)
        {
            Buyer element = context.Buyers.FirstOrDefault(rec =>
                                    rec.BuyerFIO == model.BuyerFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Buyers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.BuyerFIO = model.BuyerFIO;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Buyer element = context.Buyers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Buyers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
