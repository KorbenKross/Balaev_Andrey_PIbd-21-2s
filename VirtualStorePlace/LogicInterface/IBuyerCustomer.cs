using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirtualStorePlace.UserViewModel;
using VirtualStorePlace.ConnectingModel;

namespace VirtualStorePlace.LogicInterface
{
    public interface IBuyerCustomer
    {
        List<BuyerUserViewModel> GetList();

        BuyerUserViewModel GetElement(int id);

        void AddElement(BuyerConnectingModel model);

        void UpdElement(BuyerConnectingModel model);

        void DelElement(int id);
    }
}
