using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.UserViewModel;

namespace VirtualStorePlace.LogicInterface
{
    public interface IIngredientService
    {
        List<IngredientUserViewModel> GetList();

        IngredientUserViewModel GetElement(int id);

        void AddElement(IngredientConnectingModel model);

        void UpdElement(IngredientConnectingModel model);

        void DelElement(int id);
    }
}
