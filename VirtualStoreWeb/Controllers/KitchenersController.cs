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
    public class KitchenersController : Controller
    {
        private IKitchenerService service;

        public KitchenersController(IKitchenerService service)
        {
            this.service = service;
        }
        // GET: Kitcheners
        public ActionResult Kitcheners()
        {
            LoadData();
            return View();
        }

        private void LoadData()
        {
            try
            {
                List<KitchenerUserViewModel> list = service.GetList();
                if (list != null)
                {
                    ViewBag.DataList = list;
                }
                else
                {
                    ViewBag.DataList = null;
                }
            }
            catch (Exception ex)
            {
                Redirect("/Kitcheners/WarningKitcheners");
            }
        }

        public ActionResult WarningKitcheners()
        {
            return View();
        }


        [HttpPost]
        public RedirectResult Kitcheners(string action, string kitchenerList)
        {
            switch (action)
            {
                case "Add":
                    return KitchenerResult();
                case "Change":
                    break;
                case "Delete":
                    int id = Convert.ToInt32(kitchenerList);
                    try
                    {
                        service.DelElement(id);
                    }
                    catch (Exception ex)
                    {
                        return RedirectPermanent("/Kitcheners/WarningKitcheners");
                    }
                    LoadData();
                    return RedirectPermanent("/Kitcheners/Kitcheners");
                    break;
                case "Update":
                    return KitchenersResult();
            }
            return WarningResult();

        }

        public ActionResult WarningBuyer()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public RedirectResult KitchenerResult()
        {
            return RedirectPermanent("/Kitchener/Kitchener");
        }

        public RedirectResult WarningResult()
        {
            return RedirectPermanent("/Kitcheners/WarningKitcheners");
        }

        public RedirectResult KitchenersResult()
        {
            return RedirectPermanent("/Kitchener/Kitchener");
        }

    }
}