<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.Supplier.AcctEdit" Codebehind="AcctEdit.aspx.cs" %>

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
                    接口商账号列表            
            </tr>
            <tr>
            <td align="center">
                <table width="100%" id="Table1" border="0" align="center" cellpadding="2" cellspacing="1">
                    <tr>
                        <td>
                            当前接口商：<asp:Label ID="lblSupplierName" runat="server" ></asp:Label>
                        </td>                       
                    </tr>                    
                </table>
            </td>
        </tr>                   
            <tr>
                <td align="center">
                    <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                    <asp:Repeater ID="rptdata" runat="server" onitemcommand="rptdata_ItemCommand">
                        <HeaderTemplate>
                            <tr style="background-color: #507CD1; color: #fff; height: 22;">
                                <td>
                                    标记
                                </td>
                                <td>
                                    API账号
                                </td>
                                <td>
                                    API密钥
                                </td>
                                <td>
                                    用户名称
                                </td>
                                <td>
                                    绑定域名
                                </td>
                                <td>
                                    是否默认
                                </td>
                                <td>
                                    说明
                                </td>
                                <td>
                                    操作
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #EFF3FB">
                                <td>
                                    <strong><%# Eval("name")%></strong> 
                                </td>
                                <td>
                                    <%# Eval("apiAccount")%>
                                </td>
                                <td>
                                    <%# Eval("apiKey")%>
                                </td>
                                <td>
                                    <%# Eval("userName")%>
                                </td>
                                <td>
                                    <%# Eval("domain")%>
                                </td>
                                <td>
                                    <%# Eval("isdefault")%>
                                </td>
                                <td>
                                    <%# Eval("jumpdomain")%>
                                </td>
                                <td>
                                    <a href="?cmd=edit&ID=<%# Eval("ID") %>&code=<%# Eval("code") %>" class="cvlink">编辑</a>
                                    <asp:Button ID="btndel" runat="server" Text="删除" OnClientClick="return confirm('确定要删除吗?')" CommandArgument='<%#Eval("id")%>' CommandName="del" />

                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #ffffff">
                               <td>
                                    <strong><%# Eval("name")%></strong> 
                                </td>
                                <td>
                                    <%# Eval("apiAccount")%>
                                </td>
                                <td>
                                    <%# Eval("apiKey")%>
                                </td>
                                <td>
                                    <%# Eval("userName")%>
                                </td>
                                <td>
                                    <%# Eval("domain")%>
                                </td>
                                <td>
                                    <%# Eval("isdefault")%>
                                </td>
                                 <td>
                                    <%# Eval("jumpdomain")%>
                                </td>
                                <td>
                                    <a href="?cmd=edit&ID=<%# Eval("ID") %>&code=<%# Eval("code") %>" class="cvlink">编辑</a>
                                    <asp:Button ID="btndel" runat="server" Text="删除" OnClientClick="return confirm('确定要删除吗?')" CommandArgument='<%#Eval("id")%>' CommandName="del" />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                    
                    <tr >
                                <td>
                                     <asp:TextBox ID="txtname" runat="server" Width="50%"></asp:TextBox>
                                </td>
                                <td>
                                     <asp:TextBox ID="txtapiAccount" runat="server" Width="50%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtapiKey" runat="server" Width="50%"></asp:TextBox>
                                </td>
                                <td>
                                   <asp:TextBox ID="txtuserName" runat="server" Width="50%"></asp:TextBox>
                                </td>
                                <td>
                                   <asp:TextBox ID="txtdomain" runat="server" Width="50%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="ckbisdefault" runat="server" />
                                </td>
                                 <td>
                                   <asp:TextBox ID="txtjumpdomain" runat="server" Width="50%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click" />
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
