<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.User.UserList"
    CodeBehind="UserList.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .rptheadlink
        {
            color: White;
            font-family: ����;
            font-size: 12px;
        }
    </style>
    <script src="../js/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {
            $("#chkAll").click(function () {
                $("input[type='checkbox']").each(function () {
                    if ($("#chkAll").attr('checked') == true) {
                        $(this).attr("checked", true);
                    } else
                        $(this).attr("checked", false);
                });
            });
            var btnDeleteId = "#<%=btnDelete.ClientID%>";
            $(btnDeleteId).click(function () {
                return confirm("ȷ��Ҫɾ����Щ�̻���?");
            });
        });
        function sendMsg(uid) {
            window.location.href = "../News/SendMsg.aspx?uid=" + uid;
            // window.showModelessDialog("../News/SendMsg.aspx?uid=" + uid, window, "dialogWidth=900px;dialogHeight=600px;");
        }    
    </script>
</head>
<body class="yui-skin-sam">
    <form id="form1" runat="server">
    <div id="modelPanel" style="background-color: #F2F2F2">
    </div>
    <input id="selectedUsers" runat="server" type="hidden" />
    <table width="100%" border="0" cellspacing="1" cellpadding="1" class="table1">
        <tr>
            <td align="center" class="title">
                �̻�����
                <asp:Button ID="btnAdd" runat="server" Text="�����̻�" OnClick="btnAdd_Click" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="100%" id="Table1" border="0" align="center" cellpadding="2" cellspacing="1">
                    <tr>
                        <td>
                            �û�״̬��
                        </td>
                        <td>
                            <asp:DropDownList ID="StatusList" runat="server" EnableViewState="false">
                                <asp:ListItem Value="">--��ѡ��--</asp:ListItem>
                                <asp:ListItem Value="1">�����</asp:ListItem>
                                <asp:ListItem Value="2">����</asp:ListItem>
                                <asp:ListItem Value="4">����</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            �û�����
                        </td>
                        <td>
                            <asp:TextBox ID="txtuserName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            �û�ID��
                        </td>
                        <td>
                            <asp:TextBox ID="txtuserId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            ����ID��
                        </td>
                        <td>
                            <asp:TextBox ID="txtagent" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            ������
                        </td>
                        <td>
                            <asp:TextBox ID="txtfullname" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddluserlevel" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            �������ʣ�
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlisSpecialPayRate" runat="server">
                                <asp:ListItem Value="">���������ʡ�</asp:ListItem>
                                <asp:ListItem Value="0">δ����</asp:ListItem>
                                <asp:ListItem Value="1">����</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            ���ʱ�����
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSpecial" runat="server">
                                <asp:ListItem Value="">������ͨ����</asp:ListItem>
                                <asp:ListItem Value="1">������</asp:ListItem>
                                <asp:ListItem Value="0">δ����</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            QQ���룺
                        </td>
                        <td>
                            <asp:TextBox ID="txtQQ" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            �ֻ���
                        </td>
                        <td>
                            <asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Email��
                        </td>
                        <td>
                            <asp:TextBox ID="txtMail" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                </asp:Button>
                <asp:Button ID="btnDelete" runat="server" CssClass="button" Text=" ɾ ��" OnClick="btnDelete_Click">
                </asp:Button>
                <asp:Button ID="btn_Msg" runat="server" CssClass="button" Text="�ڲ���Ϣ" OnClick="btn_Msg_Click">
                </asp:Button>
                <asp:Button ID="btn_ClearCache" runat="server" CssClass="button" Text="������" OnClick="btn_ClearCache_Click">
                </asp:Button>
                <br />
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                    <asp:Repeater ID="rptUsers" EnableViewState="false" runat="server" OnItemDataBound="RptUsersItemDataBound">
                        <HeaderTemplate>
                            <tr style="background-color: #507CD1; color: #fff; height: 22;">
                                <td>
                                    <input id="chkAll" type="checkbox">
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    �̻�ID
                                </td>
                                <td>
                                    �û���
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlinkOrderby" runat="server" NavigateUrl="?orderby=balance&type=asc"
                                        CssClass="rptheadlink">����</asp:HyperLink>
                                </td>
                                <td>
                                    ʵ����֤
                                </td>
                                <td>
                                    �ֻ���֤
                                </td>
                                <td>
                                    �ʼ���֤
                                </td>
                                <td>
                                    ���ַ���
                                </td>
                                <td>
                                    ����¼
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    ״̬
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    ҵ��
                                </td>
                                <td>
                                    ͨ��
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    ����
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #EFF3FB">
                                <td>
                                    <input id="chkItem" type="checkbox" value='<%#Eval("id")%>' name="chkItem" />
                                </td>
                                <td>
                                    <%# Eval("province")%><%# Eval("city")%>
                                </td>
                                <td>
                                    <%# Eval("id")%>
                                </td>
                                <td>
                                    <a href='UserEdit.aspx?ID=<%# Eval("ID") %>'><strong>
                                        <%# Eval("userName")%>
                                    </strong></a>
                                </td>
                                <td>
                                    <%# Eval("balance")%>
                                </td>
                                <td>
                                    <%#Getpassview(Eval("isRealNamePass"))%>
                                </td>
                                <td>
                                    <%#Getpassview(Eval("isPhonePass"))%>
                                </td>
                                <td>
                                    <%#Getpassview(Eval("isEmailPass"))%>
                                </td>
                                <td>
                                    <%# Eval("schemename")%>
                                </td>
                                <td>
                                    <%# Eval("lastLoginTime","{0:yyyy-MM-dd HH:ss:mm}")%>
                                </td>
                                <td>
                                    <%# Eval("full_name")%>
                                </td>
                                <td>
                                    <asp:Label ID="lblUserType" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblUserStat" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblUserLevel" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="labagcmd" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <a href="UserChannel.aspx?ID=<%# Eval("id")%>">ͨ ��<%#IsSpecialChannel(Eval("ID"))%></a>
                                </td>
                                <td>
                                    <a href="../finance/UserPayRateEdit.aspx?ID=<%# Eval("id")%>">����</a>
                                </td>
                                <td>
                                    <asp:Label ID="labcmd" runat="server"></asp:Label>
                                    <a href="javascript:sendMsg(<%# Eval("ID") %>);">����Ϣ</a> <a href="gotoMerchantAdmin.aspx?userid=<%# Eval("id")%>"
                                        target="_blank">����</a> <a href="UserDomain.aspx?userid=<%# Eval("id")%>" target="_blank">
                                            ����</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #ffffff">
                                <td>
                                    <input id="chkItem" type="checkbox" value='<%#Eval("id")%>' name="chkItem" />
                                </td>
                                <td>
                                    <%# Eval("province")%><%# Eval("city")%>
                                </td>
                                <td>
                                    <%# Eval("id")%>
                                </td>
                                <td>
                                    <a href='UserEdit.aspx?ID=<%# Eval("ID") %>'><strong>
                                        <%# Eval("userName")%>
                                    </strong></a>
                                </td>
                                <td>
                                    <%# Eval("balance")%>
                                </td>
                                <td>
                                    <%#Getpassview(Eval("isRealNamePass"))%>
                                </td>
                                <td>
                                    <%#Getpassview(Eval("isPhonePass"))%>
                                </td>
                                <td>
                                    <%#Getpassview(Eval("isEmailPass"))%>
                                </td>
                                <td>
                                    <%# Eval("schemename")%>
                                </td>
                                <td>
                                    <%# Eval("lastLoginTime","{0:yyyy-MM-dd HH:ss:mm}")%>
                                </td>
                                <td>
                                    <%# Eval("full_name")%>
                                </td>
                                <td>
                                    <asp:Label ID="lblUserType" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblUserStat" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblUserLevel" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="labagcmd" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <a href="UserChannel.aspx?ID=<%# Eval("id")%>">ͨ ��<%#IsSpecialChannel(Eval("ID"))%></a>
                                </td>
                                <td>
                                    <a href="../finance/UserPayRateEdit.aspx?ID=<%# Eval("id")%>">����</a>
                                </td>
                                <td>
                                    <asp:Label ID="labcmd" runat="server"></asp:Label>
                                    <a href="javascript:sendMsg(<%# Eval("ID") %>);">����Ϣ</a> <a href="gotoMerchantAdmin.aspx?userid=<%# Eval("id")%>"
                                        target="_blank">����</a> <a href="UserDomain.aspx?userid=<%# Eval("id")%>" target="_blank">
                                            ����</a>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr style="background-color: #EFEFEF">
                        <td style="height: 16px;">
                            <aspxc:AspNetPager ID="Pager1" runat="server" OnPageChanged="Pager1_PageChanged"
                                AlwaysShow="True" CustomInfoHTML="�ܼ�¼����%RecordCount% ��ҳ����%PageCount% ��ǰҳ��%CurrentPageIndex%"
                                CustomInfoTextAlign="Left" FirstPageText="��ҳ" HorizontalAlign="Right" LastPageText="ĩҳ"
                                NavigationToolTipTextFormatString="��ת{0}ҳ" NextPageText="��һҳ" PageIndexBoxType="DropDownList"
                                PageSize="20" PrevPageText="��һҳ" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                                ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                                TextAfterPageIndexBox="ҳ" TextBeforePageIndexBox="����" Width="100%" Height="30px">
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

        var mytr = document.getElementById("tab").getElementsByTagName("tr");
        for (var i = 1; i < mytr.length; i++) {
            mytr[i].onmouseover = function () {
                var rows = this.childNodes.length;
                for (var row = 0; row < rows; row++) {
                    this.childNodes[row].style.backgroundColor = '#E6EEFF';
                }
            };
            mytr[i].onmouseout = function () {
                var rows = this.childNodes.length;
                for (var row = 0; row < rows; row++) {
                    this.childNodes[row].style.backgroundColor = '';
                }
            };
        }

    </script>
</body>
</html>
