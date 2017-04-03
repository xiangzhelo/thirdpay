using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL.Finance;
using viviapi.BLL.User;
using viviapi.Model;
using viviapi.Model.Promotion;
using viviapi.Model.User;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ApiKeyEdit : ManagePageBase
    {

        /// <summary>
        /// 
        /// </summary>
        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private UserInfo _itemInfo = null;
        public UserInfo model
        {
            get
            {
                if (_itemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        _itemInfo = Factory.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        _itemInfo = new UserInfo();
                    }
                }
                return _itemInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        

        

        

    }
}
