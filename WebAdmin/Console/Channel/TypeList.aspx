<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.web.Manage.ChannelTypeList" Codebehind="TypeList.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3c.org/TR/1999/REC-html401-19991224/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
                    通道类别列表
                    <asp:Button ID="btnAdd" runat="server" Text="新 增" OnClick="btnAdd_Click" Visible="false" /></td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="GVChannel" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" Width="100%" CellSpacing="1" OnRowDataBound="GVChannel_RowDataBound">
                        <Columns>    
                            <asp:BoundField DataField="typeId" HeaderText="代码">
                                <ControlStyle Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="modetypename" HeaderText="名称">
                                <ControlStyle Width="8%" />
                            </asp:BoundField>    
                             <asp:BoundField DataField="code" HeaderText="英文代码">
                                <ControlStyle Width="8%" />
                            </asp:BoundField>                   
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    类 型
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal ID="ltType" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    接口模式
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal ID="ltrunmode" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>                          
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    开启状态
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal ID="litOpen" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    前台显示
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButtonList ID="rblrelease" runat="server" RepeatDirection="horizontal">
                                        <asp:ListItem Value="True">显示</asp:ListItem>
                                        <asp:ListItem Value="False">关闭</asp:ListItem>
                                    </asp:RadioButtonList>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="Name" HeaderText="当前接口商">                               
                            </asp:BoundField>
                             <asp:TemplateField>
                                <HeaderTemplate>
                                    平台费率
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("supprate","{0:p2}")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <input type="button" value="设 置" onclick="javascript:location.href='TypeEdit.aspx?id=<%#Eval("id")%>'" />                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
