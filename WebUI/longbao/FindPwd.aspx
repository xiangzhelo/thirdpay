<%@ Page Title="" Language="C#" MasterPageFile="~/longbao/site.Master" AutoEventWireup="true" CodeBehind="FindPwd.aspx.cs" Inherits="viviAPI.WebUI2015.longbao.FindPwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <link rel="stylesheet" href="/css_demo/index.css" />
    <link rel="stylesheet" href="/web_js/easydialog.css" />

    <script language="JavaScript" type="text/javascript" src="/web_js/jquery.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="/web_js/common.min.js" async="false"></script>

    <!-- end -->
    <style type="text/css">
        .pageAll {
            width: 1000px;
            margin: 100px auto 50px;
            padding: 20px;
        }

        .page_right {
            border-left: 1px solid #DCDCDC;
            padding-left: 50px;
        }

        .loginform-right {
            float: none;
            border: none;
        }

        .loginleft {
            line-height: 24px;
            color: #666;
        }

        .login_b {
            margin-bottom: 8px;
            color: #444;
        }

        .page-top {
            background: url(/images/page-top2.jpg) no-repeat;
            width: 1000px;
            height: 170px;
            margin: 0 auto;
        }

        .mm-kb {
            padding-left: 180px;
            background: url(/images/page-middle.jpg) repeat-y;
            width: 1000px;
            padding-bottom: 100px;
        }

        .page-bottom {
            background: url(/images/page-bottom.jpg) no-repeat;
            width: 1000px;
            height: 30px;
            margin: 0 auto;
        }

        .th {
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
                    <div class="xxbt" style="line-height: 35px; font-size: 16px; font-weight: bold;">
                        <span class="yes font-14">找回密码</span>
                    </div>
                    <ul class="c regbd">
                        <li style="height: 45px;">
                            <div style="float: left;">
                                <span class="text">用户名：</span><input id="newusername" runat="server" name="newusername"
                                    type="text" maxlength="16" />
                            </div>
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
                        <asp:imagebutton id="ibtnSubmit" imageurl="/style/index/images/tj.png" runat="server"
                            onclick="ibtnSubmit_Click" />
                    </p>
                    <p>
                        <asp:label id="lblMessage" runat="server" visible="false"></asp:label>
                    </p>
                </div>
                <div class="page-bottom" style="width: 1000px; height: 30px; margin: 0 auto;">
                    <img src="/images/page-bottom.jpg" />
                </div>
            </div>
        </div>
    </form>
</asp:Content>
