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
    public class ReportController : ApiController
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetStocksLoad()
        {
            var list = _service.GetStocksLoad();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public IHttpActionResult GetClientOrders(ReportConnectingModel model)
        {
            var list = _service.GetClientOrders(model);
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void SaveProductPrice(ReportConnectingModel model)
        {
            _service.SaveProductPrice(model);
        }

        [HttpPost]
        public void SaveStocksLoad(ReportConnectingModel model)
        {
            _service.SaveStocksLoad(model);
        }

        [HttpPost]
        public void SaveClientOrders(ReportConnectingModel model)
        {
            _service.SaveClientOrders(model);
        }
    }
}
