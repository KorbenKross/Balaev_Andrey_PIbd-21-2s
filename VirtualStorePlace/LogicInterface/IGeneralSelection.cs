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
    [CustomInterface("Интерфейс для работы с заказами")]
    public interface IGeneralSelection
    {
        [CustomMethod("Метод получения списка заказов")]
        List<CustomerSelectionUserViewModel> GetList();

        [CustomMethod("Метод создания заказа")]
        void CreateOrder(CustomerSelectionModel model);

        [CustomMethod("Метод передачи заказа в работу")]
        void TakeOrderInWork(CustomerSelectionModel model);

        [CustomMethod("Метод передачи заказа на оплату")]
        void FinishOrder(int id);

        [CustomMethod("Метод фиксирования оплаты по заказу")]
        void PayOrder(int id);

        [CustomMethod("Метод пополнения компонент на складе")]
        void PutComponentOnStock(ProductStorageElementConnectingModel model);
    }
}
