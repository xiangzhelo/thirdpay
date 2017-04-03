﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoEmailAuthen.aspx.cs" Inherits="viviAPI.WebUI7uka.agentmodule.safety.DoEmailAuthen" Async="true" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/usermodule/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/page.css" />

    <script src="/usermodule/static/js/lib/jquery-1.4.2.js" type="text/javascript"></script>

    <script type="text/javascript">
        jQuery(document).ready(function() {
            if ($("#IsEmail").val() == "0")
                $("#oldemail").fadeOut();
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="IsEmail" runat="server" value="0" type="hidden" />
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/usermodule/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/usermodule/safety/index.aspx'">我的账户</a>
        &nbsp;&gt;&nbsp; <span>修改邮箱</span>
    </div>
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title" style="color: Black; line-height: 45px;">
            <h2>邮箱认证</h2>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td colspan="3" height="45">
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" width="150" style="border: none;">
                    确认邮箱:
                </td>
                <td align="left" class="line_01" align="left" style="border: none; padding-left: 15px;">
                    <input id="txtnewemail" runat="server" type="text" class="txt_01" size="100" />
                </td>
                <td height="45" align="left" class="line_01" style="border: none;">
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" style="border: none;">
                </td>
                <td align="left" class="line_01" align="left" style="border: none; padding-left: 15px;">
                    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn btn-primary" onclick="lbtnSave_Click">提交认证</asp:LinkButton>&nbsp;
                    <a href="/usermodule/account/safety.aspx" target="mainframe" class="btn btn-primary">
                        取消</a>
                     <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
                <td height="45" align="left" class="line_01">
                    
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
