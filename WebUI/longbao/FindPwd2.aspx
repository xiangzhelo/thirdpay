<%@ Page Title="" Language="C#" MasterPageFile="~/longbao/site.Master" AutoEventWireup="true" CodeBehind="FindPwd2.aspx.cs" Inherits="viviAPI.WebUI2015.longbao.FindPwd2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <link rel="stylesheet" href="/css_demo/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/css_demo/index.css" />
    <link rel="stylesheet" href="/web_js/easydialog.css" />

    <script language="JavaScript" type="text/javascript" src="/web_js/jquery.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="/web_js/common.min.js" async="false"></script>

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
            background: url(/images/page-top2.jpg) no-repeat;
            width: 1000px;
            height: 170px;
            margin: 0 auto;
        }
        .mm-kb
        {
            padding-left: 180px;
            background: url(/images/page-middle.jpg) repeat-y;
            width: 1000px;
            padding-bottom: 100px;
        }
        .page-bottom
        {
            background: url(/images/page-bottom.jpg) no-repeat;
            width: 1000px;
            height: 30px;
            margin: 0 auto;
        }
        .th
        {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <form runat="server" id="form1">
        <div class="container">
            <div class="pageAll" style="width: 1000px; margin: 100px auto 50px; padding: 20px;">
                <div class="page-top">
                </div>
                <div class="mm-kb" style="text-align: left; padding-left: 150px; margin-top: -1px;">
                    <p id="showmsg" runat="server" class="showno" visible="false">
                        验证码输入错误
                    </p>
                    <p style="width: 2px">
                    </p>
                    <p>
                        您需要找回密码的帐户名是 <span style="color: #FF6600; font-size: 14px; font-weight: bold">
                            <asp:Literal ID="lituserName" runat="server"></asp:Literal></span> ，请选择您已开通的方式找回密码：
                    </p>
                    <div class="getway">
                        <div class="back-form" name="back-form" id="back-form" method="get">
                            <p style="padding: 8px 0">
                                <asp:RadioButtonList ID="ddlfindmode" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="True" OnSelectedIndexChanged="type1_SelectedIndexChanged">
                                </asp:RadioButtonList>
                            </p>
                            <p id="p1" runat="server" style="padding: 8px 0">
                                <label for="put1">
                                    密保问题：</label><asp:Literal ID="litquestion" runat="server"></asp:Literal>
                            </p>
                            <p id="p2" runat="server" style="padding: 8px 0">
                                <label for="put1">
                                    密保答案：</label><input type="text" class="input" name="put1" id="txtanswer1" runat="server" />
                            </p>
                            <p id="p3" runat="server" style="padding: 8px 0">
                                <label for="put1">
                                    邮箱地址：</label><input type="text" class="input" name="put1" id="txtuseremail" runat="server" />
                            </p>
                            <p id="p4" runat="server" style="padding: 8px 0">
                                <label for="put1">
                                    手机号码：</label><input type="text" class="input" name="put1" id="txtphone" runat="server" />
                            </p>
                            <p class="b_m_t">
                                <label for="checkCode">
                                    验&nbsp;&nbsp;证&nbsp;码：</label>
                                <input class="input" name="checkCode" id="txtcheckCode" runat="server" type="text"
                                    style="width: 100px" />
                                <img src="vercode.aspx" height="25" width="80" alt="点击刷新验证码" style="cursor: pointer"
                                    onclick="this.src='/vercode.aspx/?rnd='+ Math.random();" />
                            </p>
                            <asp:Button ID="btnbacksubmit" runat="server" Text="确认提交" CssClass="submitbg" OnClick="btnbacksubmit_Click" />
                            <span id="callinfo"></span>
                        </div>
                    </div>
                </div>
                <div class="page-bottom" style="width: 1000px; height: 30px; margin: 0 auto;">
                    <img src="/images/page-bottom.jpg" />
                </div>
            </div>
        </div>
    </form>
</asp:Content>
