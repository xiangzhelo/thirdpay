<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agentlogin.aspx.cs" Inherits="viviAPI.WebUI7uka.agentlogin" %>

<%@ Register Src="head.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <title>代理登陆_<%=SiteName%></title>
    <meta name="keywords" content="<%=KeyWords%>" />
    <meta name="description" content="<%=Description%>" />
    <script language="javascript">

        function refreshValidateCode(_id, url) {
            document.getElementById(_id).src = url + "?date=" + new Date();
        }


    </script>

    <!-- 头部尾部 -->
    <link rel="shortcut icon" href="ico/favicon.ico" />
    <link rel="stylesheet" href="css_demo/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="css_demo/index.css" />
    <link rel="stylesheet" href="web_js/easydialog.css" />

    <script language="JavaScript" type="text/javascript" src="web_js/jquery.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="web_js/common.min.js" async="false"></script>

    <!-- end -->
    <style type="text/css">
        .pageAll
        {
            width: 1000px;
            margin: 100px auto 50px;
            padding: 20px;
        }
        .page_right
        {
            border-left: 1px solid #DCDCDC;
            padding-left: 50px;
        }
        .loginform-right
        {
            float: none;
            border: none;
        }
        .loginleft
        {
            line-height: 24px;
            color: #666;
        }
        .login_b
        {
            margin-bottom: 8px;
            color: #444;
        }
        .page-top
        {
            background: url(images/page-top1.jpg) no-repeat;
            width: 1000px;
            height: 170px;
            margin: 0 auto;
        }
        .mm-kb
        {
            padding-left: 180px;
            background: url(images/page-middle.jpg) repeat-y;
            width: 1000px;
            padding-bottom: 100px;
        }
        .page-bottom
        {
            background: url(images/page-bottom.jpg) no-repeat;
            width: 1000px;
            height: 30px;
            margin: 0 auto;
        }
        .th
        {
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:header ID="header1" runat="server" showtype="index" />
    <div class="container">
        <div class="pageAll" style="width: 1000px; margin: 100px auto 50px; padding: 20px;">
            <div class="page-top">
            </div>
            <div class="mm-kb" style="text-align: left; padding-left: 150px; margin-top: -1px;">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr>
                        <td rowspan="6" width="280">
                            <img src="images/logleft.png" />
                        </td>
                        <td colspan="3" height="70">
                        </td>
                    </tr>
                    <tr>
                        <td width="128" align="right" height="50">
                            账 户 名：
                        </td>
                        <td colspan="3">
                            <input id="username" name="username" type="text" class="form-control" placeholder="请输入账户名" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="128" align="right" height="50">
                            登录密码：
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="password" runat="server" TextMode="Password" CssClass="form-control"
                                placeholder="请输入登陆密码"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="128" align="right" height="50">
                            验 证 码：
                        </td>
                        <td colspan="3" align="left">
                            <table border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <input id="imycode" name="imycode" type="text" style="width: 110px;" class="form-control"
                                            placeholder="请输入验证码" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;<img id='imgValidateCode' src="/vercode.aspx" width="67" height="23" />
                                    </td>
                                    <td class="partfont" style="font-size: 11px;">
                                        &nbsp;&nbsp;<a href="javascript:refreshValidateCode('imgValidateCode','/vercode.aspx');">换一张</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="128" align="right" height="25">
                        </td>
                        <td colspan="3" align="left">
                            <table border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <input id="ckbsavepass" name="ckbsavepass" type="checkbox" checked="checked" />默认记住账号
                                    </td>
                                    <td style="padding-left: 30px;">
                                        <a href="findpwd.aspx">忘记密码？</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="128">
                            &nbsp;
                        </td>
                        <td colspan="3" align="left">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/dl1.jpg" OnClick="ImageButton1_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="page-bottom" style="width: 1000px; height: 30px; margin: 0 auto;">
                <img src="images/page-bottom.jpg" />
            </div>
        </div>
    </div>
    <div class="line">
    </div>
    <uc1:footer ID="footer1" runat="server" />
    </form>
</body>
</html>
