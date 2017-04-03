<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="viviAPI.WebUI2015.Contact" %>

<%@ Register Src="head.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>亿付商务网联系方式_点卡回收_API支付接口_游戏点卡回收_游戏支付平台接口提供商</title>
    <meta name="KeyWords" content="API,支付接口,点卡回收,游戏支付 <%=KeyWords%>" />
    <meta name="description" content="亿付商务网是全国领先的游戏点卡回收API接口提供商，拥有多条稳定的游戏支付回收接口渠道，包括盛大、骏网、网易、久游、腾讯Q币卡、完美、征途、金山、光宇等一卡通消耗渠道，与多家游戏平台合作，专业提供安全稳定高效的游戏API支付回收接口平台，是您运营网站及游戏的首要合作伙伴。<%=Description%>" />
    <link rel="shortcut icon" href="ico/favicon.ico" />
    <link rel="stylesheet" href="css_demo/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="css_demo/index.css" />
    <link rel="stylesheet" href="web_js/easydialog.css" />
    <script language="JavaScript" type="text/javascript" src="web_js/jquery.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="web_js/common.min.js" async="false"></script>
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
                <div class="about-span1" style="height: 500px;">
                    <div class="about-menu">
                        <ul>
                            <li>
                                <dd>
                                    <a href="/about.aspx">优卡介绍</a></dd>
                                <dd>
                                    <a href="/culture.aspx">联盟文化</a></dd>
                                <dd class="about-menu-current">
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
                                <img src="images/b.jpg" /></strong></p>
                    </div>
                    <br />
                    <div style="width: 100%;margin: 0 0 0 30px;">
                        <div style="margin: 3px;"><label>联系人：</label><span>姜先生</span></div>
                        <div style="margin: 3px;"><label>联系电话：</label><span>0592-6511723</span></div>
                        <div style="margin: 3px;"><label>QQ号码：</label><span>325915361</span></div>
                        <div style="margin: 3px;"><label>邮箱：</label><span>325915361@qq.com</span></div>
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
