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
    public class IngredientController : Controller
    {
        public int Id { set { id = value; } }

        private IIngredientService service;

        private int? id;

        private List<IngredientElementUserViewModel> ingredientElements = new List<IngredientElementUserViewModel>();


        public IngredientController(IIngredientService service)
        {
            this.service = service;
        }
        // GET: Ingredient
        public ActionResult Ingredient(IngredientElementUserViewModel model)
        {            
            if (id.HasValue)
            {
                try
                {
                    IngredientUserViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        ingredientElements = view.IngredientElement;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    return RedirectPermanent("/Ingredient/IngredientWarning");
                }
            }
            else
            {
                //ingredientElements = new List<IngredientElementUserViewModel>();
                if (model != null)
                {
                    ingredientElements.Add(model);
                }
                LoadData();
            }
            return View();
        }

        [HttpPost]
        public RedirectResult Ingredient(string action, string ingredientList, string ingredientName, string value)
        {
            switch (action)
            {
                case "Add":
                    return RedirectPermanent("/IngredientElement/IngredientElement");
                case "Change":
                    break;
                case "Delete":
                    int id = Convert.ToInt32(ingredientList);
                    try
                    {
                        service.DelElement(id);
                    }
                    catch (Exception ex)
                    {
                        return RedirectPermanent("/Ingredient/IngredientWarning");
                    }
                    LoadData();
                    return RedirectPermanent("/Ingredient/Ingredient");
                    break;
                case "Update":
                    LoadData();
                    return RedirectPermanent("/Ingredient/Ingredient");
                case "Save":
                    SaveButton(ingredientName, value);
                    return RedirectPermanent("/Ingredients/Ingredients");
                case "Cancel":
                    return RedirectPermanent("");
            }
            return RedirectPermanent("/Ingredient/IngredientWarning");
        }

        private void SaveButton(string ingredientName, string value)
        {
            if (string.IsNullOrEmpty(ingredientName))
            {
                 RedirectPermanent("/Ingredient/IngredientWarning");
            }
            if (value == null)
            {
                RedirectPermanent("/Ingredient/IngredientWarning");
            }
            if (ingredientElements == null || ingredientElements.Count == 0)
            {
                RedirectPermanent("/Ingredient/IngredientWarning");
            }
            try
            {
                List<IngredientElementConnectingModel> ingredientElementBM = new List<IngredientElementConnectingModel>();
                for (int i = 0; i < ingredientElements.Count; ++i)
                {
                    ingredientElementBM.Add(new IngredientElementConnectingModel
                    {
                        Id = ingredientElements[i].Id,
                        IngredientId = ingredientElements[i].IngredientId,
                        ElementId = ingredientElements[i].ElementId,
                        Count = ingredientElements[i].Count
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new IngredientConnectingModel
                    {
                        Id = id.Value,
                        IngredientName = ingredientName,
                        Value = Convert.ToInt32(value),
                        IngredientElement = ingredientElementBM
                    });
                }
                else
                {
                    service.AddElement(new IngredientConnectingModel
                    {
                        IngredientName = ingredientName,
                        Value = Convert.ToInt32(value),
                        IngredientElement = ingredientElementBM
                    });
                }
                RedirectPermanent("/Ingredients/Ingredients");
            }
            catch (Exception ex)
            {
                RedirectPermanent("/Ingredient/IngredientWarning");

            }
        }

        private void LoadModel(IngredientElementUserViewModel routeModel)
        {
            try
            {
                if (routeModel != null)
                {
                    ingredientElements.Add(routeModel);
                    ViewBag.DataList = ingredientElements;
                }
            }
            catch (Exception ex)
            {
                RedirectPermanent("/Ingredient/IngredientWarning");
            }
        }

        private void LoadData()
        {
            try
            {
                if (ingredientElements != null)
                {
                    ViewBag.DataList = ingredientElements;
                }
            }
            catch (Exception ex)
            {
                RedirectPermanent("/Ingredient/IngredientWarning");
            }
        }

    }
}