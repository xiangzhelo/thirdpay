<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.Tools.Md5" Codebehind="Md5.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
      <table width="100%" border="0" cellspacing="1" cellpadding="3">
            <tr>
                <td colspan="4" class="title">
                    MD5加密工具</td>
            </tr>
            <tr>
                <td width="10%" class="td2">
                    被加密字符串：</td>
                <td width="90%" colspan="3" class="td1">
                    <asp:TextBox ID="txtMd5Str" runat="server" Width="80%" TextMode="MultiLine" Rows="4"></asp:TextBox></td>
            </tr>
            <tr>
                <td width="10%" class="td2">
                    字符编码：</td>
                <td width="90%" colspan="3" class="td1">
                    <asp:DropDownList ID="ddlencode" runat="server">
                    <asp:ListItem Value="gb2312">GB2312</asp:ListItem>
                    <asp:ListItem Value="gbk">GBK</asp:ListItem>
                    <asp:ListItem Value="utf-8">UTF-8</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="10%" class="td2">
                    加密结果：</td>
                <td width="90%" colspan="3" class="td1">
                    <asp:TextBox ID="txtresult" runat="server" Width="80%" TextMode="MultiLine" Rows="4"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="td1" colspan="3">
                    <span style="padding-left: 3px; height: 40px">
                        <asp:Button ID="btn_Update" runat="server" Text="加密" OnClick="btnUpdate_Click" />
                    </span>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
