using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.UserViewModel;
using VirtualStore;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.UserViewModel;
using VirtualStore;
using VirtualStorePlace.LogicInterface;

namespace VirtualStorePlace.RealiseInterfaceBD
{
    public class KitchenerServiceBD : IKitchenerService
    {
        private AbstractDbContext context;

        public KitchenerServiceBD(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<KitchenerUserViewModel> GetList()
        {
            List<KitchenerUserViewModel> result = context.Kitcheners
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
            Kitchener element = context.Kitcheners.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new KitchenerUserViewModel
                {
                    Id = element.Id,
                    KitchenerFIO = element.KitchenerFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(KitchenerConnectingModel model)
        {
            Kitchener element = context.Kitcheners.FirstOrDefault(rec => rec.KitchenerFIO == model.KitchenerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            context.Kitcheners.Add(new Kitchener
            {
                KitchenerFIO = model.KitchenerFIO
            });
            context.SaveChanges();
        }

        public void UpdElement(KitchenerConnectingModel model)
        {
            Kitchener element = context.Kitcheners.FirstOrDefault(rec =>
                                        rec.KitchenerFIO == model.KitchenerFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            element = context.Kitcheners.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.KitchenerFIO = model.KitchenerFIO;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Kitchener element = context.Kitcheners.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Kitcheners.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
