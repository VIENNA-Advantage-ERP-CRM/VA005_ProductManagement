using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAdvantage.Model;
using VAdvantage.DataBase;
using VAdvantage.Utility;
using System.Data;
using Newtonsoft.Json;
using VAdvantage.Logging;


namespace VA005.Models
{
    public class Attribute
    {
        private Ctx _ctx = null;
        int mattributeid = 0;

        public Attribute(Ctx ctx)
        {
            _ctx = ctx;
        }

        public string SaveAttribute(VA005_SaveAttribute value)
        {
            MAttribute obj = new MAttribute(_ctx, value.ID, null);
            obj.SetName(value.name);
           

            if (value.description != null)
            {
                obj.SetDescription(value.description);
            }
            else
            {
                obj.SetDescription(" ");
            }


            if (value.attributetype != null)
            {
                obj.SetAttributeValueType(value.attributetype);
            }
            else
            {
                obj.SetAttributeValueType("L");
            }

            obj.SetIsMandatory(value.mandatory);
            obj.SetIsActive(value.isactivefield);
            obj.SetIsInstanceAttribute(value.istanceattribute);



            //obj.Save();
            if (obj.Save())
            {
                return obj.GetM_Attribute_ID().ToString();
            }
            // mattributeid = obj.GetM_Attribute_ID();
            //}
            //if (value.secname != null)            
            //{
            //    MAttributeValue objattval = new MAttributeValue(_ctx, 0, null);
            //    objattval.SetName(value.secname);
            //    objattval.SetValue(value.searchkey);
            //    objattval.SetM_Attribute_ID(mattributeid);
            //    if (objattval.Save())
            //    {
            //        return objattval.GetM_AttributeValue_ID().ToString();
            //    }
            //}            
            return Msg.GetMsg(_ctx, "VA005_UnableToSaveAttribute");
        }

        public string SelectionSave(VA005_SaveSelectionList value)
        {
            MAttributeValue obj = new MAttributeValue(_ctx, 0, null);
            obj.SetName(value.secname);

            if (String.IsNullOrEmpty(value.searchkey))
            {
                //obj.SetValue(value.secname);
                obj.SetValue(MSequence.GetDocumentNo(_ctx.GetAD_Client_ID(), "AD_Org", null, _ctx));
            }
            else
            {
                obj.SetValue(value.searchkey);
            }

            obj.SetM_Attribute_ID(value.attributeID);
            // Added by Shifali on 16th July 2020 to set seq no
            string str = "SELECT NVL(MAX(SeqNo),0)+10 AS DefaultValue FROM M_AttributeValue WHERE M_Attribute_ID=" + obj.GetM_Attribute_ID();
            obj.Set_Value("SeqNo", Util.GetValueOfInt(DB.ExecuteScalar(str, null, null)));
            if (obj.Save())
            {
                return obj.GetM_AttributeValue_ID().ToString();
            }
            return Msg.GetMsg(_ctx, "VA005_UnableToSelectionSave");
        }

        public List<VA005_ShowGrideData> ShowGrideData(VA005_ShowGrideData value)
        {
            List<VA005_ShowGrideData> obj = new List<VA005_ShowGrideData>();
            //string sql = "select name,value,m_attributevalue_id from m_attributevalue where m_attribute_id='" + value.attributevalueid + "' and isactive='Y'";
            string sql = "select name,value,m_attributevalue_id,seqno from m_attributevalue where m_attribute_id='" + value.attributevalueid + "' and isactive='Y' ORDER BY M_Attributevalue_ID DESC";
            DataSet ds = DB.ExecuteDataset(sql, null, null);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    obj.Add(new VA005_ShowGrideData()
                    {
                        searchkey = Convert.ToString(ds.Tables[0].Rows[i]["value"]),
                        name = Convert.ToString(ds.Tables[0].Rows[i]["name"]),
                        attributevalueid = Convert.ToInt32(ds.Tables[0].Rows[i]["m_attributevalue_id"])
                    });
                }
            }

            return obj;
        }

        public string SaveAttributeuses(VA005_SaveAttributeuses value)
        {
            MAttributeUse obj = new MAttributeUse(_ctx, 0, null);
            //obj.SetM_AttributeSet_ID(value.attributsetid);
            //obj.SetM_Attribute_ID(value.lablename);

            obj.SetM_AttributeSet_ID(value.lablename);
            obj.SetM_Attribute_ID(value.attributsetid);

            var sql = "select max(seqno) as seqno from M_AttributeUse where isactive='Y'";
            DataSet ds = DB.ExecuteDataset(sql, null, null);
            value.sqno = Util.GetValueOfInt(ds.Tables[0].Rows[0]["seqno"]);

            obj.SetSeqNo(value.sqno + 10);
            if (obj.Save())
            {
                //return obj.GetM_AttributeSet_ID().ToString();
                return obj.GetM_Attribute_ID().ToString();
            }
            return Msg.GetMsg(_ctx, "VA005_UnableToSaveAttributeUses");
        }

        public String DeleteAttributeValue(VA005_DeleteAttributeValue value)
        {
            MAttributeValue obj = new MAttributeValue(_ctx, value.attributeID, null);
            int attvalid = obj.GetM_AttributeValue_ID();
            string _result = "";
            //try
            //{
            if (!obj.Delete(true))
            {
                ValueNamePair pp = VLogger.RetrieveError();
                _result = pp.ToString();
            }
            //}
            //catch(Exception e)
            //{
            //    return e.Message;
            //}
            //if (_result)
            //{
            //    //return Msg.GetMsg(_ctx,"VA005_Deleted");
            //    return attvalid.ToString();
            //}
            //else
            //{
            //return Msg.GetMsg(_ctx,"VA005_NotDeleted");
            //}
            return _result;
        }


        public class VA005_SaveAttribute
        {
            public string name { get; set; }
            public string description { get; set; }
            public string attributetype { get; set; }
            public int ID { get; set; }

            public bool mandatory { get; set; }
            public bool isactivefield { get; set; }
            public bool istanceattribute { get; set; }

            //public string searchkey {get;set;}
            //public string secname { get; set; }
        }

        public class VA005_SaveSelectionList
        {
            public string searchkey { get; set; }
            public string secname { get; set; }
            public int attributeID { get; set; }
            // public int attributename { get; set; }
        }

        public class VA005_ShowGrideData
        {
            public string searchkey { get; set; }
            public string name { get; set; }
            public int attributevalueid { get; set; }
        }

        public class VA005_SaveAttributeuses
        {
            public int attributsetid { get; set; }
            public int lablename { get; set; }
            public int sqno { get; set; }
        }

        public class VA005_DeleteAttributeValue
        {
            public int attributeID { get; set; }

        }


    }
}