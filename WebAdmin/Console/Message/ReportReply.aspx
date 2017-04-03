<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.web.Manage.Jubao.ItemModi" Codebehind="ReportReply.aspx.cs" %>

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
                举报投诉处理
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="td2">
                序号 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblid" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                举报者 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtname" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                举报人邮件 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtemail" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                电话 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txttel" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                 信息所在详细网址 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txturl" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                被举报信息类型 ：
            </td>
            <td class="td1">
                <asp:DropDownList ID="ddltype" runat="server">
                    <asp:ListItem Value="0">-请选择-</asp:ListItem>
                    <asp:ListItem Value="1">淫秽色情</asp:ListItem>
                    <asp:ListItem Value="2">诈骗</asp:ListItem>
                    <asp:ListItem Value="3">病毒</asp:ListItem>
                    <asp:ListItem Value="4">其他违法和不良信息</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td2">
                被举报详细内容 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtremark" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                添加时间 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtaddtime" runat="server" Width="70px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                状态 ：
            </td>
            <td class="td1">
                <asp:DropDownList ID="ddlstatus" runat="server">
                    <asp:ListItem Value="1">等待处理</asp:ListItem>
                    <asp:ListItem Value="2">已处理</asp:ListItem>
                </asp:DropDownList>
            </td>            
        </tr>
        <tr>
            <td class="td2">
                处理时间 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtchecktime" runat="server" Width="70px"  Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                处理人 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtcheck" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                处理意见 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtcheckremark" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                查询密码 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtpwd" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                提交IP ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtfield1" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 20px">
                <div align="center">
                    <asp:Button ID="btnOK" runat="server" Text="确定处理" OnClick="btnOK_Click"></asp:Button>
                    <input type="button" value="关 闭" onclick="backreturn()" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
