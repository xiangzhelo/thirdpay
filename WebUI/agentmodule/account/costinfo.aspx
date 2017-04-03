<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="costinfo.aspx.cs" Inherits="viviAPI.WebUI7uka.agentmodule.account.Costinfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/agentmodule/static/style/master.css" rel="stylesheet" type="text/css" />
   
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/agentmodule/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/agentmodule/account/index.aspx'">我的账户</a>
        &nbsp;&gt;&nbsp; <span>结算信息</span>
    </div>
    <!--右部表单开始-->
    <div id="list_content">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr style="background: #d9edf7;">
                <td height="50" align="left" class="line_01">
                    <span style="color: #3a87ad; font-size: 20px; font-weight: bold; line-height: 50px;
                        padding-left: 20px;">结算信息</span>&nbsp;&nbsp;&nbsp;&nbsp;<span style="color: #ff0000;">通过实名认证后
                            可以申请变更</span><a href="/agentmodule/safety/safetrna.aspx" target="mainframe" style="color: #3a87ad;
                                font-size: 18px; font-weight: bold; text-decoration: none; float: right; margin-right: 100px;
                                line-height: 45px;">申请实名认证</a>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 2px solid #e1e7f1;">
            <tr>
                <td height="39" align="right" class="line_01" style="width: 150px; background: #e1e7f1;">
                    结算周期：
                </td>
                <td align="left" class="line_01" style="padding-left: 15px;">
                    网银收益：T+<asp:Literal ID="litbankdetentiondays" runat="server">0</asp:Literal>
                    点卡收益：T+<asp:Literal ID="litcarddetentiondays" runat="server">0</asp:Literal>
                    其它收益：T+<asp:Literal ID="litotherdetentiondays" runat="server">0</asp:Literal>
                    <div style="float: right; text-align: left; margin-right: 80px;">
                        <span style="font-weight: bold; color: Red;">申请结算模式变更请联系平台客服：</span><a target="_blank"
                            href="http://wpa.qq.com/msgrd?v=3&uin=240472266&site=qq&menu=yes"><img border="0"
                                src="http://wpa.qq.com/pa?p=2:240472266:51" alt="点击这里给我发消息" title="点击这里给我发消息" /></a>
                        <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=1954077039&site=qq&menu=yes">
                            <img border="0" src="http://wpa.qq.com/pa?p=2:1954077039:51" alt="点击这里给我发消息" title="点击这里给我发消息" /></a></div>
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 150px; background: #e1e7f1;
                    border: none;">
                    结算模式：
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none;">
                    <asp:Literal ID="litpmode" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 150px; background: #e1e7f1;
                    border: none;">
                    开户姓名：
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none;">
                    <asp:Literal ID="litPayeeName" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 150px; background: #e1e7f1;
                    border: none;">
                    结算银行：
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none;">
                    <asp:Literal ID="litPayeeBank" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 150px; background: #e1e7f1;
                    border: none;">
                    <div runat="server" id="oyhkh1">
                        银行卡号：</div>
                    <div runat="server" id="oyhkh2">
                        支付宝账号：</div>
                    <div runat="server" id="oyhkh3">
                        财付通账号：</div>
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none;">
                    <asp:Literal ID="litUserViewBankAccout" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr id="khzh" runat="server">
                <td height="39" align="right" class="line_01" style="width: 150px; background: #e1e7f1;
                    border: none;">
                    开户支行：
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none;">
                    <asp:Literal ID="litProvince" runat="server"></asp:Literal><asp:Literal ID="litBankAddress"
                        runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <br>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 2px solid #e1e7f1;">
            <tr>
                <td height="39" align="right" class="line_01" style="width: 150px; background: #e1e7f1;">
                    申请变更结算方式
                </td>
                <td class="line_01" style="padding-left: 15px; line-height: 42px; color: #ff0000;
                    font-weight: bold;">
                    结算信息变更后需联系商户审核才可以使用
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 150px; background: #e1e7f1;
                    border: none;">
                    开户姓名：
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none;">
                    <asp:Literal ID="lit_username" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 150px; background: #e1e7f1;
                    border: none;">
                    收款方式：
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none;">
                    <asp:RadioButton ID="rb_bank" runat="server" GroupName="pmode" Text="银行账户" Checked="true"
                        AutoPostBack="True" OnCheckedChanged="rb_bank_CheckedChanged" />
                    <asp:RadioButton ID="rb_alipay" AutoPostBack="True" runat="server" GroupName="pmode"
                        Text="支付宝" OnCheckedChanged="rb_alipay_CheckedChanged" />
                    <asp:RadioButton ID="rb_tenpay" AutoPostBack="True" runat="server" GroupName="pmode"
                        Text="财付通" OnCheckedChanged="rb_tenpay_CheckedChanged" />
                </td>
            </tr>
            <tr id="tr_accoutType" runat="server">
                <td height="39" align="right" class="line_01" style="width: 150px; background: #e1e7f1;
                    border: none;">
                    账号类型：
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none;">
                    <asp:RadioButton ID="rb_accoutType0" runat="server" GroupName="accoutType" Text="私人账户"
                        Checked="true" />
                    <asp:RadioButton ID="rb_accoutType1" runat="server" GroupName="accoutType" Text="公司账户" />
                </td>
            </tr>
            <tr id="tr_bankselect" runat="server">
                <td height="39" align="right" class="line_01" style="width: 150px; background: #e1e7f1;
                    border: none;">
                    开户银行：
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none;">
                    <select id="ddlbankName" runat="server" class="txt_01">
                        <option value="1002">工商银行</option>
                        <option value="1005">农业银行</option>
                        <option value="1003">建设银行</option>
                        <option value="1026">中国银行</option>
                        <option value="1066">邮政储蓄银行</option>
                    </select>
                </td>
            </tr>
            <tr id="tr_province" runat="server">
                <td height="39" align="right" class="line_01" style="width: 150px; background: #e1e7f1;
                    border: none;">
                    开户省市：
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none;">
                    <asp:DropDownList ID="ddlprovince" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlprovince_SelectedIndexChanged"
                        CssClass="txt_01">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlcity" runat="server" CssClass="txt_01">
                        <asp:ListItem Value="">--市区--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="tr_address" runat="server">
                <td height="39" align="right" class="line_01" style="width: 150px; background: #e1e7f1;
                    border: none;">
                    支行名称：
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none;">
                    <input id="txtbankAddress" runat="server" class="txt_01" maxlength="50" title="支行名称格式：某某省分行某某市支行某某分理处请认真填写，以免造成提款延误"
                        size="80" />
                </td>
            </tr>
            <tr id="tr_oldcard" runat="server" visible="false">
                <td height="39" align="right" class="line_01" style="width: 150px; background: #e1e7f1;
                    border: none;">
                    <div runat="server" id="yyhkh1">
                        原银行卡号：</div>
                    <div runat="server" id="yyhkh2">
                        原支付宝账号：</div>
                    <div runat="server" id="yyhkh3">
                        原财付通账号：</div>
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none;">
                    <input id="txtoldaccount" runat="server" class="txt_01" maxlength="200" title="更新结算账户前需要先确认原银行帐号"
                        size="80" />
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 150px; background: #e1e7f1;
                    border: none;">
                    <div runat="server" id="yhkh1">
                        新银行卡号：</div>
                    <div runat="server" id="yhkh2">
                        新支付宝账号：</div>
                    <div runat="server" id="yhkh3">
                        新财付通账号：</div>
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none;">
                    <input id="txtaccount" runat="server" class="txt_01" maxlength="200" title="更新结算账户前需要先确认原银行帐号"
                        size="80" />
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 150px; background: #e1e7f1;
                    border: none;">
                    <div runat="server" id="qryhkh1">
                        确认新银行卡号：</div>
                    <div runat="server" id="qryhkh2">
                        确认新支付宝账号：</div>
                    <div runat="server" id="qryhkh3">
                        确认新财付通账号：</div>
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none;">
                    <input id="txtreaccount" runat="server" class="txt_01" maxlength="200" title="更新结算账户前需要先确认原银行帐号"
                        size="80" />
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="60" align="center" class="font8">
                    <asp:HiddenField ID="hfaction" runat="server" Value="0" />
                    &nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="提交申请" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                    &nbsp; <span class="txtr" id="callinfo" runat="server" style="color: Red; font-weight: bold">
                    </span>
                </td>
                <td align="right">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
