﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.ConnectingModel;

namespace VirtualStoreRestApi.Controllers
{
    public class BuyerController : ApiController
    {
        private readonly IBuyerCustomer _service;

        public BuyerController(IBuyerCustomer service)
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
        public void AddElement(BuyerConnectingModel model)
        {
            _service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(BuyerConnectingModel model)
        {
            _service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(BuyerConnectingModel model)
        {
            _service.DelElement(model.Id);
        }
    }
}
