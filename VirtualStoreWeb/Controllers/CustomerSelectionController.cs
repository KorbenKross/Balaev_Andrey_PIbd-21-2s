using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.UserViewModel;
using VirtualStorePlace.ConnectingModel;
using VirtualStore;

namespace VirtualStoreWeb.Controllers
{
    public class CustomerSelectionController : Controller
    {
        private IBuyerCustomer serviceC;

        private IIngredientService serviceP;

        private IGeneralSelection serviceM;

        private static string sumPrice;

        public CustomerSelectionController(IBuyerCustomer serviceC, IIngredientService serviceP, IGeneralSelection serviceM)
        {
            this.serviceC = serviceC;
            this.serviceP = serviceP;
            this.serviceM = serviceM;
        }

        // GET: CustomerSelection
        public ActionResult CustomerSelection()
        {
            try
            {
                ViewBag.sumPrice = sumPrice;
                List<BuyerUserViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    ViewBag.listC = listC;
                }
                List<IngredientUserViewModel> listP = serviceP.GetList();
                if (listP != null)
                {
                    ViewBag.listP = listP;
                }
            }
            catch (Exception ex)
            {
                RedirectPermanent("/CustomerSelection/WarningCustomerSelection");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CustomerSelection(CustomerSelectionUserViewModel customerSelectionCount, string selectedItem1, string selectedItem2, string action, string sum)
        {
            if (string.IsNullOrEmpty(customerSelectionCount.ToString()))
            {
                return RedirectPermanent("/CustomerSelection/WarningCustomerSelection");
            }
            if (selectedItem1 == null)
            {
                return RedirectPermanent("/CustomerSelection/WarningCustomerSelection");
            }
            if (selectedItem2 == null)
            {
                return RedirectPermanent("/CustomerSelection/WarningCustomerSelection");
            }
            if (action.Equals("Подсчитать"))
            {
                CalcSum(customerSelectionCount, selectedItem1, selectedItem2);
                return RedirectPermanent("/CustomerSelection/CustomerSelection");
            }
            try
            {
                serviceM.CreateOrder(new CustomerSelectionModel
                {
                    BuyerId = Convert.ToInt32(selectedItem1),
                    IngredientId = Convert.ToInt32(selectedItem2),
                    Count = Convert.ToInt32(customerSelectionCount.Count),
                    Sum = Convert.ToInt32(sum)
                });
                return RedirectPermanent("/General/General");
            }
            catch (Exception ex)
            {
                return RedirectPermanent("/CustomerSelection/WarningCustomerSelection");
            }
        }

        private string CalcSum(CustomerSelectionUserViewModel customerSelectionCount, string selectedItem1, string selectedItem2)
        {
            if (customerSelectionCount != null)
            {
                try
                {
                    int id = Convert.ToInt32(selectedItem2);
                    IngredientUserViewModel product = serviceP.GetElement(id);
                    int count = Convert.ToInt32(customerSelectionCount.Count);
                    sumPrice = (count * product.Price).ToString();
                    ViewBag.sumPrice = sumPrice;
                    ViewBag.Count = Convert.ToInt32(customerSelectionCount.Count);
                    ViewBag.item1 = Convert.ToInt32(selectedItem1);
                    ViewBag.item2 = Convert.ToInt32(selectedItem2);
                    return sumPrice;
                }
                catch (Exception ex)
                {
                    RedirectPermanent("/CustomerSelection/WarningCustomerSelection");
                    return "Ошибка";
                }
            }
            RedirectPermanent("/CustomerSelection/WarningCustomerSelection");
            return "Ошибка";
        }

        public ActionResult WarningCustomerSelection()
        {
            return View();
        }
    }
}