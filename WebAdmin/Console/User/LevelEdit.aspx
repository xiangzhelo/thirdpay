<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.User.LevelEdit"
    CodeBehind="LevelEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>费率编辑</title>
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript">
        function backreturn() {
            location.href = 'UserLevels.aspx';
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="1" cellpadding="0">
            <tr>
                <td align="center" colspan="3" class="title" >
                    编辑等级 </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="td2b">
                    等级ID：
                </td>
                <td class="td1b">
                    <asp:TextBox ID="txtLevel" runat="server" Width="120px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    等级名称：
                </td>
                <td class="td1b">
                    <asp:TextBox ID="txtlevName" runat="server" Width="120px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                     
                    
                </td>
                <td class="td1b">
                    <asp:Button ID="btnSave" runat="server" Text="保 存" OnClick="btnSave_Click"></asp:Button>
                  <input type="button" value="返 回" onclick="backreturn()" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
