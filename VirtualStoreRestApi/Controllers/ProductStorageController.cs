using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.ConnectingModel;

namespace VirtualStoreRestApi.Controllers
{
    public class ProductStorageController : ApiController
    {
        private readonly IProductStorageService _service;

        public ProductStorageController(IProductStorageService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = _service.GetElement(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }

        [HttpPost]
        public void AddElement(ProductStorageConnectingModel model)
        {
            _service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(ProductStorageConnectingModel model)
        {
            _service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(ProductStorageConnectingModel model)
        {
            _service.DelElement(model.Id);
        }
    }
}
