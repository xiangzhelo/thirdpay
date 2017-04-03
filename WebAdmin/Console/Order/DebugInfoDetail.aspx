<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.order.Console_Order_DebugInfoShow" Codebehind="DebugInfoDetail.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../style/union.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />
    <script src="../js/common.js" type="text/javascript"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
            <td colspan="4" class="title">
                日志详细查看
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="td2">
                编号 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblid" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                类型 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblbugtype" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                用户ID ：
            </td>
            <td class="td1">
                <asp:Label ID="lbluserid" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                用户订单号 ：
            </td>
            <td class="td1">
                <asp:Label ID="lbluserorder" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                Url ：
            </td>
            <td class="td1" style="word-break:break-all">
                <asp:Label ID="lblurl" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                错误代码 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblerrorcode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                错误信息 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblerrorinfo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                详细信息 ：
            </td>
            <td class="td1">
                <asp:Label ID="lbldetail" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                时间 ：
            </td>
            <td class="td1">
                <asp:Label ID="lbladdtime" runat="server"></asp:Label>
            </td>
        </tr>
         <tr>
            <td colspan="4" style="height: 20px">
                <div align="center">
                <br />                    
                    <input type="button" value="关 闭" onclick="javascript:window.close()" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
