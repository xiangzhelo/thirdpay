<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.Supplier.List" Codebehind="List.aspx.cs" %>

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
        <table width="100%" border="0" cellpadding="1" cellspacing="1" id="table_zyads" style="width: 100%;height: 100%; border: #c9ddf0 1px solid; background-color: White;">
            <tr>
                <td align="center" colspan="2" class="title">
                    接口商列表 <asp:Button ID="btnAdd" runat="server" Text="新 增" OnClick="btnAdd_Click"  /></td>                 
            </tr>
            <tr>
            <td align="center">
                <table width="100%" id="Table1" border="0" align="center" cellpadding="2" cellspacing="1">
                    <tr>
                        <td>
                            启用状态：
                            <asp:DropDownList ID="ddlused" runat="server">
                    <asp:ListItem Value="">--请选择--</asp:ListItem>
                    <asp:ListItem Value="1" Selected="true">已启用</asp:ListItem>
                    <asp:ListItem Value="0">未启用</asp:ListItem>
                </asp:DropDownList>
                            名称：<asp:TextBox ID="txtSupplierName" runat="server"></asp:TextBox>
                            代号：<asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" 查 询 " OnClick="btnSearch_Click"></asp:Button>
                        </td>                       
                    </tr>                    
                </table>
            </td>
        </tr>                   
            <tr>
                <td align="center">
                    <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                    <asp:Repeater ID="rptdata" runat="server">
                        <HeaderTemplate>
                            <tr style="background-color: #507CD1; color: #fff; height: 22;">
                                <td>
                                    代码
                                </td>
                                <td>
                                    名称
                                </td>
                                <td>
                                    网址
                                </td>
                                <td>
                                    Logo
                                </td>
                                <td>
                                    说明
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
                                    <%# Eval("code")%>
                                </td>
                                <td>
                                    <strong><%# Eval("name")%></strong> 
                                </td>
                                <td>
                                    <%# Eval("purl")%>
                                </td>
                                <td>
                                    <a href="<%# Eval("purl") %>" target="_blank"><img src="../style/images/paykeylogo/<%# Eval("logourl") %>" width="120" height="60" style="border: 1px dotted #DADADA" alt="" /></a>
                                </td>
                                <td>
                                    <%# Eval("desc")%>
                                </td>
                                <td>
                                    <a href="../finance/PayRateEdit.aspx?billid=<%# Eval("code")%>&type=3">费率</a>
                                </td>
                                <td>
                                    <a href="Edit.aspx?cmd=edit&ID=<%# Eval("ID") %>" class="cvlink">编辑</a>
                                    <a href="AcctEdit.aspx?code=<%# Eval("code") %>" class="cvlink">多账号</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #ffffff">
                               <td>
                                    <%# Eval("code")%>
                                </td>
                                <td>
                                    <strong><%# Eval("name")%></strong> 
                                </td>
                                <td>
                                    <%# Eval("purl")%>
                                </td>
                                <td>
                                    <a href="<%# Eval("purl") %>" target="_blank"><img src="../style/images/paykeylogo/<%# Eval("logourl") %>" width="120" height="60" style="border: 1px dotted #DADADA" alt="" /></a>
                                </td>
                                <td>
                                    <%# Eval("desc")%>
                                </td>
                                <td>
                                    <a href="../finance/PayRateEdit.aspx?billid=<%# Eval("code")%>&type=3">费率</a>
                                </td>
                                <td>
                                    <a href="Edit.aspx?cmd=edit&ID=<%# Eval("ID") %>" class="cvlink">编辑</a>
                                    <a href="AcctEdit.aspx?code=<%# Eval("code") %>" class="cvlink">多账号</a>
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
                                PageSize="8" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
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
    
    <script src="../js/public.js" type="text/javascript"></script>
</body>
</html>
