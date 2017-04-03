using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using DBAccess;
using viviapi.WebComponents.Web;

namespace viviAPI.WebAdmin.Console.Sys
{
    public partial class Console_CleanUpData : ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.BLL.ManageFactory.CheckSecondPwd();
            if (!this.IsPostBack)
            {
                //this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                //this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", string.Format("WdatePicker({{maxDate:'{0}'}})",DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd 00:00:00")));
            }
        }

        protected void btndel_Click(object sender, EventArgs e)
        {
            if (!viviapi.BLL.ManageFactory.SecPwdVaild(this.txtcaozuo.Text.Trim()))
            {
                lbmsg.Text = "二级密码不正确，请重新输入!";
                return;
            }
            DateTime _endTime = DateTime.MinValue;
            DateTime.TryParse(this.EtimeBox.Text, out _endTime);
            TimeSpan _diff = DateTime.Now - _endTime;
            if (_diff.TotalDays < 7)
            {
                _endTime = DateTime.Now.AddDays(-7);
            }
            bool ordercardsuppchange = false;
            bool cardsend = false;
            bool order = false;
            foreach(ListItem li in cbl_clearType.Items)
            {
                if (li.Selected)
                {
                    if (li.Value == "order")
                    {
                        order = true;
                       
                    }
                    else if (li.Value == "ordercardsuppchange")
                    {
                        ordercardsuppchange = true;
                    }
                    else if (li.Value == "cardsend")
                    {
                        cardsend = true;
                    }
                }
            }
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            System.Text.StringBuilder where = new System.Text.StringBuilder();
            if (order)
            {
                bool _bank = false;
                bool _card = false;
                bool _sms = false;

                foreach (ListItem li in cb_where.Items)
                {
                    if (li.Selected)
                    {
                        if (li.Value == "bank")
                        {
                            sql.Append(@"
declare @t table(orderid varchar(30) collate Chinese_PRC_Stroke_90_CI_AS)
insert into @t select orderid from v_orderbank where addtime<@addtime {0}
delete from orderbankamt where orderid in (select orderid from @t)
delete from orderbanknotify where orderid in (select orderid from @t)
delete from orderbank where orderid in (select orderid from @t)");
                            _bank = true;
                        }
                        else if (li.Value == "card")
                        {
                            sql.Append(@"
declare @t1 table(orderid varchar(30) collate Chinese_PRC_Stroke_90_CI_AS)
insert into @t1 select orderid from v_ordercard where addtime<@addtime {0}
delete from ordercardamt where orderid in (select orderid from @t1)
delete from ordercardnotify where orderid in (select orderid from @t1)
delete from  ordercard where orderid in (select orderid from @t1)");
                            _card = true;
                        }
                        else if (li.Value == "sms")
                        {
                            _sms = true;
                        }

                    }
                }

                if (sql.Length > 0)
                {
                    bool isselected = false; ;
                    where.Append(" and status in (");
                    foreach (ListItem li in cb_stat.Items)
                    {
                        if (li.Selected)
                        {
                            if (!isselected)
                            {
                                where.Append(li.Value);
                            }
                            else
                            {
                                where.Append(","+li.Value);
                            }
                            isselected = true;
                        }
                    }
                    where.Append(")");

                    if (!isselected)
                    {
                        sql = new System.Text.StringBuilder();
                    }
                    else
                    {
                        sql.Replace("{0}", where.ToString());
                    }
                }                
            }

            if (ordercardsuppchange)
            {
                sql.Append(" delete from ordercardsuppchange where addtime<@addtime");
            }
            if (cardsend)
            {
                sql.Append(" delete from ordercardsend where initTime<@addtime");
            }

            if (sql.Length > 0)
            {
                try
                {
                    SqlParameter[] parameters = { new SqlParameter("@addtime", SqlDbType.DateTime,8) };
                    parameters[0].Value = _endTime;
                    if (DataBase.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters) > 0)
                    {
                        lbmsg.Text = "清理成功";
                    }
                    else
                    {
                        lbmsg.Text = "清理失败";
                    }
                }
                catch (Exception ex)
                {
                    lbmsg.Text = ex.Message;
                }
            }
            else
            { 
                
            }           
        }
    }
}