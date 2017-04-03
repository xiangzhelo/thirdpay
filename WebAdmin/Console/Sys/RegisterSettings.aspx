<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.Sys.RegisterSettings" ValidateRequest="false" Codebehind="RegisterSettings.aspx.cs" %>

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
                    商户注册/登录设置</td>
            </tr>
            <tr>
                <td class="td2">
                    是否开启注册：</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="ddlRegisterOpen" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    注册时默认等级：</td>
                <td class="td1" colspan="3">
                    <asp:DropDownList ID="ddlDefaultUserLevel" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    商户注册是否审核：</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="ddlRequiredAudit" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="td2">
                    是否允许通过邮件登录：</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rbl_AllowUserloginByEmail" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    是否允许通过手机登录：</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rbl_AllowUserloginByPhone" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    注册是否需要发送激活邮件：</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rbl_ActivationByEmail" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    被锁定账户的登录提示信息：</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txtLoginMsgForlock" runat="server" Width="400px" Text="你的账户被锁定，不能登录，请联系管理员"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    未被审核账户的登录提示信息：</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txtLoginMsgForUnCheck" runat="server" Width="400px" Text="您的账户未被审核，不能登录，请联系管理员审核"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    账户审核失败的登录提示信息：</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txtLoginMsgForCheckfail" runat="server" Width="400px" Text="您的账户未被审核通过，不能登录，请联系管理员"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    需要手机验证：</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rbl_PhoneAuthenticate" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    手机最多最大发送信息次数：</td>
                <td class="td1" colspan="3">
                    &nbsp;<asp:TextBox ID="txtSmsMaxSendTimes" runat="server" Width="227px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    用户默认扣量比例：</td>
                <td class="td1" colspan="3">
                    &nbsp;<asp:TextBox ID="txtDefaultCPSDrate" runat="server" Width="227px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    点卡版本：</td>
                <td class="td1" colspan="3">
                    &nbsp; <asp:DropDownList ID="ddlcardversion" runat="server" Width="150px">
                        <asp:ListItem Value="0" Selected="True">--点卡版本--</asp:ListItem>
                        <asp:ListItem Value="1">普及</asp:ListItem>
                        <asp:ListItem Value="2">专业</asp:ListItem>
                    </asp:DropDownList></td>
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
