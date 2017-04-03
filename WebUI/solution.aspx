<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="solution.aspx.cs" Inherits="viviAPI.WebUI2015.solution" %>

<%@ Register Src="head.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title><%=SiteName%>-行业解决方案</title>
    <meta name="Keywords" content="<%=KeyWords%>" />
    <meta name="Description" content="<%=Description%>" />
    <!-- 头部尾部 -->
    <link rel="shortcut icon" href="ico/favicon.ico" />
    <link rel="stylesheet" href="css_demo/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="css_demo/index.css" />
    <link rel="stylesheet" href="web_js/easydialog.css" />

    <script language="JavaScript" type="text/javascript" src="web_js/jquery.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="web_js/common.min.js"></script>

    <!-- end -->
</head>
<body>
    <form id="form1" runat="server">
    <uc1:header ID="header1" runat="server" showtype="solution" />
    <div class="clear">
    </div>
    <div id="page">

        <script language="JavaScript" type="text/javascript" src="web_js/tooltip_ext.js"></script>

        <link type="text/css" rel="stylesheet" href="web_css/index.css" media="all">
        <div class="wrapsemibox about-box-mm" style="margin-top:50px;">
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
                                    <a href="/solution.aspx">行业解决方案</a></dd>
                                <dd>
                                    <a href="/solution.aspx?#jkslt">接口示例图</a></dd>
                                <dd>
                                    <a href="/solution.aspx?#swhz">商务合作</a></dd>
                                <dd>
                                    <a href="/solution.aspx?#yqlj">友情链接</a></dd>
                            </li>
                        </ul>
                    </div>
                </div>
                <!--right scroll-->
                <div class="about-span2 y-last managebar">
                    <div class="aboutus">
                        <br />
                        <div style="display: block; width: 684px; height: 227px;">
                            <div style="width: 684px; height: 62px; background-image: url(/images/hyjjfa_01.jpg);">
                            </div>
                            <div style="width: 684px; height: 155px; background-color: #F6F6F6;">
                                <div style="width: 17px; height: 155px; float: left; background-image: url(/images/cptd_02.jpg);">
                                </div>
                                <div style="width: 667px; height: 155px; float: right;">
                                    <div style="width: 656px; height: 155px; float: left; text-align: left; color: #666;
                                        line-height: 22px;">
                                        &nbsp;&nbsp;&nbsp;网页游戏在近几年发展迅猛，2008年中国网页游戏的市场规模已经上亿元，预计在未来4到5年，还将继续保持20%以上的增幅，到目前为止还没有任何一种能够代替网游的新型娱乐形式出现，玩家对网游的需求如同一辆飞速开动着的列车。面对如此大的发展，网银在线消费销售模式太单一已经不能满足网游厂商及玩家的需求了。<br>
                                        &nbsp;&nbsp;&nbsp;亿付商务网采取“集成多种卡、统一调度、按时结算”的原则，API卡类接口是迅速帮助商家销售掉手上多余的点卡，减少压货，降低商家的资金压力跟风险性。为商家提供有力的多种充值卡付费方式，充值卡流通广，购卡即可充值，非常便捷，这种消费以后一定会实现必然性并且规模式发展。
                                    </div>
                                    <div style="width: 11px; height: 155px; float: right; background-image: url(/images/cptd_03.jpg);">
                                    </div>
                                </div>
                            </div>
                            <div style="width: 684px; height: 10px; background-image: url(/images/cptd_04.jpg);">
                            </div>
                        </div>
                        <div style="display: block; width: 684px; height: 347px;">
                            <div style="width: 400px; height: 327px; float: left; margin-top: 20px; text-align: left;
                                padding-left: 17px; font-family: ΢���ź�; font-size: 14px; color: #666; line-height: 28px;">
                                <span style="font-family: 微软雅黑; font-size: 18px; color: #000; font-weight: bold">&nbsp;客户需求</span><br>
                                解决充值渠道单一，提高运营效率；<br>
                                多种充值方式需求，覆盖更多玩家；<br>
                                系统智能化、人性化，财务对账方便；<br>
                                充值卡交易安全保障，玩家放心充值；<br>
                                T+1结算方式，资金回笼快；<br>
                                结算费用低，提升利润空间。<br>
                                <br>
                                <span style="font-family: 微软雅黑; font-size: 18px; color: #000; font-weight: bold">&nbsp;商业效果：（选择亿付商务网）
                                </span>
                                <br>
                                10秒极速消耗，提高运营效率，结算费率低，提升利润空间；<br>
                                多种渠道充值，满足玩家充值需求，帮助您留住更多忠实玩家。
                            </div>
                            <div style="width: 267px; height: 347px; float: right; background-image: url(/images/hyjjfa_007_02.jpg);">
                            </div>
                            <a name="jkslt"></a>
                        </div>
                        <br />
                        <p style="line-height: 35px; border: none;">
                            <div style="display: block; width: 684px; height: 130px;">
                                <div style="width: 684px; height: 61px; background-image: url(/images/hyjjfa_02.jpg);">
                                </div>
                                <div style="width: 684px; height: 56px; background-color: #F6F6F6;">
                                    <div style="width: 17px; height: 56px; float: left; background-image: url(/images/cptd_02.jpg);">
                                    </div>
                                    <div style="width: 667px; height: 56px; float: right;">
                                        <div style="width: 656px; height: 56px; float: left; text-align: left; color: #666;
                                            line-height: 22px;">
                                            例如游戏运营商新开发一款游戏，可以在临近的网吧、报刊、点卡专营店购买一张手机充值卡。
                                            <br />
                                            <span style="color: #F00">接口流程如下：</span>
                                        </div>
                                        <div style="width: 11px; height: 56px; float: right; background-image: url(/images/cptd_03.jpg);">
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 684px; height: 10px; background-image: url(/images/cptd_04.jpg);">
                                </div>
                            </div>
                            <div style="display: block; width: 684px; height: 180px; color: #065B82; margin-top: 15px;
                                text-align: left; line-height: 33px; font-size: 12px; font-weight: normal;">
                                1、玩家在游戏充值页面选择神州行充值卡充值渠道。<br />
                                2、充值卡号、卡密进入游戏运营商程序数据库。<br />
                                3、游戏运营商程序通过API接口将卡信息发送到亿付商务网处理程序。<br />
                                4、亿付商务网对卡进行处理后返回结果并进行结算给游戏运营商。<br />
                                5、游戏运营商程序对处理结果进行判断、处理发放物品。
                            </div>
                            <a name="swhz"></a>
                        </p>
                        <br />
                        <p style="line-height: 35px; border: none;">
                            <div style="display: block; width: 684px; height: 150px;">
                                <div style="width: 684px; height: 61px; background-image: url(/images/hyjjfa_03.jpg);">
                                </div>
                                <div style="width: 684px; height: 170px; background-color: #F6F6F6;">
                                    <div style="width: 17px; height: 170px; float: left; background-image: url(/images/cptd_02.jpg);">
                                    </div>
                                    <div style="width: 667px; height: 170px; float: right;">
                                        <div style="width: 656px; height: 170px; float: left; text-align: left; color: #666;
                                            line-height: 22px;">
                                            亿付商务网作为国内领先的充值卡收购平台，我们拥有巨大的经销商资源；同时拥有成熟的API接口技术（全自动化、人性化），愿与国内各大网页游戏研发公司及游戏运营商合作，将解决您的游戏充值渠道单一，覆盖更多玩家，提高运营效率，是您的利润最大化，共创双赢局面！
                                            <br />
                                            <span style="color: #ffa800; font-size: 14px; font-weight: bold;">如何申请开通接口接入：</span><span
                                                style="font-size: 14px; font-weight: bold;">（流程）</span>
                                        </div>
                                        <div style="width: 11px; height: 170px; float: right; background-image: url(/images/cptd_03.jpg);">
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 684px; height: 10px; background-image: url(/images/cptd_04.jpg);">
                                </div>
                            </div>
                            <div style="width: 651px; height: 62px; background-image: url(/images/hyjjfa_03_1.jpg);">
                                &nbsp;
                            </div>
                            <div style="display: block; width: 684px; height: 100px; color: #065B82; margin-top: 30px;
                                text-align: left; line-height: 33px; font-size: 12px; font-weight: normal;">
                                <span style="color: #ffa800; font-size: 14px; font-weight: bold;">充值卡渠道合作商 </span>
                                <br>
                                欢迎各种方式的一卡通公司与我公司洽谈合作，共同发展。<br>
                                邮箱：yfsw@ecs.59az.com <a name="yqlj"></a>
                            </div>
                            <br />
                            <p style="line-height: 35px; border: none;">
                                <div style="display: block; width: 684px; height: 300px;">
                                    <div style="width: 684px; height: 61px; background-image: url(/images/yq_01.jpg);">
                                    </div>
                                    <div style="width: 684px; height: 218px; background-color: #F6F6F6;">
                                        <div style="width: 17px; height: 218px; float: left; background-image: url(/images/cptd_02.jpg);">
                                        </div>
                                        <div style="width: 667px; height: 218px; float: right;">
                                            <div style="width: 656px; height: 218px; float: left; text-align: left; color: #666;
                                                line-height: 22px;">
                         <table border="0" width="100%">
                                <tr>
                                    <td colspan="4">以下排名不分先后顺序
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td style="width: 25%;">
                                        <a href="http://www.yeepay.com" target="_blank" style="font-size: medium; color: Gray;">易宝支付</a>
                                    </td>
                                    <td style="width: 25%;">
                                        <a href="http://www.baofoo.com" target="_blank" style="font-size: medium; color: Gray;">宝付</a>
                                    </td>
                                    <td style="width: 25%;">
                                        <a href="http://www.alipay.com" target="_blank" style="font-size: medium; color: Gray;">支付宝</a>
                                    </td>
                                    <td style="width: 25%;">
                                        <a href="http://www.tenpay.com" target="_blank" style="font-size: medium; color: Gray;">财富通 
                                        </a>
                                    </td>
                                </tr>
                            </table>                                            </div>
                                            <div style="width: 11px; height: 218px; float: right; background-image: url(/images/cptd_03.jpg);">
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
        <div class="pagebottom"></div>
    </div>
    <div class="line">
    </div>
    <uc1:footer ID="footer1" runat="server" />
    </form>
</body>
</html>
