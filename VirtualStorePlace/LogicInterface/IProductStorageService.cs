using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.UserViewModel;

namespace VirtualStorePlace.LogicInterface
{
    public interface IProductStorageService
    {
        List<ProductStorageUserViewModel> GetList();

        ProductStorageUserViewModel GetElement(int id);

        void AddElement(ProductStorageConnectingModel model);

        void UpdElement(ProductStorageConnectingModel model);

        void DelElement(int id);
    }
}
