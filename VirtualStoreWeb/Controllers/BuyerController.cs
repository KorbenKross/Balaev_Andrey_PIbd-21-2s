using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.UserViewModel;
using VirtualStore;
using Unity;
using Unity.Attributes;
using System.Web.UI.WebControls;

namespace VirtualStoreWeb.Controllers
{
    public class BuyerController : Controller
    {
        public int Id { set { id = value; } }

        private IBuyerCustomer service;

        private int? id;

        public BuyerController(IBuyerCustomer service)
        {
            this.service = service;
        }

        // GET: Buyer
        public ActionResult Buyer()
        {
            LoadData();
            return View();
        }

        [HttpPost]
        public RedirectResult Buyer(string action, string buyerList)
        {
            switch (action)
            {
                case "Add":
                    return SingleBuyerResult();                    
                case "Change":
                    break;
                case "Delete":
                    int id = Convert.ToInt32(buyerList);
                    try
                    {
                        service.DelElement(id);
                    }
                    catch (Exception ex)
                    {
                        return RedirectPermanent("/Buyer/WarningBuyer");
                    }
                    LoadData();
                    return RedirectPermanent("/Buyer/Buyer");
                    break;
                case "Update":
                    return BuyerResult();
            }
            return WarningResult();
        }

        public ActionResult WarningBuyer()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }    

        public RedirectResult SingleBuyerResult()
        {
            return RedirectPermanent("/SingleBuyer/SingleBuyer");
        }

        public RedirectResult WarningResult() 
        {
            return RedirectPermanent("/Buyer/WarningBuyer");
        }

        public RedirectResult BuyerResult()
        {
            return RedirectPermanent("/Buyer/Buyer");
        }

        private void LoadData()
        {
            try
            {
                List<BuyerUserViewModel> list = service.GetList();
                ViewBag.DataList = list;
            }
            catch (Exception ex)
            {
                View(WarningBuyer());
            }
        }
    }
}