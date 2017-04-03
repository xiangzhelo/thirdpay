<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="transfer.aspx.cs" Inherits="viviAPI.WebUI7uka.agentmodule.settlement.Transfer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/usermodule/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />

    <script src="/usermodule/static/js/lib/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="/usermodule/js/public.js" type="text/javascript"></script>
    <script src="/usermodule/js/transfer.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function() {
            $("#txtTransferMoney").numeral();
            
            $("#txtTransferMoney").blur(function() {
                    CalculateFee();
            });
            
            $("#txtToUser").focus(function() {
                $(this).val("");
                $("#touserid").val(0);
            });
            $("#txtToUser").blur(function() {
                if ($(this).val() == "")
                    return;
                $.getJSON("/usermodule/ws/getUserInfo.ashx?t=" + Math.random(), { username: $(this).val() },
                    function(data) {
                        if (data.result == 0) {
                            alert(data.msg);
                        } else {
                            $("#touserid").val(data.result);
                            $("#txtToUser").val(data.username + "(" + data.name + ")");
                        }
                    });
            });

            $("#ibtnSave").click(function() {
                var touser = $("#touserid").val();
                if (touser == '' || touser == "0") {
                    $("#callinfo").html(errico + "请输入对方账号");
                    return false;
                };
                var money = $("#txtTransferMoney").val();
                if (money == "") {
                    $("#callinfo").html(errico + "请转账金额");
                    return false;
                };
            });
        });

       


    </script>

    

</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/usermodule/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/usermodule/settlement/index.aspx'">结算提现</a>
        &nbsp;&gt;&nbsp; <span>余额转账</span>
    </div>
    
    <!--右部表单开始-->
    <div id="list_content" style="padding-top: 0px;">
        <h2>
            余额转账</h2>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="2" colspan="3" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    可用金额:
                </td>
                <td align="left" class="line_01">
                    <span class="zi23">
                        <asp:Literal ID="litenableAmount" runat="server"></asp:Literal></span> 元
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    对方账号:
                </td>
                <td align="left" class="line_01">
                    <asp:TextBox ID="txtToUser" runat="server" Class="txt_01"></asp:TextBox>
                    <asp:HiddenField ID="touserid" runat="server" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    付款金额:
                </td>
                <td align="left" class="line_01">
                    <asp:TextBox ID="txtTransferMoney" runat="server" MaxLength="15" Class="txt_01"></asp:TextBox>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    转账说明:
                </td>
                <td align="left" class="line_01">
                    <asp:TextBox ID="txtremark" runat="server" Class="txt_01"></asp:TextBox>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    提现密码:
                </td>
                <td align="left" class="line_01">
                    <asp:TextBox ID="txttocashpwd" runat="server" Class="txt_01" TextMode="Password"></asp:TextBox>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    手续费:
                </td>
                <td align="left" class="line_01">
                    <em class="font14"><b id="chargeshow" class="txtc">0</b> 元 </em>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="10" colspan="2">
                </td>
            </tr>
            <tr>
                <td height="22" align="left" class="font8">
                    <asp:Button ID="btnSave" runat="server" Text="确认提交" CssClass="btn btn-primary" OnClick="btnSave_Click" />&nbsp;
                    &nbsp;<span class="txtr" id="callinfo" runat="server" style="color: Red; font-weight: bold"></span>
                    <td align="right">
                        &nbsp;
                    </td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
