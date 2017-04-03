<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.order.CardWithholds" CodeBehind="CardWithholds.aspx.cs" %>
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
        function showDetail(id) {
            window.open("AgentDistsInfo.aspx?id=" + id, "�鿴����", "height=500,width=800");
        }
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
    </script>

    <style type="text/css">
        table
        {
            font-weight: normal;
            font-size: 12px;
            line-height: 170%;
            font-family: Arial;
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
    </style>
    
    <script src="../Js/ControlDate/WdatePicker.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; height: 100%;">
            <tr>
                <td align="center" class="title">
                    ���ܹ���
                </td>
            </tr>
            <tr>
                <td>
                    �̻�ID��<asp:TextBox ID="txtUserId" runat="server" Width="80px"></asp:TextBox>
                    ���ţ�<asp:TextBox ID="txtCardno" runat="server" Width="80px"></asp:TextBox>
                    ��ʼ��
                    <asp:TextBox ID="txtStimeBox" runat="server" Width="65px"></asp:TextBox>
                    ��ֹ��
                    <asp:TextBox ID="txtEtimeBox" runat="server" Width="65px"></asp:TextBox>
                    <asp:DropDownList ID="ddlSupplier" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlisclose" runat="server">
                        <asp:ListItem Value="">--״̬--</asp:ListItem>
                        <asp:ListItem Value="0">δ�ر�</asp:ListItem>
                        <asp:ListItem Value="1">&gt;�ѹر�</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                    </asp:Button>
                    <asp:Button ID="btnExport" runat="server" CssClass="button" Text="����" OnClick="btnExport_Click" Visible="false"></asp:Button>      
                    
                   
                            <div id="divmoney">
                    <span style="color: #ff0000; text-align: left">������<% = total_amount%></span> 
                </div>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:RadioButtonList ID="rblstatus" runat="server" 
                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1" Selected="True">�ر�</asp:ListItem>
                        <asp:ListItem Value="0">����</asp:ListItem>
                    </asp:RadioButtonList>         
                    <asp:Button ID="btnColse" runat="server" CssClass="button" Width="90px" 
                        Text="����ִ��" onclick="btnColse_Click"></asp:Button>
                    <asp:Button ID="btnAllColse" runat="server" CssClass="button" Width="90px" 
                        Text="ȫ��ִ��" onclick="btnAllColse_Click"></asp:Button>
                      <asp:Button ID="btnUnLock" runat="server" CssClass="button" Width="90px" 
                        Text="�����������" onclick="btnUnLock_Click" ></asp:Button>
                </td>
            </tr>
            
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                        <asp:Repeater ID="rptcards" runat="server" 
                            onitemdatabound="rptcards_ItemDataBound" >
                            <HeaderTemplate>
                                <tr style="background-color: #507CD1; color: #fff; height: 22px">
                                    <td style="width:3%">                                        
                                    </td>
                                    <td style="width:5%">
                                        <input id="Checkboxall" type="checkbox" class="qx" onclick="checkall(this)" />ȫѡ 
                                    </td>
                                    <td style="width:5%">
                                        �̻�
                                    </td>
                                    <td style="width:7%">
                                        ������
                                    </td>
                                    <td style="width:9%">
                                        ����
                                    </td>
                                    <td style="width:9%">
                                        ����
                                    </td>                                    
                                    <td style="width:5%">
                                        ��ֵ
                                    </td>
                                    <td style="width:5%">
                                        ״̬
                                    </td>
                                    <td style="width:5%">
                                        �ѽ���
                                    </td>                                    
                                    <td style="width:5%">
                                        ���
                                    </td>
                                    <td style="width:5%">
                                        ����
                                    </td>
                                    <td style="width:5%">
                                        ����
                                    </td>
                                    <td style="width:10%">
                                        ���ʱ��
                                    </td>
                                    <td style="width:10%">
                                        �޸�ʱ��
                                    </td>
                                    <td style="width:5%">
                                        ����ӿ�
                                    </td>
                                    <td>
                                        ����
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr bgcolor="#EFF3FB">
                                     <td style="width: 40px;">
                                        <img src="../style/images/folder_close.gif" style="cursor: hand" onclick="collapse(this, 'tr_<%#Eval("ID")%>')" alt="" />
                                    </td>
                                    <td>
                                        <input id="<%# Eval("ID") %>" type="checkbox" name="ischecked" class="qx" value="<%# Eval("ID") %>" />
                                    </td>
                                    <td>
                                        <a href="?action=paylistbyid&userid=<%#Eval("userid")%>">
                                            <%#Eval("UserName")%>(#<%#Eval("userid")%>) </a>
                                    </td>
                                    <td>
                                        <%# Eval("cardTypeName")%>
                                    </td>
                                    <td>
                                        <%# Eval("cardno")%>
                                    </td>
                                    <td>
                                        <%# Eval("cardpwd")%>
                                    </td>
                                    <td>
                                        <%# Eval("facevalue", "{0:f2}")%>
                                    </td>
                                    <td>
                                        <%#GetIsCloseViewText(Eval("isclose"))%>
                                    </td>
                                    <td>
                                        <%# Eval("settle", "{0:f2}")%>
                                    </td>
                                    <td>
                                        <%# Eval("withhold", "{0:f2}")%>
                                    </td>
                                    <td>
                                        <%# Eval("lockAmt", "{0:f2}")%>
                                    </td>
                                    <td>
                                        <%# Eval("profit", "{0:f2}")%>
                                    </td>
                                     <td>
                                        <%# Eval("addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                     <td>
                                        <%# Eval("updatetime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                     <td>
                                        <%# Eval("suppName")%>
                                    </td>
                                    <td>
                                        <asp:Literal ID="litcmd" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr id="tr_<%#Eval("ID") %>" style="display:none">
                                    <td colspan="16">
                                        <asp:Repeater ID="rptDetail" runat="server" >
                                        <HeaderTemplate>
                                            <table align="center" cellpadding="0" cellspacing="0" width="98%" class="zb" style="background-color: #f1fef1;margin: 8px;">
                                                <tr class="style3">
                                                    <td>
                                                        ������
                                                    </td>
                                                    <td>
                                                        ������
                                                    </td>
                                                    <td>
                                                        ����ʽ
                                                    </td>
                                                    <td height="30">
                                                        ʱ��
                                                    </td>                                                  
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#c4d6fc'"onmouseout='this.style.backgroundColor=c;'>
                                                <td>
                                                    &nbsp;
                                                    <%# Eval("orderid")%>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <%# Eval("settle", "{0:f2}")%>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <%#GetMethodViewText(Eval("method"))%>
                                                </td>
                                                <td>
                                                    &nbsp;<%# Eval("addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                                </td>                                              
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    </td>
                                </tr>
                            </ItemTemplate>
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
</body>
</html>
