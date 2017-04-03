using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using viviLib.Web;
using viviapi.Model.User;
using viviapi.BLL.User;

public partial class Console_User_idimg : System.Web.UI.Page
{
    public int ItemInfoId
    {
        get
        {
            return WebBase.GetQueryStringInt32("id", 0);
        }
    }

    public string  show
    {
        get
        {
            return WebBase.GetQueryStringString("show", "");
        }
    }
    public usersIdImageInfo _ItemInfo = null;
    public usersIdImageInfo ItemInfo
     {
         get
         {
             if (_ItemInfo == null)
             {
                 if (this.ItemInfoId > 0)
                 {
                     viviapi.BLL.User.usersIdImage bll = new usersIdImage();
                     _ItemInfo = bll.Get(this.ItemInfoId);
                 }
                 else
                 {
                     _ItemInfo = new usersIdImageInfo();
                 }
             }
             return _ItemInfo;
         }
     }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (show == "on" && ItemInfo != null)
        {
            this.Response.Clear();
            this.Response.ContentType = ItemInfo.ptype;    
            Response.OutputStream.Write(ItemInfo.image_on,0,ItemInfo.filesize.Value);
            this.Response.End();
        }

        if (show == "down" && ItemInfo != null)
        {
            this.Response.Clear();
            this.Response.ContentType = ItemInfo.ptype;
            Response.OutputStream.Write(ItemInfo.image_down, 0, ItemInfo.filesize1.Value);
            this.Response.End();
        }
    }
}
