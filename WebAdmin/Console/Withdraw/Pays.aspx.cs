using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Aspose.Cells;
using viviapi.BLL.Supplier;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Data;

namespace viviAPI.WebAdmin.Console.Withdraw
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Pays : ManagePageBase
    {
        protected string PageTotalAmount = "0.00";
        protected string PageTotalCharges = "0.00";
        protected string PageTotalWithdrawAmt = "0.00";

        protected string TotalAmount = "0.00";
        protected string TotalCharges = "0.00";
        protected string TotalWithdrawAmt = "0.00";

        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();

            if (!this.IsPostBack)
            {
                #region 提现方式
                this.ddlmode.Items.Add(new ListItem("--提现方式--", ""));

                foreach (int num in Enum.GetValues(typeof(viviapi.Model.Finance.WithdrawMode)))
                {
                    this.ddlmode.Items.Add(new ListItem(Enum.GetName(typeof(viviapi.Model.Finance.WithdrawMode), num)
                        , num.ToString()));
                }
                #endregion

                #region 付款接口
                DataTable list = Factory.GetList("isdistribution=1").Tables[0];
                ddlSupplier.Items.Add(new ListItem("--付款接口--", ""));
                ddlSupplier.Items.Add(new ListItem("不走接口", "0"));
                foreach (DataRow dr in list.Rows)
                {
                    this.ddlSupplier.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
                }
                #endregion

                this.BindData();
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.Financial);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        #region BindData
        /// <summary>
        /// 
        /// </summary>
        private void BindData()
        {
            DataSet pageData = GetData(false);

            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            DataTable data = pageData.Tables[1];

            if (data != null && data.Rows.Count > 0)
            {
                PageTotalAmount = string.Format("{0:f2}", data.Compute("sum(amount)", ""));
                PageTotalCharges = string.Format("{0:f2}", data.Compute("sum(charges)", ""));
                PageTotalWithdrawAmt = string.Format("{0:f2}", data.Compute("sum(withdraw)", ""));
            }


            DataTable data2 = pageData.Tables[2];
            if (data2 != null && data2.Rows.Count > 0)
            {
                TotalAmount = string.Format("{0:f2}", data2.Rows[0]["tapplyAmt"]);
                TotalCharges = string.Format("{0:f2}", data2.Rows[0]["tCharges"]);
                TotalWithdrawAmt = string.Format("{0:f2}", data2.Rows[0]["trealPay"]);
            }

            this.rptdata.DataSource = data;
            this.rptdata.DataBind();
        }
        #endregion

        #region GetData
        /// <summary>
        /// 
        /// </summary>
        private DataSet GetData(bool isexport)
        {
            var listParam = new List<SearchParam>();
            listParam.Add(new SearchParam("status", 4));

            int tempId = 0;
            if (!String.IsNullOrEmpty(txtTranno.Text.Trim()))
            {
                listParam.Add(new SearchParam("tranno", txtTranno.Text.Trim()));
            }

            if (!String.IsNullOrEmpty(txtUserId.Text.Trim()))
            {
                if (int.TryParse(this.txtUserId.Text.Trim(), out tempId))
                {
                    listParam.Add(new SearchParam("userid", tempId));
                }
            }
            if (!string.IsNullOrEmpty(ddlSupplier.SelectedValue))
            {
                listParam.Add(new SearchParam("suppId", int.Parse(ddlSupplier.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(ddlbankName.SelectedValue))
            {
                listParam.Add(new SearchParam("payeebank", ddlbankName.SelectedValue));
            }
            if (!string.IsNullOrEmpty(txtAccount.Text.Trim()))
            {
                listParam.Add(new SearchParam("account", txtAccount.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtpayeeName.Text.Trim()))
            {
                listParam.Add(new SearchParam("payeename", txtpayeeName.Text.Trim()));
            }

            DataSet pageData = null;

            if (isexport == false)
            {
                pageData = viviapi.BLL.Finance.Withdraw.Instance.PageSearch(listParam, this.Pager1.PageSize,
                     this.Pager1.CurrentPageIndex, string.Empty, true);
            }
            else
            {
                pageData = viviapi.BLL.Finance.Withdraw.Instance.PageSearch(listParam, 10000,
                     this.Pager1.CurrentPageIndex, string.Empty, false);
            }
            return pageData;
        }
        #endregion

        #region btnSearch_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        #endregion

        #region btnExport_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbl_export_mode.SelectedValue == "2")
                {
                    #region txt
                    string ids = Request.Form["ischecked"];
                    if (string.IsNullOrEmpty(ids))
                        return;
                    var builder = new StringBuilder();

                    DataTable table = GetData(true).Tables[0];

                    foreach (DataRow row in table.Rows)
                    {
                        builder.AppendFormat("{0};{1};{2};{3};--;--;{4:f2}", row["UserName"], row["PayeeName"], row["Account"], row["PayeeBank"], row["RealAmt"]);
                        //builder.Append(string.Concat(new object[] { row["UserName"].ToString(), ";", row["PayeeName"], ";", row["Account"], ";", row["PayeeBank"], ";--;--;", row["RealAmt"] }));
                        builder.Append("\r\n");
                    }
                    string str3 = builder.ToString();
                    StringWriter writer = new StringWriter();
                    writer.Write(str3);
                    writer.WriteLine();
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Buffer = false;
                    HttpContext.Current.Response.Charset = "GB2312";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                    HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                    HttpContext.Current.Response.Write(writer);
                    HttpContext.Current.Response.End();
                    #endregion
                }
                else
                {
                    DataSet ds = GetData(true);
                    if (ds != null)
                    {
                        DataTable data = ds.Tables[1];
                        data.Columns.Add("sName", typeof(string));
                        foreach (DataRow dr in data.Rows)
                        {
                            dr["sName"] = Enum.GetName(typeof(viviapi.Model.SettledStatus), dr["status"]);
                        }
                        data.AcceptChanges();

                        data.TableName = "Rpt";
                        string path = Server.MapPath("~/common/template/xls/settle.xls");

                        var designer = new Aspose.Cells.WorkbookDesigner();
                        designer.Workbook = new Workbook(path);


                        //数据源 
                        designer.SetDataSource(data);
                        designer.Process();

                        designer.Workbook.Save(this.Response
                            , DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls"
                            , ContentDisposition.Attachment
                            , designer.Workbook.SaveOptions);

                    }
                }
            }
            catch (Exception ex)
            {
                AlertAndRedirect(ex.Message);
            }
        }
        #endregion

        #region 分页
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            BindData();
        }
        #endregion

        #region btnAllSettle_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAllSettle_Click(object sender, EventArgs e)
        {
            int success = 0;
            string resultStr = "";
            string ids = Request.Form["TranNoList"];
            if (string.IsNullOrEmpty(this.txtPassWord.Text))
            {
                ShowMessageBox("请输入二级密码");
            }
            else
            {
                if (!viviapi.BLL.ManageFactory.SecPwdVaild(this.txtPassWord.Text.Trim()))
                {
                    ShowMessageBox("二级密码不正确");
                }
                else
                {
                    if (!string.IsNullOrEmpty(ids))
                    {
                        foreach (string tranNo in ids.Split(','))
                        {
                            viviapi.Model.Finance.Withdraw itemInfo 
                                = viviapi.BLL.Finance.Withdraw.Instance.GetModel(tranNo);

                            if (itemInfo != null)
                            {
                                itemInfo.Paytime = DateTime.Now;

                                bool result = viviapi.BLL.Finance.Withdraw.Instance.Complete(itemInfo);
                                if (result == true)
                                {
                                    success++;
                                    var UserInfo = viviapi.BLL.User.Factory.GetModel(itemInfo.Userid);
                                    if (UserInfo!=null&&!string.IsNullOrEmpty(UserInfo.Tel))
                                    {
                                        #region  设置短信发送信息
                                        CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
                                        //ip格式如下，不带https://
                                        //app.cloopen.com:8883
                                        bool isInit = api.init("app.cloopen.com", "8883");
                                        api.setAccount("8a48b5515018a0f4015045e342b14990", "07c2d4f927a1443fb4ffffe158ee39b8");
                                        api.setAppId("8a216da8567745c001568c78ae030d62");
                                        #endregion

                                        string[] data = { UserInfo.full_name, itemInfo.Amount.ToString("f2") };

                                        Dictionary<string, object> retData = api.SendTemplateSMS(UserInfo.Tel, "108968", data);
                                        //短信发送失败
                                        if (retData["statusCode"].ToString() != "000000")
                                        {
                                            resultStr+= "\n用户" + UserInfo.full_name+",手机号:"+ UserInfo.Tel+ "通知短信发送失败.statusCode:" + retData["statusCode"] + ",statusMsg:" + retData["statusMsg"]; 
                                        }
                                    }
                                }
                                    
                            }
                        }

                        AlertAndRedirect("成功处理" + success.ToString() + "笔"+ resultStr);
                    }
                    else
                    {
                        ShowMessageBox("请选择要支付的记录!");
                    }
                }
            }
        }
        #endregion
    }
}

