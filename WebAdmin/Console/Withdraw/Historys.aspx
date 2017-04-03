<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.Withdraw.Historys"
    CodeBehind="Historys.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript">
        function Setchkall(obj) {
            var objs = document.getElementsByName("chk");
            for (i = 0; i < objs.length; i++) {
                objs[i].checked = obj.checked;
            }
        }
        function checkall(obj) {
            var check = document.getElementsByName("ischecked");
            for (i = 0; i < check.length; i++) {
                check[i].checked = obj.checked;
            }
        }
    </script>

    <script type="text/javascript">
        function sendInfo(id) {
            window.open("../User/UserEdit.aspx?id=" + id, "�鿴�û���Ϣ", "Width=800px;Height=350px;");
        }
    </script>

    

    <script src="../Js/ControlDate/WdatePicker.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; height: 100%;">
            <tr>
                <td align="center" class="title">
                    �����¼
                </td>
            </tr>
            <tr>
                <td>
                    <span style="float: left; margin-left: 2px">
                        <asp:DropDownList ID="ddlStatusList" runat="server">
                        </asp:DropDownList>
                        �̻�ID��<asp:TextBox ID="txtUserId" runat="server" Width="80px"></asp:TextBox>
                        ����ID��<asp:TextBox ID="txtItemInfoId" runat="server" Width="80px"></asp:TextBox>
                        <asp:DropDownList ID="ddlbankName" runat="server">
                            <asp:ListItem Value="">--�տ�����--</asp:ListItem>
                            <asp:ListItem Value="0002">֧����</asp:ListItem>
                            <asp:ListItem Value="0003">�Ƹ�ͨ</asp:ListItem>
                            <asp:ListItem Value="1002">�й���������</asp:ListItem>
                            <asp:ListItem Value="1005">�й�ũҵ����</asp:ListItem>
                            <asp:ListItem Value="1003">�й���������</asp:ListItem>
                            <asp:ListItem Value="1026">�й�����</asp:ListItem>
                            <asp:ListItem Value="1001">��������</asp:ListItem>
                            <asp:ListItem Value="1006">��������</asp:ListItem>
                            <asp:ListItem Value="1020">��ͨ����</asp:ListItem>
                            <asp:ListItem Value="1025">��������</asp:ListItem>
                            <asp:ListItem Value="1009">��ҵ����</asp:ListItem>
                            <asp:ListItem Value="1027">�㷢����</asp:ListItem>
                            <asp:ListItem Value="1004">�ַ�����</asp:ListItem>
                            <asp:ListItem Value="1022">�������</asp:ListItem>
                            <asp:ListItem Value="1021">��������</asp:ListItem>
                            <asp:ListItem Value="1010">ƽ������</asp:ListItem>
                            <asp:ListItem Value="1066">�й�������������</asp:ListItem>
                        </asp:DropDownList>
                        �տ��˻���<asp:TextBox ID="txtAccount" runat="server" Width="80px"></asp:TextBox>
                        �տ��ˣ�<asp:TextBox ID="txtpayeeName" runat="server" Width="80px"></asp:TextBox>
                        <asp:DropDownList ID="ddlmode" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSupplier" runat="server">
                        </asp:DropDownList>
                        ��ʼ��
                        <asp:TextBox ID="txtStimeBox" runat="server" Width="65px"></asp:TextBox>
                        ��ֹ��
                        <asp:TextBox ID="txtEtimeBox" runat="server" Width="65px"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                        </asp:Button>
                        <asp:Button ID="btnExport" runat="server" CssClass="button" Text="����" OnClick="btnExport_Click"/>
                        </span>
                </td>
            </tr>
            <tr>
                <td bgcolor="#F9F9F9">
                    <span style="color: #ff0000; text-align: left">�������<%=TotalAmount%></span>
                    <span style="color: #ff0000; text-align: left">�������ѣ�<%=TotalCharges%></span>
                    <span style="color: #ff0000; text-align: left">��Ӧ����<%=TotalWithdrawAmt%></span>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="1" width="100%" id="tab">
                        <asp:Repeater ID="rptList" runat="server">
                            <HeaderTemplate>
                                <tr style="background-color: #507CD1; color: #fff; height: 22px">
                                    <td style="width: 10%; text-align: center">
                                        �����
                                    </td>
                                    <td style="width: 8%; text-align: center">
                                        �̻�
                                    </td>
                                    <td style="width: 25%">
                                        �տ���Ϣ
                                    </td>
                                    <td style="width: 6%; text-align: center">
                                        ������
                                    </td>
                                    <td style="width: 6%; text-align: center">
                                        ������
                                    </td>
                                    <td style="width: 6%; text-align: center">
                                        ʵ��Ӧ��
                                    </td>
                                    <td style="width: 8%; text-align: center">
                                        ���ַ�ʽ
                                    </td>
                                    <td style="width: 10%; text-align: center">
                                        ����ʱ��
                                    </td>
                                    <td style="width: 10%; text-align: center">
                                        ״̬
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr bgcolor="#EFF3FB">
                                    <td style="text-align:center">
                                       <%# Eval("tranno")%>
                                    </td>
                                    <td style="text-align:center">
                                        <a href="javascript:sendInfo('<%# Eval("userid")%>')">
                                           <%#Eval("UserName")%>(#<%#Eval("userid")%>)
                                        </a>
                                    </td>
                                    <td>
                                        <%# Eval("PayeeBank")%> <%# Eval("bankProvince")%> <%# Eval("bankCity")%> <%# Eval("Payeeaddress")%> <br />
                                         &nbsp;<%# Eval("payeeName")%> <br />
                                         &nbsp;<%# Eval("account")%>
                                    </td>  
                                    <td style="text-align:right">
                                        <%# Eval("amount", "{0:f2}")%>
                                    </td>
                                    <td style="text-align:right">
                                        <%# Eval("Charges", "{0:f2}")%>
                                    </td>
                                    <td style="text-align:right">
                                        <%# Eval("withdraw", "{0:f2}")%>
                                    </td>
                                     <td style="text-align:center">
                                        <%#viviapi.BLL.Finance.Withdraw.Instance.GetModeName(Convert.ToInt32(Eval("settmode")))%>
                                    </td>
                                    <td style="text-align:center">
                                        <%# Eval("PayTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td style="text-align:center">
                                        <%# Eval("StatusText")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr bgcolor="#EFF3FB">
                                    <td style="text-align:center">
                                      
                                    </td>
                                    <td style="text-align:center">
                                       
                                    </td>
                                     <td style="text-align:right">
                                        &nbsp;�ϼ�
                                    </td>                                   
                                    <td style="text-align:right">
                                        <%=TotalAmount%>
                                    </td>
                                    <td style="text-align:right">
                                        <%=TotalCharges%>
                                    </td>
                                    <td style="text-align:right">
                                        <%=TotalWithdrawAmt%>
                                    </td>
                                     <td style="text-align:center">
                                        
                                    </td>
                                    <td style="text-align:center">
                                        
                                    </td>
                                    <td style="text-align:center">
                                        
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                    <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CustomInfoHTML="�ܼ�¼����%RecordCount% ��ҳ����%PageCount% ��ǰҳ��%CurrentPageIndex%"
                        CustomInfoTextAlign="Left" FirstPageText="��ҳ" HorizontalAlign="Right" LastPageText="ĩҳ"
                        NavigationToolTipTextFormatString="��ת{0}ҳ" NextPageText="��һҳ" PageIndexBoxType="DropDownList"
                        PageSize="20" PrevPageText="��һҳ" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                        ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                        TextAfterPageIndexBox="ҳ" TextBeforePageIndexBox="����" Width="100%" Height="30px"
                        OnPageChanged="Pager1_PageChanged">
                    </aspxc:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
    </form>
     <script src="../js/public.js" type="text/javascript"></script>
</body>
</html>
