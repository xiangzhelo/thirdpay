<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="viviAPI.WebUI7uka.Userlogin" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta charset="gb2312" />
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <title>登陆</title>
    <link rel="stylesheet" href="css_demo/index.css">
    <link rel="stylesheet" href="css_demo/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="css_demo/bootstrap-dialog.css" />
    <link href="web_css/layout2.css" rel="stylesheet" type="text/css" media="all" />
    <link href="web_css/global2.css" rel="stylesheet" type="text/css" media="all" />
    <link rel="stylesheet" type="text/css" media="all" href="web_css/zi.css" />
    <link rel="stylesheet"  type="text/css" media="all" href="web_css/login.css" />

    <script language="JavaScript" type="text/javascript" src="web_js/jquery.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="web_js/common.min.js"></script>
    <script language="javascript">
        function refreshValidateCode(_id, url) {
            document.getElementById(_id).src = url + "?date=" + new Date();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: auto; text-align: center; width: 350px; margin-top: 20px;">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" align="left" height="240">
            <tr>
                <td width="100" align="right">
                    账 户 名：
                </td>
                <td colspan="3" align="left">
                    <input id="username" type="text" class="form-control" style="padding: 0px; padding-left: 5px;"
                        placeholder="请输入账户名" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="100" align="right">
                    登录密码：
                </td>
                <td colspan="3" align="left">
                    <asp:TextBox ID="password" runat="server" TextMode="Password" CssClass="form-control"
                        placeholder="请输入登陆密码" Style="padding: 0px; padding-left: 5px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="100" align="right">
                    验 证 码：
                </td>
                <td colspan="3" align="left">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <input id="imycode" name="imycode" type="text" style="width: 100px; padding: 0px;
                                    padding-left: 5px;" class="form-control" placeholder="请输入验证码" />
                            </td>
                            <td>
                                &nbsp;<img id='imgValidateCode' src="/vercode.aspx" width="67" height="23" />
                            </td>
                            <td class="partfont" style="font-size: 11px;">
                                &nbsp;<a href="javascript:refreshValidateCode('imgValidateCode','/vercode.aspx');">换一张</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" height="25">
                </td>
                <td colspan="3" align="left">
                    <table border="0" cellspacing="0" cellpadding="0" style="font-size: 14px;">
                        <tr>
                            <td>
                                <input id="ckbsavepass" name="ckbsavepass" type="checkbox" checked="checked" />记住登陆账号
                            </td>
                            <td style="padding-left: 30px;">
                                <a href="findpwd.html" target="_parent">忘记密码？</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="3" align="left">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/dl1.jpg" OnClick="ImageButton1_Click" />
                            </td>
                            <td class="partfont" style="font-size: 11px;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
