using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualStore;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.UserViewModel;

namespace VirtualStoreWeb.Controllers
{
    public class ElementController : Controller
    {
        public int Id { set { id = value; } }

        private IElementService service;

        private int? id;

        public ElementController(IElementService service)
        {
            this.service = service;
        }

        // GET: Element
        public ActionResult Element()
        {
            if (id.HasValue)
            {
                try
                {
                    ElementUserViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        var textBoxFIO = view.ElementName;
                        return View(textBoxFIO);
                    }
                }
                catch (Exception ex)
                {
                    return RedirectPermanent("/Element/ElementWarning");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Element(ElementUserViewModel objElement)
        {
            if (string.IsNullOrEmpty(objElement.ToString()))
            {
                return RedirectPermanent("/Element/ElementWarning");
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new ElementConnectingModel
                    {
                        Id = id.Value,
                        ElementName = objElement.ElementName
                    });
                }
                else
                {
                    service.AddElement(new ElementConnectingModel
                    {
                        ElementName = objElement.ElementName
                    });
                }
                return RedirectPermanent("/Elements/Elements");
            }
            catch (Exception ex)
            {
                return RedirectPermanent("/Element/ElementWarning");
            }
        }
    }
}