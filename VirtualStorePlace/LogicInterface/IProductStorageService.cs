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
    [CustomInterface("Интерфейс для работы со складами")]
    public interface IProductStorageService
    {
        [CustomMethod("Метод получения списка складов")]
        List<ProductStorageUserViewModel> GetList();

        [CustomMethod("Метод получения склада по id")]
        ProductStorageUserViewModel GetElement(int id);

        [CustomMethod("Метод добавления склада")]
        void AddElement(ProductStorageConnectingModel model);

        [CustomMethod("Метод изменения данных по складу")]
        void UpdElement(ProductStorageConnectingModel model);

        [CustomMethod("Метод удаления склада")]
        void DelElement(int id);
    }
}
