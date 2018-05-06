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
    public class KitchenerController : Controller
    {
        public int Id { set { id = value; } }

        private IKitchenerService service;

        private int? id;

        public KitchenerController(IKitchenerService service)
        {
            this.service = service;
        }
        // GET: Kitchener
        public ActionResult Kitchener()
        {
            if (id.HasValue)
            {
                try
                {
                    KitchenerUserViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        //Исправить. var не нужен, нужно поле класса
                        var textBoxFIO = view.KitchenerFIO;                         
                        return View(textBoxFIO);
                    }
                }
                catch (Exception ex)
                {
                    return RedirectPermanent("/Kitchener/WarningKitchener");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Kitchener(KitchenerUserViewModel objKitchener)
        {
            if (string.IsNullOrEmpty(objKitchener.KitchenerFIO))
            {
                return RedirectPermanent("/Kitchener/WarningKitchener");
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new KitchenerConnectingModel
                    {
                        Id = id.Value,
                        KitchenerFIO = objKitchener.KitchenerFIO
                    });
                }
                else
                {
                    service.AddElement(new KitchenerConnectingModel
                    {
                        KitchenerFIO = objKitchener.KitchenerFIO
                    });
                }
                return RedirectPermanent("/Kitcheners/Kitcheners");
            }
            catch (Exception ex)
            {
                return RedirectPermanent("/Kitchener/WarningKitchener");
            }
        }
    }
}