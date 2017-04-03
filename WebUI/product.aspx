<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="product.aspx.cs" Inherits="viviAPI.WebUI2015.Product" %>

<%@ Register Src="head.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>亿付商务网服务项目介绍_<%=SiteName%>_点卡回收_API支付接口_游戏点卡回收_游戏支付平台接口提供商</title>
    <meta name="KeyWords" content="API,支付接口,点卡回收,游戏支付 <%=KeyWords%>" />
    <meta name="description" content="亿付商务网是全国领先的游戏点卡回收API接口提供商，拥有多条稳定的游戏支付回收接口渠道，包括盛大、骏网、网易、久游、腾讯Q币卡、完美、征途、金山、光宇等一卡通消耗渠道，与多家游戏平台合作，专业提供安全稳定高效的游戏API支付回收接口平台，是您运营网站及游戏的首要合作伙伴。<%=Description%>" />
    <!-- 头部尾部 -->
    <link rel="shortcut icon" href="ico/favicon.ico" />
    <link rel="stylesheet" href="css_demo/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="css_demo/index.css" />
    <link rel="stylesheet" href="web_js/easydialog.css" />

    <script language="JavaScript" type="text/javascript" src="web_js/jquery.min.js"></script>

    <script language="JavaScript" type="text/javascript" src="web_js/common.min.js" async="false"></script>

    <!-- end -->
</head>
<body>
    <form id="form1" runat="server">
    <uc1:header ID="header1" runat="server" showtype="product" />
    <div class="clear">
    </div>
    <div id="page">

        <script language="JavaScript" type="text/javascript" src="web_js/tooltip_ext.js"></script>

        <link type="text/css" rel="stylesheet" href="web_css/index.css" media="all">
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
                                <dd class="about-menu-current">
                                    <a href="/product.html">产品介绍</a></dd>
                            </li>
                            <li>
                                <dd>
                                    <a href="/product.html?#cptd">产品特点</a></dd>
                            </li>
                            <li>
                                <dd>
                                    <a href="/product.html?#ywkt">业务开通过流程</a></dd>
                            </li>
                        </ul>
                    </div>
                </div>
                <!--right scroll-->
                <div class="about-span2 y-last managebar">
                    <div class="aboutus" style="border: none;">
                        <p style="line-height: 35px; border: none;">
                            <div style="width: 684px; height: 124px; margin-bottom: 5px;">
                                <div style="width: 684px; height: 62px; background-image: url(/images/cpjs_02.jpg);">
                                </div>
                                <div style="width: 684px; height: 52px; background-color: #F6F6F6;">
                                    <div style="width: 10px; height: 52px; background-image: url(/images/cpjs_03.jpg);
                                        float: left;">
                                    </div>
                                    <div style="width: 674px; height: 52px; float: right;">
                                        <div style="width: 668px; height: 52px; background-color: #F6F6F6; float: left; text-align: left;
                                            line-height: 22px; color: #666;">
                                            &nbsp;“充值卡”收购是一种在线验证和接受用户提交的充值卡（手机充值卡、游戏充值卡），方便用户使用该种非银行卡消费方式进行网购消费或对亿付商务网账户充值。</div>
                                        <div style="width: 6px; height: 52px; background-image: url(/images/cpjs_04.jpg);
                                            float: right;">
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 684px; height: 10px; background-image: url(/images/cpjs_01.jpg);">
                                </div>
                            </div>
                            <div style="width: 684px; height: 552px; margin-bottom: 15px;">
                                <table style="width: 684; text-align: center">
                                    <tbody>
                                        <tr>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/sh.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/QQ.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/wm.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/wy.jpg">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;搜狐一卡通
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;腾讯QQ
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;完美一卡通
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;网易一卡通
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/sd.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/gy.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/jy.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/zt.jpg">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;盛大一卡通
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;光宇一卡通
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;久游一卡通
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;征途一卡通
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/lt.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/szx.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/szx.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/dx.jpg">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;联通充值卡
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;神州行全国卡
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;神州行地方卡
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;电信卡一卡通
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/jw.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/zy.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/TianHong.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;骏网一卡通
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;纵游一卡通
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;天宏一卡通
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </p>
                        <a name="cptd"></a>
                        <p style="line-height: 35px; border: none;">
                            <div style="display: block; width: 684px; height: 146px; margin-bottom: 21px;">
                                <div style="width: 684px; height: 61px; background-image: url(/images/cptd_01.jpg);">
                                </div>
                                <div style="width: 684px; height: 86px; background-color: #F6F6F6;">
                                    <div style="width: 17px; height: 86px; float: left; background-image: url(/images/cptd_02.jpg);">
                                    </div>
                                    <div style="width: 667px; height: 86px; float: right;">
                                        <div style="width: 656px; height: 86px; float: left; text-align: left; color: #666;
                                            line-height: 22px;">
                                            1）用户只需输入所持有的充值卡上标明的序列号和刮开的卡密码就可以完成兑换。无需输入银行卡卡号及密码，安全性高，不记名，不关联银行卡，一次性使用，尤其便于小额、频繁消费的方式；<br>
                                            2）亿付商务网可以提高充值卡付费功能，为商家吸引更多担心网银消费安全人群、无银行卡人群、小额付费频繁人群这类潜在的消费群体。</div>
                                        <div style="width: 11px; height: 86px; float: right; background-image: url(/images/cptd_03.jpg);">
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 684px; height: 10px; background-image: url(/images/cptd_04.jpg);">
                                </div>
                            </div>
                        </p>
                        <a name="ywkt"></a>
                        <p style="line-height: 35px; border: none;">
                            <div style="display: block; width: 684px; height: 193px;">
                                <div style="width: 684px; height: 61px; background-image: url(/images/ywkt_01.jpg);">
                                </div>
                                <div style="width: 684px; height: 86px; background-color: #F6F6F6;">
                                    <div style="width: 17px; height: 86px; float: left; background-image: url(/images/cptd_02.jpg);">
                                    </div>
                                    <div style="width: 667px; height: 86px; float: right;">
                                        <div style="width: 656px; height: 86px; float: left; text-align: left; color: #666;
                                            line-height: 22px;">
                                            1） 个人/企业注册请与商务人员联系，我们了解您的需求后，帮助您顺利签约并开通服务；<br>
                                            2）接口接入请您提供企业法人营业执照、组织机构代码证、法人身份证复印件各一份；<br>
                                            3）一对一的服务人员审核好您的资料后，与您签订合作协议；<br>
                                            4）合作协议签订日起1-5个工作日内，向您提供使用账号；<br>
                                        </div>
                                        <div style="width: 11px; height: 86px; float: right; background-image: url(/images/cptd_03.jpg);">
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 684px; height: 10px; background-image: url(/images/cptd_04.jpg);">
                                </div>
                            </div>
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
    <uc1:footer ID="footer1" runat="server" />
    </form>
</body>
</html>
