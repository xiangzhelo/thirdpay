<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebUI7uka.agent.order.BankList" Codebehind="BankList.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />

    <script src="../../js/ControlDate/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css"> table {font-weight: normal;font-size: 12px; line-height: 170%;}
        td{ height: 11px; }
        A:link {color: #02418a;text-decoration: none;}
    </style>
    <script type="text/javascript">
        function sendInfo(id) {
            window.open("BankDetail.aspx?orderid=" + id, "�鿴����", "height=760,width=800");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="0" class="table1">
        <tr>
            <td align="center" colspan="3" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 28px">
                ����������ѯ
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td colspan="2">
                            &nbsp&nbsp�̻�ID��
                            <asp:TextBox ID="txtuserid" runat="server" Width="60px"></asp:TextBox>
                            <%--  ����ID��
                <asp:TextBox ID="txtpromid" runat="server" Width="30px"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddlChannelType" runat="server" Width="95px">
                                <asp:ListItem Value="">--ͨ������--</asp:ListItem>
                                <asp:ListItem Value="102">��������</asp:ListItem>
                                <asp:ListItem Value="101">֧����</asp:ListItem>
                                <asp:ListItem Value="100">�Ƹ�ͨ</asp:ListItem>
                            </asp:DropDownList>                            
                            
                            &nbsp&nbsp��ʼ��
                            <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                            &nbsp&nbsp��ֹ��
                            <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                            &nbsp&nbsp&nbsp&nbsp<asp:Button ID="btn_Search" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btn_Search_Click">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp�����ţ�<asp:TextBox ID="txtOrderId" runat="server" Width="160px"></asp:TextBox>
                            &nbsp&nbsp�̻������ţ�<asp:TextBox ID="txtUserOrder" runat="server" Width="160px"></asp:TextBox>
                            &nbsp&nbsp�ӿ��̶����ţ�<asp:TextBox ID="txtSuppOrder" runat="server" Width="160px"></asp:TextBox>
                        </td>
                        <td align="left" bgcolor="#F9F9F9">
                            <div id="divmoney">
                                <span style="color: #ff0000; text-align: left">�ܶ<% = TotalTranATM %></span>                                 
                                <span style="color: #ff0000; text-align: left;">��������ɣ�<% = TotalCommission %></span>                                
                            </div>
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
                                <asp:Repeater ID="rptOrders" runat="server" OnItemCommand="rptOrders_ItemCommand"
                                    OnItemDataBound="rptOrders_ItemDataBound">
                                    <HeaderTemplate>
                                        <tr height="22" style="background-color: #507CD1; color: #fff">
                                            <td>
                                                �̻�ID
                                            </td>
                                            <td>
                                                �ӿ�
                                            </td>
                                            <td>
                                                ��������
                                            </td>
                                            <td>
                                                �̻�������
                                            </td>
                                            <td>
                                                ������
                                            </td>
                                            <td>
                                                �ӿ��̶�����
                                            </td>
                                            <td>
                                                ͨ������
                                            </td>
                                            <td>
                                                ����
                                            </td>
                                            <td>
                                                ���
                                            </td>                                                                                      
                                            <td>
                                                ��������
                                            </td>
                                            <td>
                                                ����ʱ��
                                            </td>
                                            <td>
                                                ״̬
                                            </td>
                                          
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr bgcolor="#EFF3FB" ondblclick="javascript:sendInfo('<%# Eval("orderid")%>')">
                                            <td>
                                                <a href="?action=userpay&userid=<%# Eval("userid") %>&status=2">
                                                    <%# Eval("userid")%>
                                                </a>
                                            </td>
                                            <td>
                                                <%# Eval("version")%>
                                            </td>
                                            <td>
                                                 <%#Enum.GetName(typeof(viviapi.Model.Order.OrderTypeEnum),Eval("ordertype"))%>                                                
                                            </td>
                                            <td>
                                                 <%# Eval("userorder")%>
                                            </td>
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>
                                            <td>
                                                <%# Eval("supplierOrder")%>
                                            </td>
                                            <td>
                                                <%# Eval("modetypename")%>
                                            </td>
                                            <td>
                                                <%# Eval("modeName")%>
                                            </td>
                                            <td>
                                                <%# Eval("refervalue", "{0:f2}")%>
                                            </td>                                           
                                            <td>
                                                <%# Eval("promAmt", "{0:f2}")%>
                                            </td>
                                            <td>
                                                <%# Eval("completetime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                            </td>
                                            <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum),Eval("status"))%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr bgcolor="#ffffff" ondblclick="javascript:sendInfo('<%# Eval("orderid")%>')">
                                            <td>
                                                <a href="?action=userpay&userid=<%# Eval("userid") %>&status=2">
                                                    <%# Eval("userid")%>
                                                </a>
                                            </td>
                                            <td>
                                                <%# Eval("version")%>
                                            </td>
                                             <td>
                                                <%#Enum.GetName(typeof(viviapi.Model.Order.OrderTypeEnum),Eval("ordertype"))%>                                              
                                            </td>
                                            <td>
                                                <%# Eval("userorder")%>
                                            </td>
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>
                                            <td>
                                                <%# Eval("supplierOrder")%>
                                            </td>
                                            <td>
                                                <%# Eval("modetypename")%>
                                            </td>
                                            <td>
                                                <%# Eval("modeName")%>
                                            </td>
                                            <td>
                                                <%# Eval("refervalue", "{0:f2}")%>
                                            </td>                                           
                                            <td>
                                                <%# Eval("promAmt", "{0:f2}")%>
                                            </td>
                                            
                                            <td>
                                                <%# Eval("completetime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                            </td>
                                            <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum),Eval("status"))%>
                                            </td>
                                           
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr style="background-color: #EBEBEB">
                        <td height="22" colspan="7">
                            <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CustomInfoHTML="�ܼ�¼����%RecordCount%&nbsp;��ҳ����%PageCount%&nbsp;��ǰҳ��%CurrentPageIndex%&nbsp;"
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