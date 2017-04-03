using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using viviapi.BLL.Finance.Agent;

namespace viviapi.SysInterface.Withdraw
{
    public class Notify
    {
        private Model.Finance.Agent.WithdrawAgent _model = null;
        public Model.Finance.Agent.WithdrawAgent model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
            }
        }

        #region DoNotify
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void DoNotify()
        {
            if (model != null
                && viviLib.Text.PageValidate.IsUrl(model.return_url)
                && model.is_cancel == false)
            {

                Model.Finance.Agent.WithdrawAgentNotify notifyInfo
                    = new Model.Finance.Agent.WithdrawAgentNotify();

                notifyInfo.userid = model.userid;
                notifyInfo.trade_no = model.trade_no;
                notifyInfo.out_trade_no = model.out_trade_no;

                String notify_id = notifyInfo.notify_id;
                String service = model.service;
                String input_charset = model.input_charset;
                String partner = model.userid.ToString();
                String sign_type = model.sign_type;
                String notify_time = notifyInfo.addTime.ToString("yyyyMMddHHmmss");
                String out_trade_no = model.out_trade_no;

                string amount_str = "0.00";
                int _trade_status = 1;
                if (model.is_cancel == true)
                {
                    _trade_status = 1;
                }
                else
                {
                    if (model.audit_status == 1)
                    {
                        _trade_status = 1;
                    }
                    else if (model.audit_status == 2)
                    {
                        _trade_status = 0;
                    }
                    else if (model.audit_status == 3)
                    {
                        _trade_status = 2;
                    }
                    if (model.audit_status == 2)
                    {
                        if (model.payment_status == 2)
                        {
                            _trade_status = 3;
                            amount_str = model.amount.ToString("f2");
                        }
                        if (model.payment_status == 3)
                        {
                            _trade_status = 4;
                        }
                    }
                }
                String trade_status = _trade_status.ToString();
                String error_message = String.Empty;

                string[] oriStr = { 
                          "service=" + service,
                          "input_charset=" + input_charset,
                          "partner=" + partner,
                          "sign_type=" + sign_type,                          
                          "notify_id=" + notify_id,
                          "notify_time=" + notify_time,
                          "out_trade_no=" + out_trade_no,
                          "trade_status=" + trade_status,
                          "error_message=" + error_message,
                          "amount_str=" + amount_str
                          };

                string[] sortedParamArray = CommonHelper.BubbleSort(oriStr);

                string paramStr = CommonHelper.BuildParamString(sortedParamArray);

                string apiKey = BLL.User.Factory.GetUserApiKey(model.userid);


                string sign = CommonHelper.md5(input_charset, paramStr
                    + apiKey).ToLower();



                string retUrl = model.return_url;

                string postData = string.Format("service={0}", service);
                postData += string.Format("&input_charset={0}", input_charset);
                postData += string.Format("&partner={0}", partner);
                postData += string.Format("&sign_type={0}", sign_type);
                postData += string.Format("&notify_id={0}", notify_id);
                postData += string.Format("&notify_time={0}", notify_time);
                postData += string.Format("&out_trade_no={0}", out_trade_no);
                postData += string.Format("&trade_status={0}", trade_status);
                postData += string.Format("&error_message={0}", error_message);
                postData += string.Format("&amount_str={0}", amount_str);
                postData += string.Format("&sign={0}", sign);

                try
                {
                    string retText = viviLib.Web.WebClientHelper.GetString(retUrl
                        , postData
                        , "get"
                        , System.Text.Encoding.GetEncoding(input_charset)
                        , 10000);

                    notifyInfo.resTime = DateTime.Now;

                    int notifystatus = 1;
                    if (retText == notify_id)
                    {
                        notifystatus = 2;
                    }
                    else
                    {
                        notifystatus = 0;
                    }
                    notifyInfo.notifyurl = retUrl;
                    notifyInfo.resText = retText;
                    notifyInfo.notifystatus = notifystatus;
                }
                catch (Exception ex)
                {
                    notifyInfo.notifyurl = retUrl;
                    notifyInfo.resText = "";
                    notifyInfo.notifystatus = 0;
                    notifyInfo.remark = ex.Message;
                    notifyInfo.resTime = DateTime.Now;
                }

                BLL.Finance.Agent.WithdrawAgentNotify bll
                    = new WithdrawAgentNotify();
                bll.Add(notifyInfo);
            }
        }
        #endregion

    }
}
