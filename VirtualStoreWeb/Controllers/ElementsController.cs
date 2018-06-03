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
    public class ElementsController : Controller
    {
        private  IElementService service;

        public ElementsController(IElementService service)
        {
            this.service = service;
        }
        // GET: Elements
        public ActionResult Elements()
        {
            LoadData();
            return View();
        }

        [HttpPost]
        public ActionResult Elements(string action, string elementList)
        {
            switch (action)
            {
                case "Add":
                    return ElementResult();
                case "Change":
                    break;
                case "Delete":
                    int id = Convert.ToInt32(elementList);
                    try
                    {
                        service.DelElement(id);
                    }
                    catch (Exception ex)
                    {
                        return RedirectPermanent("/Elements/ElementsWarning");
                    }
                    LoadData();
                    return RedirectPermanent("/Elements/Elements");
                    break;
                case "Update":
                    return BuyerResult();
            }
            return WarningResult();
        }

        public ActionResult ElementsWarning()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult OkElements()
        {
            return View();
        }

        public RedirectResult ElementResult()
        {
            return RedirectPermanent("/Element/Element");
        }

        public RedirectResult WarningResult()
        {
            return RedirectPermanent("/Elements/ElementsWarning");
        }

        public RedirectResult BuyerResult()
        {
            return RedirectPermanent("/Elements/Elements");
        }

        private void LoadData()
        {
            try
            {
                List<ElementUserViewModel> list = service.GetList();
                ViewBag.DataList = list;
            }
            catch (Exception ex)
            {
                View(ElementsWarning());
            }
        }
        
    }
}