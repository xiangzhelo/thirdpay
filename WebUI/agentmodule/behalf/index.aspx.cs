﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.Communication;

namespace viviAPI.WebUI7uka.agentmodule.behalf
{
    public partial class index : viviapi.WebComponents.Web.AgentPageBase
    {
        public string getnid = "";
        public string getnm = "";
        public int getmsgcount;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
            }
        }

        void InitForm()
        {
            getnid = CurrentUser.ID.ToString();
            getnm = CurrentUser.UserName;

            try
            {
                getmsgcount = viviapi.BLL.Communication.InternalMessage.GetUserMsgCount(CurrentUser.ID);

            }
            catch { }
        }
    }
}
