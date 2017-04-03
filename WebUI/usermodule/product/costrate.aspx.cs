using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.Channel;
using viviapi.Model.Channel;
using viviapi.BLL.Payment;
using viviapi.Model.Payment;
using viviapi.Model;
using viviapi.Model.Order;
using viviapi.Model.User;
using viviapi.WebComponents.Web;
using viviLib;
using viviLib.Web;
using viviLib.Security;
using viviapi.BLL;
using DBAccess;
using System.Data;

namespace viviAPI.WebUI7uka.usermodule.product
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Costrate : UserPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string flag = Request.QueryString["flag"];
                int uid = CurrentUser.ID;
                string channelId = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(flag) && !string.IsNullOrEmpty(channelId))
                {

                    ChannelTypeUserInfo info = new ChannelTypeUserInfo();
                    info.typeId = Convert.ToInt32(channelId);
                    info.userId = CurrentUser.ID;
                    info.userIsOpen = flag == "on" ? true : false;
                    info.sysIsOpen = null;
                    info.addTime = DateTime.Now;
                    info.updateTime = DateTime.Now;
                    viviapi.BLL.Channel.ChannelTypeUsers.Add(info);
                    
                }
                LoadData();
            }
        }

        void LoadData()
        {
            DataTable data = viviapi.BLL.Channel.ChannelType.GetCacheList();

            if (!data.Columns.Contains("payrate"))
                data.Columns.Add("payrate", typeof(double));
            if (!data.Columns.Contains("plmodestatus"))
                data.Columns.Add("plmodestatus", typeof(string));
            if (!data.Columns.Contains("usermodestatus"))
                data.Columns.Add("usermodestatus", typeof(string));
            
            foreach (DataRow dr in data.Rows)
            {
                //if (dr["release"] != DBNull.Value && Convert.ToBoolean(dr["release"]) == false)
                //{
                //    //data.Rows.Remove(dr);
                //    continue;
                //}

                int typeId = int.Parse(dr["typeId"].ToString());

                bool isuserOpen = true;
                bool issysOpen = false;
                ChannelTypeUserInfo setting = ChannelTypeUsers.GetCacheModel(UserId, typeId);
                if (setting != null)
                {
                    if (setting.userIsOpen.HasValue)
                    {
                        isuserOpen = setting.userIsOpen.Value;
                    }
                }

                ChannelTypeInfo typeInfo = ChannelType.GetCacheModel(typeId);
                switch (typeInfo.isOpen)
                {
                    case OpenEnum.AllClose:
                        issysOpen = false;
                        break;
                    case OpenEnum.AllOpen:
                        issysOpen = true;
                        break;
                    case OpenEnum.Close:
                        issysOpen = false;
                        if (setting != null)
                        {
                            if (setting.sysIsOpen.HasValue)
                                issysOpen = setting.sysIsOpen.Value;
                        }
                        break;
                    case OpenEnum.Open:
                        issysOpen = true;
                        if (setting != null && setting.sysIsOpen.HasValue)
                        {
                            issysOpen = setting.sysIsOpen.Value;
                        }
                        break;
                }

                dr["payrate"] = 100 * viviapi.BLL.Finance.PayRate.Instance.GetUserPayRate(this.UserId, Convert.ToInt32(dr["typeId"]));
                if (isuserOpen)
                {
                    dr["usermodestatus"] = "right";
                }
                else
                {
                    dr["usermodestatus"] = "wrong";
                }
                if (issysOpen)
                {
                    dr["plmodestatus"] = "right";
                }
                else
                {
                    dr["plmodestatus"] = "wrong";
                }
            }

            rpt_paymode.DataSource = data;
            rpt_paymode.DataBind();

        }

        protected void b_search_Click(object sender, EventArgs e)
        {
            LoadData();
        }


    }
}
