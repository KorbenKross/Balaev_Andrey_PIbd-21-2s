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
            List<KitchenerUserViewModel> result = source.Kitcheners
                .Select(rec => new KitchenerUserViewModel
                {
                    Id = rec.Id,
                    KitchenerFIO = rec.KitchenerFIO
                })
                .ToList();
            return result;
        }

        public KitchenerUserViewModel GetElement(int id)
        {
            Kitchener component = source.Kitcheners.FirstOrDefault(rec => rec.Id == id);
            if (component != null)
            {
                return new KitchenerUserViewModel
                {
                    Id = component.Id,
                    KitchenerFIO = component.KitchenerFIO
                };
            }
            throw new Exception("Элемент не найден");
        }


        public void AddElement(KitchenerConnectingModel model)
        {
            Kitchener component = source.Kitcheners.FirstOrDefault(rec => rec.KitchenerFIO == model.KitchenerFIO);
            if (component != null)
            {
                throw new Exception("Уже есть повар с таким ФИО");
            }
            int maxId = source.Kitcheners.Count > 0 ? source.Kitcheners.Max(rec => rec.Id) : 0;
            source.Kitcheners.Add(new Kitchener
            {
                Id = maxId + 1,
                KitchenerFIO = model.KitchenerFIO
            });
        }

        public void UpdElement(KitchenerConnectingModel model)
        {
            Kitchener component = source.Kitcheners.FirstOrDefault(rec =>
                                        rec.KitchenerFIO == model.KitchenerFIO && rec.Id != model.Id);
            if (component != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            component = source.Kitcheners.FirstOrDefault(rec => rec.Id == model.Id);
            if (component == null)
            {
                throw new Exception("Элемент не найден");
            }
            component.KitchenerFIO = model.KitchenerFIO;
        }

        public void DelElement(int id)
        {
            Kitchener component = source.Kitcheners.FirstOrDefault(rec => rec.Id == id);
            if (component != null)
            {
                source.Kitcheners.Remove(component);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
