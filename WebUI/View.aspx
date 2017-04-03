<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="viviAPI.WebUI7uka.View" %>

<%@ Register Src="head.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="footer" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title><%=SiteName%>-<%=PageTitle%></title>

    <script language="javascript">
        function secBoard(n) {
            for (i = 0; i < secTable.cells.length; i++)
                secTable.cells[i].className = "sec1";
            secTable.cells[n].className = "sec2";
            for (i = 0; i < mainTable.tBodies.length; i++)
                mainTable.tBodies[i].style.display = "none";
            mainTable.tBodies[n].style.display = "block";
        }
        function mOvr(src) {
            if (!src.contains(event.fromElement)) {
                src.style.cursor = 'hand'; src.bgColor = "#ffffff";
            }
        }
        function mOut(src) {
            if (!src.contains(event.toElement)) {
                src.style.cursor = 'default'; src.bgColor = "#FAFAFA";
            }
        }
        function mClk(src) {
            if (event.srcElement.tagName == 'TD') {
                src.children.tags('A')[0].click();
            }
        }
        function refreshValidateCode(_id, url) {
            document.getElementById(_id).src = url + "?date=" + new Date();
        }
    </script>

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
    <uc1:header ID="header1" runat="server" showtype="index" />
    <div class="clear">
    </div>
    <div id="csdnblog_allwrap" style="margin-top: 69px; background: #59B2E5; border-bottom: 1px solid #E0EDED;
        height: 45px; line-height: 45px;">
        <div style="height: 30px; text-align: left; font-size: 14px; width: 970px; margin: 0 auto;">
            <table width="100%">
                <tbody>
                    <tr>
                        <td height="30" style="color: White; font-size: 14px;">
                            <a href="/" style="color: White;">亿付商务网</a> > <a href="/news.aspx"
                                style="color: White;">新闻公告</a> > <a href="/news.aspx" style="color: White;">
                                    <%=gettp %></a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div id="page">

        <script language="JavaScript" type="text/javascript" src="web_js/tooltip_ext.js"></script>

        <link type="text/css" rel="stylesheet" href="web_css/index.css" media="all">
        <div class="wrapsemibox about-box-mm" style="margin-top: auto;">
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
                                <dd  <%=tid_1_str %>>
                                    <a href="/news.aspx?tid=1">新闻动态</a></dd>
                                <dd  <%=tid_2_str %>>
                                    <a href="/news.aspx?tid=2">站内公告</a></dd>
                            </li>
                        </ul>
                    </div>
                </div>
                <!--right scroll-->
                <div class="about-span2 y-last managebar">
                    <div class="aboutus">
                        <br />
                        <h2 style="border-bottom: 2px solid gainsboro; padding: 0 0 10px 0; width: 100%;
                            text-align: right; color: #666; font-size: 12px;">
                            发布时间：<asp:Literal ID="newstime" runat="server"></asp:Literal>&nbsp;&nbsp;</h2>
                        <h1 style="font-family: 'Microsoft yahei', verdana, sans-serif; color: #428BCA; font-size: 16px;
                            padding: 0px; padding-bottom: 10px; text-align: center; margin-top: 20px; padding: 0px 0px 10px;">
                            <asp:Literal ID="newstitle" runat="server"></asp:Literal></h1>
                        <div style="line-height: 200%; width: 100%; float: left; font-size: 14px; color: black;">
                            <asp:Literal ID="newscontenct" runat="server"></asp:Literal>
                        </div>
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
