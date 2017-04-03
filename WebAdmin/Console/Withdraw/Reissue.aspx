<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.Withdraw.Reissue" Codebehind="Reissue.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />
    <script src="../js/common.js" type="text/javascript"></script>
    
<script type="text/javascript">
    $().ready(function() {
    var usertype = $("input[name='rbluserType']:checked").val();       
        if (usertype == "3") {
            $("#ddlmemvip").show();
            $("#ddlpromvip").hide();
        }
        else if (usertype == "4") {
            $("#ddlpromvip").show();
            $("#ddlmemvip").hide();
        }
        $("input[name='rbluserType']").click(function() {
            var usertype = $(this).val();   
             alert(usertype);         
            if (usertype == "3") {
                $("#ddlmemvip").show();
                $("#ddlpromvip").hide();
            }
            else if (usertype == "4") {
                $("#ddlpromvip").show();
                $("#ddlmemvip").hide();
            }
        });
    })     
function backreturn(){
    history.go(-1);
}
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <table width="100%" border="0" cellspacing="1" cellpadding="3">
            <tr>
                <td colspan="4" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);color: teal; background-repeat: repeat-x; height: 24px">
                    补单功能</td>
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
                    交易单号：</td>
                <td class="td1">
                    <asp:TextBox ID="txttrade_no" runat="server" Width="200px" MaxLength="30" ></asp:TextBox>                    
                </td>
            </tr>
            <tr>
                <td class="td2">
                    状态：</td>
                <td class="td1">
                    <asp:DropDownList
                        ID="ddlstatus" runat="server">
                        <asp:ListItem Value="255">初始状态</asp:ListItem>
                        <asp:ListItem Value="0">已受理</asp:ListItem>
                        <asp:ListItem Value="1">未受理</asp:ListItem>
                        <asp:ListItem Value="2">审核拒绝</asp:ListItem>
                        <asp:ListItem Value="3">代发成功</asp:ListItem>
                        <asp:ListItem Value="4">代发失败</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    代付金额：</td>
                <td class="td1">
                    <asp:TextBox ID="txtamount" runat="server" Width="200px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    提交接口商：</td>
                <td class="td1">
                    <asp:DropDownList ID="ddlSupplier" runat="server">
                        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    接口商处理单号：</td>
                <td class="td1">
                    <asp:TextBox ID="txtout_trade_no" runat="server" Width="200px" Text="system_Reissue" ></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="td2">
                    说明：</td>
                <td class="td1">
                    <asp:TextBox ID="txterror_message" runat="server" Width="200px" Text="后台补发" ></asp:TextBox>
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
