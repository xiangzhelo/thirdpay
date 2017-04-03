<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.manage.ManageEdit" Codebehind="Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style/union.css" type="text/css" rel="stylesheet" />
     <link href="../style/edit.css" type="text/css" rel="stylesheet" />
    <script src="../js/common.js" type="text/javascript"></script>

    <script type="text/javascript">
        $().ready(function() {
            var isUpdate = $("input[name='hf_isupdate']").val();
            if (isUpdate == "0") {
                $("#tr_lastloginip").hide();
                $("#tr_lastlogintime").hide();
                $("#tr_balance").hide();
            } else if (isUpdate == "1") {
                $("#tr_lastloginip").show();
                $("#tr_lastlogintime").show();
                $("#tr_balance").show();
            }
        });    
function backreturn(){
    history.go(-1);
}
    </script>

</head>
<body>    
    <form id="form1" runat="server">
    <asp:HiddenField ID="hf_isupdate" runat="server" Value="0" />
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
            <td colspan="4" class="title">
                后台操作员信息编辑 
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="td2">
                用户名：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtusername" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr_1" runat="server">
            <td class="td2">
                新密码：</td>
            <td class="td1">
               <asp:TextBox ID="txtpassword" runat="server" TextMode="Password"  Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr_2" runat="server">
            <td class="td2">
                二级密码：</td>
            <td class="td1">
                 <asp:TextBox ID="txtpassword2" runat="server" TextMode="Password"  Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                属性：
            </td>
            <td class="td1">
                <asp:CheckBox ID="ckb_SuperAdmin" runat="server" Text="超级管理员" />
                <asp:CheckBox ID="ckb_Agent" runat="server" Text="代理" Visible="false" />
            </td>
        </tr>        
        <tr>
            <td class="td2">
                权限 ：
            </td>
            <td class="td1" style="width:80%">
                <asp:CheckBoxList ID="cbl_roles" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="td2">
                状态 ：
            </td>
            <td class="td1">
                <asp:DropDownList ID="ddlStus" runat="server">
                    <asp:ListItem Value="1">正常</asp:ListItem>
                    <asp:ListItem Value="0">锁定</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td2">
                姓名 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtrelname" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr_lastloginip" style="display: none">
            <td class="td2">
                最近登录IP ：
            </td>
            <td class="td1">
                <asp:Label ID="lbllastloginip" runat="server" CssClass="lable" Width="160px" ></asp:Label>
            </td>
        </tr>
        <tr id="tr_lastlogintime" style="display: none">
            <td class="td2">
                最近登录时间 ：
            </td>
            <td class="td1">
                <asp:Label ID="lbllastlogintime" runat="server" CssClass="lable" Width="160px" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                提成类型 ：
            </td>
            <td class="td1">
                <asp:DropDownList ID="ddlCommissionType" runat="server">
                    <asp:ListItem Value="1">按条固定提成</asp:ListItem>
                    <asp:ListItem Selected="True" Value="2">按支付金额%</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td2">
                网银提成 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtCommission" runat="server">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                卡类提成 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtCardCommission" runat="server">0</asp:TextBox>
            </td>
        </tr>
        <tr id="tr_balance" style="display: none">
            <td class="td2">
                账号余额 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="lblbalance" runat="server" Enabled="false">0</asp:TextBox>
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
