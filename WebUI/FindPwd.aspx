<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindPwd.aspx.cs" Inherits="viviAPI.WebUI7uka.FindPwd" %>

<%@ Register Src="head.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>
        <%=SiteName%>
        -
        <%=WebSiteTitleSuffix%></title>
    <meta name="Keywords" content="<%=KeyWords%>" />
    <meta name="Description" content="<%=Description%>" />
    <link type="image/x-icon" href="/style/index/images/favicon.ico" rel="shortcut icon" />
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
            background: url(images/page-top2.jpg) no-repeat;
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
    <uc1:header ID="header1" runat="server" showtype="news" />
    <div class="container">
        <div class="pageAll" style="width: 1000px; margin: 0px auto 50px; padding: 20px;">
            <div class="page-top">
            </div>
            <div class="mm-kb" style="text-align: left; padding-left: 150px; margin-top: -1px;">
                <div class="xxbt" style="line-height: 35px; font-size: 16px; font-weight: bold;">
                    <span class="yes font-14">找回密码</span></div>
                <ul class="c regbd">
                    <li style="height: 45px;">
                        <div style="float: left;">
                            <span class="text">用户名：</span><input id="newusername" runat="server" name="newusername"
                                type="text" maxlength="16" /></div>
                        <div style="float: left;">
                            请输入您您需要找回密码用户名
                        </div>
                    </li>
                    <li style="height: 45px;"><span class="text">验证码：</span><input id="txtCode" runat="server"
                        name="txtCode" type="text" class="yzm" />
                        <span class="yzm">
                            <img id="codeimg" align="absmiddle" src="/vercode.aspx" width="64" height="23" alt="点击刷新验证码"
                                onclick="this.src='/vercode.aspx?rnd='+ Math.random();" /></span></li>
                </ul>
                <p style="height: 55px;">
                    <asp:ImageButton ID="ibtnSubmit" ImageUrl="style/index/images/tj.png" runat="server"
                        OnClick="ibtnSubmit_Click" /></p>
                <p>
                    <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label></p>
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
