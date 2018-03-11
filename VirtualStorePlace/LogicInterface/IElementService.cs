using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.UserViewModel;

namespace VirtualStorePlace.LogicInterface
{
    public interface IElementService
    {
        List<ElementUserViewModel> GetList();

        ElementUserViewModel GetElement(int id);

        void AddElement(ElementConnectingModel model);

        void UpdElement(ElementConnectingModel model);

        void DelElement(int id);
    }
}
