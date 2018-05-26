using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualStorePlace.Attributies;
using VirtualStorePlace.UserViewModel;
using VirtualStorePlace.ConnectingModel;

namespace VirtualStorePlace.LogicInterface
{
    [CustomInterface("Интерфейс для работы с клиентами")]
    public interface IBuyerCustomer
    {
        [CustomMethod("Метод получения списка клиентов")]
        List<BuyerUserViewModel> GetList();

        [CustomMethod("Метод получения клиента по id")]
        BuyerUserViewModel GetElement(int id);
        
        [CustomMethod("Метод добавления клиента")]
        void AddElement(BuyerConnectingModel model);

        [CustomMethod("Метод изменения данных по клиенту")]
        void UpdElement(BuyerConnectingModel model);

        [CustomMethod("Метод удаления клиента")]
        void DelElement(int id);
    }
}
