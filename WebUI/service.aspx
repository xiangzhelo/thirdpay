<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="service.aspx.cs" Inherits="viviapi.web.Contact" %>

<%@ Register Src="head.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>亿付商务网客户服务中心_<%=SiteName%>_点卡回收_API支付接口_游戏点卡回收_游戏支付平台接口提供商</title>
    <meta name="KeyWords" content="API,支付接口,点卡回收,游戏支付 <%=KeyWords%>" />
    <meta name="description" content="亿付商务网是全国领先的游戏点卡回收API接口提供商，拥有多条稳定的游戏支付回收接口渠道，包括盛大、骏网、网易、久游、腾讯Q币卡、完美、征途、金山、光宇等一卡通消耗渠道，与多家游戏平台合作，专业提供安全稳定高效的游戏API支付回收接口平台，是您运营网站及游戏的首要合作伙伴。<%=Description%>" />
    <!-- 头部尾部 -->
    <link rel="shortcut icon" href="ico/favicon.ico">
    <link rel="stylesheet" href="css_demo/bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" href="css_demo/index.css">
    <link rel="stylesheet" href="web_js/easydialog.css" />

    <script language="JavaScript" type="text/javascript" src="web_js/jquery.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="web_js/common.min.js" async="false"></script>

    <!-- end -->
    <style>
        .qq_box
        {
            margin: 30px 0;
        }
        .qq_box_title
        {
            border-bottom: 1px dotted #E4E9F0;
            display: block;
            margin-bottom: 15px;
        }
        .qq_box_title span
        {
            border-bottom: 2px solid #999999;
            color: #585F69;
            display: inline-block;
            font-size: 20px;
            margin: 0 0 -2px;
            padding-bottom: 5px;
            line-height: 32px;
        }
        .qq_box_body
        {
            margin: 30px 0;
            background: #FAFAFA;
        }
        .qq_box_body ul
        {
            padding: 20px;
            padding-top: 0px;
            overflow: hidden;
        }
        .qq_box_body ul li
        {
            float: left;
            width: 155px;
            line-height: 38px;
        }
    </style>
    <link type="text/css" rel="stylesheet" href="web_css/index.css" media="all">

    <script language="JavaScript" type="text/javascript" src="web_js/tooltip_ext.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <uc1:header ID="header1" runat="server" showtype="contact" />
    <div class="clear">
    </div>
    <div id="page">
        <div class="wrapsemibox about-box-mm">
            <div class="semiboxshadow">
                <img alt="" class="semiboxshadow_bg" src="images/shp.png">
            </div>
            <!--中间部分开始-->
            <div class="w-1200">
                <!--left -->
                <div class="about-span1" style="height: 500px;">
                    <div class="about-menu">
                        <ul>
                            <li>
                                <dd>
                                    <a href="/about.aspx">平台介绍</a></dd>
                                <dd>
                                    <a href="/culture.aspx">平台文化</a></dd>
                                <dd>
                                    <a href="/contact.aspx">联系我们</a></dd>
                                <dd class="about-menu-current">
                                    <a href="/service.aspx">客服中心</a></dd>
                            </li>
                        </ul>
                    </div>
                </div>
                <!--right scroll-->
                <div class="about-span2 y-last managebar">
                    <div class="aboutus">
                        <p>
                            <strong>
                                <img src="images/d.jpg" /></strong></p>
                    </div>
                    <div class="qq_box">
                        <div class="qq_box_title">
                            <span>商务QQ</span></div>
                        <div class="qq_box_body">
                            <ul>
                                <li><span>商务:小穆</span> <a href="http://wpa.qq.com/msgrd?v=3&amp;uin=325915361&amp;site=qq&amp;menu=yes"
                                    target="_blank">
                                    <img width="72" height="22" border="0" title="点击与优卡商务交谈" alt="点击与优卡商务交谈" src="http://wpa.qq.com/pa?p=2:177688013:41">
                                </a></li>
                                <li><span>商务:阿祥</span> <a href="http://wpa.qq.com/msgrd?v=3&amp;uin=325915362&amp;site=qq&amp;menu=yes"
                                    target="_blank">
                                    <img width="72" height="22" border="0" title="点击与优卡商务交谈" alt="点击与优卡商务交谈" src="http://wpa.qq.com/pa?p=2:177688014:41">
                                </a></li>
                                <li><span>商务:小小</span> <a href="http://wpa.qq.com/msgrd?v=3&amp;uin=325915363&amp;site=qq&amp;menu=yes"
                                    target="_blank">
                                    <img width="72" height="22" border="0" title="点击与优卡商务交谈" alt="点击与优卡商务交谈" src="http://wpa.qq.com/pa?p=2:177688015:41">
                                </a></li>
                                <li><span>商务:小王</span> <a href="http://wpa.qq.com/msgrd?v=3&amp;uin=325915364&amp;site=qq&amp;menu=yes"
                                    target="_blank">
                                    <img width="72" height="22" border="0" title="点击与优卡商务交谈" alt="点击与优卡商务交谈" src="http://wpa.qq.com/pa?p=2:177688016:41">
                                </a></li>
                                <div class="clear">
                                </div>
                            </ul>
                        </div>
                    </div>
                    <div class="qq_box">
                        <div class="qq_box_title">
                            <span>技术QQ</span></div>
                        <div class="qq_box_body">
                            <ul>
                                <li><span>技术:低调</span> <a href="http://wpa.qq.com/msgrd?v=3&amp;uin=325915365&amp;site=qq&amp;menu=yes"
                                    target="_blank">
                                    <img width="72" height="22" border="0" title="点击与优卡技术交谈" alt="点击与优卡技术交谈" src="http://wpa.qq.com/pa?p=2:139070998:41">
                                </a></li>
                                <div class="clear">
                                </div>
                            </ul>
                        </div>
                    </div>
 
                </div>
            </div>
        </div>
        <div class="pagebottom">
        </div>
    </div>
    <div class="line">
    </div>
    <!--footer------------------------------->
    <uc1:footer ID="footer1" runat="server" showtype="news" />
    <!--footer end------------------------------->
    </form>
</body>
</html>
