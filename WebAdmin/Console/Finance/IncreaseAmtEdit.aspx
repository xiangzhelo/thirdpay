<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="viviAPI.WebAdmin.Console.Finance.IncreaseAmtEdit" Codebehind="IncreaseAmtEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../style/union.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />

    <script src="../js/common.js" type="text/javascript"></script>

    

    <script type="text/javascript">
        $().ready(function() {
            $("input[name$='txtuserId']").blur(function() {
                var userid = $(this).val();
                if (userid > 0) {
                    $.get("IncreaseAmtEdit.aspx", { user: userid, t: Math.random() }, function(result) {
                        $("#lblbalance").text(result);
                    });
                }
            });
            $("input[name$='txtincreaseAmt']").blur(function() {
                var amt = $(this).val();
                var patt = /^[0-9]*(\.[0-9]{1,2})?$/;
                if (!patt.test(amt)) {
                    alert("格式不正确");
                    return false;
                }
            });
            $("#btnAdd").click(function() {
                var amt = $("#txtincreaseAmt").val();
                if (amt == null || amt == "") {
                    alert("表输入金额");
                    return;
                }
                var patt = /^[0-9]*(\.[0-9]{1,2})?$/;
                if (!patt.test(amt)) {
                    alert("格式不正确");
                    return false;
                }
            });
        })
        function backreturn() {
            history.go(-1);
        }
    </script>
  
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
            <td colspan="4" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 24px">
                加款扣款
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="td2">
                操作类型：
            </td>
            <td class="td1">
                <asp:RadioButtonList ID="rbl_optype" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">加款</asp:ListItem>
                    <asp:ListItem Value="2">扣款</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="td2">
                用户ID ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtuserId" runat="server" Width="200px"></asp:TextBox>
                <asp:CustomValidator ID="CustomValidator1" runat="server" 
                    ControlToValidate="txtuserId" Display="Dynamic" ErrorMessage="不存在此用户" 
                    onservervalidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td class="td2">
               账上余额：
            </td>
            <td class="td1">
                <asp:Label ID="lblbalance" runat="server" Text="0" CssClass="input" Width="50px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                异动金额 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtincreaseAmt" runat="server" Width="200px"></asp:TextBox>                
            </td>
        </tr>
        <tr>
            <td class="td2">
                备注 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtdesc" runat="server" Width="60%" Rows="4" 
                    TextMode="MultiLine"  CssClass="lable"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 20px">
                <div align="center">
                    <asp:Button ID="btnAdd" runat="server" Text="提 交" OnClick="btnAdd_Click"></asp:Button>
                    <input type="button" value="返 回" onclick="backreturn()" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
