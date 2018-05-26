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
    public class TakeFromProductStorageController : Controller
    {
        public int Id { set { id = value; } }

        private IKitchenerService serviceI;

        private  IGeneralSelection serviceM;

        private int? id;


        public TakeFromProductStorageController(IKitchenerService serviceI, IGeneralSelection serviceM)
        {
            this.serviceI = serviceI;
            this.serviceM = serviceM;
        }
        // GET: TakeFromProductStorage
        public ActionResult TakeFromProductStorage()
        {
            if (id.HasValue)
            {
                try
                {
                    List<KitchenerUserViewModel> listI = serviceI.GetList();
                    if (listI != null)
                    {
                        ViewBag.DataList = listI;
                    }
                }
                catch (Exception ex)
                {
                    return RedirectPermanent("/TakeFromProductStorageWarning/TakeFromProductStorageWarning");
                }
            }
            return View();
        }

        public ActionResult TakeFromProductStorageWarning()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TakeFromProductStorage(string selectedItem)
        {
            if (selectedItem == null)
            {
                return RedirectPermanent("/TakeFromProductStorage/TakeFromProductStorageWarning");
            }
            try
            {
                serviceM.TakeOrderInWork(new CustomerSelectionModel
                {
                    Id = id.Value,
                    KitchenerId = Convert.ToInt32(selectedItem)
                });
                return RedirectPermanent("/General/General");
            }
            catch (Exception ex)
            {
                return RedirectPermanent("/TakeFromProductStorage/TakeFromProductStorageWarning");
            }
        }
    }
}