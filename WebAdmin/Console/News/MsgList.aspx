<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.News.MsgList" Codebehind="MsgList.aspx.cs" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />   
    <script src="../js/common.js" type="text/javascript"></script> 
    <script src="../js/ControlDate/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function() {
            $("#chkAll").click(function() {
                $("input[type='checkbox']").each(function() {
                    if ($("#chkAll").attr('checked') == true) {
                        $(this).attr("checked", true);
                    } else
                        $(this).attr("checked", false);
                });
            });
            var btnDeleteId = "#<%=btnDelete.ClientID%>";
            $(btnDeleteId).click(function() {
                return confirm("确定要删除吗?");
            });
        });   
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" border="0" cellpadding="1" cellspacing="1" class="table1">
                <tr>
                    <td align="center" class="title">
                        内部消息</td>
                </tr>
                <tr>
                    <td style="height: 30px">
                         &nbsp&nbsp开始：
                            <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                            &nbsp&nbsp截止：
                            <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                            <asp:Button ID="btnDelete" runat="server" CssClass="button" Text=" 删 除" OnClick="btnDelete_Click">
                </asp:Button>
                            </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="1" width="100%" id="tab">
                                <asp:Repeater ID="rptMsgs" runat="server">
                                    <HeaderTemplate>
                                        <tr height="22" style="background-color: #507CD1; color: #fff">
                                            <td>
                                                <input id="chkAll" type="checkbox">
                                            </td>
                                            <td>
                                                发信人
                                            </td>
                                            <td>
                                                收信人
                                            </td>
                                            <td>
                                                标题
                                            </td>
                                            <td>
                                                时间
                                            </td>
                                            <td>
                                                已读
                                            </td>
                                            <td>
                                                
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr bgcolor="#EFF3FB">
                                            <td>
                                                <input id="chkItem" type="checkbox" value='<%#Eval("ID")%>' name="msgIds" />
                                            </td>
                                            <td>
                                                <%# viviapi.BLL.Communication.InternalMessage.GetUserTypeName(Convert.ToByte(Eval("senderUserType")))%><%# Eval("sender")%>(# <%# Eval("sendId")%> )
                                            </td>
                                            <td>
                                                <%# viviapi.BLL.Communication.InternalMessage.GetUserTypeName(Convert.ToByte(Eval("receiverType")))%> <%# Eval("receiver")%>(# <%# Eval("receiverId")%> )                            
                                            </td>
                                            <td>
                                                <%# Eval("msgtitle")%>
                                            </td>
                                            <td>
                                                <%# Eval("addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                            </td>
                                            <td>
                                                <%# Eval("isRead")%>
                                            </td>
                                            <td>
                                                <a href="MsgView.aspx?Id=<%# Eval("id")%>" >查看</a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr bgcolor="#ffffff">
                                             <td>
                                                <input id="chkItem" type="checkbox" value='<%#Eval("ID")%>' name="msgIds" />
                                            </td>
                                            <td>
                                                <%# viviapi.BLL.Communication.InternalMessage.GetUserTypeName(Convert.ToByte(Eval("senderUserType")))%><%# Eval("sender")%>(# <%# Eval("sendId")%> )
                                            </td>
                                            <td>
                                                <%# viviapi.BLL.Communication.InternalMessage.GetUserTypeName(Convert.ToByte(Eval("receiverType")))%> <%# Eval("receiver")%>(# <%# Eval("receiverId")%> )                            
                                            </td>
                                            <td>
                                                <%# Eval("msgtitle")%>
                                            </td>
                                            <td>
                                                <%# Eval("addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                            </td>
                                            <td>
                                                <%# Eval("isRead")%>
                                            </td>
                                            <td>
                                                <a href="MsgView.aspx?Id=<%# Eval("id")%>" >查看</a>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </table>
                        <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CustomInfoHTML="总记录数：%RecordCount% 总页数：%PageCount% 当前页：%CurrentPageIndex%"
                CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="DropDownList"
                PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到"
                Width="100%" Height="30px" onpagechanged="Pager1_PageChanged"></aspxc:AspNetPager>
                    </td>
                </tr>
            </table>
        </div>
    </form>
    
</body>
</html>
