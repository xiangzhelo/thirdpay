<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.Sys.OtherSettings" ValidateRequest="false" Codebehind="OtherSettings.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>站点设置</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="1" cellpadding="3">
            <tr>
                <td colspan="4" class="title">
                    其它设置</td>
            </tr>
            <tr>
                <td class="td2">
                    AppKey：</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txtAppKey" runat="server" Width="400px" Text=""></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    AppSecret：</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txtAppSecret" runat="server" Width="400px" Text=""></asp:TextBox></td>
            </tr>
            
            <tr>
                <td class="td2">
                    支付宝扫码logo_name：</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txtlogo_name" runat="server" Width="400px" Text=""></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    支付宝扫码goods_info_name：</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txtgoods_info_name" runat="server" Width="400px" Text=""></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    支付宝扫码goods_info_desc：</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txtgoods_info_desc" runat="server" Width="400px" Text=""></asp:TextBox></td>
            </tr>
            
            <tr>
                <td>
                    &nbsp;</td>
                <td class="td1" colspan="3">
                    <span style="padding-left: 3px; height: 40px">
                        <asp:Button ID="btn_Update" runat="server" Text="确认更新" OnClick="btnUpdate_Click" OnClientClick="allQQ()" />
                    </span>
                </td>
            </tr>
        </table>

        

    </form>
</body>
</html>
