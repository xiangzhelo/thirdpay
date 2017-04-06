﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="viviAPI.WebUI7uka.usermodule.account.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>亿付商务网-中国互联网在线收款服务商</title>
    <meta name="description" content="">
    <link href="/ico/favicon.ico" rel="shortcut icon" type="image/ico">
    <link rel="stylesheet" href="/usermodule/css/frame.css" />

    <script src="/usermodule/js/jquery-1.8.1.min.js"></script>
    <script src="/usermodule/js/jquery.hoverdelay.js"></script>
    <script src="/usermodule/js/frame.js"></script>
    <script type="text/javascript" src="../js/jquery.artDialog.js"></script>
    <script src="/usermodule/js/jquery.qtip.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/usermodule/js/hermes.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="position: absolute; left: -9999em; top: 299px; width: auto; z-index: 1987;"
        class="aui_state_focus">
        <div class="aui_outer">
            <table class="aui_border">
                <tbody>
                    <tr>
                        <td class="aui_nw">
                        </td>
                        <td class="aui_n">
                        </td>
                        <td class="aui_ne">
                        </td>
                    </tr>
                    <tr>
                        <td class="aui_w">
                        </td>
                        <td class="aui_c">
                            <div class="aui_inner">
                                <table class="aui_dialog">
                                    <tbody>
                                        <tr>
                                            <td colspan="2" class="aui_header">
                                                <div class="aui_titleBar">
                                                    <div class="aui_title" style="cursor: move;">
                                                        消息</div>
                                                    <a class="aui_close" href="javascript:/*artDialog*/;" style="">×</a></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="aui_icon" style="display: none;">
                                                <div class="aui_iconBg" style="background-image: none; background-position: initial initial;
                                                    background-repeat: initial initial;">
                                                </div>
                                            </td>
                                            <td class="aui_main" style="width: auto; height: auto;">
                                                <div class="aui_content" style="padding: 20px 25px;">
                                                    <div class="aui_loading">
                                                        <span>loading..</span></div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="aui_footer">
                                                <div class="aui_buttons" style="display: none;">
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </td>
                        <td class="aui_e">
                        </td>
                    </tr>
                    <tr>
                        <td class="aui_sw">
                        </td>
                        <td class="aui_s">
                        </td>
                        <td class="aui_se" style="cursor: se-resize;">
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div id="frame-wrapper">
        <div class="top-warning">
        </div>
        <!-- header -->
        <div id="frame-header">
            <div id="logo">
                <a href="/usermodule/account/index.aspx">
                    <img src="../images/logo2.gif" alt=""></a></div>
            <ul id="headerNav">
                <li class="current"><a href="/usermodule/account/index.aspx">我的账户</a></li>
                <li class=""><a href="/usermodule/order/index.aspx">订单管理</a></li>
                <li class=""><a href="/usermodule/product/index.aspx">商品管理</a></li>
                <li class=""><a href="/usermodule/settlement/index.aspx">结算提现</a></li>
                <li class=""><a href="/usermodule/recharg/index.aspx">账户充值</a></li>
                <li class=""><a href="/usermodule/behalf/index.aspx">对私代发</a></li>
                 <li><a href="/usermodule/behalf/shouka.aspx">点卡消耗</a></li>
            </ul>
            <ul id="header-info">
                <li class="item user-info hoverToggle-wrapper"><a href="javascript:void(0);" class="hoverToggle-trigger">
                    <%=getnm %>&nbsp;<i class="icon icon-chevron-down"></i> </a>
                    <div class="hoverToggle hoverToggle-left">
                        <div class="mtitle">
                            &nbsp;</div>
                        <ul class="mbody top-user-list">
                            <li class="title">
                                <label for="">
                                    编号：</label>
                                <span class="label label-info">
                                    <%=getnid %></span> </li>
                        </ul>
                    </div>
                </li>
                <li class="item return-desktop"><a href="/usermodule/account/index.aspx" data-hasqtip="0"
                    oldtitle="返回首页" title="返回首页"><i class="icon-top icon-top-desktop"></i></a></li>
                <li class="item messages-top"><a href="/usermodule/account/messages.aspx" target="mainframe"
                    class="link-message" title="未读信息 <%=getmsgcount %> 条"><span id="msghead" class="badge badge-error">
                        <%=getmsgcount %></span><i class="icon-top icon-top-message"></i> </a></li>
                <li class="item top-exit last-item"><a href="/usermodule/logout.aspx" data-hasqtip="3"
                    oldtitle="退出" title="退出"><i class="icon-top icon-top-exit" title="退出"></i></a>
                </li>
            </ul>
        </div>
        <!-- sider & main -->
        <div id="frame-content">
            <div class="mainbar">
                <div class="module" style="height: 823px;">
                    <iframe src="account.aspx" marginwidth="0" marginheight="0" frameborder="0" scrolling="auto"
                        class="mainframe" id="mainframe" name="mainframe" style="height: 823px;"></iframe>
                </div>
            </div>
            <div class="sidebar">
                <div class="module">
                    <a href="javascript:void(0);" class="sideBar-trigger" style="height: 823px;"><span
                        style="margin-top: 396.5px;">&nbsp;</span></a>
                    <div class="mbody" style="height: 793px;">
                        <div class="sideNav-content" style="height: 759px;">
                            <!--文字侧边导航-->
                            <ul id="side-menu" class="text-nav">
                                <li class="current"><a href="account.aspx" target="mainframe"><i class="icon-nav"></i>账户首页</a></li>
                                <li><a href="info.aspx" target="mainframe"><i class="icon-nav"></i>基本信息</a></li>
                                <li><a href="safety.aspx" target="mainframe"><i class="icon-nav"></i>安全设置</a></li>
                                <li><a href="costinfo.aspx" target="mainframe"><i class="icon-nav"></i>结算信息</a></li>
                                <li><a href="api.aspx" target="mainframe"><i class="icon-nav"></i>API接入</a></li>
                                <li><a href="messages.aspx" target="mainframe"><i class="icon-nav"></i>站内消息</a></li>
                                <li><a href="feedbacks.aspx" target="mainframe"><i class="icon-nav"></i>留言反馈</a></li>
                                <li><a href="/usermodule/quota/quotarecharge.aspx" target="mainframe"><i class="icon-nav"></i>额度转换</a></li>
                                <li><a href="/usermodule/quota/quotaorder.aspx" target="mainframe"><i class="icon-nav"></i>额度转换记录</a></li>
                                <li><a href="/usermodule/quota/quotapayrate.aspx" target="mainframe"><i class="icon-nav"></i>额度转换费率</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- footer -->
        <div id="frame-footer">
            <ul class="footer-info">
                <li class="item hoverToggle-wrapper"><a href="javascript:void(0);" class="trigger"><i
                    class="icon icon-user"></i>客服</a>
                    <div class="hoverToggle hoverToggle-bottomRight service-popup">
                        <div class="mbody">
                            <dl>
                                <dt>在线客服：</dt>
                                <dd>
                                    <a target="_blank" href="/service.aspx">
                                        进入客服中心
                                    </a>
                                </dd>
                            </dl>
                            <dl>
                                <dt>客服电话：</dt>
                                <dd>
                                    <b class="red"> 0592-6511723</b></dd>
                            </dl>
                            <p class="tcenter">
                                <a target="mainframe" href="/usermodule/account/feedbacks.aspx" class="btn">问题反馈</a></p>
                        </div>
                        <div class="mfooter">
                            &nbsp;</div>
                    </div>
                </li>
                <!--需要通知默认显示时，在hoverToggle-wrapper后加class="active"-->
                <li class="item clickToggle-wrapper last-item"><a href="javascript:void(0);" class="trigger">
                    <i class="icon icon-comment"></i>通知 <span id="msgfoot2" class="badge badge-error">
                        <%=getmsgcount %></span></a>
                    <div class="clickToggle hoverToggle-bottomLeft notify-popup">
                        <a href="javascript:void(0);" class="close">×</a>
                        <div class="mbody">
                            <div class="alert">
                                <a href="/usermodule/account/messages.aspx" target="mainframe">未读信息</a> <span id="msgfoot"
                                    class="badge badge-error">
                                    <%=getmsgcount %></span>
                            </div>
                        </div>
                        <div class="mfooter">
                            &nbsp;</div>
                    </div>
                </li>
            </ul>
        </div>
    </div>

    <script type="text/javascript">

        $(function() {
            //侧边导航tab点击切换
            $("#side-menu li a").click(function() {
                $("#side-menu li").removeClass("current");
                jQuery(this).parent("li").addClass("current");
            });
        });

        // 探测屏幕分辨率以更改布局
        jQuery(window).bind("resize", changeLayout);
        function changeLayout(e) {
            var winHeight = jQuery(window).height();
            var winWidth = jQuery(window).width();
            if (winWidth < 1000) {
                jQuery('#header-info').addClass("info-lite");
                jQuery('#topSearch input').addClass("input-small");
            } else {
                jQuery('#header-info').removeClass("info-lite");
                jQuery('#topSearch input').removeClass("input-small");
            }
        }
        jQuery(document).ready(function() {
            var winWidth = jQuery(window).width();
            if (winWidth < 1000) {
                jQuery('#header-info').addClass("info-lite");
                jQuery('#topSearch input').addClass("input-small");
            } else {
                jQuery('#header-info').removeClass("info-lite");
                jQuery('#topSearch input').removeClass("input-small");
            }
            unReadNum();
        });

        //弹层方法
        function dialogPOP(url, title1, width1, height1) {
            dialog = art.dialog({
                title: title1,
                width: width1,
                height: height1,
                lock: true,
                background: '#000', // 背景色
                opacity: 0.67	// 透明度
            });
            $.ajax({
                url: url,
                dataType: 'html',
                cache: false,
                success: function(data) {
                    dialog.content(data);
                },
                cache: false
            });
        }
        //关闭弹层
        function closeDialog() {
            try {
                if (timer) {
                    clearInterval(timer);
                }
            } catch (err) { };
            if (dialog) {
                dialog.close();
            }
        }

        // 未读消息数量
        function unReadNum() {
            $.getJSON('/message/unRead', function(json) {
                if (json.success) {
                    $("#msghead").html(json.data.unread);
                    $("#msghead").parent().attr("title", "未读信息 " + json.data.unread + " 条");
                    $("#msgfoot").html(json.data.unread);
                    $("#msgfoot2").html(json.data.unread);
                } else {
                    $("#msg").html("0");
                }
            }
        )
        }
</script>

    </form>
</body>
</html>
