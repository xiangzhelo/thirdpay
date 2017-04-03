<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="viviAPI.Gateway2018.Connect.Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>违法和不良信息举报中心</title>
    <link href="../static/style/report.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="top">
        <img src="/static/images/top.gif" alt="违法和不良信息举报中心" style="width: 910px" /></div>
        <div class="intro">
        <strong>通知：</strong>为了鼓励大家积极举报违法与不良信息，营造更加纯净健康的网络环境，我们为举报人准备了丰厚的奖品。如果您的举报信息经核查准确、真实，就能领取我们定期派发的精美礼品。我们会对您填写的所有个人资料严格保密。</div>
        <table width="910" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#ffffff">
        <tbody>
            <tr>
                <td>
                    <img alt="" height="32" src="/static/images/jbrk.gif" width="530" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table align="center" bgcolor="#cccccc" border="0" cellpadding="3" cellspacing="1"
                        width="100%">
                        <tr bgcolor="#ecf9e6">
                            <td colspan="2" height="23" style="padding-right: 8px; padding-left: 20px">
                                请如实填写用户信息<font color="#ff0000">（带*号为必填项，所有资料均会严格保密）</font>
                            </td>
                        </tr>
                        <tr bgcolor="#f5f5f5">
                            <td align="right" height="32" style="padding-right: 8px; padding-left: 8px; width: 30%;">
                                姓名：
                            </td>
                            <td style="padding-left: 8px" width="316">
                                <div align="left">
                                    <input name="txtUserName" type="text" id="txtUserName" runat="server" maxlength="20" style="border: 1px solid #A4ABB1;" tabindex="1" />
                                    <font color="red">*</font>
                                </div>
                            </td>
                        </tr>
                        <tr bgcolor="#ffffff">
                            <td align="right" height="32" style="padding-right: 8px; padding-left: 8px">
                                电子邮件：
                            </td>
                            <td style="padding-right: 8px; padding-left: 8px">
                                <div align="left">
                                    <input name="txtEmail" type="text" id="txtEmail" runat="server" maxlength="100" size="30" style="border: 1px solid #A4ABB1;" tabindex="1" />
                                    <font color="red">*</font></div>
                            </td>
                        </tr>
                        <tr bgcolor="#f5f5f5">
                            <td align="right" height="30" style="padding-right: 8px; padding-left: 8px">
                                联系电话：
                            </td>
                            <td style="padding-right: 8px; padding-left: 8px">
                                <div align="left">
                                    <input name="txtMoblie" type="text" id="txtMoblie" runat="server" maxlength="20" style="border: 1px solid #A4ABB1;" tabindex="1" />
                                    <font color="red">*</font></div>
                            </td>
                        </tr>
                        <tr bgcolor="#ffffff">
                            <td align="right" height="14" style="padding-right: 8px; padding-left: 8px">
                                &nbsp;信息所在详细网址<br />
                                (url)：
                            </td>
                            <td style="padding-right: 8px; padding-left: 8px">
                                <textarea name="txtUrl" id="txtUrl" runat="server" cols="35" rows="6" style="border: 1px solid #A4ABB1;" tabindex="1"></textarea>
                                <font color="red">*<br />
                                    *多个地址请使用回车分开</font>
                            </td>
                        </tr>
                        <tr bgcolor="#f5f5f5">
                            <td align="right" height="32" style="padding-right: 8px; padding-left: 8px">
                                被举报信息类型：
                            </td>
                            <td style="padding-right: 8px; padding-left: 8px">
                                <div align="left">                                    
                                    <select name="ddlType" id="ddlType" runat="server">
	<option selected="selected" value="0">-请选择-</option>
	<option value="1">淫秽色情</option>
	<option value="2">诈骗</option>
	<option value="3">病毒</option>
	<option value="4">其他违法和不良信息</option>
 
</select>
                                    <font color="red">*</font></div>
                            </td>
                        </tr>
                        <tr bgcolor="#ffffff">
                            <td align="right" height="32" style="padding-right: 8px; padding-left: 8px">
                                被举报详细内容：
                            </td>
                            <td style="padding-right: 8px; padding-left: 8px">
                                <textarea name="txtReason" id="txtReason" runat="server" cols="35" rows="8" style="border: 1px solid #A4ABB1;" tabindex="2" wrap="virtual"></textarea>
                            </td>
                        </tr>
                        <tr bgcolor="#f5f5f5">
                            <td align="center" height="32" style="padding-right: 8px; padding-left: 8px" colspan="2">
                                <span id="lblInfo" runat="server" style="color:Red;"></span>
                            </td>
                        </tr>
                        <tr align="middle" bgcolor="#ffffff">
                            <td colspan="2" height="30">
                                <asp:Button ID="btnSub" runat="server" Text="举报" Width="40px" 
                                    onclick="btnSub_Click" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnSearch" runat="server" Text="查询" Width="40px" 
                                    onclick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
     <br />
    <table width="910" border="0" align="center" cellpadding="0" cellspacing="0" style="border-top: 1px solid #ccc">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="27%" align="center">
                            <img src="/static/images/wjlogo1.gif" width="41" height="51" />
                        </td>
                        <td width="48%" align="center">
                            <p>
                                <span class="dbt">违法与不良信息举报中心</span><br />
                                <span class="ct3">净化网络环境 共建和谐网络</span></p>
                        </td>
                        <td width="25%" align="center">
                            <img src="/static/images/wjlogo2.gif" width="42" height="51" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
