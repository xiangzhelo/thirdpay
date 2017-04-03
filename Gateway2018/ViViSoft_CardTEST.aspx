<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViViSoft_CardTEST.aspx.cs" Inherits="viviAPI.Gateway2018.ViViSoft_CardTEST" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        订单号：<asp:TextBox ID="txtSysOrderId" runat="server"></asp:TextBox>
&nbsp;<br />
        真实值：<asp:TextBox ID="txtTranAmt" runat="server"></asp:TextBox>
        <br />
        errCode：<asp:TextBox ID="txterrCode" runat="server"></asp:TextBox>
        <br />
         Msg：<asp:TextBox ID="txtMsg" runat="server"></asp:TextBox>
        <br />
        银行代号：<asp:TextBox ID="txtSuppId" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    </form>
</body>
</html>
