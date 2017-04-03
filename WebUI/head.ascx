<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="head.ascx.cs" Inherits="viviAPI.WebUI7uka.Head" %>
<link href="/assets/src/css/reset.css?201506234" rel="stylesheet" type="text/css" />
    <link href="/assets/src/css/v6/home.css?201506234" rel="stylesheet" type="text/css" />
    <link href="/assets/src/css/v6/base.css?201506234" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/assets/src/lib/jquery_1.10.2.js"></script>
        <script type="text/javascript" src="/assets/src/lib/sea.js"></script>
        <script type="text/javascript">            seajs.use("page/v6/home")</script>
    <!--环迅代码-->
    <style type="text/css">
        @import url(http://www.ips.com.cn/r/cms/www/ips/css/style.css);
    </style>
<div id="page">
            <!--顶部开始-->
            <div class="ui-bg-gary-f1">
                <div class="container part-top-allnav">
                    <div class="fn-left part-top-allnav-l-b">
                        <span class="ui-text-gray">欢迎致电</span>&nbsp;&nbsp;<span class="ui-text-red fn-font-18">0592-6511723</span>
                    </div>
                    <div class="fn-right">
                     </div>
                    <div class="fn-right part-top-allnav-list">
                    </div>
                </div>
            </div>
            <!--顶部end-->
            <!--导航栏-->
            <div id="header" class="ui-bg-white">
                <div class="container">
                    <h1 class="fn-left fn-mr-20 fn-mt-15">
                        <a href="/" title="">
                            <img src="/style/images/logo.png" alt="" style="width: 160px;"></a></h1>
                    <div class="trjcn-title">
                        专业的支付API接口平台</div>
                    <div class="fn-left fn-mt-5">
                    </div>
                    <nav class="fn-right part-nav-all-a fn-clear">
            <ul>
                <li class="current"><a href="/" title="首页">首页</a></li>
                <li><a href="/solution.aspx" title="行业解决方案">行业解决方案</a></li>
                <li><a href="/Access.aspx" title="商业服务号">商业服务号</a></li>
                <li><a href="/news.aspx" title="新闻中心">新闻中心</a></li>
                <li><a href="/service.aspx" title="客服中心">客服中心</a></li>
                <!--<li><a href="" title="">安全保障</a></li>
                <li><a href="" title="">点卡寄售</a></li>-->
                <li><a href="/About.aspx" title="关于我们">关于我们</a></li>
            </ul>
        </nav>
                </div>
            </div>
            <!--导航栏end-->
            <div id="content">
                <div id="focus">
                    <div class="container">
                        <span class="prev" style="opacity: 0; display: inline;"></span><span class="next"
                            style="opacity: 0; display: inline;"></span>
                    </div>
                    <div class="focus-images">
                        <ul style="position: relative; width: 1903px; height: 400px;">
                            <li style="position: absolute; width: 1903px; left: 0px; top: 0px; display: none;
                                background: url(/trj/401_1440669373.jpg) 50% 0px no-repeat rgb(238, 238, 238);">
                                <a target="_blank" href="" rel="nofollow"></a></li>
                            <li style="position: absolute; width: 1903px; left: 0px; top: 0px; display: none;
                                background: url(/trj/401_1440665392.jpg) 50% 0px no-repeat rgb(238, 238, 238);">
                                <a target="_blank" href="" rel="nofollow"></a></li>
                            <li style="position: absolute; width: 1903px; left: 0px; top: 0px; display: none;
                                background: url(/trj/401_1439956228.jpg) 50% 0px no-repeat rgb(238, 238, 238);">
                                <a target="_blank" href="" rel="nofollow"></a></li>
                            <li style="position: absolute; width: 1903px; left: 0px; top: 0px; display: list-item;
                                opacity: 0.0441; background: url(/trj/401_1438827044.jpg) 50% 0px no-repeat rgb(238, 238, 238);">
                                <a target="_blank" href="" rel="nofollow"></a></li>
                            <li style="position: absolute; width: 1903px; left: 0px; top: 0px; display: list-item;
                                opacity: 0.9559; background: url(/trj/401_1437729603.jpg) 50% 0px no-repeat rgb(238, 238, 238);">
                                <a target="_blank" href="" rel="nofollow"></a></li>
                        </ul>
                    </div>
                    <div class="focus-nav">
                        <ul>
                            <li class="">1</li><li class="">2</li><li class="">3</li><li class="">4</li><li class="on">
                                5</li></ul>
                    </div>
                </div>
            </div>
        </div>

<!-- tooltip -->
<script language="JavaScript" type="text/javascript" src="/web_js/spinners.min.js"></script>
<script language="JavaScript" type="text/javascript" src="/web_js/tipped_min.js"></script>
<!--[if lt IE 9]>
  <script type="text/javascript" src="/web_js/excanvas.js"></script>
<![endif]-->
<script type="text/javascript" language="javascript" src="/web_js/cookie.js"></script>

<style> 
 /*
这段只应用到弹出注册窗口
  */
#userreg_dialog_content #regformdiv{
    margin-left:200px;
    border-left:1px solid #efefef;
 }
#user_dlg_footer{
padding-top:1em;
}
</style>

<script src="/web_js/easydialog.min.js" type="text/javascript"></script>
<script language="JavaScript" type="text/javascript" src="/web_js/jquery.validate.min.js"></script>
<!--script language="JavaScript" type="text/javascript" src="/js/bootstrap/ext/bootbox.min.js"></script-->
<!--script type="text/javascript" src="/js/bootstrap/ext/zi/zmodal.js"></script-->
<script language="JavaScript" type="text/javascript" src="/web_js/user_common.js"></script>     
<script type="text/javascript" src="/web_js/check_rule.js"></script>

<script type="text/javascript">

    var D$ = function() {
        return document.getElementById(arguments[0]);
    };

    var btnFn = function(e) {
        alert(e.target);
        return false;
    };

    /*D$('userlogin').onclick = function() {
        easyDialog.open({
            container: {
                header: '商户登录',
                content: '<iframe src="userlogin.aspx" width="500" height="310" scrolling="no" frameborder="0"></iframe>'
            },
            drag: false
        });
    };

    D$('userreg').onclick = function() {
        easyDialog.open({
            container: {
                header: '商户签约申请',
                content: '<iframe src="userreg.aspx" width="720" height="480" scrolling="no" frameborder="0"></iframe>'
            },
            //            drag: false
            fixed: false
        });
    };*/
    //Sudu.alert("json.errors.othererr");
</script>



