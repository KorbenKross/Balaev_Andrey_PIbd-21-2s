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
    public class ProductStoragesController : Controller
    {
        private IProductStorageService service;

        public ProductStoragesController(IProductStorageService service)
        {
            this.service = service;
        }
        // GET: ProductStorages
        public ActionResult ProductStorages()
        {
            LoadData();
            return View();
        }

        [HttpPost]
        public RedirectResult ProductStorages(string action, string productStorageList)
        {
            switch (action)
            {
                case "Add":
                    return RedirectPermanent("/ProductStorage/ProductStorage");
                case "Change":
                    break;
                case "Delete":
                    int id = Convert.ToInt32(productStorageList);
                    try
                    {
                        service.DelElement(id);
                    }
                    catch (Exception ex)
                    {
                        return RedirectPermanent("/ProductStorages/ProductStoragesWarning");
                    }
                    LoadData();
                    return RedirectPermanent("/ProductStorages/ProductStorages");
                    break;
                case "Update":
                    return RedirectPermanent("/ProductStorages/ProductStorages");
            }
            return RedirectPermanent("/ProductStorages/ProductStoragesWarning");
        }

        private void LoadData()
        {
            try
            {
                List<ProductStorageUserViewModel> list = service.GetList();
                ViewBag.DataList = list;
            }
            catch (Exception ex)
            {
                RedirectPermanent("/ProductStorages/ProductStoragesWarning");
            }
        }
    }
}