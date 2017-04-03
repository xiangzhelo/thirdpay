<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="viviAPI.WebUI2015.WebForm4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<head>
    <title>云数卡 全国权威的数字点卡交易平台-后台管理</title>
    <meta name="author" content="http://www.kayixiao.com">
    <link rel="Stylesheet" type="text/css" href="/lib/css/main.css?v=0.2">
    <link rel="stylesheet" type="text/css" href="http://card.oss.aliyuncs.com//lib/ligerui/skins/aqua/css/ligerui-all.css">
    <link rel="stylesheet" type="text/css" href="http://card.oss.aliyuncs.com//lib/css/dialog.css">
    <link rel="stylesheet" type="text/css" href="http://card.oss.aliyuncs.com//lib/css/style.css">
    <link rel="stylesheet" type="text/css" href="http://card.oss.aliyuncs.com//lib/css/demo.css">
    <link rel="shortcut icon" type="image/x-icon" href="http://card.oss.aliyuncs.com/lib/theme/yun/lekatong.ico">
    <link rel="Bookmark" type="image/x-icon" href="http://card.oss.aliyuncs.com/lib/theme/yun/lekatong.ico">
    <script type="text/javascript" src="http://card.oss.aliyuncs.com//lib/jquery/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="http://card.oss.aliyuncs.com//lib/ligerui/js/core/base.js"></script>
    <script type="text/javascript" src="http://card.oss.aliyuncs.com//lib/ligerui/js/plugins/ligerLayout.js"></script>
    <script type="text/javascript" src="http://card.oss.aliyuncs.com//lib/ligerui/js/plugins/ligerAccordion.js"></script>
    <script type="text/javascript" src="http://card.oss.aliyuncs.com//lib/ligerui/js/plugins/ligerTab.js"></script>
    <script type="text/javascript" src="http://card.oss.aliyuncs.com//lib/ligerui/js/plugins/ligerMenu.js"></script>
    <script type="text/javascript" src="http://card.oss.aliyuncs.com//lib/js/dialog.js"></script>
    <style>
        #header-inner
        {
            background: #70a3ee;
        }
    </style>
    <script type="text/javascript">
        var tab = null;
        var accordion = null;
        var tree = null;
        $(function () {
            refreshmessage();
            $.getJSON("/service/DataHandler.ashx?View=AdminPopedom&UserId=1446&SystemType=0&r=" + Math.random(), {},
            function (json) {
                var html = "";
                var ParentName = "";
                if (json.Rows.length == 0) { alert("当前用户没有任何模组权限!请联系管理员!", "error"); return; };
                for (var i = 0; i <= json.Rows.length - 1; i++) {
                    if (ParentName != json.Rows[i].ModuleName) {
                        if (html != "")
                        { html += "</div>" };
                        html += "<div title=\"" + json.Rows[i].ModuleName + "\"><div style=\"height:7px;\"></div>";
                        ParentName = json.Rows[i].ModuleName;
                    }
                    if (json.Rows[i].PopedomName == "刷新网站网站基本信息缓存") {

                    }
                    else {
                        html += "<a class=\"l-link\" pid=\"" + json.Rows[i].PopedomId + "\" groupid=\"" + json.Rows[i].groupid + "\" url=\"" + json.Rows[i].folder + "/" + json.Rows[i].PopedomUrl + "\">" + json.Rows[i].PopedomName + "</a>";
                    }
                }
                $("#accordion1").html(html);

                $("#accordion1 a[groupid=11]").css("float", "left").css("padding-right", "10px");
                $("#accordion1 a[groupid=13]").css("float", "left").css("padding-right", "10px");
                $("#accordion1 a[groupid=15]").css("float", "left").css("padding-right", "10px");
                $("#accordion1 a[groupid=17]").css("float", "left").css("padding-right", "10px");
                $("#accordion1 a[groupid=21]").css("float", "left").css("padding-right", "10px");
                $("#accordion1 a[groupid=56]").css("float", "left").css("padding-right", "10px");
                $("#accordion1 a[groupid=11]").last().css("float", "none").after("<a style='clear:both; float:none'></a>");
                $("#accordion1 a[groupid=13]").last().css("float", "none").after("<a style='clear:both; float:none'></a>");
                $("#accordion1 a[groupid=15]").last().css("float", "none").after("<a style='clear:both; float:none'></a>");
                $("#accordion1 a[groupid=17]").last().css("float", "none").after("<a style='clear:both; float:none'></a>");
                $("#accordion1 a[groupid=21]").last().css("float", "none").after("<a style='clear:both; float:none'></a>");
                $("#accordion1 a[groupid=56]").last().css("float", "none").after("<a style='clear:both; float:none'></a>");
                $("#accordion1").ligerAccordion({ height: height - 24, speed: null });
                accordion = $("#accordion1").ligerGetAccordionManager();

                $("a[url]").attr("href", "javascript:;");
                //$("a[url]").each(function () {$(this).attr("class", "l-link");});
                $("a[url]").click(function () {
                    var url = $(this).attr("url");
                    var sub = "";
                    if (url.indexOf("?") == -1) {
                        if (url.indexOf("/") == -1) sub = "pages/"; else sub = "";

                        if (tab.getTabItemCount() >= 5) {
                            tab.overrideSelectedTabItem({ text: $(this).text(), url: '/lktadmin_e63d315/' + sub + $(this).attr("url") + '?r=' + new Date() });
                        }
                        else {
                            f_addTab($(this).attr("pid"), $(this).text(), '/lktadmin_e63d315/' + sub + $(this).attr("url") + '?r=' + new Date());
                            //                            tab.reload();
                            //                            tab.addTabItem({ tabid: $(this).attr("pid"), text: $(this).text(), url: '/lktadmin_e63d315/' + sub + $(this).attr("url") + '?r=' + new Date() });
                        }

                    }
                    else {
                        if (url.indexOf("/") == -1) sub = "pages/"; else sub = "";

                        if (tab.getTabItemCount() >= 5) {
                            tab.overrideSelectedTabItem({ text: $(this).text(), url: '/lktadmin_e63d315/' + sub + $(this).attr("url") + '&r=' + new Date() });
                        }
                        else {
                            f_addTab($(this).attr("pid"), $(this).text(), '/lktadmin_e63d315/' + sub + $(this).attr("url") + '&r=' + new Date());
                            //                            tab.reload($(this).attr("pid"));
                            //                            tab.addTabItem({ tabid: $(this).attr("pid"), text: $(this).text(), url: '/lktadmin_e63d315/' + sub + $(this).attr("url") + '&r=' + new Date() });
                        }


                        // tab.overrideSelectedTabItem({ text: $(this).text(), url: '/lktadmin_e63d315/' + sub + $(this).attr("url") + '&r=' + new Date() });
                    }
                });
            });

            //$("#header").css("background-color", "#0033CC");
            $("#layout1").ligerLayout({ leftWidth: 190, height: '100%', heightDiff: -4, space: 4, onHeightChanged: f_heightChanged });
            var height = $(".l-layout-center").height();
            $("#framecenter").ligerTab({ height: height });

            $(".l-link").hover(function () { $(this).addClass("l-link-over"); }, function () { $(this).removeClass("l-link-over"); });
            tab = $("#framecenter").ligerGetTabManager();

            $(window).resize(function () {
                f_resize();
            });

            $("#pageloading").hide();
        });

        function refreshmessage() {
            $.getJSON("/service/DataHandler.ashx?View=adminMessage&r=" + new Date(), {},
            function (json) {
                $("#handcount").html("手工 " + json[0].handcount.toString());
                $("#autocount").html("自动 " + json[0].autocount.toString());
                $("#messagecount").html("短信 " + json[0].messagecount.toString());
                $("#nostockcount").html("断货" + json[0].nostockcount.toString());
                $("#remitcount").html("汇款 " + json[0].remitcount.toString());
                $("#dubiousordercount").html("可疑 " + json[0].dubiousordercount.toString());
                $("#complaincount").html("投诉 " + json[0].complaincount.toString());
                $("#proposecount").html(json[0].complainPropose.toString()); //cherry 2013.8.6
            });
        }

        function f_heightChanged(options) {
            if (tab) tab.addHeight(options.diff);
            accordion.setHeight($(".l-layout-center").height() - 4);
        }

        function f_resize() {
            accordion.setHeight($(".l-layout-center").height() - 4);
        }

        function f_addTab(tabid, text, url) {
            if (tab.isTabItemExist(tabid) == true) tab.reload(tabid);
            tab.addTabItem({ tabid: tabid, text: text, url: url });
        }

        function f_reloadTab(text, url) {
            tab.overrideSelectedTabItem({ text: text, url: url + '&r=' + new Date() });
        }

        //window.attachEvent("onresize", f_resize);

        setInterval('refreshmessage()', 60000);

    </script>
</head>
<body>
    <div style="z-index: 949; filter: alpha(opacity=10); background-color: #000; width: 100%;
        display: none; height: 100%; top: 0px; left: 0px" id="iframeFather">
    </div>
    <div id="header">
        <div id="header-inner">
            <table style="width: 100%" cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <td style="width: 20px" rowspan="2">
                        </td>
                        <td style="height: 52px">
                            <div>
                                <a class="logo">云数卡 全国权威的数字点卡交易平台...-后台管理</a>
                            </div>
                        </td>
                        <td style="text-align: right; padding-right: 5px; vertical-align: middle">
                            <div class="topmenu">
                                <ul>
                                    <li>欢迎您：<b>yskadmin(陈勇)</b> <i></i><a href="index.aspx">首页</a><i> </i><a href="Logout.aspx"
                                        target="_top">退出</a><i> </i><a id="handcount" href="javascript:;" url="service/GameSavingTreat.aspx"
                                            jquery16206603060198626224="43">手工 0</a><i> </i><a id="autocount" href="javascript:;"
                                                url="service/GameSavingTreat_Auto.aspx" jquery16206603060198626224="44">自动 0</a><i>
                                                </i><a id="messagecount" href="javascript:;" url="basic/Messages.aspx?Type=1" jquery16206603060198626224="45">
                                                    短信 0</a><i> </i><a id="nostockcount" href="javascript:;" url="product/CaminInventory.aspx?InventoryStatus=1"
                                                        jquery16206603060198626224="46">断货0</a><i> </i><a id="remitcount" href="javascript:;"
                                                            url="money/TreatRemitNotice.aspx" jquery16206603060198626224="47">汇款 0</a><i>
                                        </i><a style="color: #ff6600; font-weight: bold" id="ComplaintPropose" href="javascript:;"
                                            url="service/ComplaintPropose.aspx" jquery16206603060198626224="48">我要提建议 <span style="top: -6px"
                                                id="proposecount">0</span></a><i></i><a id="complaincount" href="javascript:;" url="service/OnLineService.aspx"
                                                    jquery16206603060198626224="49">投诉 0</a><i> </i><a id="dubiousordercount" href="javascript:;"
                                                        url="service/GameSavingList.aspx?TreatWith=4" jquery16206603060198626224="50">可疑
                                                        0</a> </li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div id="mainwrap">
        <div id="content">
            <div style="display: none" id="pageloading" jquery16206603060198626224="18">
            </div>
            <div style="margin: 4px auto 0px; width: 99.2%; height: 267px" id="layout1" class="l-layout"
                ligeruiid="layout1">
                <div style="width: 190px; height: 265px; top: 0px; left: 0px" class="l-layout-left"
                    sizcache="0" sizset="0">
                    <div class="l-layout-header" jquery16206603060198626224="6">
                        <div class="l-layout-header-toggle" jquery16206603060198626224="7">
                        </div>
                        <div class="l-layout-header-inner">
                            菜单</div>
                    </div>
                    <div style="height: 261px" id="accordion1" class="l-layout-content l-accordion-panel"
                        title="" position="left" ligeruiid="accordion1">
                        <div class="l-accordion-header" jquery16206603060198626224="27">
                            <div class="l-accordion-toggle l-accordion-toggle-open" jquery16206603060198626224="35">
                            </div>
                            <div class="l-accordion-header-inner">
                                常规管理
                            </div>
                        </div>
                        <div style="height: 61px" class="l-accordion-content" title="">
                            <div style="height: 7px">
                            </div>
                            <a class="l-link" href="javascript:;" url="basic/BasicInfo.aspx" jquery16206603060198626224="51"
                                groupid="null" pid="1">网站基本信息设置</a><a class="l-link" href="javascript:;" url="basic/Bulletin_Add.aspx"
                                    jquery16206603060198626224="52" groupid="null" pid="53">发布公告</a><a class="l-link"
                                        href="javascript:;" url="basic/Bulletin.aspx" jquery16206603060198626224="53"
                                        groupid="null" pid="3">公告管理</a><a class="l-link" href="javascript:;" url="basic/SendMessages.aspx"
                                            jquery16206603060198626224="54" groupid="null" pid="54">短信发送</a><a class="l-link"
                                                href="javascript:;" url="basic/Messages.aspx" jquery16206603060198626224="55"
                                                groupid="null" pid="4">短信管理</a><a class="l-link" href="javascript:;" url="basic/FriendLink_Add.aspx"
                                                    jquery16206603060198626224="56" groupid="null" pid="55">添加友情链接</a><a class="l-link"
                                                        href="javascript:;" url="basic/FriendLinkList.aspx" jquery16206603060198626224="57"
                                                        groupid="null" pid="5">友情连接管理</a><a class="l-link" href="javascript:;" url="basic/CreateSite.aspx"
                                                            jquery16206603060198626224="58" groupid="0" pid="61">首页生成</a></div>
                        <div class="l-accordion-header" jquery16206603060198626224="28">
                            <div class="l-accordion-toggle l-accordion-toggle-close" jquery16206603060198626224="36">
                            </div>
                            <div class="l-accordion-header-inner">
                                系统管理
                            </div>
                        </div>
                        <div style="display: none; height: 61px" class="l-accordion-content" title="" jquery16206603060198626224="20">
                            <div style="height: 7px">
                            </div>
                            <a class="l-link" href="javascript:;" url="system/SystemUser.aspx" jquery16206603060198626224="59"
                                groupid="null" pid="7">管理员添加</a><a class="l-link" href="javascript:;" url="system/SystemUserList.aspx"
                                    jquery16206603060198626224="60" groupid="7" pid="8">管理员管理</a><a class="l-link" href="javascript:;"
                                        url="system/SystemLogList.aspx" jquery16206603060198626224="61" groupid="null"
                                        pid="9">登录日志</a><a class="l-link" href="javascript:;" url="system/vipsite.aspx" jquery16206603060198626224="62"
                                            groupid="null" pid="44">分站权限开停</a><a class="l-link" href="javascript:;" url="system/ApiWhiteList.aspx"
                                                jquery16206603060198626224="63" groupid="null" pid="80">Api接口访问白名单管理</a><a class="l-link"
                                                    href="javascript:;" url="system/cache.aspx" jquery16206603060198626224="64" groupid="null"
                                                    pid="81">刷新网站基本信息缓存</a></div>
                        <div class="l-accordion-header" jquery16206603060198626224="29">
                            <div class="l-accordion-toggle l-accordion-toggle-close" jquery16206603060198626224="37">
                            </div>
                            <div class="l-accordion-header-inner">
                                商品管理</div>
                        </div>
                        <div style="display: none; height: 61px" class="l-accordion-content" title="" jquery16206603060198626224="21">
                            <div style="height: 7px">
                            </div>
                            <a class="l-link" href="javascript:;" url="product/Category_Add.aspx" jquery16206603060198626224="65"
                                groupid="null" pid="11">目录添加</a><a class="l-link" href="javascript:;" url="product/Category.aspx"
                                    jquery16206603060198626224="66" groupid="null" pid="12">目录管理</a><a class="l-link"
                                        href="javascript:;" url="product/Product_Add.aspx" jquery16206603060198626224="67"
                                        groupid="null" pid="13">商品添加</a><a class="l-link" href="javascript:;" url="product/Product.aspx"
                                            jquery16206603060198626224="68" groupid="null" pid="14">商品管理</a><a class="l-link"
                                                href="javascript:;" url="product/SGTemplate_Add.aspx" jquery16206603060198626224="69"
                                                groupid="null" pid="15">模板添加</a><a class="l-link" href="javascript:;" url="product/SGTemplate.aspx"
                                                    jquery16206603060198626224="70" groupid="null" pid="16">模板管理</a><a style="padding-right: 10px;
                                                        float: none" class="l-link" href="javascript:;" url="product/ProductStandardPrice.aspx"
                                                        jquery16206603060198626224="71" groupid="17" pid="17">商品定价</a><a style="float: none;
                                                            clear: both"></a><a class="l-link" href="javascript:;" url="product/ProductInventory.aspx"
                                                                jquery16206603060198626224="72" groupid="null" pid="23">库存管理</a><a class="l-link"
                                                                    href="javascript:;" url="product/ImportCardsHistory.aspx" jquery16206603060198626224="73"
                                                                    groupid="null" pid="24">导卡记录查询</a><a class="l-link" href="javascript:;" url="product/inventoryhistory.aspx"
                                                                        jquery16206603060198626224="74" groupid="null" pid="25">虚拟卡密交易记录</a><a class="l-link"
                                                                            href="javascript:;" url="product/CardStocksList.aspx" jquery16206603060198626224="75"
                                                                            groupid="null" pid="26">库存未售卡号密码管理</a><a class="l-link" href="javascript:;" url="product/ExportList.aspx"
                                                                                jquery16206603060198626224="76" groupid="null" pid="78">虚拟卡密数据导出</a><a class="l-link"
                                                                                    href="javascript:;" url="product/CaminInventory.aspx" jquery16206603060198626224="77"
                                                                                    groupid="null" pid="27">虚拟卡密商品库存查询</a><a class="l-link" href="javascript:;" url="product/ESAccountList.aspx"
                                                                                        jquery16206603060198626224="78" groupid="null" pid="28">直储账号管理</a><a class="l-link"
                                                                                            href="javascript:;" url="product/selfproductmanage.aspx" jquery16206603060198626224="79"
                                                                                            groupid="null" pid="45">自有商品清单及管理</a><a class="l-link" href="javascript:;" url="product/selfproductsalereport.aspx"
                                                                                                jquery16206603060198626224="80" groupid="null" pid="46">自有商品销售明细</a><a class="l-link"
                                                                                                    href="javascript:;" url="product/CoinCustomerLimit.aspx" jquery16206603060198626224="81"
                                                                                                    groupid="null" pid="67">积分商品供货商管理</a></div>
                        <div class="l-accordion-header" jquery16206603060198626224="30">
                            <div class="l-accordion-toggle l-accordion-toggle-close" jquery16206603060198626224="38">
                            </div>
                            <div class="l-accordion-header-inner">
                                客户管理</div>
                        </div>
                        <div style="display: none; height: 61px" class="l-accordion-content" title="" jquery16206603060198626224="22">
                            <div style="height: 7px">
                            </div>
                            <a class="l-link" href="javascript:;" url="customer/CustomerType.aspx" jquery16206603060198626224="82"
                                groupid="null" pid="29">客户级别体系</a><a class="l-link" href="javascript:;" url="customer/customerlist.aspx"
                                    jquery16206603060198626224="83" groupid="null" pid="30">客户管理</a><a class="l-link"
                                        href="javascript:;" url="customer/customerbalancehistory.aspx" jquery16206603060198626224="84"
                                        groupid="null" pid="31">客户统计</a><a class="l-link" href="javascript:;" url="customer/HistoryCustomerBalance.aspx"
                                            jquery16206603060198626224="85" groupid="null" pid="79">历史客户统计</a><a class="l-link"
                                                href="javascript:;" url="customer/fluctuationsetup.aspx" jquery16206603060198626224="86"
                                                groupid="null" pid="32">经销上下级关系定义</a></div>
                        <div class="l-accordion-header" jquery16206603060198626224="31">
                            <div class="l-accordion-toggle l-accordion-toggle-close" jquery16206603060198626224="39">
                            </div>
                            <div class="l-accordion-header-inner">
                                订单管理</div>
                        </div>
                        <div style="display: none; height: 61px" class="l-accordion-content" title="" jquery16206603060198626224="23">
                            <div style="height: 7px">
                            </div>
                            <a class="l-link" href="javascript:;" url="order/b2csaleorder.aspx" jquery16206603060198626224="87"
                                groupid="null" pid="34">零售订单记录</a><a class="l-link" href="javascript:;" url="order/cardproductsalereport.aspx"
                                    jquery16206603060198626224="88" groupid="null" pid="35">销售报表</a><a class="l-link"
                                        href="javascript:;" url="order/trancard.aspx" jquery16206603060198626224="89"
                                        groupid="null" pid="36">虚拟卡密转账</a><a class="l-link" href="javascript:;" url="order/trancardhistory.aspx"
                                            jquery16206603060198626224="90" groupid="null" pid="37">虚拟卡密记录</a></div>
                        <div class="l-accordion-header" jquery16206603060198626224="32">
                            <div class="l-accordion-toggle l-accordion-toggle-close" jquery16206603060198626224="40">
                            </div>
                            <div class="l-accordion-header-inner">
                                财务管理</div>
                        </div>
                        <div style="display: none; height: 61px" class="l-accordion-content" title="" jquery16206603060198626224="24">
                            <div style="height: 7px">
                            </div>
                            <a class="l-link" href="javascript:;" url="money/bankaccount.aspx?Action=Add" jquery16206603060198626224="91"
                                groupid="38" pid="38">汇款账号定义</a><a class="l-link" href="javascript:;" url="money/bankaccount.aspx"
                                    jquery16206603060198626224="92" groupid="38" pid="39">管理</a><a class="l-link" href="javascript:;"
                                        url="money/CreateChongZhiCard.aspx" jquery16206603060198626224="93" groupid="47"
                                        pid="47">充值卡生成</a><a class="l-link" href="javascript:;" url="money/ChongZhiCard.aspx"
                                            jquery16206603060198626224="94" groupid="47" pid="48">管理</a><a class="l-link" href="javascript:;"
                                                url="money/MoveMoney.aspx" jquery16206603060198626224="95" groupid="63" pid="63">转账</a><a
                                                    class="l-link" href="javascript:;" url="money/MoveMoneyHistory.aspx" jquery16206603060198626224="96"
                                                    groupid="63" pid="60">记录</a><a class="l-link" href="javascript:;" url="money/PayMentOnLine.aspx"
                                                        jquery16206603060198626224="97" groupid="null" pid="56">网银手工处理</a><a class="l-link"
                                                            href="javascript:;" url="money/PayMentOnLineHistory.aspx" jquery16206603060198626224="98"
                                                            groupid="null" pid="57">网银手工记录</a><a class="l-link" href="javascript:;" url="money/CardClearingTotalSubmit.aspx"
                                                                jquery16206603060198626224="99" groupid="76" pid="76">消费卡结算</a><a class="l-link"
                                                                    href="javascript:;" url="money/CardClearingTotalHistory.aspx" jquery16206603060198626224="100"
                                                                    groupid="76" pid="77">记录</a><a class="l-link" href="javascript:;" url="money/SellToBalance.aspx"
                                                                        jquery16206603060198626224="101" groupid="null" pid="62">提成或收入转余额</a><a class="l-link"
                                                                            href="javascript:;" url="money/customerbalance.aspx" jquery16206603060198626224="102"
                                                                            groupid="null" pid="42">客户余额检查</a><a class="l-link" href="javascript:;" url="money/cardproductsalehistory.aspx"
                                                                                jquery16206603060198626224="103" groupid="null" pid="43">商品销售报表</a><a class="l-link"
                                                                                    href="javascript:;" url="money/SalesCommissionReport.aspx" jquery16206603060198626224="104"
                                                                                    groupid="null" pid="64">销售提成报表</a></div>
                        <div class="l-accordion-header" jquery16206603060198626224="33">
                            <div class="l-accordion-toggle l-accordion-toggle-close" jquery16206603060198626224="41">
                            </div>
                            <div class="l-accordion-header-inner">
                                客服管理</div>
                        </div>
                        <div style="display: none; height: 61px" class="l-accordion-content" title="" jquery16206603060198626224="25">
                            <div style="height: 7px">
                            </div>
                            <a class="l-link" href="javascript:;" url="service/saleorderservice.aspx" jquery16206603060198626224="105"
                                groupid="null" pid="49">销售订单售后查询</a><a class="l-link" href="javascript:;" url="service/gamesavingtreat.aspx"
                                    jquery16206603060198626224="106" groupid="null" pid="50">代客手工充值</a><a class="l-link"
                                        href="javascript:;" url="service/gamesavinglist.aspx" jquery16206603060198626224="107"
                                        groupid="null" pid="51">代客手工记录</a><a class="l-link" href="javascript:;" url="service/onlineservice.aspx"
                                            jquery16206603060198626224="108" groupid="null" pid="52">在线客服</a></div>
                        <div class="l-accordion-header" jquery16206603060198626224="34">
                            <div class="l-accordion-toggle l-accordion-toggle-close" jquery16206603060198626224="42">
                            </div>
                            <div class="l-accordion-header-inner">
                                一卡通管理</div>
                        </div>
                        <div style="display: none; height: 61px" class="l-accordion-content" title="" jquery16206603060198626224="26">
                            <div style="height: 7px">
                            </div>
                            <a class="l-link" href="javascript:;" url="ykt/Onecardsolutiocreate.aspx" jquery16206603060198626224="109"
                                groupid="null" pid="68">一卡通生成</a><a class="l-link" href="javascript:;" url="ykt/Onecarduploading.aspx"
                                    jquery16206603060198626224="110" groupid="null" pid="69">一卡通上传</a><a class="l-link"
                                        href="javascript:;" url="ykt/OnecardAddShowMoney.aspx" jquery16206603060198626224="111"
                                        groupid="null" pid="70">兑换卡面值添加/管理</a><a class="l-link" href="javascript:;" url="ykt/OneCarTypeAddOrD.aspx"
                                            jquery16206603060198626224="112" groupid="null" pid="71">兑换卡类型添加/管理</a><a class="l-link"
                                                href="javascript:;" url="ykt/Onecardsolutiocreatehistory.aspx" jquery16206603060198626224="113"
                                                groupid="null" pid="72">生成/上传记录</a><a class="l-link" href="javascript:;" url="ykt/thebanoncard.aspx"
                                                    jquery16206603060198626224="114" groupid="null" pid="73">一卡通开卡/禁卡</a><a class="l-link"
                                                        href="javascript:;" url="ykt/Yktbangdingkehu.aspx" jquery16206603060198626224="115"
                                                        groupid="null" pid="74">一卡通代理绑定/记录</a><a class="l-link" href="javascript:;" url="ykt/YktPwdNewSet.aspx"
                                                            jquery16206603060198626224="116" groupid="null" pid="75">一卡通卡密管理/记录</a></div>
                    </div>
                </div>
                <div style="width: 1387px; height: 265px; top: 0px; left: 196px" class="l-layout-center">
                    <div style="height: 265px" id="framecenter" class="l-layout-content l-tab" position="center"
                        ligeruiid="framecenter" jquery16206603060198626224="17">
                        <div class="l-tab-links">
                            <ul style="left: 0px">
                                <li class="l-selected" tabid="home" jquery16206603060198626224="16"><a>管理首页</a>
                                    <div class="l-tab-links-item-left">
                                    </div>
                                    <div class="l-tab-links-item-right">
                                    </div>
                                </li>
                            </ul>
                        </div>
                        <div style="height: 239px" class="l-tab-content">
                            <div style="height: 239px" class="l-tab-content-item" title="" tabid="home">
                                <div style="display: none" class="l-tab-loading" jquery16206603060198626224="19">
                                </div>
                                <iframe id="home" src="main.aspx" frameborder="0" name="home" jquery16206603060198626224="15">
                                </iframe>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="l-layout-lock">
                </div>
                <div style="display: block; height: 265px; top: 0px; left: 190px" class="l-layout-drophandle-left"
                    jquery16206603060198626224="1">
                </div>
                <div class="l-layout-dragging-xline">
                </div>
                <div class="l-layout-dragging-yline">
                </div>
                <div style="display: none; height: 265px; top: 0px" class="l-layout-collapse-left"
                    jquery16206603060198626224="2">
                    <div class="l-layout-collapse-left-toggle" jquery16206603060198626224="3">
                    </div>
                </div>
                <div style="display: none" class="l-layout-collapse-right" jquery16206603060198626224="4">
                    <div class="l-layout-collapse-right-toggle" jquery16206603060198626224="5">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="margin-top: -40px; display: none" id="guide-step">
    </div>
    <div style="width: 100px; display: none; top: 0px; left: 0px" class="l-menu" ligeruiid="Menu1000"
        jquery16206603060198626224="9">
        <div class="l-menu-yline">
        </div>
        <div class="l-menu-over">
            <div class="l-menu-over-l">
            </div>
            <div class="l-menu-over-r">
            </div>
        </div>
        <div class="l-menu-inner">
            <div class="l-menu-item" ligeruimenutemid="1" menuitemid="close" jquery16206603060198626224="10">
                <div class="l-menu-item-text">
                    关闭当前页</div>
            </div>
            <div class="l-menu-item" ligeruimenutemid="2" menuitemid="closeother" jquery16206603060198626224="11">
                <div class="l-menu-item-text">
                    关闭其他</div>
            </div>
            <div class="l-menu-item" ligeruimenutemid="3" menuitemid="closeall" jquery16206603060198626224="12">
                <div class="l-menu-item-text">
                    关闭所有</div>
            </div>
            <div class="l-menu-item" ligeruimenutemid="4" menuitemid="reload" jquery16206603060198626224="13">
                <div class="l-menu-item-text">
                    刷新</div>
            </div>
        </div>
    </div>
    <div style="width: 2px; display: none; height: 4px; top: auto; left: auto" class="l-menu-shadow"
        jquery16206603060198626224="8">
    </div>
    <div>
        <div style="z-index: 10000; position: absolute; text-align: center; display: none;
            top: 0px; left: 0px" id="maskLevel">
        </div>
        <div style="z-index: 10001; position: absolute; display: none" id="ym-window">
            <div id="ym-tl" class="ym-tl">
                <div class="ym-tr">
                    <div style="cursor: move" class="ym-tc">
                        <div class="ym-header-text">
                        </div>
                        <div class="ym-header-tools">
                            <div class="Dialog_min" title="最小化">
                                <strong>0</strong></div>
                            <div class="Dialog_max" title="最大化">
                                <strong>1</strong></div>
                            <div class="Dialog_close" title="关闭">
                                <strong>r</strong></div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="ym-ml" class="ym-ml">
                <div class="ym-mr">
                    <div class="ym-mc">
                        <div style="position: relative" class="ym-body">
                        </div>
                    </div>
                </div>
            </div>
            <div id="ym-btnl" class="ym-ml">
                <div class="ym-mr">
                    <div class="ym-btn">
                    </div>
                </div>
            </div>
            <div id="ym-bl" class="ym-bl">
                <div class="ym-br">
                    <div class="ym-bc">
                    </div>
                </div>
            </div>
        </div>
        <div style="z-index: 10000; position: absolute; filter: alpha(opacity=80) progid:DXImageTransform.Microsoft.Blur(pixelradius=2);
            display: none; background: #808080" id="ym-shadow">
        </div>
    </div>
</body>
k 