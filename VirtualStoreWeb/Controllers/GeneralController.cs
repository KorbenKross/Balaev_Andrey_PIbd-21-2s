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
    public class GeneralController : Controller
    {
        private IGeneralSelection service;

        public GeneralController(IGeneralSelection service)
        {
            this.service = service;
        }
        
        // GET: General
        public ActionResult General()
        {
            LoadData();
            return View();
        }

        [HttpPost]
        public RedirectResult General(string action, string generalList)
        {
            switch (action)
            {
                case "Создать заказ":
                    return RedirectPermanent("/CustomerSelection/CustomerSelection");
                case "Отдать на выполнение":
                    return RedirectPermanent("/TakeFromProductStorage/TakeFromProductStorage");
                case "Заказ готов":
                    int id1 = Convert.ToInt32(generalList);
                    try
                    {
                        service.FinishOrder(id1);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        return RedirectPermanent("/General/GeneralWarning");
                    }
                    break;
                case "Заказ оплачен":
                    int id2 = Convert.ToInt32(generalList);
                    try
                    {
                        service.PayOrder(id2);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        return RedirectPermanent("/General/GeneralWarning");
                    }
                    break;
                case "Обновить список":
                    LoadData();
                    return RedirectPermanent("/General/General");
            }
            return RedirectPermanent("/General/GeneralWarning");
        }

        private void LoadData()
        {
            try
            {
                List<CustomerSelectionUserViewModel> list = service.GetList();
                if (list != null)
                {
                    ViewBag.DataList = list;
                }
            }
            catch (Exception ex)
            {
                RedirectPermanent("/General/GeneralWarning");
            }
        }

        public ActionResult GeneralWarning()
        {
            return View();
        }
    }
}