<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.Finance.PayRate" Codebehind="PayRate.aspx.cs" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>���ʵ���</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
table {FONT-WEIGHT: normal; FONT-SIZE:12px;LINE-HEIGHT: 170%;}
td{height:11px;}
A:link {COLOR: #02418a; TEXT-DECORATION: none}
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="modelPanel" style="background: #F2F2F2">
        </div>
        <table width="100%" border="0" cellspacing="1" cellpadding="0">
            <tr>
                <td align="center" colspan="3" class="title" >
                    �����б�(��λ%)</td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td align="center">
                <table width="100%" id="Table1" border="0" align="center" cellpadding="2" cellspacing="1">
                    <tr>
                        <td>
                            �������ͣ�
                            <asp:DropDownList ID="ddlused" runat="server">
                    <asp:ListItem Value="">--��ѡ��--</asp:ListItem>
                    <asp:ListItem Value="1" Selected="true">�̼ҵȼ�</asp:ListItem>
                    <asp:ListItem Value="2">�����̼�</asp:ListItem>
                    <asp:ListItem Value="3">�ӿ���</asp:ListItem>
                </asp:DropDownList>
                            ID��<asp:TextBox ID="txtBillId" runat="server"></asp:TextBox>
                            ���ƣ�<asp:TextBox ID="txtBillName" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click"></asp:Button>
                        </td>
                       <%-- <td><asp:Button ID="btnAdd" runat="server" CssClass="button" Text=" �� �� " OnClick="btnAdd_Click"></asp:Button></td>  --%>                     
                    </tr>                    
                </table>
            </td>
                </tr>
                <tr>
                    <td align="center">
                        <table width="99%" border="0" cellpadding="1" cellspacing="1" bgcolor="#cccccc" id="tab">
                            <tbody bgcolor="#cccccc">
                                <asp:Repeater ID="rptdata" runat="server">
                                    <HeaderTemplate>
                                        <tr style="height: 22px; background: #507CD1; color: #fff; font-size: 10px;">
                                            <td>
                                                ����</td>
                                            <td>
                                                ����</td> 
                                            <td>
                                                ����</td> 
                                            
                                            <td>
                                                ֧����</td>   <td>
                                                ΢��</td> 
                                             <td>
                                                �Ƹ�ͨ</td>
                                                   <td>
                                                QQ֧��</td>
                                            <td>
                                                ������</td>
                                             <td>
                                                ��ͨ��</td>
                                             <td>
                                                ���ſ�</td>

                                          <%--  <td>
                                                �㽭</td>
                                            <td>
                                                ����</td>
                                            <td>
                                                ����</td>
                                             <td>
                                                ����</td>--%>
                                            <td>
                                                ʢ��</td> <td>
                                                ������</td> <td>
                                                Q�ҿ�</td>
                                           <%-- <td>
                                                ��;��</td>--%>
                                           
                                           
                                           
                                            <td>
                                                ���ο�</td>
                                            <td>
                                                ���׿�</td>
                                            <td>
                                                ������</td>
                                            <td>
                                                �Ѻ���</td>
                                           
                                        <%--    <td>
                                                ���</td>
                                            <td>
                                                ��ɽ</td>--%>
                                            <td>
                                                ����</td>
                                          <%--  <td>
                                                ����</td>--%>
                                            <td>
                                                ���</td>  
                                             <td>
                                                ��;��</td>
                                            <td>
                                                ����</td>       <td>
                                                wap΢��֧��</td>     <td>
                                                wap֧����</td>                                       
                                           <%-- <td>
                                                ħ��</td>
                                            <td>
                                                ����</td>
                                            <td>
                                                ����</td>
                                            <td>
                                                ��Ѷ</td>--%>
                                            <td>
                                                ����</td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr bgcolor="#ffffff">
                                            <td>
                                                <%#viviapi.BLL.Finance.PayRate.Instance.GetRateTypeName(Eval("rateType"))%>
                                            </td>
                                            <td>
                                                <%# Eval("billame")%>(#<%# Eval("billId")%>)
                                            </td>
                                         
                                            
                                            <td>
                                                <%# Convert.ToDouble(Eval("p102"))*100%>
                                                </td>  <td>
                                                <%# Convert.ToDouble(Eval("p101"))*100%>
                                                </td> 
                                                <td>
                                                <%# Convert.ToDouble(Eval("p207"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p100"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p203")) * 100%><%--�㽭���qq֧��--%>
                                                </td>

                                            <td>
                                                <%# Convert.ToDouble(Eval("p103"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p108")) * 100%><%--��ͨ--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p113")) * 100%><%--����--%>
                                                </td>
                                             <td>
                                                <%# Convert.ToDouble(Eval("p104")) * 100%><%--ʢ��--%>
                                                </td>
                                              
                                            <td>
                                                <%# Convert.ToDouble(Eval("p106"))*100%><%-- ����--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p107"))*100%><%-- Q��--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p109"))*100%><%-- 109--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p110"))*100%><%-- ����--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p111"))*100%><%-- ����--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p112"))*100%><%-- �Ѻ�--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p117"))*100%><%-- ����--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p119"))*100%><%-- ���--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p105"))*100%><%-- ��;--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p113"))*100%><%-- ����--%>
                                                </td>  <td>
                                                <%# Convert.ToDouble(Eval("p204"))*100%><%-- wap΢��--%>
                                                </td>
                                          <td>
                                                <%# Convert.ToDouble(Eval("p200"))*100%><%-- wap֧����--%>
                                                </td>
                                            <td>
                                                <a href="PayRateEdit.aspx?billid=<%# Eval("billid") %>&type=<%# Eval("rateType") %>" class='ljbg'>�༭</a></td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                       <%--<tr bgcolor="#F0F6FC">
                                             <td>
                                                <%#viviapi.BLL.Finance.PayRate.Instance.GetRateTypeName(Eval("rateType"))%>
                                            </td>
                                            <td>
                                                <%# Eval("billame")%>(#<%# Eval("billId")%>)
                                            </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p100"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p101"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p102"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p103"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p200")) * 100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p201")) * 100%>
                                                </td>
                                             <td>
                                                <%# Convert.ToDouble(Eval("p202")) * 100%>
                                                </td>
                                              <td>
                                                <%# Convert.ToDouble(Eval("p203")) * 100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p104"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p105"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p106"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p107"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p108"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p109"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p110"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p111"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p112"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p113"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p115"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p116"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p117"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p118"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p119"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p118"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p204")) * 100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p205")) * 100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p114"))*100%>
                                                </td>
                                            <td>
                                                <a href="PayRateEdit.aspx?billid=<%# Eval("billid") %>&type=<%# Eval("rateType") %>" class='ljbg'>�༭</a></td>
                                        </tr>--%>


                                        <tr bgcolor="#F0F6FC">
                                            <td>
                                                <%#viviapi.BLL.Finance.PayRate.Instance.GetRateTypeName(Eval("rateType"))%>
                                            </td>
                                            <td>
                                                <%# Eval("billame")%>(#<%# Eval("billId")%>)
                                            </td>
                                         
                                            
                                            <td>
                                                <%# Convert.ToDouble(Eval("p102"))*100%>
                                                </td>  
                                            
                                            <td>
                                                <%# Convert.ToDouble(Eval("p101"))*100%>
                                                </td> 
                                                <td>
                                                <%# Convert.ToDouble(Eval("p107"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p100"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p203")) * 100%><%--�㽭���qq֧��--%>
                                                </td>

                                            <td>
                                                <%# Convert.ToDouble(Eval("p103"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p108")) * 100%><%--��ͨ--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p113")) * 100%><%--����--%>
                                                </td>
                                             <td>
                                                <%# Convert.ToDouble(Eval("p104")) * 100%><%--ʢ��--%>
                                                </td>
                                              
                                            <td>
                                                <%# Convert.ToDouble(Eval("p106"))*100%><%-- ����--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p107"))*100%><%-- Q��--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p109"))*100%><%-- 109--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p110"))*100%><%-- ����--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p111"))*100%><%-- ����--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p112"))*100%><%-- �Ѻ�--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p117"))*100%><%-- ����--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p119"))*100%><%-- ���--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p105"))*100%><%-- ��;--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p113"))*100%><%-- ����--%>
                                                </td>
                                         <td>
                                                <%# Convert.ToDouble(Eval("p204"))*100%><%-- wap΢��--%>
                                                </td>
                                          <td>
                                                <%# Convert.ToDouble(Eval("p200"))*100%><%-- wap֧����--%>
                                                </td>
                                            <td>
                                                <a href="PayRateEdit.aspx?billid=<%# Eval("billid") %>&type=<%# Eval("rateType") %>" class='ljbg'>�༭</a></td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                                
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr style="background-color: #EFEFEF">
                        <td style="height: 16px;">
                            <aspxc:AspNetPager ID="Pager1" runat="server" OnPageChanged="Pager1_PageChanged"
                                AlwaysShow="True" CustomInfoHTML="�ܼ�¼����%RecordCount% ��ҳ����%PageCount% ��ǰҳ��%CurrentPageIndex%"
                                CustomInfoTextAlign="Left" FirstPageText="��ҳ" HorizontalAlign="Right" LastPageText="ĩҳ"
                                NavigationToolTipTextFormatString="��ת{0}ҳ" NextPageText="��һҳ" PageIndexBoxType="DropDownList"
                                PageSize="8" PrevPageText="��һҳ" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                                ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                                TextAfterPageIndexBox="ҳ" TextBeforePageIndexBox="����" Width="100%" Height="30px">
                            </aspxc:AspNetPager>
                        </td>
                    </tr>
                </table>
                    </td>
                </tr>
            </tbody>
        </table>

        

    </form>

    <script src="../js/public.js" type="text/javascript"></script>
</body>
</html>
