<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.order.Reconciliation" ValidateRequest="false" Codebehind="Reconciliation.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>���˲�ѯ</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
      <link href="../style/union.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="1" cellpadding="3">
            <tr>
                <td colspan="4" class="title">
                    ���˲�ѯ</td>
            </tr> 
            <tr>
                <td class="td2">
                    �ӿ��̣�</td>
                <td colspan="3" class="td1">
                    <asp:DropDownList ID="ddlsupp" runat="server">
                        <asp:ListItem Value="80">ŷ��</asp:ListItem>
                        <asp:ListItem Value="60866">60866</asp:ListItem>
                        <asp:ListItem Value="81">��Ԫ</asp:ListItem>
                        <asp:ListItem Value="70">70card</asp:ListItem>
                        <asp:ListItem Value="700">����</asp:ListItem>
                        <asp:ListItem Value="86">���ݸ�</asp:ListItem>
                    </asp:DropDownList>
                    </td>
            </tr>            
            <tr>
                <td class="td2">
                    �����ţ�</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtorders" runat="server" Width="600px" TextMode="MultiLine" Height="200px"></asp:TextBox></td>
            </tr>              
            <tr>
                <td>
                    &nbsp;</td>
                <td class="td1" colspan="3">
                    <span style="padding-left: 3px; height: 40px">
                        <asp:Button ID="btn_search" runat="server" Text="�ύ��ѯ" 
                        onclick="btn_search_Click"/>
                    </span>
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
                                <asp:Repeater ID="rptOrders" runat="server" >
                                    <HeaderTemplate>
                                        <tr height="22" style="background-color: #507CD1; color: #fff">
                                            <td>
                                                ������
                                            </td>
                                            <td>
                                                ��ˮ��
                                            </td>
                                            <td>
                                                ֧�����
                                            </td>
                                            <td>
                                                ��ѯ���
                                            </td>
                                            <td>
                                                ֧��״̬
                                            </td>
                                            <td>
                                                ���ױ���
                                            </td>
                                            <td>
                                                ����
                                            </td>                                           
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr bgcolor="#EFF3FB" >
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>
                                            <td>
                                                <%# Eval("supporder")%>
                                            </td>
                                            <td>
                                                <%# Eval("realamt")%>                                               
                                            </td>
                                            <td>
                                                <%# Eval("result")%>
                                            </td>                                           
                                            <td>
                                                <%# Eval("status")%>
                                            </td>
                                            <td>
                                                <%# Eval("coin")%>
                                            </td>
                                            <td>
                                                <%# Eval("cardtype")%>
                                            </td>                                            
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr bgcolor="#ffffff" >
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>
                                            <td>
                                                <%# Eval("supporder")%>
                                            </td>
                                            <td>
                                                <%# Eval("realamt")%>                                               
                                            </td>
                                            <td>
                                                <%# Eval("result")%>
                                            </td>                                           
                                            <td>
                                                <%# Eval("status")%>
                                            </td>
                                            <td>
                                                <%# Eval("coin")%>
                                            </td>
                                            <td>
                                                <%# Eval("cardtype")%>
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
