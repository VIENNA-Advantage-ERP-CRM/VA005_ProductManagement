using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using VAdvantage.Model;
using VAdvantage.Utility;
using System.Web.Helpers;
using System.Web.Hosting;
using VIS.Classes;

namespace VA005.Models
{
    public class ProductCategortModel
    {
        public KeyNamePair AddCategory(string name, VAdvantage.Utility.Ctx ctx)
        {
            KeyNamePair category = null;
            VAdvantage.Model.MProductCategory pcat = new VAdvantage.Model.MProductCategory(ctx, 0, null);
            pcat.SetAD_Client_ID(ctx.GetAD_Client_ID());
            pcat.SetAD_Org_ID(ctx.GetAD_Org_ID());
            pcat.SetName(name);
            if (pcat.Save())
            {
                category = new KeyNamePair(pcat.Get_ID(), name);
                return category;
            }
            return category;
        }

        public bool UpdateCategory(Int32 id, string name, VAdvantage.Utility.Ctx ctx)
        {
            VAdvantage.Model.MProductCategory pcat = new VAdvantage.Model.MProductCategory(ctx, id, null);
            pcat.SetName(name);
            if (pcat.Save())
            {
                return true;
            }
            return false;
        }

        public bool UpdateCategory(Int32 id, String Name, String Value, String Producttype, String matPolicy, String Desc, Int32 attrSet, Int32 taxcat, Int32 assetGrp, Boolean Consumable, Int32 image_ID, VAdvantage.Utility.Ctx ctx)
        {
            VAdvantage.Model.MProductCategory pcat = new VAdvantage.Model.MProductCategory(ctx, id, null);
            pcat.SetName(Name);
            pcat.SetValue(Value);
            pcat.SetProductType(Producttype);
            pcat.SetMMPolicy(matPolicy);
            pcat.SetDescription(Desc);
            pcat.SetM_AttributeSet_ID(attrSet);
            pcat.SetC_TaxCategory_ID(taxcat);
            pcat.SetA_Asset_Group_ID(assetGrp);
            pcat.SetAD_Image_ID(image_ID);
            Tuple<String, String, String> mInfo = null;
            if (Env.HasModulePrefix("DTD001_", out mInfo))
            {
                pcat.SetDTD001_IsConsumable(Consumable);
            }
            if (pcat.Save())
            {
                return true;
            }
            return false;
        }        
        
        /// <summary>
        /// Save images
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="serverPath"></param>
        /// <param name="file"></param>
        /// <param name="imageID"></param>
        /// <param name="isDatabaseSave"></param>
        /// <returns></returns>
        public int SaveImage(Ctx ctx, string serverPath, HttpPostedFileBase file, int imageID, bool isDatabaseSave)
        {
            HttpPostedFileBase hpf = file as HttpPostedFileBase;

            string savedFileName = Path.Combine(serverPath, Path.GetFileName(hpf.FileName));
            hpf.SaveAs(savedFileName);
            MemoryStream ms = new MemoryStream();
            hpf.InputStream.CopyTo(ms);
            byte[] byteArray = ms.ToArray();
            FileInfo file1 = new FileInfo(savedFileName);
            if (file1.Exists)
            {
                file1.Delete(); //Delete Temporary file             
            }

            string imgByte = Convert.ToBase64String(byteArray);
            var id = CommonFunctions.SaveImage(ctx, byteArray, imageID, hpf.FileName.Substring(hpf.FileName.LastIndexOf('.')), isDatabaseSave);
            return id;
        }
    }
}