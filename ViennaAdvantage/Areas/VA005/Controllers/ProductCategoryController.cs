using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using VAdvantage.Model;
using VAdvantage.Utility;
using VIS.Filters;
using VIS.Models;


namespace VA005.Controllers
{
    public class ProductCategoryController : Controller
    {
        public ActionResult Index(int windowno)
        {
            ViewBag.WindowNumber = windowno;
            return PartialView();
        }

        [HttpPost]
        public JsonResult Save(String Name)
        {
            KeyNamePair retJSON = null;
            if (Session["ctx"] != null)
            {
                VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
                VA005.Models.ProductCategortModel model = new Models.ProductCategortModel();
                retJSON = model.AddCategory(Name, ctx);
            }
            return Json(JsonConvert.SerializeObject(retJSON), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(Int32 ID, String Name)
        {
            bool value = false;
            if (Session["ctx"] != null)
            {
                VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
                VA005.Models.ProductCategortModel model = new Models.ProductCategortModel();
                value = model.UpdateCategory(ID, Name, ctx);
            }
            return Json(new { result = value }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update1(Int32 id, String Name, String Value, String ProductType, String MatPolicy, String Desc, Int32 attrSet, Int32 taxcat, Int32 assetGrp, Boolean consumable, Int32 imageId)
        {
            bool value = false;
            if (Session["ctx"] != null)
            {
                VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
                VA005.Models.ProductCategortModel model = new Models.ProductCategortModel();
                value = model.UpdateCategory(id, Name, Value, ProductType, MatPolicy, Desc, attrSet, taxcat, assetGrp, consumable, imageId, ctx);
            }
            return Json(new { result = value }, JsonRequestBehavior.AllowGet);
        }        

        [AjaxAuthorizeAttribute]
        [AjaxSessionFilterAttribute]
        public JsonResult SaveImage(HttpPostedFileBase file, bool isDatabaseSave, string ad_image_id)
        {
            if (file == null)
            {
                Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }

            VA005.Models.ProductCategortModel obj = new VA005.Models.ProductCategortModel();
            var value = 0;
            if (Session["Ctx"] != null)
            {
                var ctx = Session["ctx"] as Ctx;
                if (!Directory.Exists(Path.Combine(Server.MapPath("~/Images"))))
                {
                    Directory.CreateDirectory(Path.Combine(Server.MapPath("~/Images")));
                }
                value = obj.SaveImage(ctx, Server.MapPath("~/Images"), file, Convert.ToInt32(ad_image_id), isDatabaseSave);
            }
            return Json(new { result = value }, JsonRequestBehavior.AllowGet);
        }  
        /// <summary>
        /// Method to delete product category
        /// </summary>
        /// <param name="pcats">ProductCategoryID</param>
        /// <returns>Result</returns>
        [HttpPost]
        public JsonResult DeleteCategory(string[] pcats)
        {
            List<KeyNamePair> value=null;
            //string[] param = pcats.Split(',');
            if (Session["ctx"] != null)
            {
                VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
                VA005.Models.ProductCategortModel model = new Models.ProductCategortModel();
                value = model.DeleteCategory(ctx, pcats);
            }
            return Json(new { result = value }, JsonRequestBehavior.AllowGet);
        }
    }
}