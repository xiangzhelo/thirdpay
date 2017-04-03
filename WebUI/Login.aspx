<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="viviAPI.WebUI7uka.Login1" %>

<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>商户登陆_<%=SiteName%></title>
    <meta name="description" content="<%=Description%>">
    <meta name="keywords" content="<%=KeyWords%>">
    <link rel="shortcut icon" href="ico/favicon.ico" />
    <link rel="stylesheet" href="/itouzi/min.css" />
    <link rel="stylesheet" href="/itouzi/login_register.css" />
    <link href="/assets/src/css/reset.css?201506234" rel="stylesheet" type="text/css" />
    <link href="/assets/src/css/v6/home.css?201506234" rel="stylesheet" type="text/css" />
    <link href="/assets/src/css/v6/base.css?201506234" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.js"></script>
    <script type="text/javascript">
        function checkform() {
            if ($("#username").val == '' || $("#username").val == "用户名") {
                $("#errorContainer").html('<label class="error">用户名不能为空</label>');
                return false;
            }
            if ($('#password').val == "" || $('#password').val == "密码") {
                $('#errorContainer').html('<label class="error">密码不能为空</label>');
                return false;
            }
            if ($('#imycode').val == "" || $('#imycode').val == "验证码") {
                $('#errorContainer').html('<label class="error">验证码不能为空</label>');
                return false;
            }
            return true;
        }
        function refreshValidateCode(_id, url) {
            document.getElementById(_id).src = url + "?date=" + new Date();
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="page">
        <!--顶部开始-->
        <div class="ui-bg-gary-f1">
            <div class="container part-top-allnav">
                <div class="fn-left part-top-allnav-l-b">
                    <span class="ui-text-gray">欢迎致电</span>&nbsp;&nbsp;<span class="ui-text-red fn-font-18">400-083-3523</span>
                    <span class="fn-m1-5"><a href="" target="_blank" class="top-allnav-icon1">
                    </a></span><span class="fn-m1-5"><a href="#" class="top-allnav-icon2">
                        <div class="top-allnav-icon2-bd">
                            <img src="/trj/wechat-qrcode.jpg" alt="微信二维码" height="97" width="97">
                        </div>
                    </a></span>
                </div>
                <div class="fn-right">
                    <a href="" class="btn-invest-recruit" target="_blank"><i class="icon-recruit-add-a">
                    </i><i class="icon-recruit-add-b"></i>企业免费注册</a></div>
                <div class="fn-right part-top-allnav-list">
                    <ul>
                        <li class="part-top-allnav-link"><a href="">我的乐收卡</a></li>
                        <li class="part-top-allnav-link"><a href="">新手指导</a></li>
                        <li class="part-top-allnav-pr j-hover-all"><span class="part-icon-mobile-box"><i
                            class="part-icon-mobile"></i>手机版</span>
                            <div class="part-sn-dropdown-bd">
                                <div class="part-sn-mobile-appdowm">
                                    <a href="" target="_blank">下载手机客户端
                                        <img src="/trj/hot.gif"></a></div>
                                <div class="part-mobile-qrcode">
                                </div>
                                <p>
                                    扫描我，立刻打开触屏站<br>
                                    手机触屏站：<span class="ui-text-red"></span></p>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <!--顶部end-->
        <!--导航栏-->
        <div id="header" class="ui-bg-white">
            <div class="container">
                <h1 class="fn-left fn-mr-20 fn-mt-15">
                    <a href="/" title="">
                        <img src="/trj/logo.png" alt=""></a></h1>
                <div class="trjcn-title">
                    专业的支付API接口平台</div>
                <div class="fn-left fn-mt-5">
                </div>
                <nav class="fn-right part-nav-all-a fn-clear">
            <ul>
                <li class="current"><a href="" title="首页">首页</a></li>
                <li><a href="" title="">解决方案</a></li>
                <li><a href="" title="">接入服务</a></li>
                <li><a href="" title="">安全保障</a></li>
                <li><a href="" title="">点卡寄售</a></li>
                <li><a href="" title="">帮助中心</a></li>
            </ul>
        </nav>
            </div>
        </div>
        <!--导航栏end-->
    </div>
    <div class="login_register-con login-page" style="background: #abecf4 url(/itouzi/loginBanner2.jpg) center no-repeat;">
        <div class="login_register-inner">
            <div class="login-inner">
                <h2>
                    用户登录</h2>
                <div id="errorContainer" class="login-form-error clearfix" style="display: none">
                </div>
                <ul class="login-form clearfix">
                    <li><span class="login-form-username icon-login_register icon-ren"></span>
                        <input id="username" name="username" runat="server" type="text" autocomplete="off"
                            class="input-text-style-3" placeholder="用户名/手机号/邮箱" value="" />
                    </li>
                    <li><span class="login-form-password icon-login_register icon-suo"></span>
                        <input id="password" name="password" runat="server" type="password" autocomplete="off"
                            class="input-text-style-3" placeholder="密码" />
                    </li>
                    <li id="vcode" class="rcf-valicode" style="">
                        <input id="imycode" name="imycode" type="text" autocomplete="off" class="input-text-style-3"
                            placeholder="验证码" />
                        <span class="vcodeWrapper"><a href="javascript:refreshValidateCode('imgValidateCode','/vercode.aspx');">
                            <img title="点击获取验证码" id='imgValidateCode' src="/vercode.aspx" alt="点击获取验证码" width="100"
                                height="40" /></a></span> <span class="icon-refresh"></span></li>
                    <li class="login-form-checkbox"><span class="fl">
                        <label>
                            <input name="remember_me" type="checkbox" checked="checked" value="1" class="input-checkbox-style-3 fl" />
                            <span class="fl">记住用户名</span>
                        </label>
                    </span><span class="fr"><a href="/findpwd.aspx">忘记密码</a> | <a href="/reg.aspx" style="color: #e24d46;"
                        class="setRegLog" _reg_val="header">免费注册</a> </span></li>
                    <li>
                        <input id="Button1" onserverclick="Button1_Click" name="Button1" runat="server" type="submit" class="input-submit-style-3"
                            value="立即登录" />
                        <input type="hidden" name="ret_url" value="">
                    </li>
                </ul>
                <dl class="login-other clearfix">
                    <dt>使用合作账户登录：</dt>
                    <dd>
                        <a href="https://api.weibo.com/oauth2/authorize?client_id=24875680&redirect_uri=http%3A%2F%2Fwww.itouzi.com%2Fnewuser%2Findex%2FweiboOauthCallback&response_type=code"
                            target="_blank"><span class="icon-login_register icon-sina fl"></span><span>新浪微博</span>
                        </a>
                    </dd>
                    <dd>
                        <a href="https://graph.qq.com/oauth2.0/authorize?response_type=code&client_id=100450749&redirect_uri=http%3A%2F%2Fwww.itouzi.com%2Fnewuser%2Findex%2FqqOauthCallback&state=492431482b76db4882e5a8588324a4c3&scope=get_user_info,add_share,list_album,add_album,upload_pic,add_topic,add_one_blog,add_weibo,check_page_fans,add_t,add_pic_t,del_t,get_repost_list,get_info,get_other_info,get_fanslist,get_idolist,add_idol,del_idol,get_tenpay_addr"
                            target="_blank"><span class="icon-login_register icon-qq fl"></span><span class="fl">
                                腾讯QQ</span> </a>
                    </dd>
                </dl>
            </div>
        </div>
    </div>
    <div id="update_pw">
        <div class="newLogin-pop-title">
            <h2>
                提示</h2>
        </div>
        <p>
            抢红包时发送的随机登陆密码的短信存储在您的手机容易被泄露，并且为了便于记忆，建议您重置密码</p>
        <a href="https://www.itouzi.com/newuser/main/safe?curMod=pwd" class="input-submit-style-3 goResetPass">
            好的</a>
    </div>
    <div id="ft" class="footer-tmpl-1">
        <p>
            <a href="">关于我们</a> &nbsp;&nbsp;<span class="tint">|</span>&nbsp;&nbsp; <a href="http://www.itouzi.com/itzdefault/about/contact">
                联系我们</a> &nbsp;&nbsp;<span class="tint">|</span>&nbsp;&nbsp; <a href="http://www.itouzi.com/itzdefault/event/userguide">
                    新手指引</a> &nbsp;&nbsp;<span class="tint">|</span>&nbsp;&nbsp; <a href="http://www.itouzi.com/itzdefault/business/index">
                        业务模式</a> &nbsp;&nbsp;<span class="tint">|</span>&nbsp;&nbsp; <a href="http://www.itouzi.com/itzdefault/safe/index">
                            安全保障</a> &nbsp;&nbsp;<span class="tint">|</span>&nbsp;&nbsp; <a href="http://www.itouzi.com/itzdefault/about/media">
                                媒体报道</a> &nbsp;&nbsp;<span class="tint">|</span>&nbsp;&nbsp;
            <a href="http://www.itouzi.com/itzdefault/about/joinus">加入我们</a> &nbsp;&nbsp;<span
                class="tint">|</span>&nbsp;&nbsp; <a href="http://www.itouzi.com/itzdefault/help/index">
                    帮助中心</a>
        </p>
        <p>
            <strong class="ffA">© </strong>2015 爱投资 All rights reserved&nbsp;&nbsp;&nbsp;<span
                class="color-e6">|</span>&nbsp;&nbsp;&nbsp;安投融(北京)网络科技有限公司&nbsp;&nbsp;&nbsp;<span
                    class="color-e6">|</span>&nbsp;&nbsp;&nbsp;<a href="http://www.itouzi.com/icp" target="_blank">京ICP证150033号</a>
        </p>
    </div>
    </form>
</body>
</html>
