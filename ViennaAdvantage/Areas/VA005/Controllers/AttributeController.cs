using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using VAdvantage.Utility;
using ViennaAdvantage.Model;
using VAdvantage.Model;
using VA005.Models;



namespace VA005.Controllers
{
    public class AttributeController : Controller
    {
        //
        // GET: /VA005/Attribute/
        public ActionResult Index(int windowNo)
        {
            ViewBag.WindowNumber = windowNo;
            return View();
        }

        public JsonResult SaveAttributemodel(VA005.Models.Attribute.VA005_SaveAttribute value)
        {
            Ctx ctx = Session["ctx"] as Ctx;
            VA005.Models.Attribute obj = new VA005.Models.Attribute(ctx);

            value.name = Server.HtmlDecode(value.name);
            value.description = Server.HtmlDecode(value.description);

            var text = obj.SaveAttribute(value);
            return Json(JsonConvert.SerializeObject(text), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SelectionSavemodel(VA005.Models.Attribute.VA005_SaveSelectionList value)
        {
            Ctx ctx = Session["ctx"] as Ctx;
            VA005.Models.Attribute obj = new VA005.Models.Attribute(ctx);

            value.secname = Server.HtmlDecode(value.secname);
            value.searchkey = Server.HtmlDecode(value.searchkey);

            var text = obj.SelectionSave(value);         
            return Json(JsonConvert.SerializeObject(text), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowGrideData(VA005.Models.Attribute.VA005_ShowGrideData value)
        {
            Ctx ctx = Session["ctx"] as Ctx;
            VA005.Models.Attribute obj = new VA005.Models.Attribute(ctx);
            var text = obj.ShowGrideData(value);
            return Json(new { result =text}, JsonRequestBehavior.AllowGet);           
        }
//=================================================
        //public JsonResult SaveAttributeuses(VA005.Models.Attribute.VA005_SaveAttributeuses value)
        //{
        //    Ctx ctx = Session["ctx"] as Ctx;
        //    VA005.Models.Attribute obj = new VA005.Models.Attribute(ctx);
        //    var text = obj.SaveAttributeuses(value);
        //    return Json(JsonConvert.SerializeObject(text), JsonRequestBehavior.AllowGet);
        //}

        // Added by Shifali on 04 Aug 2020 to Save multiple attributes
        /// <summary>
        /// Save Attribute Uses
        /// </summary>
        /// <param name="attributsetid">attributesetid</param>
        /// <param name="nid">node id</param>
        /// <returns>Result</returns>
        public JsonResult SaveAttributeuses(string attributsetid,int nid)
        {
            Ctx ctx = Session["ctx"] as Ctx;
            VA005.Models.Attribute obj = new VA005.Models.Attribute(ctx);
            var text = obj.SaveAttributeuses(ctx, attributsetid,nid);
            return Json(JsonConvert.SerializeObject(text), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteAttributeValue(VA005.Models.Attribute.VA005_DeleteAttributeValue value)
        {
            Ctx ctx = Session["ctx"] as Ctx;
            VA005.Models.Attribute obj = new VA005.Models.Attribute(ctx);
            var text = obj.DeleteAttributeValue(value);
            return Json(JsonConvert.SerializeObject(text), JsonRequestBehavior.AllowGet);
           // return Json(new { text }, JsonRequestBehavior.AllowGet);
        }
    }
}