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
    public class BuyerSelectionList : IBuyerCustomer
    {
        private BaseListSingleton source;

        public BuyerSelectionList()
        {
            source = BaseListSingleton.GetInstance();
        }

        public List<BuyerUserViewModel> GetList()
        {
            List<BuyerUserViewModel> result = source.Buyers
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
            Buyer component = source.Buyers.FirstOrDefault(rec => rec.Id == id);
            if (component != null)
            {
                return new BuyerUserViewModel
                {
                    Id = component.Id,
                    BuyerFIO = component.BuyerFIO
                };
            }
            throw new Exception("Элемент не найден");
        }


        public void AddElement(BuyerConnectingModel model)
        {
            Buyer component = source.Buyers.FirstOrDefault(rec => rec.BuyerFIO == model.BuyerFIO);
            if (component != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            int maxId = source.Buyers.Count > 0 ? source.Buyers.Max(rec => rec.Id) : 0;
            source.Buyers.Add(new Buyer
            {
                Id = maxId + 1,
                BuyerFIO = model.BuyerFIO
            });
        }

        public void UpdElement(BuyerConnectingModel model)
        {
            Buyer component = source.Buyers.FirstOrDefault(rec =>
                                    rec.BuyerFIO == model.BuyerFIO && rec.Id != model.Id);
            if (component != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            component = source.Buyers.FirstOrDefault(rec => rec.Id == model.Id);
            if (component == null)
            {
                throw new Exception("Элемент не найден");
            }
            component.BuyerFIO = model.BuyerFIO;
        }

        public void DelElement(int id)
        {
            Buyer component = source.Buyers.FirstOrDefault(rec => rec.Id == id);
            if (component != null)
            {
                source.Buyers.Remove(component);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

    }
}
