<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.channel.ChannelEdit" Codebehind="Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建编辑供应商</title>
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />
    

    <script type="text/javascript">
function backreturn(){
    location.href='List.aspx';
}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                <td align="center" colspan="2" class="title">
                    通道编辑
                    </td>
            </tr>
                <tr>
                    <td class="td2">
                        通道代号 ：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtcode" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        英文代码 ：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtenmodeName" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        通道类别 ：</td>
                    <td class="td1">
                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        通道类别供应商 ：</td>
                    <td class="td1">
                        <asp:Literal ID="litTypeSupplier" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        供应商 ：</td>
                    <td class="td1">
                        <asp:DropDownList ID="ddlSupp" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        辅助接口 ：</td>
                    <td class="td1">
                        <asp:DropDownList ID="ddlSupp2" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        通道名称 ：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtmodeName" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        面值 ：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtfaceValue" runat="server" Width="200px" Text="0"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        类别开启状态 ：</td>
                    <td class="td1">
                         <asp:RadioButtonList ID="rblTypeOpen" runat="server" RepeatDirection="horizontal">                                        
                                <asp:ListItem Value="2" Selected="true">全部开启</asp:ListItem>
                                <asp:ListItem Value="1">全部关闭</asp:ListItem>
                                <asp:ListItem Value="8" Selected="true">按配置(默认开启)</asp:ListItem>
                                <asp:ListItem Value="4">按配置(默认关闭)</asp:ListItem>
                         </asp:RadioButtonList>
                    </td>
                    
                </tr>  
                <tr>
                    <td class="td2">
                        是否开启 ：</td>
                    <td class="td1">
                         <asp:RadioButtonList ID="rblOpen" runat="server" RepeatDirection="horizontal">
                                <asp:ListItem Value="-1" Selected="true">默认</asp:ListItem>
                                <asp:ListItem Value="1">开启</asp:ListItem>
                                <asp:ListItem Value="0">关闭</asp:ListItem>
                         </asp:RadioButtonList>
                    </td>
                    
                </tr>               
                <tr>
                    <td class="td2">
                        排序 ：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtsort" runat="server" Width="200px" Text="0"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        <asp:Button ID="btnSave" runat="server" Text="保 存" OnClick="btnSave_Click"></asp:Button>
                    </td>
                    <td class="td1">
                        <input type="button" value="返 回" onclick="backreturn()" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
