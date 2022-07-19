using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using VAdvantage.Utility;
using ViennaAdvantage.Model;
using VA005.Models;
using VAdvantage.Model;
using static VA005.Models.AttributeListing;

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
            Ctx ctx = Session["ctx"] as Ctx;
            AttributeListing obj = new AttributeListing(ctx);
            var value = obj.CreateTree(Expend);
            return Json(JsonConvert.SerializeObject(value), JsonRequestBehavior.AllowGet);
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
            Ctx ctx = Session["ctx"] as Ctx;
            AttributeListing obj = new AttributeListing(ctx);

            values.name = Server.HtmlDecode(values.name);
            values.description = Server.HtmlDecode(values.description);

            var value = obj.AddAttributeSet(values);
            return Json(JsonConvert.SerializeObject(value), JsonRequestBehavior.AllowGet);
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
        /// <summary>
        /// Load Attribute
        /// </summary>
        /// <returns>JSON Data</returns>
        public JsonResult GetattributeAppendDiv()
        {
            AttributeListing model = new AttributeListing(Session["Ctx"] as Ctx);
            List<AttributeAppend> value = model.GetattributeAppendDiv();
            return Json(JsonConvert.SerializeObject(value), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Save Attribute
        /// </summary>
        /// <returns>JSON Data</returns>
        public JsonResult SaveAttributeOnAdd()
        {
            Ctx ctx = Session["ctx"] as Ctx;
            AttributeListing obj = new AttributeListing(ctx);
            int value = obj.SaveAttributeOnAdd();
            return Json(new { value }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Load Attribute Grid Value
        /// </summary>
        /// <param name="SelectAttributeID">SelectAttributeID</param>
        /// <returns>JSON Data</returns>
        public JsonResult GetAttributeValue(int SelectAttributeID)
        {
            AttributeListing model = new AttributeListing(Session["Ctx"] as Ctx);
            List<AttributeValue> value = model.GetAttributeValue(SelectAttributeID);
            return Json(JsonConvert.SerializeObject(value), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Edit Local Attribute
        /// </summary>
        /// <param name="Control">Control</param>
        /// <returns>JSON Attribute</returns>
        public JsonResult EditAttributebtn(string Control)
        {
            Ctx ctx = Session["ctx"] as Ctx;
            AttributeListing obj = new AttributeListing(ctx);
            var value = obj.EditAttributebtn(Control);
            return Json(new { value }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Method For Mandatory DropDown
        /// </summary>
        /// <returns>JSON Data</returns>
        public JsonResult GetMandatoryType()
        {
            AttributeListing model = new AttributeListing(Session["Ctx"] as Ctx);
            List<ValueNamePair> value = model.GetMandatoryType();
            return Json(JsonConvert.SerializeObject(value), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lot Drop Dowm
        /// </summary>
        /// <returns>JSON Data</returns>
        public JsonResult GetLotData()
        {
            AttributeListing model = new AttributeListing(Session["Ctx"] as Ctx);
            List<KeyNamePair> value = model.GetLotData();
            return Json(JsonConvert.SerializeObject(value), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Serial Drop Down
        /// </summary>
        /// <returns>JSON Data</returns>
        public JsonResult GetSerialData()
        {
            AttributeListing model = new AttributeListing(Session["Ctx"] as Ctx);
            List<KeyNamePair> value = model.GetSerialData();
            return Json(JsonConvert.SerializeObject(value), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Edit Attribute Set
        /// </summary>
        /// <param name="NodeID">Node ID</param>
        /// <returns>JSON</returns>
        public JsonResult EditAttributeSet(int NodeID)
        {
            AttributeListing model = new AttributeListing(Session["Ctx"] as Ctx);
            List<EditAttributeList> value = model.EditAttributeSet(NodeID);
            return Json(JsonConvert.SerializeObject(value), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Remove Attribute Formatt
        /// </summary>
        /// <param name="AttributeID">Attribute ID</param>
        /// <param name="ParentId">Parent ID</param>
        /// <returns>JSON Data</returns>
        public JsonResult RemoveAttFormatt(int AttributeID, int ParentId)
        {
            Ctx ctx = Session["ctx"] as Ctx;
            AttributeListing obj = new AttributeListing(ctx);
            var value = obj.RemoveAttFormatt(AttributeID, ParentId);
            return Json(new { value }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Load Link Attribute
        /// </summary>
        /// <param name="AttributeID">Attribute ID</param>
        /// <returns>JSON Data</returns>
        public JsonResult LoadLinkAttSetAnd(int AttributeID)
        {
            Ctx ctx = Session["ctx"] as Ctx;
            AttributeListing obj = new AttributeListing(ctx);
            var value = obj.LoadLinkAttSetAnd(AttributeID);
            return Json(new { value }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Attribute ID
        /// </summary>
        /// <param name="M_Attributeset">Attribute Set</param>
        /// <returns>JSON Data</returns>
        public JsonResult GetTableAttribute(string M_Attributeset)
        {
            Ctx ctx = Session["ctx"] as Ctx;
            AttributeListing obj = new AttributeListing(ctx);
            string value = obj.GetTableAttribute(M_Attributeset);
            return Json(new { value }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Field Length
        /// </summary>
        /// <param name="TableattributesetID">Table ID</param>
        /// <param name="COLUMNNAME">Columnname</param>
        /// <returns>JSON Data</returns>
        public JsonResult GetFieldLength(string TableattributesetID, string COLUMNNAME)
        {
            Ctx ctx = Session["ctx"] as Ctx;
            AttributeListing obj = new AttributeListing(ctx);
            var value = obj.GetFieldLength(TableattributesetID, COLUMNNAME);
            return Json(new { value }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Lot Table ID
        /// </summary>
        /// <param name="M_lotCtl">Lot ID</param>
        /// <returns>JSON Data</returns>
        public JsonResult GetlottableId(string M_lotCtl)
        {
            Ctx ctx = Session["ctx"] as Ctx;
            AttributeListing obj = new AttributeListing(ctx);
            string value = obj.GetlottableId(M_lotCtl);
            return Json(new { value }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Field Length
        /// </summary>
        /// <param name="lottableID">LotTable_ID</param>
        /// <returns>JSON Data</returns>
        public JsonResult GetField(string lottableID)
        {
            AttributeListing model = new AttributeListing(Session["Ctx"] as Ctx);
            List<ColumnData> value = model.GetField(lottableID);
            return Json(JsonConvert.SerializeObject(value), JsonRequestBehavior.AllowGet);
        }
    }
}
