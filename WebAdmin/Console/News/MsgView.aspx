<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" Inherits="viviAPI.WebAdmin.Console.News.MsgView"
    CodeBehind="MsgView.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
     <table width="100%" border="0" cellspacing="1" cellpadding="3">
            <tr>
                <td colspan="4" class="title">
                    消息查看</td>
            </tr>
        </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="td2b">
                序号 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblID" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                发信息人类型 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblsenderUserType" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                发信息人ID ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblsendId" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                发信息人名称 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblsender" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                收信人类型 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblreceiverType" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                收信人ID ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblreceiverId" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                收信人名称 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblreceiver" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                标题 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblmsgtitle" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                内容 ：
            </td>
            <td class="td1b">
                <asp:Literal ID="lblmsgContent" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                新增时间 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lbladdtime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                是否已阅 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblisRead" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                阅读时间 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblreadTime" runat="server"></asp:Label>
            </td>
        </tr>
         <tr>
                        <td align="center" style="width: 118px; height: 25px;">
                        </td>
                        <td align="left" style="width: 550px; height: 25px;">
                          
                            <input type="button" value="返  回" onclick="javascript:location.href='MsgList.aspx'" />
                        </td>
                    </tr>
    </table>
    </form>
</body>
</html>
