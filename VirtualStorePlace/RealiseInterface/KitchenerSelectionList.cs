using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.UserViewModel;
using VirtualStore;

namespace VirtualStorePlace.RealiseInterface
{
    public class KitchenerSelectionList : IKitchenerService
    {

        private BaseListSingleton source;

        public KitchenerSelectionList()
        {
            source = BaseListSingleton.GetInstance();
        }

        public List<KitchenerUserViewModel> GetList()
        {
            List<KitchenerUserViewModel> result = new List<KitchenerUserViewModel>();
            for (int i = 0; i < source.Kitcheners.Count; ++i)
            {
                result.Add(new KitchenerUserViewModel
                {
                    Id = source.Kitcheners[i].Id,
                    KitchenerFIO = source.Kitcheners[i].KitchenerFIO
                });
            }
            return result;
        }

        public KitchenerUserViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Kitcheners.Count; ++i)
            {
                if (source.Kitcheners[i].Id == id)
                {
                    return new KitchenerUserViewModel
                    {
                        Id = source.Kitcheners[i].Id,
                        KitchenerFIO = source.Kitcheners[i].KitchenerFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(KitchenerConnectingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Kitcheners.Count; ++i)
            {
                if (source.Kitcheners[i].Id > maxId)
                {
                    maxId = source.Kitcheners[i].Id;
                }
                if (source.Kitcheners[i].KitchenerFIO == model.KitchenerFIO)
                {
                    throw new Exception("Уже есть сотрудник с таким ФИО");
                }
            }
            source.Kitcheners.Add(new Kitchener
            {
                Id = maxId + 1,
                KitchenerFIO = model.KitchenerFIO
            });
        }

        public void UpdElement(KitchenerConnectingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Kitcheners.Count; ++i)
            {
                if (source.Kitcheners[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Kitcheners[i].KitchenerFIO == model.KitchenerFIO &&
                    source.Kitcheners[i].Id != model.Id)
                {
                    throw new Exception("Уже есть сотрудник с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Kitcheners[index].KitchenerFIO = model.KitchenerFIO;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Kitcheners.Count; ++i)
            {
                if (source.Kitcheners[i].Id == id)
                {
                    source.Kitcheners.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
