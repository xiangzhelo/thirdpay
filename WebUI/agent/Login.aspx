<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebUI7uka.agent.Login"
    CodeBehind="login.aspx.cs" %>

<html>
<head>
    <title>商家后台管理登录</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta http-equiv="pragma" content="no-cache">
    <meta http-equiv="cache-control" content="no-cache">
    <meta http-equiv="expires" content="0">
    <meta charset="utf-8">
    <link href="/assets/src/css/reset.css?201506234" rel="stylesheet" type="text/css" />
    <link href="/assets/src/css/v6/home.css?201506234" rel="stylesheet" type="text/css" />
    <link href="/assets/src/css/v6/base.css?201506234" rel="stylesheet" type="text/css" />
    <link href="/guomei/newcps.css" rel="stylesheet">
    <script language="javascript" src="/guomei/jquery-1.7.1.min.js"></script>
    <style type="text/css">
        .imagesStyle
        {
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">

        function checkform() {
            if (document.all.UserNameBox.value == "") {
                alert("请输入用户名")
                document.all.UserNameBox.focus();
                return false
            }
            if (document.all.pas.value == "") {
                alert("请输入密码");
                document.all.pas.focus();
                return false
            }
            if (document.all.CCode.value == "") {
                alert("请输入验证码");
                document.all.CCode.focus();
                return false
            }
            return true
        }
        //重新生成验证码
        function ChangeMap(obj) {
            obj.src = "/Vercode.aspx?code=" + Math.random();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return checkform()">
    <div id="page">
        <!--顶部开始-->
        <div class="ui-bg-gary-f1">
            <div class="container part-top-allnav">
                <div class="fn-left part-top-allnav-l-b">
                    <span class="ui-text-gray">欢迎致电</span>&nbsp;&nbsp;<span class="ui-text-red fn-font-18">400-858-9000</span>
                    <span class="fn-m1-5"><a href="http://e.weibo.com/trjcn1" target="_blank" class="top-allnav-icon1">
                    </a></span><span class="fn-m1-5"><a href="#" class="top-allnav-icon2">
                        <div class="top-allnav-icon2-bd">
                            <img src="/trj/wechat-qrcode.jpg" alt="微信二维码" height="97" width="97">
                        </div>
                    </a></span>
                </div>
                <div class="fn-right">
                    <a href="http://www.trjcn.com.cn/capital/zjrz.html" class="btn-invest-recruit" target="_blank">
                        <i class="icon-recruit-add-a"></i><i class="icon-recruit-add-b"></i>资本机构免费入驻</a></div>
                <div class="fn-right part-top-allnav-list">
                    <ul>
                        <li class="part-top-allnav-link"><a href="http://www.trjcn.com.cn/manage/home.html">
                            我的</a></li>
                        <li class="part-top-allnav-link"><a href="http://www.trjcn.com.cn/help.html">新手指导</a></li>
                        <li class="part-top-allnav-pr j-hover-all"><span class="part-icon-mobile-box"><i
                            class="part-icon-mobile"></i>手机</span>
                            <div class="part-sn-dropdown-bd">
                                <div class="part-sn-mobile-appdowm">
                                    <a href="http://www.trjcn.com.cn/zhuanti/webapp/index.html" target="_blank">下载手机客户端
                                        <img src="/trj/hot.gif"></a></div>
                                <div class="part-mobile-qrcode">
                                </div>
                                <p>
                                    扫描我，立刻打开触屏站<br>
                                    手机触屏站：<span class="ui-text-red">m.trjcn.com</span></p>
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
                    <a href="http://www.trjcn.com.cn" title="">
                        <img src="/trj/logo.png" alt=""></a></h1>
                <div class="trjcn-title">
                    专业的融资服务交易平台</div>
                <div class="fn-left fn-mt-5">
                </div>
                <nav class="fn-right part-nav-all-a fn-clear">
            <ul>
                <li class="current"><a href="http://www.trjcn.com.cn" title="首页">首页</a></li>
                <li><a href="http://zijin.trjcn.com.cn" title="找资金">找资金</a></li>
                <li><a href="http://xiangmu.trjcn.com.cn" title="选项目">选项目</a></li>
                <li><a href="http://huodong.trjcn.com.cn" title="活动">活动</a></li>
                <li><a href="http://zhiku.trjcn.com.cn" title="投融学院">投融学院</a></li>
                <li><a href="http://www.trjcn.com.cn/service.html" title="投融服务">投融服务</a></li>
            </ul>
        </nav>
            </div>
        </div>
        <!--导航栏end-->
    </div>
    <div class="wrap-990">
        <div class="main-login clearfix">
            <div class="left-login">
                <div class="img-cont">
                    <img width="550" height="330" id="reImage" src="/guomei/login_r.jpg" />
                </div>
            </div>
            <div class="right-login">
                <h3>
                    代理商后台管理系统</h3>
                <!-- 广告平台跳转登录 -->
                <input type="hidden" name="returnUrl" value="" />
                <div class="inpl-wrap">
                    <input class="inp-text" type="text" name="username" runat="server" id="username" placeholder="用户名"
                        value="" />
                    <p class="inp-notice" style="display: none" id="usernamenotice">
                    </p>
                </div>
                <div class="inpl-wrap">
                    <input class="inp-text" name="password" id="password" runat="server" type="password" placeholder="密码"
                        value="" />
                    <p class="inp-notice" id="passwordnotice" style="display: none">
                    </p>
                </div>
                <div style="" id="verifyCodeContainer" class="inpl-wrap">
                    <input class="inp-text vcode" type="text" name="CCode" id="CCode" placeholder="验证码" />
                    
                    <img alt="验证码" title="点击更换" id="codeimg" src="/Vercode.aspx" onclick="ChangeMap(this)">
                    <p class="inp-notice" style="display: none" id="verifyCodenotice">
                    </p>
                </div>
                <div style="display: none" id="getPhonecodeContainer" class="inpl-wrap f-text">
                    <span class="reg-tel" name="mobileDisplay" id="mobileDisplay"></span><a href="javascript:void(0);"
                        class="btn-vcode-m" id="btnGetCode">获取验证码</a>
                </div>
                <div style="display: none" id="pohonecodeContainer" class="inpl-wrap">
                    <input class="inp-text" type="text" name="messageVerifyCode" id="messageVerifyCode"
                        placeholder="短信密码" />
                    <p class="inp-notice" style="display: none" id="messageVerifyCodenotice">
                    </p>
                </div>
                <a href="javascript:void(0);" style="height: 24px;" class="btn btn-login" id="BtnOk"
                    runat="server" onclick="javascript:document.getElementById('form1').submit()">登
                    录</a>
            </div>
        </div>
        <input type="hidden" name="verifyCodeError" id="verifyCodeError" value="" />
        <input type="hidden" name="messageCodeError" id="messageCodeError" value="" />
        <input type="hidden" name="passwordError" id="passwordError" value="" />
        <input type="hidden" name="closeBrowserError" id="closeBrowserError" value="" />
        <input type="hidden" name="noRoleError" id="noRoleError" value="" />
        <div class="footer footer-login">
            <p>
                沪ICP备11009419号 | Copyright © 2000-2012 All Rights Reserved 国美电器有限公司版权所有</p>
        </div>
    </div>
    <script language="javascript" src="/guomei/command.js"></script>
    <script language="javascript" src="/guomei/verify.js"></script>
    </form>
</body>
</html>
