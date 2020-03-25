using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using VAdvantage.Utility;
using ViennaAdvantage.Model;
using VA005.Models;

namespace VA005.Controllers
{
    public class AttributeListingController : Controller
    {
        //
        // GET: /VA005/AttributeListing/
        public ActionResult Index(int windowNo)
        {
            ViewBag.WindowNumber = windowNo;
            return PartialView();
        }

        public JsonResult TreeViewForAttributeSetListing(string Expend)
        {
            Ctx ctx=Session["ctx"] as Ctx;
            AttributeListing obj=new AttributeListing(ctx);
            var value = obj.CreateTree(Expend);
            return Json(JsonConvert.SerializeObject(value),JsonRequestBehavior.AllowGet);
        }

        //public JsonResult TreeViewForAttributeSetListingLeft()
        //{
        //    Ctx ctx = Session["ctx"] as Ctx;
        //    AttributeListing obj = new AttributeListing(ctx);
        //    var value = obj.createTreeAttributeName();
        //    return Json(JsonConvert.SerializeObject(value), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult SaveAttributeSetValue(VA005_AddAttributeSet values)
        {
            Ctx ctx=Session["ctx"] as Ctx;
            AttributeListing obj=new AttributeListing(ctx);

            values.name = Server.HtmlDecode(values.name);
            values.description = Server.HtmlDecode(values.description);

            var value = obj.AddAttributeSet(values);
            return Json(JsonConvert.SerializeObject(value),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveLotData(VA005_LotCtrl values)
        {
            values.Name = Server.HtmlDecode(values.Name);
           // values.StartNo = Server.HtmlDecode(values.StartNo);
            //values.Increment = Server.HtmlDecode(values.Increment);
           // values.CurrentNext = Server.HtmlDecode(values.CurrentNext);
            values.prefix = Server.HtmlDecode(values.prefix);
            values.Suffix = Server.HtmlDecode(values.Suffix);

            Ctx ctx = Session["ctx"] as Ctx;
            AttributeListing obj = new AttributeListing(ctx);            
            var value = obj.CreateNewLot(values);
            return Json(JsonConvert.SerializeObject(value), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult saveSerialData(VA005_SerialCtr values)
        {
            values.name = Server.HtmlDecode(values.name);           
            values.prefix = Server.HtmlDecode(values.prefix);
            values.sufix = Server.HtmlDecode(values.sufix);

            Ctx ctx = Session["ctx"] as Ctx;
            AttributeListing obj = new AttributeListing(ctx);
            var value = obj.CresteNewSerial(values);
            return Json(JsonConvert.SerializeObject(value), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddAttributeSet(VA005_AddAttributeSet values)
        {
            Ctx ctx = Session["ctx"] as Ctx;
            AttributeListing obj = new AttributeListing(ctx);
            var value = obj.AddAttributeSet(values);
            return Json(JsonConvert.SerializeObject(value), JsonRequestBehavior.AllowGet);
        }


        public JsonResult DeleteAttributeSetValue(VA005_DeleteAttributeSetValue values)
        {
            Ctx ctx = Session["ctx"] as Ctx;
            AttributeListing obj = new AttributeListing(ctx);
            var value = obj.DeleteAttributeSetValue(values);
            return Json(JsonConvert.SerializeObject(value), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteAttributeFromData(List<Int32> values)
        {
            Ctx ctx = Session["ctx"] as Ctx;
            AttributeListing obj = new AttributeListing(ctx);
            var value = obj.DeleteAttributeSetValue(values);
            return Json(new { value }, JsonRequestBehavior.AllowGet);
        }
	}
}
