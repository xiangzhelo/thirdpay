<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="viviapi.web.search" %>

<%@ Register Src="UserCtrls/header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="UserCtrls/footer.ascx" TagName="footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>
        <%=SiteName%>
        -
        <%=WebSiteTitleSuffix%></title>
    <meta name="Keywords" content="<%=KeyWords%>" />
    <meta name="Description" content="<%=Description%>" />
    <link type="image/x-icon" href="favicon.ico" rel="shortcut icon" />
    <link type="text/css" href="/style/index/images/style.css" rel="stylesheet">

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
        #searchheaderout
        {
            margin-top: 10px;
            border: 1px #ddd solid;
            background: #fff;
        }
        #searchheaderout .i1
        {
            background: url(images/001.gif) repeat-x #fff;
        }
        #searchheaderout .input
        {
            width: 300px;
            height: 36px;
            line-height: 36px;
            border: 1px #ddd solid;
            float: left;
        }
        #searchheaderout .search
        {
            padding-left: 200px;
        }
        #searchheaderout .search span
        {
            float: left;
            margin: 10px 0px;
            padding: 0px;
        }
        .result
        {
            clear: both;
            width: 890px;
            height: 210px;
            padding-top: 10px;
            margin: 10px 0 0px 0px;
        }
        .result img
        {
            margin-bottom: 10px;
        }
        .result h3
        {
            height: 28px;
            margin-bottom: 20px;
            text-indent: -9999px;
            background: url(images/result.jpg) no-repeat left center;
        }
        .result table
        {
            width: 890px;
            text-align: center;
            background: #e0e0e0;
        }
        .result table tr
        {
            height: 32px;
        }
        .result table tr.dark
        {
            background: #efefef;
        }
        .result table tr th
        {
            color: #666;
            width: 130px;
            font-size: 14px;
        }
        .result table tr td
        {
            width: 130px;
        }
        .result table tr.result td
        {
            background: #fff;
        }
        .font-14
        {
            font-size: 14px;
            font-weight: bold;
        }
        .yes
        {
            background: url(style/index/images/002.gif) no-repeat;
            color: #fff;
        }
    </style>

    <script>

        $(function() {
            var type = $("#hfsearchtype").val();
            if (type == 1) {
                document.getElementById("img_searchtype").src = "/style/index/images/001.jpg";
                document.getElementById("sp_orderno").className = "yes font-14";
                document.getElementById("sp_cardno").className = "font-14";
            }
            else {
                document.getElementById("img_searchtype").src = "/style/index/images/003.jpg";
                document.getElementById("sp_orderno").className = "font-14";
                document.getElementById("sp_cardno").className = "yes font-14";
            }
        });

        function changetype(type) {
            window.location.href = 'Search.aspx?stype=' + type;
        };
    </script>

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

    <script type="text/javascript" src="js/2/index.js"></script>

    <script type="text/javascript">
	    dojo.require("dojo.io.bind");
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <uc1:header ID="header1" runat="server" showtype="search" />
    <div id="pd">
        <div class="pdrr">
            <strong>查询服务 <span class="pd-en"></span></strong><span class="pd-info">查询服务</span></div>
    </div>
    <div id="searchheaderout" class="main">
        <div id="searchheader">
        </div>
        <div id="leisure2_1">
            <div id="searchcontent">
                <div class="i1">
                    <div class="xxbt" style="padding-left: 300px;">
                        <asp:HiddenField ID="hfsearchtype" Value="1" runat="server" />
                        <span id="sp_orderno" class="yes font-14" onclick="changetype(1)">订单号查询</span> <span
                            id="sp_cardno" class="font-14" onclick="changetype(2)">支付卡号查询</span>
                    </div>
                    <div class="search c">
                        <span>
                            <img id="img_searchtype" src="/style/index/images/001.jpg" alt="" /></span>
                        <span>
                            <input id="txtuserorder" runat="server" name="" type="text" class="input" /></span>
                        <span>
                            <asp:ImageButton ID="ibtnSearch" runat="server" ImageUrl="/style/index/images/002.jpg"
                                OnClick="ibtnSearch_Click" /></span>
                    </div>
                    <div class="result">
                        <table>
                            <tr class="dark">
                                <th>
                                    系统订单
                                </th>
                                <th>
                                    商户订单
                                </th>
                                <th>
                                    面值
                                </th>
                                <th>
                                    状态
                                </th>
                                <th>
                                    提交时间
                                </th>
                                <th>
                                    支付类型
                                </th>
                            </tr>
                            <asp:Repeater ID="order" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%#Eval("orderid")%>
                                        </td>
                                        <td>
                                            <%#Eval("userorder")%>
                                        </td>
                                        <td>
                                            <%#Eval("realvalue","{0:f2}")%>
                                        </td>
                                        <td>
                                            <%#GetViewStatusName(Eval("status"))%>
                                        </td>
                                        <td>
                                            <%#Eval("addtime","{0:yyyy-MM-dd HH:ss:mm}")%>
                                        </td>
                                        <td>
                                            <%# Eval("modeName")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr>
                                        <td>
                                            <%#Eval("orderid")%>
                                        </td>
                                        <td>
                                            <%#Eval("userorder")%>
                                        </td>
                                        <td>
                                            <%#Eval("realvalue","{0:f2}")%>
                                        </td>
                                        <td>
                                            <%#GetViewStatusName(Eval("status"))%>
                                        </td>
                                        <td>
                                            <%#Eval("addtime","{0:yyyy-MM-dd HH:ss:mm}")%>
                                        </td>
                                        <td>
                                            <%# Eval("modeName")%>
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <div class="c">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
    <uc1:footer ID="footer1" runat="server" />
    </form>
</body>
</html>
