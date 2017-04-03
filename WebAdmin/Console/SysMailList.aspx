<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.SysMailList"
    CodeBehind="SysMailList.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="style/admin.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .rptheadlink
        {
            color: White;
            font-family: 宋体;
            font-size: 12px;
        }
    </style>

    <script src="js/common.js" type="text/javascript"></script>

    

</head>
<body class="yui-skin-sam">
    <form id="form1" runat="server">
    <div id="modelPanel" style="background-color: #F2F2F2">
    </div>
    <input id="selectedUsers" runat="server" type="hidden" />
    <table width="100%" border="0" cellspacing="1" cellpadding="1" class="table1">
        <tr>
            <td align="center" class="title">
                系统邮箱管理
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                    <asp:Repeater ID="rptdata" runat="server" onitemcommand="rptdata_ItemCommand">
                        <HeaderTemplate>
                            <tr style="background-color: #507CD1; color: #fff; height: 22;">                                
                                <td>
                                    服务器
                                </td>
                                <td>
                                    邮箱地址
                                </td>
                                <td>
                                    端口
                                </td>
                                <td>
                                    显示名称
                                </td>
                                <td>
                                    当前使用
                                </td>
                                <td>
                                    操作
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #EFF3FB">
                                <td>
                                    <%# Eval("host")%>
                                </td>
                                <td>
                                    <%# Eval("address")%>
                                </td>
                                <td>
                                    <%# Eval("port")%>
                                </td>
                                <td>
                                    <%# Eval("displayName")%>
                                </td>
                                <td>
                                    <%# Eval("used")%>
                                </td>
                                <td>
                                    <a href="SysMailConfig.aspx?ID=<%# Eval("id")%>">配置</a>
                                    <asp:LinkButton ID="lbtnDel" runat="server" CommandArgument='<%# Eval("id")%>' CommandName="del">删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #ffffff">
                                <td>
                                    <%# Eval("host")%>
                                </td>
                                <td>
                                    <%# Eval("address")%>
                                </td>
                                <td>
                                    <%# Eval("port")%>
                                </td>
                                <td>
                                    <%# Eval("displayName")%>
                                </td>
                                <td>
                                    <%# Eval("used")%>
                                </td>
                                <td>
                                    <a href="SysMailConfig.aspx?ID=<%# Eval("id")%>">配置</a>
                                   <asp:LinkButton ID="lbtnDel" runat="server" CommandArgument='<%# Eval("id")%>' CommandName="del">删除</asp:LinkButton>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
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
