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
    public class ElementSelectionList : IElementService
    {
        private BaseListSingleton source;

        public ElementSelectionList()
        {
            source = BaseListSingleton.GetInstance();
        }

        public List<ElementUserViewModel> GetList()
        {
            List<ElementUserViewModel> result = source.Elements
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
            Element component = source.Elements.FirstOrDefault(rec => rec.Id == id);
            if (component != null)
            {
                return new ElementUserViewModel
                {
                    Id = component.Id,
                    ElementName = component.ElementName
                };
            }
            throw new Exception("Элемент не найден");
        }


        public void AddElement(ElementConnectingModel model)
        {
            Element component = source.Elements.FirstOrDefault(rec => rec.ElementName == model.ElementName);
            if (component != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            int maxId = source.Elements.Count > 0 ? source.Elements.Max(rec => rec.Id) : 0;
            source.Elements.Add(new Element
            {
                Id = maxId + 1,
                ElementName = model.ElementName
            });
        }

        public void UpdElement(ElementConnectingModel model)
        {
            Element component = source.Elements.FirstOrDefault(rec =>
                                        rec.ElementName == model.ElementName && rec.Id != model.Id);
            if (component != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            component = source.Elements.FirstOrDefault(rec => rec.Id == model.Id);
            if (component == null)
            {
                throw new Exception("Элемент не найден");
            }
            component.ElementName = model.ElementName;
        }

        public void DelElement(int id)
        {
            Element component = source.Elements.FirstOrDefault(rec => rec.Id == id);
            if (component != null)
            {
                source.Elements.Remove(component);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
