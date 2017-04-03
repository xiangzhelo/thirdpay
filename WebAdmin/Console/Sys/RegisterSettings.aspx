<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.Sys.RegisterSettings" ValidateRequest="false" Codebehind="RegisterSettings.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>վ������</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="1" cellpadding="3">
            <tr>
                <td colspan="4" class="title">
                    �̻�ע��/��¼����</td>
            </tr>
            <tr>
                <td class="td2">
                    �Ƿ���ע�᣺</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="ddlRegisterOpen" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    ע��ʱĬ�ϵȼ���</td>
                <td class="td1" colspan="3">
                    <asp:DropDownList ID="ddlDefaultUserLevel" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    �̻�ע���Ƿ���ˣ�</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="ddlRequiredAudit" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="td2">
                    �Ƿ�����ͨ���ʼ���¼��</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rbl_AllowUserloginByEmail" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    �Ƿ�����ͨ���ֻ���¼��</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rbl_AllowUserloginByPhone" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    ע���Ƿ���Ҫ���ͼ����ʼ���</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rbl_ActivationByEmail" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    �������˻��ĵ�¼��ʾ��Ϣ��</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txtLoginMsgForlock" runat="server" Width="400px" Text="����˻������������ܵ�¼������ϵ����Ա"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    δ������˻��ĵ�¼��ʾ��Ϣ��</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txtLoginMsgForUnCheck" runat="server" Width="400px" Text="�����˻�δ����ˣ����ܵ�¼������ϵ����Ա���"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    �˻����ʧ�ܵĵ�¼��ʾ��Ϣ��</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txtLoginMsgForCheckfail" runat="server" Width="400px" Text="�����˻�δ�����ͨ�������ܵ�¼������ϵ����Ա"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    ��Ҫ�ֻ���֤��</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rbl_PhoneAuthenticate" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    �ֻ�����������Ϣ������</td>
                <td class="td1" colspan="3">
                    &nbsp;<asp:TextBox ID="txtSmsMaxSendTimes" runat="server" Width="227px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    �û�Ĭ�Ͽ���������</td>
                <td class="td1" colspan="3">
                    &nbsp;<asp:TextBox ID="txtDefaultCPSDrate" runat="server" Width="227px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    �㿨�汾��</td>
                <td class="td1" colspan="3">
                    &nbsp; <asp:DropDownList ID="ddlcardversion" runat="server" Width="150px">
                        <asp:ListItem Value="0" Selected="True">--�㿨�汾--</asp:ListItem>
                        <asp:ListItem Value="1">�ռ�</asp:ListItem>
                        <asp:ListItem Value="2">רҵ</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="td1" colspan="3">
                    <span style="padding-left: 3px; height: 40px">
                        <asp:Button ID="btn_Update" runat="server" Text="ȷ�ϸ���" OnClick="btnUpdate_Click" OnClientClick="allQQ()" />
                    </span>
                </td>
            </tr>
        </table>

        

    </form>
</body>
</html>
