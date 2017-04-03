<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.User.UserDomain"
    CodeBehind="UserDomain.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../style/admin.css?v=1" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />
    <script src="../js/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function backreturn() {
            history.go(-1);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
            <td colspan="4" class="title">
                商户域名编辑
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="td2b">
                商户ID ：
            </td>
            <td class="td1b">
                <asp:TextBox ID="txtuserid" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr style="display: none">
            <td class="td2b">
                siteip ：
            </td>
            <td class="td1b">
                <asp:TextBox ID="txtsiteip" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr style="display: none">
            <td class="td2b">
                sitetype ：
            </td>
            <td class="td1b">
                <asp:TextBox ID="txtsitetype" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2b">
               名称 ：
            </td>
            <td class="td1b">
                <asp:TextBox ID="txthostName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                域名 ：
            </td>
            <td class="td1b">
                <asp:TextBox ID="txthostUrl" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                状态 ：
            </td>
            <td class="td1b">
                <asp:RadioButtonList ID="rblstatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1" Selected="True">已开启</asp:ListItem>
                        <asp:ListItem Value="2">已关闭</asp:ListItem>
                    </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                说明 ：
            </td>
            <td class="td1b">
                <asp:TextBox ID="txtdesc" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
                        <td align="center" style="width: 118px; height: 25px;">
                        </td>
                        <td align="left" style="width: 550px; height: 25px;">
                            <asp:Button ID="BtnSubmit" runat="server" Text="提  交" OnClick="BtnSubmit_Click" OnClientClick="return getContent();" />
                            <input type="button" value="返  回" onclick="javascript:location.href='DomainList.aspx'" />
                        </td>
                    </tr>
    </table>
    </form>
</body>
</html>
