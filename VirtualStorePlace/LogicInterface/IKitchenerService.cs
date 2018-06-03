using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.UserViewModel;
using VirtualStorePlace.Attributies;

namespace VirtualStorePlace.LogicInterface
{
    [CustomInterface("Интерфейс для работы с работниками")]
    public interface IKitchenerService
    {
        [CustomMethod("Метод получения списка работников")]
        List<KitchenerUserViewModel> GetList();

        [CustomMethod("Метод получения работника по id")]
        KitchenerUserViewModel GetElement(int id);

        [CustomMethod("Метод добавления работника")]
        void AddElement(KitchenerConnectingModel model);

        [CustomMethod("Метод изменения данных по работнику")]
        void UpdElement(KitchenerConnectingModel model);

        [CustomMethod("Метод удаления работника")]
        void DelElement(int id);
    }
}
