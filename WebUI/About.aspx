<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="viviAPI.WebUI7uka.About" %>
<%@ Register Src="head.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="footer" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>亿付商务网企业简介_游戏点卡回收_API消耗接口_游戏支付平台接口提供商</title>
    <meta name="KeyWords" content="API,支付接口,点卡回收,游戏支付 <%=KeyWords%>" />
    <meta name="description" content="亿付商务网是全国领先的游戏点卡回收API接口提供商，拥有多条稳定的游戏支付回收接口渠道，包括盛大、骏网、网易、久游、腾讯Q币卡、完美、征途、金山、光宇等一卡通消耗渠道，与多家游戏平台合作，专业提供安全稳定高效的游戏API支付回收接口平台，是您运营网站及游戏的首要合作伙伴。<%=Description%>" />
    <link rel="shortcut icon" href="ico/favicon.ico" />
    <link rel="stylesheet" href="css_demo/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="css_demo/index.css" />
    <link rel="stylesheet" href="web_js/easydialog.css" />
    <script language="JavaScript" type="text/javascript" src="web_js/jquery.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="web_js/common.min.js"></script>
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
            <div class="w-1200">
                <!--left -->
                <div class="about-span1" style="height: 500px;">
                    <div class="about-menu">
                        <ul>
                            <li>
                                <dd class="about-menu-current">
                                    <a href="/about.aspx">亿付简介</a></dd>
                                <dd>
                                    <a href="/culture.aspx">亿付文化</a></dd>
                                <dd>
                                    <a href="/contact.aspx">联系我们</a></dd>
                                <dd>
                                    <a href="/service.aspx">客服中心</a></dd>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="about-span2 y-last managebar">
                    <div class="aboutus">
                        <p>
                            <strong>
                                <img src="images/a.jpg" /></strong></p>
                        <p style="line-height: 35px; border: none;">
				<br>&nbsp;&nbsp;&nbsp;&nbsp;“亿付商务网”是国内领先的游戏点卡消耗平台，官方网址：www.59az.com。亿付商务网由济宁畅游网络科技有限公司于2013年10月创立至今，
				本着互惠互利，合作共赢的原则，为国内众多的游戏运营商及电子商务运营商延伸出多种安全、便捷、稳定的充值卡在线收费方式。
				<br>&nbsp;&nbsp;&nbsp;&nbsp;随着中国经济的快速发展和网络应用的不断成熟，电子商务产业已进入高速发展阶段。在电子商务领域，亿付商务网凭借创新而务实的风格、领先的技术、敏锐的市场预见力，将一直与各				大商家共享卡类API接口技术打造自身的品牌，不断根据客户的需求推出创新产品，为促进电子商务产业的持续发展做出不懈努力。
				<br>&nbsp;&nbsp;&nbsp;&nbsp;截至今日，亿付商务网已与中国移动、中国联通、中国电信、支付宝、财付通、环迅支付、腾讯、骏网、盛大、宝付、易宝等多家老牌支付及游戏企业建立了战略合作伙伴关系，
				并赢得了伙伴的高度认同。	
				<br>&nbsp;&nbsp;&nbsp;&nbsp;亿付商务网目前主要从事各类游戏点卡的回收与消耗业务，并致力于为广大电子商务运营上提供一个安全、稳定、快捷、便利的API点卡消耗接口平台。如果您是优质的游戏运营商，
				电子商务运营商，我们将以最竭诚的态度为您提供最优质的点卡消耗及回收服务。<br><br>
                            <span class="sp">——最大限度让利，实现双赢</span><br><br>
                            <span class="sp">——市场经验分享指导</span><br><br>
                            <span class="sp">——稳健发展，长期合作</span><br><br>
                            <span class="sp">——紧密协作，充分的服务支持</span><br> <br>
                        </p>
                    </div>
                </div>
            </div>
            <!--中间部分结束-->
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
