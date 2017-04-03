<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebUI7uka.agent.order.BankReportList" Codebehind="BankReports.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />

    <script src="../js/ControlDate/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css"> table {font-weight: normal;font-size: 12px; line-height: 170%;}
        td{ height: 11px; }
        A:link {color: #02418a;text-decoration: none;}
    </style>
    <script type="text/javascript">
        function sendInfo(id) {
            window.open("BankDetail.aspx?orderid=" + id, "查看订单", "height=760,width=800");
        }        
    </script>
    <script type="text/javascript">
        function openuserurl(url) {
            window.open(url, "查看用户信息");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="0" class="table1">
        <tr>
            <td align="center" colspan="3" class="title">
                网银状态报告
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td colspan="2">
                            &nbsp&nbsp商户ID：
                            <asp:TextBox ID="txtuserid" runat="server" Width="60px"></asp:TextBox>
                           
                            <asp:DropDownList ID="ddlNotifyStatus" runat="server" Width="95px">
                                <asp:ListItem>--下发状态--</asp:ListItem>
                                <asp:ListItem Value="1">处理中</asp:ListItem>
                                <asp:ListItem Value="2">已成功</asp:ListItem>
                                <asp:ListItem Value="4">失败</asp:ListItem>
                            </asp:DropDownList>
                           
                            &nbsp&nbsp开始：
                            <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                            &nbsp&nbsp截止：
                            <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                            &nbsp&nbsp&nbsp&nbsp<asp:Button ID="btn_Search" runat="server" CssClass="button" Text=" 查 询 " OnClick="btn_Search_Click">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp订单号：<asp:TextBox ID="txtOrderId" runat="server" Width="160px"></asp:TextBox>
                            &nbsp&nbsp商户订单号：<asp:TextBox ID="txtUserOrder" runat="server" Width="160px"></asp:TextBox>
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
                                                商户ID
                                            </td>  
                                            <td>
                                                接口版本
                                            </td>                                         
                                            <td>
                                                商户订单号
                                            </td>
                                            <td>
                                                报告状态
                                            </td> 
                                            <td>
                                                时间
                                            </td>                                           
                                            <td>
                                                消息
                                            </td>
                                            <td>
                                                System消息
                                            </td>
                                            <td>
                                                返回内容
                                            </td>
                                             <td>
                                                发送次数
                                            </td>
                                            <td>
                                                操作
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr bgcolor="#EFF3FB" ondblclick="javascript:sendInfo('<%# Eval("orderid")%>')">
                                            <td>
                                                <%# Eval("userid")%>
                                            </td>  
                                            <td>
                                                <%#viviapi.SysInterface.Bank.Utility.GetVersionName(Eval("version").ToString())%>
                                            </td>                                         
                                            <td>
                                                <%# Eval("userorder")%>
                                            </td>
                                            <td>
                                                <%#viviapi.BLL.Order.Bank.BankNotify.Instance.GetNotifyStatViewText(Eval("notifystat"))%> 
                                            </td>
                                            <td>
                                               <%# Eval("notifytime","{0:yyyy-MM-dd HH:mm:ss}")%>
                                            </td>
                                            <td>
                                                <%# Eval("httpStatusCode")%> <%# Eval("StatusDescription")%>
                                            </td> 
                                            <td>
                                                <%# Eval("message")%>
                                            </td>                                            
                                            <td>
                                                <span class="rowhidden" title="<%#Server.HtmlEncode(Eval("notifycontext").ToString())%>">
                                                    <%#Server.HtmlEncode(Eval("notifycontext").ToString())%>
                                                </span>
                                            </td>
                                            <td>
                                                <%#Eval("notifycount")%>
                                            </td>                                          
                                            <td>
                                                <a href="javascript:openuserurl('<%# Eval("againNotifyUrl")%>')">查看</a>
                                                <asp:Button ID="btnReissue" runat="server" Text="补发" ToolTip="手动回发" CommandName="Reissue" CommandArgument='<%# Eval("orderid")%>' />                                              
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr bgcolor="#ffffff" ondblclick="javascript:sendInfo('<%# Eval("orderid")%>')">
                                             <td>
                                                <%# Eval("userid")%>
                                            </td>  
                                            <td>
                                                <%#viviapi.SysInterface.Bank.Utility.GetVersionName(Eval("version").ToString())%>
                                            </td>                                         
                                            <td>
                                                <%# Eval("userorder")%>
                                            </td>
                                            <td>
                                                <%#viviapi.BLL.Order.Bank.BankNotify.Instance.GetNotifyStatViewText(Eval("notifystat"))%> 
                                            </td>
                                            <td>
                                               <%# Eval("notifytime","{0:yyyy-MM-dd HH:mm:ss}")%>
                                            </td>
                                            <td>
                                                <%# Eval("httpStatusCode")%> <%# Eval("StatusDescription")%>
                                            </td> 
                                            <td>
                                                <%# Eval("message")%>
                                            </td>                                            
                                            <td>
                                                <span class="rowhidden" title="<%#Server.HtmlEncode(Eval("notifycontext").ToString())%>">
                                                    <%#Server.HtmlEncode(Eval("notifycontext").ToString())%>
                                                </span>
                                            </td>
                                            <td>
                                                <%#Eval("notifycount")%>
                                            </td>                                          
                                            <td>
                                                <a href="javascript:openuserurl('<%# Eval("againNotifyUrl")%>')">查看</a>
                                                <asp:Button ID="btnReissue" runat="server" Text="补发" ToolTip="手动回发" CommandName="Reissue" CommandArgument='<%# Eval("orderid")%>' />                                              
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr style="background-color: #EBEBEB">
                        <td height="22" colspan="7">
                            <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CustomInfoHTML="总记录数：%RecordCount% 总页数：%PageCount% 当前页：%CurrentPageIndex%"
                                CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                                NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="DropDownList"
                                PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                                ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                                TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到" Width="100%" Height="30px"
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
