<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.channel.WithdrawChannels" Codebehind="WithdrawChannels.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3c.org/TR/1999/REC-html401-19991224/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
   
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="Form1" runat="server">
        <table width="100%" border="0" cellpadding="1" cellspacing="1" id="table_zyads" style="width: 100%;
            height: 100%; border: #c9ddf0 1px solid; background-color: White;">
            <tr>
                <td align="center" colspan="2" class="title" >
                    结算通道
                 </td>
            </tr>
            <tr>             
                <td align="center">
                    <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                    <asp:Repeater ID="rptChnls" runat="server" onitemcommand="rptChnls_ItemCommand" 
                            onitemdatabound="rptChnls_ItemDataBound" >
                        <HeaderTemplate>
                            <tr style="background-color: #507CD1; color: #fff; height: 22;">                                
                                <td >
                                    结算银行
                                </td>  
                                <td>
                                    银行代码
                                </td>                                
                                <td>
                                    通道
                                </td>
                                <td>
                                    操作
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #EFF3FB">                                
                                <td>
                                    <%# Eval("bankName")%>
                                </td>                              
                                <td>
                                    <%# Eval("bankCode")%>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlsupp" runat="server"></asp:DropDownList>
                                </td>                                          
                                <td>
                                    <asp:Button ID="btnUpdate" runat="server" Text="保存" CommandName="update" CommandArgument='<%#Eval("id")%>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #ffffff">
                                <td>
                                    <%# Eval("bankName")%>
                                </td>                              
                                <td>
                                    <%# Eval("bankCode")%>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlsupp" runat="server"></asp:DropDownList>
                                </td>                                          
                                <td>
                                    <asp:Button ID="btnUpdate" runat="server" Text="保存" CommandName="update" CommandArgument='<%#Eval("id")%>' />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
