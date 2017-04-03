<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.User.ApiKeyEdit" Codebehind="ApiKeyEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../style/admin.css?v=1" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />
    <script src="../js/common.js" type="text/javascript"></script>
    
<script type="text/javascript">  
function backreturn(){
    history.go(-1);
}
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="1" cellpadding="3">
            <tr>
                <td colspan="4" class="title">
                    商户ApiKey编辑</td>
            </tr>
        </table>
        
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="td2b">
                    用户名ID：</td>
                <td class="td1b">
                    <asp:Label ID="lblid" runat="server" Width="160px" CssClass="lable"></asp:Label>
                </td>
                <td class="td2b">
                    用户名：</td>
                <td class="td1b">
                     <asp:Label ID="lblUserName" runat="server" Width="160px" CssClass="lable"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    API通讯密钥：</td>
                <td class="td1b" colspan="5" style="width:100%">
                    <asp:TextBox ID="txtapikey" runat="server" Rows="2" TextMode="MultiLine" Width="80%" CssClass="lable"></asp:TextBox>
                    <asp:Button ID="btnNew" runat="server" Text="生成" onclick="btnNew_Click"></asp:Button>
                </td>
            </tr>           
            <tr>
                <td colspan="4" style="height: 20px">
                    <div align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="提 交" OnClick="btnAdd_Click"></asp:Button>
                        
                        <input type="button" value="返 回" onclick="backreturn()" />
                    </div>
                </td>
            </tr>
        </table>
        
    </form>
</body>
</html>
