<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.Withdraw.Audits" CodeBehind="Audits.aspx.cs" %>

<%@ Import Namespace="viviapi.BLL.Supplier" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>�������</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
     <script src="../js/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function sendInfo(id) {
            window.open("../User/UserEdit.aspx?id=" + id, "�鿴�û���Ϣ", "Width=800px;Height=350px;");
        }
    </script>

    <script type="text/javascript" language="javascript">
        function Setchkall(obj) {
            var objs = document.getElementsByName("TrannoList");
            for (i = 0; i < objs.length; i++) {
                objs[i].checked = obj.checked;
            }
        }
        function checkall(obj) {
            var check = document.getElementsByName("TrannoList");
            for (i = 0; i < check.length; i++) {
                check[i].checked = obj.checked;
            }
        }
    </script>

    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; height: 100%;">
            <tr>
                <td align="center" class="title">
                    �������
                </td>
            </tr>
            <tr style="height: 30px">
                <td>
                    �̻�ID��<asp:TextBox ID="txtUserId" runat="server" Width="80px"></asp:TextBox>
                    ����ţ�<asp:TextBox ID="txtTranno" runat="server" Width="80px"></asp:TextBox>
                    <asp:DropDownList ID="ddlbankName" runat="server">                        
                        <asp:ListItem value="">--�տ�����--</asp:ListItem>
                        <asp:ListItem value="0002">֧����</asp:ListItem>
                        <asp:ListItem value="0003">�Ƹ�ͨ</asp:ListItem>
                        <asp:ListItem value="1002">�й���������</asp:ListItem>
                        <asp:ListItem value="1005">�й�ũҵ����</asp:ListItem>
                        <asp:ListItem value="1003">�й���������</asp:ListItem>
                        <asp:ListItem value="1026">�й�����</asp:ListItem>
                        <asp:ListItem value="1001">��������</asp:ListItem>
                        <asp:ListItem value="1006">��������</asp:ListItem>
                        <asp:ListItem value="1020">��ͨ����</asp:ListItem>
                        <asp:ListItem value="1025">��������</asp:ListItem>
                        <asp:ListItem value="1009">��ҵ����</asp:ListItem>
                        <asp:ListItem value="1027">�㷢����</asp:ListItem>
                        <asp:ListItem value="1004">�ַ�����</asp:ListItem>
                        <asp:ListItem value="1022">�������</asp:ListItem>
                        <asp:ListItem value="1021">��������</asp:ListItem>
                        <asp:ListItem value="1010">ƽ������</asp:ListItem>
                        <asp:ListItem value="1066">�й�������������</asp:ListItem>
                    </asp:DropDownList>
                    �տ��˻���<asp:TextBox ID="txtAccount" runat="server" Width="80px"></asp:TextBox>
                    �տ��ˣ�<asp:TextBox ID="txtpayeeName" runat="server" Width="80px"></asp:TextBox>                    
                    <asp:DropDownList ID="ddlmode" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSupplier" runat="server">
                    </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                    </asp:Button>
                    <asp:Button ID="btnPass" runat="server" CssClass="button" Width="90px" Text="����ͨ�����"
                        OnClick="btnPass_Click"></asp:Button>
                    <asp:Button ID="btnAllPass" runat="server" CssClass="button" Width="90px" Text="ȫ��ͨ�����"
                        OnClick="btnAllPass_Click"></asp:Button>
                    <asp:Button ID="btnallfail" runat="server" CssClass="button" Width="90px" Text="ȫ���ܾ�"
                        OnClick="btnallfail_Click"></asp:Button>
                     <asp:Button ID="btnExport" runat="server" CssClass="button" Text="����" OnClick="btnExport_Click">
                </asp:Button>
                </td>
            </tr>
            <tr>
                <td bgcolor="#F9F9F9">
                    <span style="color: #ff0000; text-align: left">�������<%=TotalAmount%></span>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="1" width="100%" id="tab">
                        <asp:Repeater ID="rptdata" runat="server" 
                            onitemdatabound="rptdata_ItemDataBound" 
                            onitemcommand="rptdata_ItemCommand">
                            <HeaderTemplate>
                                <tr style="background-color: #507CD1; color: #fff; height:22px">
                                    <td style="width: 3%;text-align: center">
                                        <input id="Checkboxall" type="checkbox" class="qx" onclick="checkall(this)" />                                   
                                    </td>
                                    <td style="width: 10%;text-align: center">
                                        �����
                                    </td>
                                    <td style="width: 8%;text-align: center">
                                        �̻���
                                    </td>
                                    <td style="width: 25%">
                                        �տ���Ϣ
                                    </td>
                                    <td style="width: 8%;text-align: center">
                                        ���ַ�ʽ
                                    </td>
                                    <td style="width: 8%;text-align: center">
                                        ������
                                    </td>
                                    <td style="width: 10%;text-align: center">
                                        ����ʱ��
                                    </td>
                                    <td style="width: 10%;text-align: center">
                                        ֧������
                                    </td>
                                    <td style="width: 8%;text-align: center">
                                        ����ӿ�
                                    </td> 
                                    <td style="text-align: center">
                                        ����
                                    </td>                                     
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr bgcolor="#EFF3FB">
                                    <td style="text-align: center">
                                        <input id="<%# Eval("tranno") %>" type="checkbox" name="TrannoList" class="qx" value="<%# Eval("tranno") %>" />
                                    </td>
                                    <td style="text-align: center">
                                        <%# Eval("tranno")%>
                                    </td>
                                    <td style="text-align: center">
                                        <a href="javascript:sendInfo('<%# Eval("userid")%>')">
                                            <%#Eval("UserName")%>(#<%#Eval("userid")%>)
                                        </a>
                                    </td>
                                    <td>
                                         <%# Eval("PayeeBank")%> <%# Eval("bankProvince")%> <%# Eval("bankCity")%> <%# Eval("Payeeaddress")%> <br />
                                         &nbsp;<%# Eval("payeeName")%> <br />
                                         &nbsp;<%# Eval("account")%>
                                    </td>                                    
                                    <td style="text-align: center">
                                        <%#viviapi.BLL.Finance.Withdraw.Instance.GetModeName(Convert.ToInt32(Eval("settmode")))%>
                                    </td>
                                    <td style="text-align: right">
                                        <%# Eval("amount","{0:f2}")%>
                                    </td>
                                    <td style="text-align: center">
                                        <%# Eval("AddTime","{0:yyyy-MM-dd HH:mm:ss}") %>
                                    </td>
                                    <td style="text-align: center">
                                        <%# Eval("required", "{0:yyyy-MM-dd}")%>
                                    </td>
                                    <td style="text-align: center">
                                        <%# viviapi.BLL.Supplier.Factory.GetSupplierName(Eval("suppId"))%>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnAudit" runat="server" Text="ͨ��" CommandName="Pass" CommandArgument='<%# Eval("tranno") %>'  />
                                        <asp:Button ID="btnRefuse" runat="server" Text="�ܾ�" CommandName="Refuse" CommandArgument='<%# Eval("tranno") %>'  />
                                    </td>                                    
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr bgcolor="#f9f9f9">                                    
                                     <td style="text-align: center">
                                        <input id="<%# Eval("tranno") %>" type="checkbox" name="TrannoList" class="qx" value="<%# Eval("tranno") %>" />
                                    </td>
                                    <td style="text-align: center">
                                        <%# Eval("tranno")%>
                                    </td>
                                    <td style="text-align: center">
                                        <a href="javascript:sendInfo('<%# Eval("userid")%>')">
                                            <%#Eval("UserName")%>(#<%#Eval("userid")%>)
                                        </a>
                                    </td>
                                    <td>
                                         <%# Eval("PayeeBank")%> <%# Eval("bankProvince")%> <%# Eval("bankCity")%> <%# Eval("Payeeaddress")%> <br />
                                         &nbsp;<%# Eval("payeeName")%> <br />
                                         &nbsp;<%# Eval("account")%>
                                    </td>                                    
                                    <td style="text-align: center">
                                        <%#viviapi.BLL.Finance.Withdraw.Instance.GetModeName(Convert.ToInt32(Eval("settmode")))%>
                                    </td>
                                    <td style="text-align: right">
                                        <%# Eval("amount","{0:f2}")%>
                                    </td>
                                    <td style="text-align: center">
                                        <%# Eval("AddTime","{0:yyyy-MM-dd HH:mm:ss}") %>
                                    </td>
                                    <td style="text-align: center">
                                        <%# Eval("required", "{0:yyyy-MM-dd}")%>
                                    </td>
                                    <td style="text-align: center">
                                        <%# viviapi.BLL.Supplier.Factory.GetSupplierName(Eval("suppId"))%>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnAudit" runat="server" Text="ͨ��" CommandName="Pass" CommandArgument='<%# Eval("tranno") %>'  />
                                        <asp:Button ID="btnRefuse" runat="server" Text="�ܾ�" CommandName="Refuse" CommandArgument='<%# Eval("tranno") %>'  />
                                    </td>       
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                <tr bgcolor="#EFF3FB">
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="text-align:center">
                                        &nbsp;
                                    </td>
                                    <td style="text-align:center">
                                        &nbsp;
                                    </td>
                                    <td style="text-align:right">
                                       
                                    </td>                                   
                                    <td style="text-align:right">
                                         &nbsp;�ϼ�
                                    </td>
                                    <td style="text-align:right">
                                        <%=PageTotalAmount%>
                                    </td>
                                    <td style="text-align:right">
                                        
                                    </td>
                                    <td style="text-align:center">
                                    </td>
                                    <td style="text-align:center">
                                    </td>
                                    <td style="width: 10%; text-align: center">
                                        
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                        <tr>
                            <td colspan="10">
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
    </div>
    </form>
     <script src="../js/public.js" type="text/javascript"></script>
</body>
</html>

