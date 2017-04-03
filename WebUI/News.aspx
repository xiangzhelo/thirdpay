<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="viviAPI.WebUI7uka.News" %>
<%@ Register Src="head.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="footer" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title><%=SiteName%> - 新闻中心 </title>
    <meta name="KeyWords" content="<%=KeyWords%>" />
    <meta name="description" content="<%=Description%>" />
    <link type="image/x-icon" href="favicon.ico" rel="shortcut icon">
    <script type="text/javascript" src="/style/index/images/js/jquery.min.js"></script>

    <style type="text/css">
        /*频道通用*/#pd
        {
            background-image: url(/style/index/images/pdbj.gif);
            height: 101px;
            margin-bottom: 15px;
        }
        .pdrr
        {
            height: 101px;
            width: 930px;
            margin-right: auto;
            margin-left: auto;
            background-image: url(/style/index/images/pdbj2.gif);
            background-repeat: no-repeat;
            position: relative;
        }
        .pdrr strong
        {
            position: absolute;
            left: 51px;
            top: 19px;
            width: 605px;
            height: 25px;
            font-size: 25px;
            font-family: "微软雅黑";
            font-weight: bold;
            color: #464646;
        }
        .pdrr strong span
        {
            color: #999999;
            font-size: 25px;
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
            line-height: 30px;
        }
        .pd-info
        {
            position: absolute;
            left: 52px;
            top: 59px;
            color: #7E8284;
            width: 863px;
        }
        /*支持卡类*/.zckl
        {
            width: 930px;
            margin-right: auto;
            margin-left: auto;
            line-height: 30px;
            font-size: 14px;
            color: #696969;
            padding-bottom: 10px;
            border-bottom-width: 1px;
            border-bottom-style: dashed;
            border-bottom-color: #CECECE;
        }
        .main
        {
            width: 930px;
            margin: auto;
        }
        #neiright
        {
            width: 930px;
            margin-right: auto;
            margin-left: auto;
            padding-top: 15px;
            padding-bottom: 15px;
        }
        #neiright .list
        {
            border-bottom: 1px #ccc dashed;
            padding: 10px 0px;
        }
        #neiright .list img
        {
            border: 1px #999999 solid;
            padding: 4px;
            float: left;
            margin-right: 10px;
        }
        #neiright .list .jj .bt a
        {
            color: #1865b1;
        }
        .kl-list
        {
            width: 930px;
            margin-right: auto;
            margin-left: auto;
            padding-top: 15px;
            padding-bottom: 15px;
        }
        .kl-list ul
        {
            margin: 0px;
            padding: 0px;
            list-style-type: none;
        }
        .kl-list li
        {
            position: relative;
            height: 105px;
            width: 299px;
            background-image: url(/style/index/images/cardbj.gif);
            background-repeat: no-repeat;
            float: left;
            padding-right: 10px;
            margin-bottom: 10px;
        }
        .kl-list li:hover
        {
            background-image: url(/style/index/images/cardbj2.gif);
        }
        .card-pic
        {
            position: absolute;
            top: 1px;
            left: 1px;
        }
        .card-pic:hover
        {
            opacity: 0.7;
            filter: alpha(opacity=70);
            zoom: 1;
        }
        .card-name
        {
            position: absolute;
            left: 109px;
            top: 10px;
            width: 178px;
            height: 25px;
            font-size: 14px;
            font-weight: bold;
            color: #0070BC;
            line-height: 25px;
        }
        .card-name:hover
        {
            color: #FF3300;
            text-decoration: underline;
        }
        .card-info
        {
            position: absolute;
            left: 108px;
            top: 38px;
            color: #888888;
            line-height: 24px;
            height: 48px;
            overflow: hidden;
            width: 180px;
        }
        .clear
        {
            clear: both;
        }
        .page
        {
            margin-left: 5px;
        }
        .page span
        {
            padding: 3px;
            min-width: 22px;
            text-align: center;
            margin-top: 30px;
            display: block;
            float: left;
            border: 1px #ccc solid;
            margin-right: 10px;
        }
        .page a
        {
            padding: 3px;
            min-width: 22px;
            text-align: center;
            margin-top: 30px;
            display: block;
            float: left;
            border: 1px #ccc solid;
            margin-right: 10px;
        }
        .page .yes
        {
            background: #0f8ffc;
            color: #fff;
        }
        .page .no
        {
            width: auto;
            border: none;
        }
    </style>
    <!--–[if lte IE]-->

    <script src="js/2/html5.js"></script>

    <!--[endif]–-->
    <link rel="stylesheet" href="style/2/index.css">
    <style type="text/css">
        #bk2
        {
            background-image: url(style/2/b5_fund.jpg);
            z-index: 4;
        }
        #bk5
        {
            background-image: url(style/2/b2.jpg);
            z-index: 1;
        }
        #bk2 .bText
        {
            position: absolute;
            top: 5px;
            left: -50px;
        }
        #bk2 .bPic
        {
            padding: 130px 0 0 400px;
        }
    </style>

    <script type="text/javascript" src="js/2/dojo.js"></script>
    <script type="text/javascript" src="js/2/common_080717.js"></script>
    <script type="text/javascript" src="js/2/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="js/2/jquery.pngFix.pack.js"></script>
    <script type="text/javascript" src="js/2/index.js?"></script>

    <script type="text/javascript">
        dojo.require("dojo.io.bind");
    </script>

    <!-- 头部尾部 -->
    <link rel="shortcut icon" href="ico/favicon.ico">
    <link rel="stylesheet" href="css_demo/bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" href="css_demo/index.css">
    <link rel="stylesheet" href="web_js/easydialog.css" />

    <script language="JavaScript" type="text/javascript" src="web_js/jquery.min.js"></script>

    <script language="JavaScript" type="text/javascript" src="web_js/common.min.js" async="false"></script>

    <!-- end -->
</head>
<body>
    <form id="form1" runat="server">
    <uc1:header ID="header1" runat="server" showtype="news" />
    <div class="clear">
    </div>
    <div id="csdnblog_allwrap" style="margin-top: 69px; background: #59B2E5; border-bottom: 1px solid #E0EDED;
        height: 45px; line-height: 45px;">
        <div style="height: 30px; text-align: left; font-size: 14px; width: 970px; margin: 0 auto;">
            <table width="100%">
                <tbody>
                    <tr>
                        <td height="30" style="color: White; font-size: 14px;">
                            <a href="http://www.59az.com" style="color: White;">亿付商务网</a> > <a href="/news.html"
                                style="color: White;">新闻公告</a> > <a href="/news.html" style="color: White;">
                                    <%=Gettp %></a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div id="Div1">

        <script language="JavaScript" type="text/javascript" src="web_js/tooltip_ext.js"></script>

        <link type="text/css" rel="stylesheet" href="web_css/index.css" media="all">
        <div class="wrapsemibox about-box-mm" style="margin: auto;">
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
                                <dd <%=tid_1_str %>>
                                    <a href="/news.aspx?tid=1">新闻动态</a></dd>
                                <dd <%=tid_2_str %>>
                                    <a href="/news.aspx?tid=2">站内公告</a></dd>
                            </li>
                        </ul>
                    </div>
                </div>
                <!--right scroll-->
                <div class="about-span2 y-last managebar">
                    <div class="aboutus">
                        <br>
                        <asp:Repeater ID="rptnewlist" runat="server">
                            <ItemTemplate>
                                <div class="list">
                                    <div class="jj">
					<p style="border: none; border-bottom: #dcdcdc 1px dashed;">
                        <%# Convert.ToDateTime(Eval("addtime")).ToString("yyyy年MM月dd日")%></p>
                                        <p class="bt font-14" style="border: none; font-size: 16px;">
                                            <img src="images/authorship.gif" />
                                            <a href="/view.aspx?newsid=<%#Eval("newsid")%>" title="<%#Eval("newstitle")%>">
                                                <%#Eval("newstitle")%></a></p>
                                        <p style="border: none;">
                                            &nbsp;&nbsp;&nbsp;&nbsp;<%# NoHTML(Eval("newscontent").ToString())%>
                                        </p>
                                        <p style="border: none; border-bottom: #dcdcdc 1px dashed;"></p><br />
                                    </div>
                                    <div class="c">
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <aspxc:AspNetPager ID="Pager1" runat="server" CustomInfoHTML="共%RecordCount%/%CurrentPageIndex%条"
                            OnPageChanged="Pager1_PageChanged" CssClass="page c" AlwaysShow="true" PageSize="15"
                            ShowPageIndexBox="Never">
                        </aspxc:AspNetPager>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="pagebottom"></div>
    <div class="clear">
    </div>
    <!--footer------------------------------->
    <uc1:footer ID="footer1" runat="server" showtype="news" />
    <!--footer end------------------------------->
    </form>
</body>
</html>
