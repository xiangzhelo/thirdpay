<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.order.CardSendLog"
    CodeBehind="CardSendLog.aspx.cs" %>

<%@ Import Namespace="viviapi.BLL.Channel" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />

    <script src="../js/ControlDate/WdatePicker.js" type="text/javascript"></script>

    <style type="text/css">
        table
        {
            font-weight: normal;
            font-size: 12px;
            line-height: 170%;
        }
        td
        {
            height: 11px;
        }
        A:link
        {
            color: #02418a;
            text-decoration: none;
        }
        .style4
        {
            width: 517px;
        }
    </style>

    <script type="text/javascript">
        function sendInfo(id) {
            window.open("CardOrderShow.aspx?id=" + id, "�鿴���ඩ����ϸ", "");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="0" class="table1">
        <tr>
            <td align="center" colspan="3" class="title">
                �㿨�ύ��¼
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td colspan="3">
                            �����ţ�
                            <asp:TextBox ID="txtorderid" runat="server" Width="60px"></asp:TextBox>
                            &nbsp&nbsp<asp:DropDownList ID="ddlChannelType" runat="server" Width="95px">
                                <asp:ListItem Value="">--ͨ������--</asp:ListItem>
                                <asp:ListItem Value="103">�����г�ֵ��</asp:ListItem>
                                <asp:ListItem Value="104">ʢ��һ��ͨ</asp:ListItem>
                                <asp:ListItem Value="210">ʢ��ͨ��</asp:ListItem>
                                <asp:ListItem Value="105">��;֧����</asp:ListItem>
                                <asp:ListItem Value="106">����һ��ͨ</asp:ListItem>
                                <asp:ListItem Value="107">��ѶQ�ҿ�</asp:ListItem>
                                <asp:ListItem Value="108">��ͨ��ֵ��</asp:ListItem>
                                <asp:ListItem Value="109">����һ��ͨ</asp:ListItem>
                                <asp:ListItem Value="110">����һ��ͨ</asp:ListItem>
                                <asp:ListItem Value="111">����һ��ͨ</asp:ListItem>
                                <asp:ListItem Value="112">�Ѻ�һ��ͨ</asp:ListItem>
                                <asp:ListItem Value="113">���ų�ֵ��</asp:ListItem>
                                <asp:ListItem Value="114">��Ѷ��</asp:ListItem>
                                <asp:ListItem Value="115">����һ��ͨ</asp:ListItem>
                                <asp:ListItem Value="116">��ɽһ��ͨ</asp:ListItem>
                                <asp:ListItem Value="117">����һ��ͨ</asp:ListItem>
                                <asp:ListItem Value="118">5173��</asp:ListItem>
                                <asp:ListItem Value="119">��Ѫ��</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp&nbsp<asp:DropDownList ID="ddlsuccess" runat="server" Width="95px">
                                <asp:ListItem>--�ύ״̬--</asp:ListItem>
                                <asp:ListItem Value="0">ʧ��</asp:ListItem>
                                <asp:ListItem Value="1">�ɹ�</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp&nbsp &nbsp&nbsp���ţ�<asp:TextBox ID="txtCardNo" runat="server" Width="120px"></asp:TextBox>
                            &nbsp&nbsp��ʼ��
                            <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                            &nbsp&nbsp��ֹ��
                            <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                            <asp:Button ID="btn_Search" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btn_Search_Click">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td bgcolor="#ffffff">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="center">
                            <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                                <asp:Repeater ID="rptSendLogs" runat="server">
                                    <HeaderTemplate>
                                        <tr height="22" style="background-color: #507CD1; color: #fff">
                                            <td>
                                                ������
                                            </td>
                                            <td>
                                                �ӿ���
                                            </td>
                                            <td>
                                                ͨ������
                                            </td>
                                            <td>
                                                ����
                                            </td>
                                            <td>
                                                ��ֵ
                                            </td>
                                            <td>
                                                �ɹ�
                                            </td>
                                            <td>
                                                �ύʱ��
                                            </td>
                                            <td>
                                                �ύ״̬
                                            </td>
                                            <td>
                                                ͬ������
                                            </td>
                                            <td>
                                                ��Ϣ
                                            </td>
                                            <td>
                                                ϵͳ����
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr bgcolor="#EFF3FB">
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>
                                            <td>
                                                <%# viviapi.BLL.Supplier.Factory.GetSupplierName(Eval("suppId"))%>(#<%# Eval("suppId")%>��
                                            </td>
                                            <td>
                                                <%# CardUtility.GetCardName(Convert.ToInt32(Eval("typeid")))%>
                                            </td>
                                            <td>
                                                <%# Eval("cardno")%>
                                            </td>
                                            <td>
                                                <%# Eval("faceValue")%>
                                            </td>
                                            <td>
                                                <%# Eval("success")%>
                                            </td>
                                             <td>
                                                <%# Eval("summitStatus")%>
                                            </td>
                                            <td>
                                                <%# Eval("initTime")%>
                                            </td>
                                            <td>
                                                <%# Eval("errCode")%>
                                            </td>
                                            <td>
                                                <%# Eval("errMsg")%>
                                            </td>
                                            <td>
                                                <%# Eval("message")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr bgcolor="#ffffff">
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>
                                            <td>
                                                <%# viviapi.BLL.Supplier.Factory.GetSupplierName(Eval("suppId"))%>(#<%# Eval("suppId")%>��
                                            </td>
                                            <td>
                                                <%# CardUtility.GetCardName(Convert.ToInt32(Eval("typeid")))%>
                                            </td>
                                            <td>
                                                <%# Eval("cardno")%>
                                            </td>
                                            <td>
                                                <%# Eval("faceValue")%>
                                            </td>
                                            <td>
                                                <%# Eval("success")%>
                                            </td>
                                            <td>
                                                <%# Eval("summitStatus")%>
                                            </td>
                                            <td>
                                                <%# Eval("initTime")%>
                                            </td>
                                            <td>
                                                <%# Eval("errCode")%>
                                            </td>
                                            <td>
                                                <%# Eval("errMsg")%>
                                            </td>
                                            <td>
                                                <%# Eval("message")%>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr style="background-color: #EBEBEB">
                        <td height="22" colspan="7">
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
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript">
        function collapse(img, objName) {
            var obj = document.getElementById(objName);
            if (img.src.indexOf('open') != -1) {
                img.src = img.src.replace('open', 'close');
                obj.style.display = 'none';
            }
            else {
                img.src = img.src.replace('close', 'open');
                obj.style.display = '';
            }
        }
        function handler(tp) {
        }

        var mytr = document.getElementById("table2").getElementsByTagName("tr");
        for (var i = 1; i < mytr.length; i++) {
            mytr[i].onmouseover = function() {
                var rows = this.childNodes.length;
                for (var row = 0; row < rows; row++) {
                    this.childNodes[row].style.backgroundColor = '#E6EEFF';
                }
            };
            mytr[i].onmouseout = function() {
                var rows = this.childNodes.length;
                for (var row = 0; row < rows; row++) {
                    this.childNodes[row].style.backgroundColor = '';
                }
            };
        }

    </script>

</body>
</html>
