<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="viviAPI.WebUI7uka.Index" %>

<%--<%@ Register Src="head.ascx" TagName="head" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="foot" TagPrefix="uc1" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8">
    <title>专业的融资服务交易平台－亿付商务网</title>
    <meta name="Keywords" content="" />
    <meta name="Description" content="" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="renderer" content="webkit">
    <%--<script type="text/javascript" src="/assets/src/lib/jquery_1.10.2.js"></script>
    <script type="text/javascript" src="/trj/jquery.SuperSlide.2.1.1.js"></script>--%>
    <link href="/assets/src/css/reset.css?201506234" rel="stylesheet" type="text/css" />
    <link href="/assets/src/css/v6/home.css?201506234" rel="stylesheet" type="text/css" />
    <link href="/assets/src/css/v6/base.css?201506234" rel="stylesheet" type="text/css" />
    <link href="/longbao/style/css/site.css" rel="stylesheet">
    <!--环迅代码-->
    <style type="text/css">
        @import url(http://www.ips.com.cn/r/cms/www/ips/css/style.css);
    </style>
    <script type="text/javascript" src="/assets/src/lib/jquery_1.10.2.js"></script>
    <script type="text/javascript" src="/longbao/style/js/site.js"></script>
</head>
<body>
    <%--  <uc1:head ID="header1" runat="server" showtype="news" />--%>
    <div style="background: white;">
        <div id="page">
            <!--顶部开始-->
            <div class="ui-bg-gary-f1">
                <div class="container part-top-allnav">
                    <div class="fn-left part-top-allnav-l-b">
                        <span class="ui-text-gray">欢迎致电</span>&nbsp;&nbsp;<span class="ui-text-red fn-font-18">0592-6511723</span>
                    </div>
                    <div class="fn-right"></div>
                    <div class="fn-right part-top-allnav-list">
                    </div>
                </div>
            </div>
            <!--顶部end-->
            <!--导航栏-->
            <div id="header" class="ui-bg-white">
                <div class="container">
                    <h1 class="fn-left fn-mr-20 fn-mt-15">
                        <a href="/" title="">
                            <img src="/style/images/logo.png" alt="" style="width: 160px;"></a></h1>
                    <div class="trjcn-title">
                        专业的支付API接口平台</div>
                    <div class="fn-left fn-mt-5">
                    </div>
                    <nav class="fn-right part-nav-all-a fn-clear">
            <ul>
                <li class="current"><a href="/" title="首页">首页</a></li>
                <li><a href="/solution.aspx" title="行业解决方案">行业解决方案</a></li>
                <li><a href="/Access.aspx" title="商业服务号">商业服务号</a></li>
                <li><a href="/news.aspx" title="新闻中心">新闻中心</a></li>
                <li><a href="/service.aspx" title="客服中心">客服中心</a></li>
                <!--<li><a href="" title="">安全保障</a></li>
                <li><a href="" title="">点卡寄售</a></li>-->
                <li><a href="/About.aspx" title="关于我们">关于我们</a></li>
            </ul>
        </nav>
                </div>
            </div>
            <!--导航栏end-->
            <div id="content">
                <div id="focus">
                    
                    <div class="header" id ="lunbo" style="height:auto;padding-top:0;">
                        <div class="container">
                                <div class="login" id="login" style="position:absolute;top: 50px;right: 0;">
                                    <div class="loginbg"></div>
                                    <div class="tabs">
                                        <a href="javascript:;" class="selected">账号登录</a>
                                       <%--  <b>|</b>
                                        <a href="javascript:;">微信登录</a>--%>
                                    </div>
                                    <div class="loginnr">
                                        <form name="form1" method="post" action="/longbao/index.aspx?m=login" id="form1">
                                        <div><input type="text" name="username" id="username" placeholder="请输入用户名" /></div>
                                        <div><input type="password" name="password" placeholder="请输入密码" /></div>
                                        <div class="yzm cl"><input type="text" name="imycode" id="yanzm" placeholder="请输入图形码" /><span><img alt="换一个" title="换一个" src="/vercode.aspx" onclick="this.src = '/vercode.aspx?t='+ Math.round().toString()" /></span></div>
                                        <div class="cl"><a href="FindPwd.aspx">忘记密码？</a><a href="Regedit.aspx">立即注册</a></div>
                                        <div><input type="submit" value="" class="loginbtn" /></div>
                                        </form>
                                    </div>
                                    <%--  <div class="loginnr ewm" style="display:none;">
                                        <img src="/longbao/style/images/ewm.png" />
                                        <span>请使用微信扫描二维码登录</span>
                                    </div>--%>
                                </div>
                        
                            <span class="prev" style="opacity: 0; display: inline;"></span><span class="next"
                                style="opacity: 0; display: inline;"></span>
                        </div>
                        <div class="focus-images">
                            <ul style="position: relative; width: 1903px; height: 400px;">
                                <li style="position: absolute; width: 1903px; left: 0px; top: 0px; display: none;
                                    background: url(/trj/401_1440669373.jpg) 50% 0px no-repeat rgb(238, 238, 238);">
                                    <a target="_blank" href="" rel="nofollow"></a></li>
                                <li style="position: absolute; width: 1903px; left: 0px; top: 0px; display: none;
                                    background: url(/trj/401_1440665392.jpg) 50% 0px no-repeat rgb(238, 238, 238);">
                                    <a target="_blank" href="" rel="nofollow"></a></li>
                                <li style="position: absolute; width: 1903px; left: 0px; top: 0px; display: none;
                                    background: url(/trj/401_1439956228.jpg) 50% 0px no-repeat rgb(238, 238, 238);">
                                    <a target="_blank" href="" rel="nofollow"></a></li>
                                <li style="position: absolute; width: 1903px; left: 0px; top: 0px; display: list-item;
                                    opacity: 0.0441; background: url(/trj/401_1438827044.jpg) 50% 0px no-repeat rgb(238, 238, 238);">
                                    <a target="_blank" href="" rel="nofollow"></a></li>
                                <li style="position: absolute; width: 1903px; left: 0px; top: 0px; display: list-item;
                                    opacity: 0.9559; background: url(/trj/401_1437729603.jpg) 50% 0px no-repeat rgb(238, 238, 238);">
                                    <a target="_blank" href="" rel="nofollow"></a></li>
                            </ul>
                        </div>
                        <div class="focus-nav">
                            <ul>
                                <li class="">1</li><li class="">2</li><li class="">3</li><li class="">4</li><li class="on">
                                    5</li></ul>
                        </div>
                    
                    </div>
                </div>
            </div>
        </div>
        <!--底部区域开始-->
        <!--方案开始-->
        <div class="fangan">
            <div class="hangye">
                <ul>
                    <a href="solution.aspx">
                        <img src="/ips/04113200pjq2.jpg" width="96" height="96" /></a>
                    <h4>
                        <a href="solution.aspx">行业应用方案</a></h4>
                    <li class="hafont"><a href="solution.aspx">方案量身定制，专业支付贴心安全</a></li>
                </ul>
                <ul class="yebo">
                    <li><a href='/solution.aspx?#jkslt'>接口示例</a></li>
                    <li><a href='/solution.aspx?#swhz'>商务合作</a></li>
                    <li><a href='/solution.aspx?#yqlj'>友情链接</a></li>
                </ul>
            </div>
            <div class="yert">
                <ul>
                    <a href="product.shtml">
                        <img src="/ips/04113214r3h7.jpg" width="96" height="96" /></a>
                    <h4>
                        <a href="/Access.aspx">标准支付服务</a></h4>
                    <li class="yefont"><a href="/Access.aspx">帮助企业实现在线消费和支付功能</a></li>
                </ul>
                <ul class="yebo">
                    <li><a href='/Access.aspx#3'>网银支付</a></li>
                    <li><a href='/Access.aspx#1'>支付宝</a></li>
                    <li><a href='/Access.aspx'>微信支付</a></li>
                </ul>
            </div>
            <div class="yert">
                <ul>
                    <a href="/Access.aspx#5">
                        <img src="/ips/04113224ncc5.jpg" width="96" height="96" /></a>
                    <h4>
                        <a href="/Access.aspx#5">商户接入</a></h4>
                    <li class="yefont"><a href="/Access.aspx#5">优势-结算灵活、开通快捷、费率优惠</a></li>
                </ul>
                <ul class="yebo">
                    <li><a href='/Access.aspx#5'>结算灵活</a></li>
                    <li><a href='/Access.aspx#6'>开通快捷</a></li>
                    <li><a href='/Access.aspx#9'>费率优惠</a></li>
                </ul>
            </div>
            <div class="border">
            </div>
        </div>
        <!--方案结束-->
        <!--新闻自助活动开始-->
        <div class="clear">
        </div>
        <div class="new">
            <ul class="news">
                <h4>
                    <a href="/news.aspx">新闻动态</a></h4>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <li><a href="/view.aspx?newsid=<%#Eval("newsid")%>">
                            <%#Eval("newstitle")%></a></li>
                    </ItemTemplate>
                </asp:Repeater>
                <%--<li><a href="/newsdt/20150526/104270.html">环迅支付应邀参加特许加盟展，助企业插...</a></li>
                <li><a href="/newsdt/20150415/104116.html">环迅支付：助力中国连锁业加速互联网+...</a></li>
                <li><a href="/newsdt/20150130/103895.html">环迅支付荣膺“互联网金融领军品牌”</a></li>
                <li><a href="/newsdt/20141231/103867.html">国内第三方支付的四大转型方向</a></li>--%>
            </ul>
            <ul class="help">
                <h4>
                    <a href="/about.aspx">选择我们 没有错</a></h4>
                <li><a href="/service.aspx"><font class="tp01"></font></a><font><a href="/service.aspx">
                    7×24小时贴心客服</a></font></li>
                <li><a href="/solution.aspx?#jkslt"><font class="tp02"></font></a><font><a
                    href="/solution.aspx?#jkslt">自动快捷对账服务</a></font></li>
                <li><a href="/Access.aspx#7"><font class="tp03"></font></a><font><a href="">贴心服务</a></font></li>
            </ul>
            <div class="module">
                <div class="activity fr">
                    <span class="title">
                        <h4>
                            <a href="">近期活动</a></h4>
                        <span class="fr num"><a href="#"></a><a href="#"></a></span></span>
                    <ul class="slide" id="slideIMG">
                        <li><a href="">
                            <img src="/ips/27095849lktn.jpg" width="280" height="100" /></a></li>
                        <li><a href="">
                            <img src="/ips/20150427etsm.jpg" width="280" height="100" /></a></li>
                    </ul>
                </div>
            </div>
        </div>
        <!--新闻自助活动结束-->
        <!--friend_link_begin-->
        <div class="clear">
        </div>
        <!--底部开始-->
        <div class="foot">
            <div class="footnav">
                <ul class="bank">
                    <h1>
                        <a href="">合作银行</a></h1>
                    <li><a href="" target="_blank">
                        <img src="/ips/07093830x06z.jpg" alt="bk2" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/07093848p8g3.jpg" alt="bk3" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/070938593eq0.jpg" alt="bk4" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/070939134jac.jpg" alt="bk5" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/07093923gzip.jpg" alt="bk6" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/070939330fmg.jpg" alt="bk7" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/07093945yt62.jpg" alt="bk8" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/070940022xrd.jpg" alt="bk9" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/07094038fy3u.jpg" alt="bk10" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/07094049xbag.jpg" alt="bk11" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/070941251s7v.jpg" alt="bk14" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/07094142zcn0.jpg" alt="bk15" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/070942046k0b.jpg" alt="bk17" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/070942141i8t.jpg" alt="bk18" /></a></li>
                    <li><a href="organizations/index.shtml" target="_blank">
                        <img src="/ips/07094224dxj9.jpg" alt="bk19" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/07094248jdkc.jpg" alt="bk20" /></a></li>
                </ul>
                <ul class="vipspec">
                    <h1>
                        <a href="">合作商户</a></h1>
                    <li><a href="" target="_blank">
                        <img src="/ips/191639423wcv.jpg" alt="东华大学" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/19164017pr6t.jpg" alt="ATA" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/19164059qtpa.jpg" alt="1号店" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/191643382xon.jpg" alt="格瓦拉" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/191644392lmj.jpg" alt="趙涌在线" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/19164527z5wh.jpg" alt="途牛旅行网" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/191646105180.jpg" alt="都市丽人" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/19164648d9q4.jpg" alt="海南航空" /></a></li>
                </ul>
                <ul class="accr">
                    <h1>
                        <a href="">资质认证</a></h1>
                    <li><a href="" target="_blank">
                        <img src="/ips/131735006nhg.png" alt="visa" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/10081820n8tm.jpg" alt="gs1" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/10081841u1mt.jpg" alt="gs2" /></a></li>
                    <li><a href="/" target="_blank">
                        <img src="/ips/10081900j538.jpg" alt="gs3" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/100820570ids.jpg" alt="gs4" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/010113251dfb.jpg" alt="gs5" /></a></li>
                    <li><a href="/"
                        target="_blank">
                        <img src="/ips/1008194400hx.jpg" alt="gs6" /></a></li>
                    <li><a href="/"
                        target="_blank">
                        <img src="/ips/10082000x4x4.jpg" alt="gs7" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/10082018xyku.jpg" alt="gs8" /></a></li>
                    <li><a href="" target="_blank">
                        <img src="/ips/10082128pazh.jpg" alt="gs9" /></a></li>
                </ul>
            </div>
            <div class="clear">
            </div>
            <!--friend_link_end-->
            <!--footer_begin-->
            ﻿<!-- footer -->
            <!--底部-->
            <div class="us">
                <ul class="usnav">
                    <li><a href="/About.aspx">关于我们</a><font>|</font><a href="/news.aspx">新闻中心</a><font>|</font><a
                        href="/contact.aspx">联系我们</a><font>|</font><a href="http://www.baidu.com/">友情链接</a></li>
                    <li>Copyright 2000 - 2015 IPS. All Rights Reserved 亿付商务网 版权所有</li>
                    <li>&nbsp; ICP证：闽ICP备14011788号-2</li>
                </ul>
            </div>
        </div>
        <!--底部-->
        <!-- footer -->
        <!--footer_end-->
        <script type="text/javascript" src="/assets/src/lib/sea.js"></script>
        <script type="text/javascript">            seajs.use("page/v6/home")</script>
    </div>
</body>
</html>
