using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.UserViewModel;
using VirtualStore;
using VirtualStorePlace.LogicInterface;

namespace VirtualStorePlace.RealiseInterfaceBD
{
    public class ElementServiceBD : IElementService
    {
        private AbstractDbContext context;

        public ElementServiceBD(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<ElementUserViewModel> GetList()
        {
            List<ElementUserViewModel> result = context.Elements
                .Select(rec => new ElementUserViewModel
                {
                    Id = rec.Id,
                    ElementName = rec.ElementName
                })
                .ToList();
            return result;
        }

        public ElementUserViewModel GetElement(int id)
        {
            Element element = context.Elements.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ElementUserViewModel
                {
                    Id = element.Id,
                    ElementName = element.ElementName
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ElementConnectingModel model)
        {
            Element element = context.Elements.FirstOrDefault(rec => rec.ElementName == model.ElementName);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            context.Elements.Add(new Element
            {
                ElementName = model.ElementName
            });
            context.SaveChanges();
        }

        public void UpdElement(ElementConnectingModel model)
        {
            Element element = context.Elements.FirstOrDefault(rec =>
                                        rec.ElementName == model.ElementName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            element = context.Elements.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ElementName = model.ElementName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Element element = context.Elements.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Elements.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
