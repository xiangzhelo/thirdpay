<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.SysMailConfig" ValidateRequest="false" Codebehind="SysMailConfig.aspx.cs" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>վ������</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="style/admin.css?" type="text/css" rel="stylesheet" />
    <link href="style/edit.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="1" cellpadding="3">
            <tr>
                <td colspan="4" class="title">
                    �ʼ�����</td>
            </tr>
            <tr>
                <td width="10%" class="td2">
                    �����ʼ���������</td>
                <td width="90%" colspan="3" class="td1">
                    <asp:TextBox ID="txtEmailServerAddress" runat="server" Width="227px"></asp:TextBox>�ʼ���������ַ ��ʽ��: smtp.gmail.com</td>
            </tr>
            <tr>
                <td class="td2">
                    �˿ڣ�</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtEmailServerAddressPort" runat="server" Width="50px"></asp:TextBox>�磺�˿� 465
                    </td>
            </tr>
            <tr>
                <td class="td2">
                    �ʼ��ĵ�ַ���ƣ�</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtServerUserName" runat="server" Width="227px"></asp:TextBox>
                    ��ʽ��: ggp@gmail.com</td>
            </tr>
            <tr>
                <td class="td2">
                    ���룺</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtServerUserPass" runat="server"  Width="227px"></asp:TextBox>
                    ��ʽ��: ggp@gmail.com</td>
            </tr>
            <tr>
                <td class="td2">
                    �����ַ��</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtaddress" runat="server"   Width="227px"></asp:TextBox>
                    ��ʽ��: ttpay@service.com</td>
            </tr>
            <tr>
                <td class="td2">
                    ��ʾ���ƣ�</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtMailDisplayName" runat="server"   Width="227px"></asp:TextBox>
                    ��ʽ��: ����֧��ƽ̨�ͷ�</td>
            </tr>
            <tr>
                <td class="td2">
                    �Ƿ�Ssl���ӣ�</td>
                <td colspan="3" class="td1">
                    <asp:CheckBox ID="ckb_ssl" runat="server" Text="�Ƿ�Ssl����" /></td>
            </tr>
             <tr style="display: none">
                <td class="td2">
                    UseDefaultCredentials��</td>
                <td colspan="3" class="td1">
                    <asp:CheckBox ID="ckb_useDefaultCredentials" runat="server" Text="UseDefaultCredentials" /></td>
            </tr>
            <tr>
                <td class="td2">
                    ��ǰʹ�ã�</td>
                <td colspan="3" class="td1">
                    <asp:CheckBox ID="ckb_used" runat="server" /></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="td1" colspan="3">
                    <span style="padding-left: 3px; height: 40px">
                        <asp:Button ID="btn_Update" runat="server" Text="����" OnClick="btnUpdate_Click"/>
                    </span>
                </td>
            </tr>
        </table>
        
        <table width="100%" border="0" cellspacing="1" cellpadding="3">
            <tr>
                <td colspan="4" style="font-weight: bold; font-size: 14px; background: url(style/images/topbg.gif) repeat-x;
                    color: teal; height: 28px">
                    �ʼ�����</td>
            </tr>
            <tr>
                <td class="td2">
                    �������䣺</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtmailto" runat="server"   Width="227px">vivisoft@foxmail.com</asp:TextBox>
                    </td>
            </tr>
             <tr>
                <td class="td2">
                    ���ݣ�</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="4"  Width="600px">test</asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="td1" colspan="3">
                    <span style="padding-left: 3px; height: 40px">
                        <asp:Button ID="btnSend" runat="server" Text="ͬ������" 
                        onclick="btnSend_Click"  />
                        
                        <asp:Button ID="btnSend2" runat="server" Text="�첽����" 
                        onclick="btnSend2_Click"  />
                    </span>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
