<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="applycost.aspx.cs" Inherits="viviAPI.WebUI7uka.usermodule.settlement.Applycost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/usermodule/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />
</head>
<body>
    <form id="form1" runat="server">
    <%--<div class="breadCrumb">
        <a href="" onclick="parent.location.href='/usermodule/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/usermodule/settlement/index.aspx'">结算提现</a>
        &nbsp;&gt;&nbsp; <span>申请提现</span>
    </div>--%>
    <input name="v$id" type="hidden" value="applycost" />
    <input name="v$fid" type="hidden" value="ruili" />
    <!--右部表单开始-->

    <div id="list_content" style="padding-top: 0px;">
        <h2>
            申请提现</h2>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="6" align="left" >
                    即时提现申请,如果是按天结算客户,不必提交申请,财务会定时进行转款,谢谢合作
                </td>
            </tr>
            <tr>
                <td height="39" align="right"  style="width: 15%">
                    商户名称:
                </td>
                <td align="left"  style="padding-left: 15px; width: 35%">
                    <input id="txtUserName" runat="server" type="text"  size="25" />
                </td>
                <td height="39" align="left"  colspan="4">
                </td>
            </tr>
            <tr>
                <td height="39" align="right" >
                    可提金额:
                </td>
                <td align="left"  style="padding-left: 15px;">
                    <asp:Literal ID="litAnableAmount" runat="server"></asp:Literal>
                </td>
                <td height="39" align="left"  style="width: 15%" colspan="4">
                   
                </td>
            </tr>
            <tr>
                <td height="39" align="right" >
                    银行帐号:
                </td>
                <td align="left"  style="padding-left: 15px;">
                    <asp:Literal ID="litBankAccout" runat="server"></asp:Literal>
                </td>
                <td height="39" align="left"  style="width: 15%" colspan="4">
                   
                </td>
            </tr>
            <tr>
                <td height="39" align="right" >
                    提现密码:
                </td>
                <td align="left"  style="padding-left: 15px;">
                    <asp:TextBox ID="txtcashpwd" runat="server" CssClass="txt_01" TextMode="Password" MaxLength="25"></asp:TextBox>
                    <asp:Literal ID="litWithdrawPwd" runat="server"></asp:Literal> 
                </td>
                <td height="39" align="left"  colspan="2">
                </td>
            </tr>
            <tr>
                <td height="39" align="right" >
                    提现金额:
                </td>
                <td align="left"  style="padding-left: 15px;">
                    <input id="txtApplyMoney" runat="server" type="text" class="txt_01" size="25" value="" />
                </td>
                <td height="39" align="left"  colspan="2">
                </td>
            </tr>
           
            
        </table>
        <table width="60%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="35" align="center" valign="bottom">
                    <asp:Button ID="btnpost" runat="server" Text="申请" CssClass="btn btn-primary" OnClick="btnpost_Click" />&nbsp;
                    <input name="b$close" type="submit" class="btn btn-primary" value="关闭" onclick="javascript:window.parent.TINY.box.hide();" />
                    &nbsp;<asp:Label ID="lblMessage" runat="server"  Font-Bold="True"
                        ForeColor="#FF3300"></asp:Label>
                    <td align="right">
                        &nbsp;
                    </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
