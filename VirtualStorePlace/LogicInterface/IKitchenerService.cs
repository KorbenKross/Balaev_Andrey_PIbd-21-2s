using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.UserViewModel;

namespace VirtualStorePlace.LogicInterface
{
    public interface IKitchenerService
    {
        List<KitchenerUserViewModel> GetList();

        KitchenerUserViewModel GetElement(int id);

        void AddElement(KitchenerConnectingModel model);

        void UpdElement(KitchenerConnectingModel model);

        void DelElement(int id);
    }
}
