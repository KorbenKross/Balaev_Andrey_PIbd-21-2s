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
    public class PutOnProductStorageController : Controller
    {
        private IProductStorageService serviceS;

        private IElementService serviceC;

        private IGeneralSelection serviceM;

        public PutOnProductStorageController(IProductStorageService serviceS, IElementService serviceC, IGeneralSelection serviceM)
        {
            this.serviceS = serviceS;
            this.serviceC = serviceC;
            this.serviceM = serviceM;
        }

        // GET: PutOnProductStorage
        public ActionResult PutOnProductStorage()
        {
            try
            {
                List<ElementUserViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    ViewBag.listC = listC;
                }
                List<ProductStorageUserViewModel> listS = serviceS.GetList();
                if (listS != null)
                {
                    ViewBag.listS = listS;
                }
            }
            catch (Exception ex)
            {
                return RedirectPermanent("/PutOnProductStorage/PutOnProductStorageWarning");
            }
            return View();
        }


        [HttpPost]
        public ActionResult PutOnProductStorage(string count, string selectedItem1, string selectedItem2, string action)
        {
            if (action.Equals("Проверить"))
            {
                return RedirectPermanent("/PutOnProductStorage/PutOnProductStorageWarning");
            }
            if (count == null)
            {
                return RedirectPermanent("/PutOnProductStorage/PutOnProductStorageWarning");
            }
            if (selectedItem1 == null)
            {
                return RedirectPermanent("/PutOnProductStorage/PutOnProductStorageWarning");
            }
            if (selectedItem2 == null)
            {
                return RedirectPermanent("/PutOnProductStorage/PutOnProductStorageWarning");
            }
            try
            {
                serviceM.PutComponentOnStock(new ProductStorageElementConnectingModel
                {
                    ElementId = Convert.ToInt32(selectedItem1),
                    ProductStorageId = Convert.ToInt32(selectedItem2),
                    Count = Convert.ToInt32(count)
                });
                return RedirectPermanent("/General/General");
            }
            catch (Exception ex)
            {
                return RedirectPermanent("/PutOnProductStorage/PutOnProductStorageWarning");
            }
        }
    }
}