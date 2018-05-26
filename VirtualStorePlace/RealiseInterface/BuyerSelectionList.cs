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
            List<BuyerUserViewModel> result = new List<BuyerUserViewModel>();
            for (int i = 0; i < source.Buyers.Count; ++i)
            {
                result.Add(new BuyerUserViewModel
                {
                    Id = source.Buyers[i].Id,
                    BuyerFIO = source.Buyers[i].BuyerFIO
                });
            }
            return result;
        }

        public BuyerUserViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Buyers.Count; ++i)
            {
                if (source.Buyers[i].Id == id)
                {
                    return new BuyerUserViewModel
                    {
                        Id = source.Buyers[i].Id,
                        BuyerFIO = source.Buyers[i].BuyerFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(BuyerConnectingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Buyers.Count; ++i)
            {
                if (source.Buyers[i].Id > maxId)
                {
                    maxId = source.Buyers[i].Id;
                }
                if (source.Buyers[i].BuyerFIO == model.BuyerFIO)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            source.Buyers.Add(new Buyer
            {
                Id = maxId + 1,
                BuyerFIO = model.BuyerFIO
            });
        }

        public void UpdElement(BuyerConnectingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Buyers.Count; ++i)
            {
                if (source.Buyers[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Buyers[i].BuyerFIO == model.BuyerFIO &&
                    source.Buyers[i].Id != model.Id)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Buyers[index].BuyerFIO = model.BuyerFIO;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Buyers.Count; ++i)
            {
                if (source.Buyers[i].Id == id)
                {
                    source.Buyers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }


    }
}
