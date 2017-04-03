<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.Order.BankResetOrder" Codebehind="BankResetOrder.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../js/common.js" type="text/javascript"></script>
     <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />
<script type="text/javascript">
     
function backreturn(){
    history.go(-1);
}
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <table width="100%" border="0" cellspacing="1" cellpadding="3">
            <tr>
                <td colspan="4" class="title">
                   网银补单</td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="td2">
                    </td>
                <td class="td1">
                   上家接口卡单的情况下，可以在这里给客户补单！
                </td>
            </tr>
            <tr>
                <td class="td2">
                    订单号：</td>
                <td class="td1">
                    <asp:TextBox ID="txtOrderId" runat="server" Width="200px" MaxLength="30" ></asp:TextBox>
                    
                    <asp:RequiredFieldValidator ID="rfv_order" runat="server" 
                        ControlToValidate="txtOrderId" Display="Dynamic" ErrorMessage="请输入订单号"></asp:RequiredFieldValidator>
                    
                </td>
            </tr>
            <tr>
                <td class="td2">
                    订单成功金额：</td>
                <td class="td1">
                    <asp:TextBox ID="txtOrderAmt" runat="server" Width="200px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv_amt" runat="server" 
                        ControlToValidate="txtOrderAmt" Display="Dynamic" ErrorMessage="请输入订单金额"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rev_amt" runat="server" 
                        ControlToValidate="txtOrderAmt" Display="Dynamic" ErrorMessage="订单金额不正确" 
                        ValidationExpression="^{0,1}\d{1,}\.{0,1}\d{0,}$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    提交接口商：</td>
                <td class="td1">
                    <asp:DropDownList ID="ddlSupp" runat="server">
                        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    </td>
                <td class="td1">
                   <asp:Button ID="btnAdd" runat="server" Text="提 交" OnClick="btnAdd_Click"/>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    </td>
                <td class="td1">
                   <%--说明：如果是扣量，被商户发现了，就让用户提交一个差不多的订单，然后通过这里给他补单！--%>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
