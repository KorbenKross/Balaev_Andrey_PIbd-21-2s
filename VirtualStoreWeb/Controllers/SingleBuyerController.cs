using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using VirtualStore;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.UserViewModel;


namespace VirtualStoreWeb.Controllers
{
    public class SingleBuyerController : Controller
    {
        public int Id { set { id = value; } }

        private IBuyerCustomer service;

        private int? id;
        
        private string strBuyerFIO = "";

        public SingleBuyerController(IBuyerCustomer service)
        {
            this.service = service;
        }

        // GET: SingleBuyer
        public ActionResult SingleBuyer()
        {
            if (id.HasValue)
            {
                try
                {
                    BuyerUserViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        strBuyerFIO = view.BuyerFIO;
                        return View(strBuyerFIO);
                    }
                }
                catch (Exception ex)
                {
                    Redirect("/SingleBuyer/SingleBuyerWarning");
                }
            }
            return View(new Buyer());
        }

        public ActionResult SingleBuyerOK()
        {
            ViewBag.Message = "Покупатель успешно создан.";

            return View();
        }

        public ActionResult SingleBuyerWarining()
        {
            ViewBag.Message = "Ошибка.";

            return View();
        }


        [HttpPost]
        public ActionResult SingleBuyer(BuyerUserViewModel objBuyer)
        {
            if (string.IsNullOrEmpty(objBuyer.ToString()))
            {
                return RedirectPermanent("/SingleBuyer/SingleBuyerWarining");
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new BuyerConnectingModel
                    {
                        Id = id.Value,
                        BuyerFIO = objBuyer.BuyerFIO
                    });
                }
                else
                {
                    service.AddElement(new BuyerConnectingModel
                    {
                        BuyerFIO = objBuyer.BuyerFIO
                    });
                }
                return RedirectPermanent("/Buyer/Buyer");
            }
            catch (Exception ex)
            {
                return RedirectPermanent("/SingleBuyer/SingleBuyerWarining");
            }
            //return $"Покупатель {BuyerFIO} зарегистрирован";
        }
    }
}