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
    public class IngredientElementController : Controller
    {
        public IngredientElementUserViewModel Model { set { model = value; } get { return model; } }

        private IElementService service;

        private IngredientElementUserViewModel model;

        public IngredientElementController(IElementService service)
        {
            this.service = service;
        }
        // GET: IngredientElement
        public ActionResult IngredientElement()
        {
            try
            {
                List<ElementUserViewModel> list = service.GetList();
                if (list != null)
                {
                    ViewBag.DataList = list;
                }
            }
            catch (Exception ex)
            {
                return RedirectPermanent("/IngredientElement/IngredientElementWarning");
            }
            if (model != null)
            {
                ViewBag.DataCountBox = model.Count.ToString();
            }
            return View();
        }

        public ActionResult IngredientElementWarning()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IngredientElement(IngredientElementUserViewModel ingredientElementCount, string selectedItem)
        {
            List<ElementUserViewModel> list = service.GetList();
            ElementUserViewModel x = list.ElementAt(Convert.ToInt32(selectedItem));
            if (ingredientElementCount == null)
            {
                return RedirectPermanent("/IngredientElement/IngredientElementWarning");
            }
            if (selectedItem == null)
            {
                return RedirectPermanent("/IngredientElement/IngredientElementWarning");
            }
            try
            {
                if (model == null)
                {
                    model = new IngredientElementUserViewModel
                    {
                        ElementId = Convert.ToInt32(x.Id),
                        ElementName = x.ElementName,
                        Count = Convert.ToInt32(ingredientElementCount.Count)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(ingredientElementCount.Count);
                }
                return RedirectToAction("Ingredient","Ingredient", model);
                //return RedirectPermanent("/Ingredient/Ingredient");
            }
            catch (Exception ex)
            {
                return RedirectPermanent("/IngredientElement/IngredientElementWarning");
            }
        }
    }
}