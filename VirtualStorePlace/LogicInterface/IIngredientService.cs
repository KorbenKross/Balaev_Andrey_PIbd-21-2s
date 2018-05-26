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
    [CustomInterface("Интерфейс для работы с изделиями")]
    public interface IIngredientService
    {
        [CustomMethod("Метод получения списка изделий")]
        List<IngredientUserViewModel> GetList();

        [CustomMethod("Метод получения изделия по id")]
        IngredientUserViewModel GetElement(int id);

        [CustomMethod("Метод добавления изделия")]
        void AddElement(IngredientConnectingModel model);

        [CustomMethod("Метод изменения данных по изделию")]
        void UpdElement(IngredientConnectingModel model);

        [CustomMethod("Метод удаления изделия")]
        void DelElement(int id);
    }
}
