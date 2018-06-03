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
    public class ProductStorageController : Controller
    {
        public int Id { set { id = value; } }

        private IProductStorageService service;

        private int? id;

        public ProductStorageController(IProductStorageService service)
        {
            this.service = service;
        }
        // GET: ProductStorage
        public ActionResult ProductStorage()
        {
            if (id.HasValue)
            {
                try
                {
                    ProductStorageUserViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        var textBoxProductStorageName = view.ProductStorageName;
                        return View(textBoxProductStorageName);
                    }
                }
                catch (Exception ex)
                {
                    return RedirectPermanent("/ProductStorage/ProductStorageWarning");
                }
            }
            return View();            
        }

        [HttpPost]
        public ActionResult ProductStorage(ProductStorageUserViewModel objProductStorage)
        {
            if (string.IsNullOrEmpty(objProductStorage.ToString()))
            {
                return RedirectPermanent("/ProductStorage/ProductStorageWarning");
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new ProductStorageConnectingModel
                    {
                        Id = id.Value,
                        StockName = objProductStorage.ProductStorageName
                    });
                }
                else
                {
                    service.AddElement(new ProductStorageConnectingModel
                    {
                        StockName = objProductStorage.ProductStorageName
                    });
                }
                return RedirectPermanent("/ProductStorages/ProductStorages");
            }
            catch (Exception ex)
            {
                return RedirectPermanent("/ProductStorage/ProductStorageWarning");
            }
        }

    }
}