<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.Message.FeedbackReply" Codebehind="FeedbackReply.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />

    <script src="../js/common.js" type="text/javascript"></script>

    <script type="text/javascript">
    $().ready(function() {
    var usertype = $("input[name='rbluserType']:checked").val();       
        if (usertype == "3") {
            $("#ddlmemvip").show();
            $("#ddlpromvip").hide();
        }
        else if (usertype == "4") {
            $("#ddlpromvip").show();
            $("#ddlmemvip").hide();
        }
        $("input[name='rbluserType']").click(function() {
            var usertype = $(this).val();   
             alert(usertype);         
            if (usertype == "3") {
                $("#ddlmemvip").show();
                $("#ddlpromvip").hide();
            }
            else if (usertype == "4") {
                $("#ddlpromvip").show();
                $("#ddlmemvip").hide();
            }
        });
    })
    function backreturn() {
        window.close();
}
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
            <td colspan="4" class="title">
                商户信息编辑
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">       
        <tr>
            <td class="td2">
                反馈用户ID ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtuserid" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                类型 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txttypeid" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                标题 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txttitle" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                内容 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtcont" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                状态 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtstatus" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                发送时间 ：
            </td>
            <td class="td1">
                <input onselectstart="return false;" onkeypress="return false" id="txtaddtime" onfocus="setday(this)"
                    readonly type="text" size="10" name="Text1" runat="server" style="width:200px">
            </td>
        </tr>
        <tr>
            <td class="td2">
                回复 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtreply" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                回复者 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtreplyer" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                回复日期 ：
            </td>
            <td class="td1">
                <input onselectstart="return false;" onkeypress="return false" id="txtreplytime"
                    onfocus="setday(this)" readonly type="text" size="10" name="Text1" runat="server" style="width:200px">
            </td>
        </tr>       
        <tr>
            <td colspan="4" style="height: 20px">
                <div align="center">
                    <asp:Button ID="btnOK" runat="server" Text="回复" OnClick="btnOK_Click"></asp:Button>
                    <input type="button" value="关 闭" onclick="backreturn()" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
