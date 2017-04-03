<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.channel.ChannelList" Codebehind="List.aspx.cs" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3c.org/TR/1999/REC-html401-19991224/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="Form1" runat="server">
        <table width="100%" border="0" cellpadding="1" cellspacing="1" id="table_zyads" style="width: 100%;
            height: 100%; border: #c9ddf0 1px solid; background-color: White;">
            <tr>
                <td align="center" colspan="2" class="title">
                    通道管理
                    </td>
            </tr>
            <tr>
            <td>
               类别：<asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                </asp:DropDownList><asp:Button ID="btnSearch" runat="server" Text="查 找" OnClick="btnSearch_Click" />
                <asp:Button ID="btnAdd" runat="server" Text="新 增" OnClick="btnAdd_Click" />
            </td>
            </tr>
            <tr>             
                <td align="center">
                    <asp:Repeater ID="rptchannels" runat="server" 
                        onitemcommand="rptchannels_ItemCommand" 
                        onitemdatabound="rptchannels_ItemDataBound">
                        <HeaderTemplate>
                            <table cellspacing="1" cellpadding="4" border="0" style="color:#333333;width:100%;">
                                <tr style="color:White;background-color:#507CD1;font-weight:bold;">
                                <th scope="col">类别</th>
                                <th scope="col">代码</th>
                                <th scope="col">英文代码</th>
                                <th scope="col">名称</th>
                                <th scope="col">面值</th>
                                <th scope="col">通道接口商</th>
                                <th scope="col">通道费率</th>
                                <th scope="col">通道类别接口商</th>
                                <th scope="col">通道类别费率</th>
                                <th scope="col">开启状态</th>
                                <th scope="col">&nbsp;</th>
                                </tr>
                        </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color:#EFF3FB;">
			                        <td><%#Eval("modetypename")%></td>
			                        <td><%#Eval("code")%></td>
			                        <td><%#Eval("modeEnName")%></td>
			                        <td><%#Eval("modeName")%></td>
			                        <td><%#Eval("faceValue")%></td>
			                        <td><%#Eval("usingSupplierName")%></td>
			                        <td><%#Eval("supprate", "{0:p2}")%></td>
			                        <td><%#Eval("typesupp")%></td>
			                        <td><%#Eval("typesupprate", "{0:p2}")%></td>
			                        <td><asp:Literal ID="litopen" runat="server"></asp:Literal></td>
			                        <td>
                                        <input type="button" value="设 置" onclick="javascript:location.href='Edit.aspx?id=<%#Eval("id")%>'" /> 
                                    <asp:Button ID="btnDel" runat="server" Text="删除" CommandName="del" CommandArgument='<%#Eval("id")%>' OnClientClick="javascript:return confirm('您确认要删除此通道吗？');" />                                   
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                 <tr style="background-color:White;">
			                        <td><%#Eval("modetypename")%></td>
			                        <td><%#Eval("code")%></td>
			                        <td><%#Eval("modeEnName")%></td>
			                        <td><%#Eval("modeName")%></td>
			                        <td><%#Eval("faceValue")%></td>
			                        <td><%#Eval("usingSupplierName")%></td>
			                        <td><%#Eval("supprate", "{0:p2}")%></td>
			                        <td><%#Eval("typesupp")%></td>
			                        <td><%#Eval("typesupprate", "{0:p2}")%></td>
			                        <td><asp:Literal ID="litopen" runat="server"></asp:Literal></td>
			                        <td>
                                        <input type="button" value="设 置" onclick="javascript:location.href='Edit.aspx?id=<%#Eval("id")%>'" /> 
                                        <asp:Button ID="btnDel" runat="server" Text="删除" CommandName="del" CommandArgument='<%#Eval("id")%>' OnClientClick="javascript:return confirm('您确认要删除此通道吗？');" />                                   
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                            </asp:Repeater>
                    
                </td>
            </tr>
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
    </form>
</body>
</html>
