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

    [CustomInterface("Интерфейс для работы с компонентами")]
    public interface IElementService
    {
        [CustomMethod("Метод получения списка компонент")]
        List<ElementUserViewModel> GetList();

        [CustomMethod("Метод получения компонента по id")]
        ElementUserViewModel GetElement(int id);

        [CustomMethod("Метод добавления компонента")]
        void AddElement(ElementConnectingModel model);

        [CustomMethod("Метод изменения данных по компоненту")]
        void UpdElement(ElementConnectingModel model);

        [CustomMethod("Метод удаления компонента")]
        void DelElement(int id);
    }
}
