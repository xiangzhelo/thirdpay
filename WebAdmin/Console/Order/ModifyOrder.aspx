<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.order.ModifyOrder" Codebehind="ModifyOrder.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />
    <script src="../js/common.js" type="text/javascript"></script>
    
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
                    修改订单功能</td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="td2">
                    </td>
                <td class="td1">
                  切换接口重新提交
                </td>
            </tr>
            
            <tr>
                <td class="td2">
                    订单号：</td>
                <td class="td1">
                    <asp:TextBox ID="txtOrder" runat="server" Width="200px" MaxLength="30" ></asp:TextBox>
                    
                    <asp:RequiredFieldValidator ID="rfv_order" runat="server" 
                        ControlToValidate="txtOrder" Display="Dynamic" ErrorMessage="请输入订单号"></asp:RequiredFieldValidator>
                    
                </td>
            </tr>
            <tr>
                <td class="td2">
                    卡面值：</td>
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
                   <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click"/>
                   <asp:Button ID="btnSend" runat="server" Text="保存并重新提交" onclick="btnSend_Click"/>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    </td>
                <td class="td1">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
