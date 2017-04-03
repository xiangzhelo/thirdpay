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
            font-family: 宋体;
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
                return confirm("确定要删除这些商户吗?");
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
                商户管理
                <asp:Button ID="btnAdd" runat="server" Text="新增商户" OnClick="btnAdd_Click" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="100%" id="Table1" border="0" align="center" cellpadding="2" cellspacing="1">
                    <tr>
                        <td>
                            用户状态：
                        </td>
                        <td>
                            <asp:DropDownList ID="StatusList" runat="server" EnableViewState="false">
                                <asp:ListItem Value="">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1">待审核</asp:ListItem>
                                <asp:ListItem Value="2">正常</asp:ListItem>
                                <asp:ListItem Value="4">锁定</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            用户名：
                        </td>
                        <td>
                            <asp:TextBox ID="txtuserName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            用户ID：
                        </td>
                        <td>
                            <asp:TextBox ID="txtuserId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            代理ID：
                        </td>
                        <td>
                            <asp:TextBox ID="txtagent" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            姓名：
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
                            单独费率：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlisSpecialPayRate" runat="server">
                                <asp:ListItem Value="">—单独费率—</asp:ListItem>
                                <asp:ListItem Value="0">未启用</asp:ListItem>
                                <asp:ListItem Value="1">启用</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            费率比例：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSpecial" runat="server">
                                <asp:ListItem Value="">—独立通道—</asp:ListItem>
                                <asp:ListItem Value="1">已设置</asp:ListItem>
                                <asp:ListItem Value="0">未设置</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            QQ号码：
                        </td>
                        <td>
                            <asp:TextBox ID="txtQQ" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            手机：
                        </td>
                        <td>
                            <asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Email：
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
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" 查 询 " OnClick="btnSearch_Click">
                </asp:Button>
                <asp:Button ID="btnDelete" runat="server" CssClass="button" Text=" 删 除" OnClick="btnDelete_Click">
                </asp:Button>
                <asp:Button ID="btn_Msg" runat="server" CssClass="button" Text="内部消息" OnClick="btn_Msg_Click">
                </asp:Button>
                <asp:Button ID="btn_ClearCache" runat="server" CssClass="button" Text="清理缓存" OnClick="btn_ClearCache_Click">
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
                                    地区
                                </td>
                                <td>
                                    商户ID
                                </td>
                                <td>
                                    用户名
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlinkOrderby" runat="server" NavigateUrl="?orderby=balance&type=asc"
                                        CssClass="rptheadlink">余额↑</asp:HyperLink>
                                </td>
                                <td>
                                    实名认证
                                </td>
                                <td>
                                    手机认证
                                </td>
                                <td>
                                    邮件认证
                                </td>
                                <td>
                                    提现方案
                                </td>
                                <td>
                                    最后登录
                                </td>
                                <td>
                                    姓名
                                </td>
                                <td>
                                    类型
                                </td>
                                <td>
                                    状态
                                </td>
                                <td>
                                    级别
                                </td>
                                <td>
                                    业务
                                </td>
                                <td>
                                    通道
                                </td>
                                <td>
                                    比率
                                </td>
                                <td>
                                    操作
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
                                    <a href="UserChannel.aspx?ID=<%# Eval("id")%>">通 道<%#IsSpecialChannel(Eval("ID"))%></a>
                                </td>
                                <td>
                                    <a href="../finance/UserPayRateEdit.aspx?ID=<%# Eval("id")%>">费率</a>
                                </td>
                                <td>
                                    <asp:Label ID="labcmd" runat="server"></asp:Label>
                                    <a href="javascript:sendMsg(<%# Eval("ID") %>);">发信息</a> <a href="gotoMerchantAdmin.aspx?userid=<%# Eval("id")%>"
                                        target="_blank">进入</a> <a href="UserDomain.aspx?userid=<%# Eval("id")%>" target="_blank">
                                            域名</a>
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
                                    <a href="UserChannel.aspx?ID=<%# Eval("id")%>">通 道<%#IsSpecialChannel(Eval("ID"))%></a>
                                </td>
                                <td>
                                    <a href="../finance/UserPayRateEdit.aspx?ID=<%# Eval("id")%>">费率</a>
                                </td>
                                <td>
                                    <asp:Label ID="labcmd" runat="server"></asp:Label>
                                    <a href="javascript:sendMsg(<%# Eval("ID") %>);">发信息</a> <a href="gotoMerchantAdmin.aspx?userid=<%# Eval("id")%>"
                                        target="_blank">进入</a> <a href="UserDomain.aspx?userid=<%# Eval("id")%>" target="_blank">
                                            域名</a>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr style="background-color: #EFEFEF">
                        <td style="height: 16px;">
                            <aspxc:AspNetPager ID="Pager1" runat="server" OnPageChanged="Pager1_PageChanged"
                                AlwaysShow="True" CustomInfoHTML="总记录数：%RecordCount% 总页数：%PageCount% 当前页：%CurrentPageIndex%"
                                CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                                NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="DropDownList"
                                PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                                ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                                TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到" Width="100%" Height="30px">
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
