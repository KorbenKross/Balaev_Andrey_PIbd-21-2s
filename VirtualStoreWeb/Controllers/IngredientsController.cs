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
    public class IngredientsController : Controller
    {
        private IIngredientService service;

        public IngredientsController(IIngredientService service)
        {
            this.service = service;
        }
        // GET: Ingredients
        public ActionResult Ingredients()
        {
            LoadData();
            return View();
        }

        [HttpPost]
        public RedirectResult Ingredients(string action, string ingredientsList)
        {
            switch (action)
            {
                case "Add":
                    return RedirectPermanent("/Ingredient/Ingredient");
                case "Change":
                    break;
                case "Delete":
                    int id = Convert.ToInt32(ingredientsList);
                    try
                    {
                        service.DelElement(id);
                    }
                    catch (Exception ex)
                    {
                        return RedirectPermanent("/Ingredients/IngredientsWarning");
                    }
                    LoadData();
                    return RedirectPermanent("/Buyer/Buyer");
                    break;
                case "Update":
                    return RedirectPermanent("/Ingredients/Ingredients");
            }
            return RedirectPermanent("/Ingredients/IngredientsWarning");

        }

        private void LoadData()
        {
            try
            {
                List<IngredientUserViewModel> list = service.GetList();
                ViewBag.DataList = list;
            }
            catch (Exception ex)
            {
                RedirectPermanent("/Ingredients/IngredientsWarning");
            }
        }
    }
}