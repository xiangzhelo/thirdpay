<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="safetrna.aspx.cs" Inherits="viviAPI.WebUI7uka.usermodule.safety.Safetrna" ValidateRequest="false"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/usermodule/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/page.css?" />
    <style type="text/css"></style>
    <script type="text/javascript" src="../js/jquery-1.8.1.min.js"></script>
    <script type="text/javascript" src="../js/formValidator.js"></script>
        <script type="text/javascript">
            jQuery(document).ready(function($) {
                jQuery.formValidator.initConfig({
                    formid: "form1",
                    submitonce: true,
                    onerror: function(msg) {
                        $("#lblMessage").text(msg);
                    }
                });
                jQuery("#txtpernumber").formValidator({ tipid: "tip_pernumber", onshow: "（请输入您的身份证号！）", onfocus: "（请输入您的身份证号！）", oncorrect: "" }).inputValidator({ min: 1, empty: { leftempty: false, rightempty: false, emptyerror: "（请输入您的身份证号！）" }, onerror: "（请输入您的身份证号！）" });
                jQuery("#txtrpernumber").formValidator({ tipid: "tip_rpernumber", onshow: "（请确认您的身份证号！）", onfocus: "（请确认您的身份证号！）", oncorrect: "" }).inputValidator({ min: 1, empty: { leftempty: false, rightempty: false, emptyerror: "（请确认您的身份证号！）" }, onerror: "（请确认您的身份证号！）" }).compareValidator({
                desid: "txtpernumber",
                    operateor: "=",
                    onerror: "（两次身份证号输入不一致，请重新输入！）"
                });
            });

            function frmCheck() {
                if (jQuery.formValidator.pageIsValid("1") == false) {
                    return false;
                }
                return true;
            }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <%--<div class="breadCrumb">
        <a href="" onclick="parent.location.href='/usermodule/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/usermodule/safety/index.aspx'">安全中心</a>
        &nbsp;&gt;&nbsp; <span>实名认证</span>
    </div>--%>
    <!--右部表单开始-->
    <div id="list_content">
        <div class="page-wrapper">
            <h2>
                实名认证</h2>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td height="60" colspan="3" align="left">
                        ① 系统不支持一代身份证实名认证；<br />
                        ② 实名认证的真实姓名必须与银行卡的户名一致,以免影响提现；<br />
                        ③ 通过实名认证后，账户才能进行提现操作。
                    </td>
                </tr>
                <tr>
                    <td colspan="3" height="45">
                    </td>
                </tr>
                <tr>
                    <td height="45" align="right" width="150">
                        真实姓名:
                    </td>
                    <td align="left" style="padding-left: 15px;">
                        <input id="txtpername" runat="server" type="text" class="txt_01" size="100" />
                    </td>
                    <td height="45" align="left">
                    </td>
                </tr>
                <tr id="tr_pernumber" runat="server">
                    <td height="45" align="right">
                        身份证号:
                    </td>
                    <td align="left" style="padding-left: 15px;">
                        <input id="txtpernumber" runat="server" type="text" class="txt_01" size="100" />
                        <span id="tip_pernumber"></span>
                    </td>
                    <td height="45" align="left">
                    </td>
                </tr>
                <tr id="tr_repernumber" runat="server">
                    <td height="45" align="right">
                        确认身份证号:
                    </td>
                    <td align="left"  style="padding-left: 15px;">
                        <input id="txtrpernumber" runat="server" type="text" class="txt_01" size="100" />
                        <span id="tip_rpernumber"></span>
                    </td>
                    <td height="45" align="left">
                        
                    </td>
                </tr>
                <tr id="tr1" runat="server">
                    <td height="45" align="right">
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn btn-primary" onclick="lbtnSave_Click" OnClientClick="return frmCheck();">确认提交</asp:LinkButton>
                        &nbsp;
                        <a id="rzqx" href="/usermodule/account/safety.aspx" target="mainframe" class="btn btn-primary"
                            runat="server">取消</a>&nbsp; &nbsp;<asp:Label ID="lblMessage" runat="server" Visible="False"
                                Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                    <td height="45" align="left">
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
