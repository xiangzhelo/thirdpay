<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.Sys.TransactionSettings" ValidateRequest="false" Codebehind="TransactionSettings.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>站点设置</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="1" cellpadding="3">
            <tr>
                <td colspan="4" class="title">
                    交易参数设置</td>
            </tr>
            <tr>
                <td width="10%" class="td2">
                    最小交易金额：</td>
                <td width="90%" colspan="3" class="td1">
                    <asp:TextBox ID="txtMinTranATM" runat="server" Width="227px">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    最大交易金额：</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtMaxTranATM" runat="server" Width="227px">5000</asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    网银来路检验：</td>
                <td colspan="3" class="td1">
                     <asp:RadioButtonList ID="rbl_CheckUrlReferrer" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="是" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                    </td>
            </tr>
            <tr style="display: none">
                <td class="td2">
                    订单号前缀：</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtOrderPrefix" runat="server" Width="227px">5000</asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    订单号重复：</td>
                <td colspan="3" class="td1">
                     <asp:RadioButtonList ID="rbl_CheckUserOrderNo" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="是" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                    </td>
            </tr>
            <tr>
                <td class="td2">
                    交易风险提示：</td>
                <td colspan="3" class="td1">
                    <asp:CheckBox ID="ckb_rw_bank" runat="server"  Text="网银"/>
                     <asp:CheckBox ID="ckb_rw_alipay" runat="server" Text="支付宝"/>
                      <asp:CheckBox ID="ckb_rw_alicode" runat="server" Text="支付宝扫码"/>
                       <asp:CheckBox ID="ckb_rw_wxpay" runat="server" Text="微信支付"/>
                    </td>
            </tr>
            <tr>
                <td width="10%" class="td2">
                    订单信息缓存时间：</td>
                <td width="90%" colspan="3" class="td1">
                    <asp:TextBox ID="txtExpiresTime" runat="server" Width="227px">5</asp:TextBox>(分钟)</td>
            </tr>
            <tr>
                <td class="td2">
                    记录交易错误日志（方便查错）：</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rbl_Debuglog" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="是" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="display: none">
                <td class="td2">
                    有来路站点个数：</td>
                <td class="td1" colspan="3">
                    &nbsp;<asp:TextBox ID="txtRefCount" runat="server" Width="227px">2</asp:TextBox>
                    （每用户）</td>
            </tr>
            <tr>
                <td class="td2">
                    无来路开启状态：</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rbl_WithoutRef" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
            </tr>
            <tr>
                <td class="td2">
                    启用扣量：</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rblOpenDeduct" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="td1" colspan="3">
                    <span style="padding-left: 3px; height: 40px">
                        <asp:Button ID="btn_Update" runat="server" Text="确认更新" OnClick="btnUpdate_Click" OnClientClick="allQQ()" />
                    </span>
                </td>
            </tr>
        </table>

        

    </form>
</body>
</html>
