using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.UserViewModel;

namespace VirtualStorePlace.LogicInterface
{
    public interface IGeneralSelection
    {
        List<CustomerSelectionUserViewModel> GetList();

        void CreateOrder(CustomerSelectionModel model);

        void TakeOrderInWork(CustomerSelectionModel model);

        void FinishOrder(int id);

        void PayOrder(int id);

        void PutComponentOnStock(ProductStorageElementConnectingModel model);
    }
}
