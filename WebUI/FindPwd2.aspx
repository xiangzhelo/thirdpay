<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindPwd2.aspx.cs" Inherits="viviAPI.WebUI7uka.FindPwd2" %>

<%@ Register Src="head.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>
        <%=SiteName%>
        - <%=WebSiteTitleSuffix%></title>
    <meta name="Keywords" content="<%=KeyWords%>" />
    <meta name="Description" content="<%=Description%>" />
    <!-- ͷ��β�� -->
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
        <div class="pageAll" style="width: 1000px; margin: 100px auto 50px; padding: 20px;">
            <div class="page-top">
            </div>
            <div class="mm-kb" style="text-align: left; padding-left: 150px; margin-top: -1px;">
                <p id="showmsg" runat="server" class="showno" visible="false">
                    ��֤���������</p>
                <p style="width: 2px">
                </p>
                <p>
                    ����Ҫ�һ�������ʻ����� <span style="color: #FF6600; font-size: 14px; font-weight: bold">
                        <asp:Literal ID="lituserName" runat="server"></asp:Literal></span> ����ѡ�����ѿ�ͨ�ķ�ʽ�һ����룺</p>
                <div class="getway">
                    <div class="back-form" name="back-form" id="back-form" method="get">
                        <p style="padding: 8px 0">
                            <asp:RadioButtonList ID="ddlfindmode" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="type1_SelectedIndexChanged">
                            </asp:RadioButtonList>
                        </p>
                        <p id="p1" runat="server" style="padding: 8px 0">
                            <label for="put1">
                                �ܱ����⣺</label><asp:Literal ID="litquestion" runat="server"></asp:Literal></p>
                        <p id="p2" runat="server" style="padding: 8px 0">
                            <label for="put1">
                                �ܱ��𰸣�</label><input type="text" class="input" name="put1" id="txtanswer1" runat="server" /></p>
                        <p id="p3" runat="server" style="padding: 8px 0">
                            <label for="put1">
                                �����ַ��</label><input type="text" class="input" name="put1" id="txtuseremail" runat="server" /></p>
                        <p id="p4" runat="server" style="padding: 8px 0">
                            <label for="put1">
                                �ֻ����룺</label><input type="text" class="input" name="put1" id="txtphone" runat="server" /></p>
                        <p class="b_m_t">
                            <label for="checkCode">
                                ��&nbsp;&nbsp;֤&nbsp;�룺</label>
                            <input class="input" name="checkCode" id="txtcheckCode" runat="server" type="text"
                                style="width: 100px" />
                            <img src="vercode.aspx" height="25" width="80" alt="���ˢ����֤��" style="cursor: pointer"
                                onclick="this.src='/vercode.aspx/?rnd='+ Math.random();" />
                        </p>
                        <asp:Button ID="btnbacksubmit" runat="server" Text="ȷ���ύ" CssClass="submitbg" OnClick="btnbacksubmit_Click" />
                        <span id="callinfo"></span>
                    </div>
                </div>
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
