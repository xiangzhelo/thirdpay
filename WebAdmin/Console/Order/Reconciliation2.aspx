<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.order.Reconciliation2"
    ValidateRequest="false" CodeBehind="Reconciliation2.aspx.cs" %>
<%@ Import Namespace="viviapi.BLL.Supplier" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>���˲�ѯ</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    
     <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />

    <script src="../js/ControlDate/WdatePicker.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
            <td colspan="4" class="title">
                ͨ���ӿڲ�ѯ ����
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp&nbsp��ʼ��
                <asp:TextBox ID="StimeBox" runat="server" Width="120px"></asp:TextBox>
                &nbsp&nbsp��ֹ��
                <asp:TextBox ID="EtimeBox" runat="server" Width="120px"></asp:TextBox>
                <asp:Button ID="btn_search" runat="server" Text="��ѯ" OnClick="btn_search_Click" />
                <asp:Button ID="btn_Reconciliation" runat="server" Text="ͨ����ѯ�ӿڲ���" OnClick="btn_Reconciliation_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="1" cellpadding="0" class="table1">
        <tr>
            <td bgcolor="#ffffff">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="center">
                            <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                                <asp:Repeater ID="rptOrders" runat="server">
                                    <HeaderTemplate>
                                        <tr height="22" style="background-color: #507CD1; color: #fff">
                                            <td>�̻�ID</td>
                                            <td>������ </td>
                                            <td>�ύʱ��</td>
                                            <td>������</td>
                                            <td>����</td>
                                            <td>���</td>
                                            <td>�ӿ�</td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr bgcolor="#EFF3FB">
                                            <td>
                                                <%# Eval("userid")%>
                                            </td>
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>
                                            <td>
                                                <%# Eval("processingtime","{0:yyyy-MM-dd}")%>
                                            </td>
                                            <td>
                                                <%# Eval("modetypename")%>
                                            </td>
                                            <td>
                                                <%# Eval("cardNo")%>
                                            </td>
                                            <td>
                                                <%# Eval("refervalue", "{0:f2}")%>
                                            </td>
                                            <td id="tr_supplier" runat="server">
                                                <%# viviapi.BLL.Supplier.Factory.GetSupplierName(Eval("supplierId"))%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr bgcolor="#ffffff">
                                           <td>
                                                <a href="?action=userpay&userid=<%# Eval("userid") %>&status=2">
                                                    <%# Eval("userid")%>
                                                </a>
                                            </td>
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>
                                            <td>
                                                <%# Eval("processingtime","{0:yyyy-MM-dd}")%>
                                            </td>
                                            <td>
                                                <%# Eval("modetypename")%>
                                            </td>
                                            <td>
                                                <%# Eval("cardNo")%>
                                            </td>
                                            <td>
                                                <%# Eval("refervalue", "{0:f2}")%>
                                            </td>
                                            <td id="tr_supplier" runat="server">
                                                <%# viviapi.BLL.Supplier.Factory.GetSupplierName(Eval("supplierId"))%>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
