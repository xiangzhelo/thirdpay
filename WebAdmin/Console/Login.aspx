<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.web.Manage.Login"
    CodeBehind="login.aspx.cs" %>

<html>
<!--<![endif]-->
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta name="renderer" content="webkit">
    <title>登录-360个人中心</title>
    <meta name="Keywords" content="360个人中心">
    <meta name="Description" content="360个人中心">
    <!--公共的样式-->
    <link rel="stylesheet" href="/360/base.css">
    <!--页面自定义样式-->
    <link rel="stylesheet" href="/360/uc_login_reg.css">
    <script type="text/javascript" src="/360/jquery-1.9.1.min.js"></script>
    <script type="text/javascript">

        function checkform() {
            if (document.all.UserNameBox.value == "" || $('#UserNameBox').val == '输入您的用户名') {
                alert("请输入用户名")
                document.all.UserNameBox.focus();
                return false
            }
            if (document.all.pas.value == "" || $('#UserNameBox').val == '输入您的密码') {
                alert("请输入密码");
                document.all.pas.focus();
                return false;
            }
            if (document.all.CCode.value == "" || $('#UserNameBox').val == '请输入验证码') {
                alert("请输入验证码");
                document.all.CCode.focus();
                return false;
            }
            return true;
        }
        //重新生成验证码
        function ChangeMap(obj) {
            obj.src = "/vercode.aspx?code=" + Math.random();
        }
    </script>
</head>
<body>
<form  id="form1" runat="server" onsubmit="return checkform()">
    <div id="doc">
        <!--通用头部-->
        <div id="hd" class="clearfix" style="padding-top: 96px;">
            <div class="logo">
                <a href="/" hidefocus="true"></a>
            </div>
        </div>
        <!--内容部分-->
        <div class="info" style="margin-top: 132px;">
            
            <span><a target="_blank" href="http://www.360.cn">360首页</a></span> <span class="split">
                |</span> <span><a class="registerNew" href="#">注册新帐号</a></span>
        </div>
        <div id="quc-bd" class="quc-clearfix reg-wrapper11">
            <div class="content">
                
                <div id="loginWrap">
                    <div class="mod-qiuser-pop quc-qiuser-panel">
                        <div class="login-wrapper quc-wrapper quc-page">
                            <div class="quc-mod-sign-in quc-mod-normal-sign-in">
                                <div class="quc-tip-wrapper">
                                    <p class="quc-tip">
                                    </p>
                                </div>
                                <div class="quc-main">
                                    <%--<form class="quc-form">--%>
                                    <div class="quc-ipbox">
                                        <p class="quc-field quc-field-account quc-input-long botborder">
                                            <label class="quc-label" for="quc_account_461468378">
                                                帐号：</label><span class="quc-input-bg"><input class="quc-input quc-input-account"
                                                    type="text" runat=server name="account" placeholder="输入您的用户名" autocomplete="off" id="UserNameBox"></span></p>
                                        <p class="quc-field quc-field-password quc-input-long">
                                            <label class="quc-label" for="quc_password_461468379">
                                                密码：</label><span class="quc-input-bg"><input class="quc-input quc-input-password"
                                                    type="password" runat=server name="password" maxlength="20" placeholder="输入您的密码" id="pas"></span></p>
                                    </div>
                                    <p class="quc-field quc-field-captcha quc-input-short quc-clearfix" style="display:block;">
                                        <span class="quc-ipbox fl">
                                            
                                                    验证码：</label><span class="quc-input-bg quc-checkcode"><input class="quc-input quc-input-captcha"
                                                    type="text" name="CCode" maxlength="7" autocomplete="off" placeholder="请输入验证码"
                                                    id="CCode"></span></span>
                                                    
                                                    <img class="quc-captcha-img quc-captcha-change"
                                                        alt="验证码" title="点击更换" tabindex="99" id="codeimg" src="/vercode.aspx" onclick="ChangeMap(this)"></p>
                                    <p class="quc-field quc-field-keep-alive">
                                        <a class="quc-link quc-link-about quc-link-about-normal quc-findPwd" href="http://i.360.cn/findpwd/?account="
                                            target="_blank">忘记密码？</a><label><input class="quc-checkbox quc-checkbox-keep-alive"
                                                type="checkbox" name="iskeepalive" checked="checked">下次自动登录</label></p>
                                    <p class="quc-field quc-field-submit">
                                        <input type="submit" name="submit" value="登录" class="quc-submit quc-button quc-button-sign-in">
                                    </p>
                                    <p class="quc-field quc-field-third-part" style="display:none;">
                                        <span>其他帐号登录：</span><span class="quc-third-part"><a href="#" class="quc-third-part-icon quc-third-part-icon-sina"
                                            title="新浪微博登录"></a><a href="#" class="quc-third-part-icon quc-third-part-icon-renren"
                                                title="人人登录"></a><a href="#" class="quc-third-part-icon quc-third-part-icon-fetion"
                                                    title="飞信登录"></a><a href="#" class="quc-third-part-icon quc-third-part-icon-telecom"
                                                        title="天翼登录"></a></span></p>
                                    <%--</form>--%>
                                </div>
                                <%--<div class="quc-footer" style="">
                                    <a class="quc-link quc-link-grey quc-link-normal-sign-in" href="#">使用其他帐号登录</a><a
                                        class="quc-link quc-link-grey quc-link-quick-sign-in" href="#" style="display: none;">返回快速登录</a><a
                                            class="quc-link quc-link-grey quc-link-sign-up" href="#">注册新帐号</a><a class="quc-link quc-link-about quc-link-about-normal"
                                                href="http://i.360.cn/findpwd/" target="_blank">忘记密码？</a><a class="quc-link quc-link-about quc-link-about-mobile"
                                                    href="http://i.360.cn/help/smscode" target="_blank">校验码常见问题</a><a class="quc-link quc-link-about quc-link-about-qrcode"
                                                        href="#">什么是二维码登录？</a><a class="quc-link quc-link-about quc-link-about-onekey" href="#">什么是一键登录？</a></div>--%>
                            </div>
                        </div>
                    </div>
                    <div class="otherMessage clearfix">
                         </div>
                </div>
            </div>
        </div>
        <div id="ft">
            Copyright©2005-<span id="currentYear">2015</span> 360.CN All Rights Reserved 360安全中心</div>
    </div>
    <!--公共需要的全局变量-->
   
    <!--公共的js-->
    
    <%--<script type="text/javascript" src="/360/account.js"></script>
    <script type="text/javascript" src="/360/base.js"></script>
    <script type="text/javascript" src="/360/uc_login.js"></script>--%>
    
    </form>
</body>
</html>
