using System;
using viviapi.BLL.User;
using viviapi.Model.User;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka.usermodule.safety
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Safeques : UserPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
            }
        }

        void InitForm()
        {
            if (string.IsNullOrEmpty(CurrentUser.question))
            {
                this.p_oldans.Visible = false;
                this.p_oldques.Visible = false;
            }
            else
            {
                this.p_oldans.Visible = true;
                this.p_oldques.Visible = true;
                this.txtoldques.Value = CurrentUser.question;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Isnew
        {
            get
            {
                return string.IsNullOrEmpty(this.CurrentUser.question);
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            string msg = "";

            string newques = txtnewques.Value;
            string newans = txtnewans.Value;
            string oldans = txtoldans.Value;

            if (!Isnew)
            {
                if (oldans != CurrentUser.answer)
                {
                    msg = "问题答案不对";
                }
            }
            if (string.IsNullOrEmpty(newques))
            {
                msg = "新问题为空";
            }
            else if (string.IsNullOrEmpty(newans))
            {
                msg = "新问题为空";
            }
            if (string.IsNullOrEmpty(msg))
            {
                CurrentUser.question = newques;
                CurrentUser.answer = newans;

                if (Factory.Update(CurrentUser, null))
                {
                    msg = "更新成功";
                }
                else
                {
                    msg = "更新失败";
                }
            }

            AlertAndRedirect(msg);
        }

      
    }
}
