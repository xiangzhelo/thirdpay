<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cashpass.aspx.cs" Inherits="viviAPI.WebUI7uka.usermodule.safety.Cashpass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/usermodule/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/page.css" />
</head>
<body>
    <form id="form1" runat="server">
    <%--<div class="breadCrumb">
        <a href="" onclick="parent.location.href='/usermodule/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/usermodule/account/index.aspx'">我的账户</a>
        &nbsp;&gt;&nbsp; <span>提现密码</span>
    </div>--%>
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title" style="color: Black;">
            <h2>
                提现密码修改</h2>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left"  style="color: Black; border: none;">
                    定期修改密码,有助于加强账户安全
                </td>
            </tr>
            <tr>
                <td height="45" align="right"  style="border: none; font-weight: bold;
                    color: Black; width: 150px;">
                    商户ID:
                </td>
                <td align="center"  style="padding-left: 15px; border: none; width: 240px;">
                    <input id="txtuserid" runat="server" type="text" class="txt_02" size="25" style="border: none;
                        background: none;" />
                </td>
                <td height="45" align="left"  style="border: none;">
                </td>
            </tr>
            <tr>
                <td height="45" align="right"  style="border: none; font-weight: bold;
                    color: Black;">
                    登录名称:
                </td>
                <td align="center"  style="padding-left: 15px; border: none; width: 240px;">
                    <input id="txtusername" runat="server" type="text" class="txt_02" size="25" style="border: none;
                        background: none;" />
                </td>
                <td height="45" align="left"  style="border: none;">
                </td>
            </tr>
            <tr>
                <td height="45" align="right"  style="border: none; font-weight: bold;
                    color: Black;">
                    登录密码:
                </td>
                <td align="left"  style="padding-left: 15px; border: none; width: 240px;">
                    <input id="txtloginpwd" runat="server" type="password" class="txt_01" size="25" value="" />
                </td>
                <td height="45" align="left"  style="border: none;">
                    请输入登录密码
                </td>
            </tr>
            <tr>
                <td height="45" align="right"  style="border: none; font-weight: bold;
                    color: Black;">
                    您的账户邮箱:
                </td>
                <td align="left"  style="padding-left: 15px; border: none; width: 240px;">
                    <input id="txtmail" runat="server" type="text" class="txt_01" size="25" value="" />
                </td>
                <td height="45" align="left"  style="border: none;">
                    请输入你的安全邮箱
                </td>
            </tr>
            <tr>
                <td height="45" align="right"  style="border: none; font-weight: bold;
                    color: Black;">
                    提现密码:
                </td>
                <td align="left"  style="padding-left: 15px; border: none; width: 240px;">
                    <input id="txtcashpass" runat="server" type="password" class="txt_01" size="25" value="" />
                </td>
                <td height="45" align="left"  style="border: none;">
                    请输入提现密码
                </td>
            </tr>
            <tr>
                <td height="45" align="right"  style="border: none; font-weight: bold;
                    color: Black;">
                    确认提现密码:
                </td>
                <td align="left"  style="padding-left: 15px; border: none; width: 240px;">
                    <input id="txtrecashpass" runat="server" type="password" class="txt_01" size="25"
                        value="" />
                </td>
                <td height="45" align="left"  style="border: none;">
                    请再次输入提现密码
                </td>
            </tr>
            <tr>
                <td height="45" align="right"  style="border: none; font-weight: bold;
                    color: Black;">
                </td>
                <td align="left"  style="padding-left: 15px; border: none; ">
                     <asp:LinkButton ID="lbtnSave" runat="server" Text="确认提交" CssClass="btn btn-primary" onclick="lbtnSave_Click"></asp:LinkButton>&nbsp;
                     <a href="/usermodule/account/safety.aspx" target="mainframe" class="btn btn-primary">
                        取消</a>
                </td>
                <td height="45" align="left" >
                    <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
