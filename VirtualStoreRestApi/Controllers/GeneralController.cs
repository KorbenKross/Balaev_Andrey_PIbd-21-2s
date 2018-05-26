using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.ConnectingModel;
using VirtualStoreRestApi.Services;

namespace VirtualStoreRestApi.Controllers
{
    public class GeneralController : ApiController
    {
        private readonly IGeneralSelection _service;

        public GeneralController(IGeneralSelection service)
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

        [HttpPost]
        public void CreateOrder(CustomerSelectionModel model)
        {
            _service.CreateOrder(model);
        }

        [HttpPost]
        public void TakeOrderInWork(CustomerSelectionModel model)
        {
            _service.TakeOrderInWork(model);
        }

        [HttpPost]
        public void FinishOrder(CustomerSelectionModel model)
        {
            _service.FinishOrder(model.Id);
        }

        [HttpPost]
        public void PayOrder(CustomerSelectionModel model)
        {
            _service.PayOrder(model.Id);
        }

        [HttpPost]
        public void PutComponentOnStock(ProductStorageElementConnectingModel model)
        {
            _service.PutComponentOnStock(model);
        }

        [HttpGet]
        public IHttpActionResult GetInfo()
        {
            ReflectionService service = new ReflectionService();
            var list = service.GetInfoByAssembly();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
    }
}
