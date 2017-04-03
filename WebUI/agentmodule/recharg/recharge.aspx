<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="recharge.aspx.cs" Inherits="viviAPI.WebUI7uka.agentmodule.recharg.Recharge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/usermodule/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />

    <script src="/usermodule/js/jquery-1.8.1.min.js" type="text/javascript"></script>
    <script src="/usermodule/js/formValidator.js" type="text/javascript"></script>
    <script src="/usermodule/js/public.js" type="text/javascript"></script>
    <script src="/usermodule/js/recharg.js" type="text/javascript"></script>

    <style type="text/css">
        /*原validator.css*/
.onShow,.onError,.onFocus,.onSuccess,.onLoad{ margin-top:2px; vertical-align:middle; padding:2px 6px 0px 2px; color:#69F; display:inline-block;*display:inline;zoom:1; background:#fff; font-size:12px}
.onShow,.onFocus,.onLoad{}
.onError{ color:#C00;}
.onSuccess{ color:#090}
         .c_btn_bg1
         {
             width: 120px;
             height: 26px;
             border: none;
             background: url("/style/images/c_btn_bg1.gif") no-repeat;
         }
        .c_btn_bg
        {
            width: 89px;
            height: 26px;
            border: none;
            background: url("/style/images/c_btn_bg.gif") no-repeat;
        }
        .r_content
        {
            padding: 10px 10px 0px 10px;
            width: 847px;
            min-height: 413px;
            border-top: 1px #cdcdcd solid;
            border-left: 1px #cdcdcd solid;
            border-right: 1px #cdcdcd solid;
            float: left;
        }
        .r_content .top_content
        {
            behavior: url(pie.htc);
            border-radius: 8px;
            position: relative;
            color: #ffffff;
            padding: 8px;
            margin: 0px auto;
            width: 831px;
            height: 47px;
            background-color: #acacac;
            margin-bottom: 10px;
            float: left;
        }
        .rightborder
        {
            width: 28px;
            height: 453px;
            background-image: url(/style/images/rightborder.jpg);
            background-repeat: no-repeat;
            float: left;
        }
        .czk
        {
            width: 821px;
            float: left;
        }
        .czk dt
        {
            margin-bottom: 10px;
            width: 801px;
            border-bottom: 1px #cccccc solid;
            float: left;
            height: 25px;
            line-height: 25px;
            padding-left: 20px;
            font-family: "微软雅黑";
            font-size: 13px;
            color: #060606;
        }
        .czk dd
        {
            margin-bottom: 10px;
            margin-left: 47px;
            width: 214px;
            height: 38px;
            float: left;
            line-height: 38px;
        }
        .czk dd a
        {
            color: #333333;
            text-decoration: none;
            float: left;
        }
        .czk dd input
        {
            margin-top: 13px;
            margin-right: 9px;
            float: left;
        }
        .czk dd a:link img
        {
            border: 0px #dddddd solid;
            width: 190px;
            height: 36px;
            float: left;
        }
        .czk dd a:hover img
        {
            border: 1px #ffaa33 solid;
            width: 190px;
            height: 36px;
            float: left;
        }
    </style>
</head>
<body>
    <form id="rechargeForm" method="post" action="GoToRecharge.aspx" target="_blank">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/usermodule/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/usermodule/recharg/index.aspx'">账户充值</a>
        &nbsp;&gt;&nbsp; <span>账户充值</span>
    </div>
    <!--右部表单开始-->
    <div id="list_content" style="padding-top: 0px;">
        <h2>
            在线充值</h2>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="2" colspan="3" align="left" >
                </td>
            </tr>
            <tr>
                <td height="39" align="left" >
                    充值金额:
                </td>
                <td align="left" >
                    <input type="text" name="rechargeMoney" id="rechargeMoney" maxlength="5" class="txt_01" title="请输入提现金额" otitle="请输入提现金额" />
				   
                </td>
                <td height="39" align="left" >
                   
                </td>
            </tr>
            <tr>
                <td height="39" align="left" >
                    到账金额:
                </td>
                <td align="left" >
                   <em class="font14"><b class="txtc">0</b></em>
                </td>
                <td height="39" align="left" >
                </td>
            </tr>
            <tr>
                <td height="39" colspan="10">
                    <div class="r_content">
                        <div class="top_content">
                            充值费率为通道费率
                        </div>
                        <dl class="czk">
                            <dt>选择充值方式</dt>
                            <dd>
                                <input type="radio" name="bank_list" id="bank_list" value="967" 
                                       checked="checked"/><img src="/style/images/paybank/pic08.jpg" align="absmiddle" alt="工商银行"
                                        title="工商银行" /></dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio1" value="970" />
                                    <img src="/style/images/paybank/pic09.jpg" align="absmiddle" alt="招商银行" title="招商银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio2" value="965" /><img
                                    src="/style/images/paybank/pic10.jpg" align="absmiddle" alt="建设银行" title="建设银行" /></dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio3" value="964" /><img
                                    src="/style/images/paybank/pic12.jpg" align="absmiddle" alt="农业银行" title="农业银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio4" value="980" /><img
                                    src="/style/images/paybank/pic20.jpg" align="absmiddle" alt="民生银行" title="民生银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio5" value="978" /><img
                                    src="/style/images/paybank/vpic23.jpg" align="absmiddle" alt="平安银行" title="平安银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio9" value="981" /><img
                                    src="/style/images/paybank/pic19.jpg" align="absmiddle" alt="交通银行" title="交通银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio10" value="986" /><img
                                    src="/style/images/paybank/pic18.jpg" align="absmiddle" alt="光大银行" title="光大银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio11" value="971" /><img
                                    src="/style/images/paybank/vpic19.jpg" align="absmiddle" alt="邮政储蓄" title="邮政储蓄" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio6" value="972" /><img
                                    src="/style/images/paybank/pic15.jpg" align="absmiddle" alt="兴业银行" title="兴业银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio7" value="963" /><img
                                    src="/style/images/paybank/vpic01.jpg" align="absmiddle" alt="中国银行" title="中国银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio13" value="989" /><img
                                    src="/style/images/paybank/pic16.jpg" align="absmiddle" alt="北京银行" title="北京银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio8" value="992" /><img
                                    src="/style/images/paybank/vpic26.jpg" align="absmiddle" alt="支付宝支付" title="支付宝支付" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio12" value="993" /><img
                                    src="/style/images/paybank/pic100.gif" align="absmiddle" alt="财付通" title="财付通" />
                            </dd>
                            <dd>
                            </dd>
                        </dl>
                        <div class="content_bot">
                            <input type="submit" name="btnsubmit" value="提交充值"  id="btnsubmit" class="btn btn-primary" onclick="return CheckForm();" />
                            <span id="callinfo" class="txtr" style="color: Red;
                                    font-weight: bold"> </span>
                        </div>

                    </div>
                </td>
            </tr>
        </table>
    </div>
    
    </form>
</body>
</html>
