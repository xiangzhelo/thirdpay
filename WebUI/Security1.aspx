<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Security1.aspx.cs" Inherits="viviAPI.WebUI2015.Security1" %>
<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="footer" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>安全保障 - <%=SiteName%></title>
<meta name="KeyWords" content="API,支付接口,点卡回收,游戏支付 <%=KeyWords%>" />
<meta name="description" content="亿付商务网是全国领先的游戏点卡回收API接口提供商，拥有多条稳定的游戏支付回收接口渠道，包括盛大、骏网、网易、久游、腾讯Q币卡、完美、征途、金山、光宇等一卡通消耗渠道，与多家游戏平台合作，专业提供安全稳定高效的游戏API支付回收接口平台，是您运营网站及游戏的首要合作伙伴。<%=Description%>" />
<link href="/themes/css/skin.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/themes/js/jquery.min.js"></script>
<script type="text/javascript" src="/themes/js/jquery.easing.1.3.js"></script>
<script src="/themes/js/ainatec.js" type="text/javascript"></script>
</head>
<body>
<script src="/themes/js/sub.js" type="text/javascript"></script>
<uc1:header ID="header1" runat="server" showtype="contact" />
<div class="sub_top">
	<h2><img src="/themes/images/news_title.png" width="200" height="50" border="0" alt=""></h2>
	<div class="sub_nav clearbox">
		<ul >
			<li><a href="/Product.aspx" title="支持卡类">支持卡类</a></li><li class="active"><a href="/Security.aspx" title="安全保障">安全保障</a></li><li><a href="/news.aspx?tid=1" title="新闻动态">新闻动态</a></li><li><a href="/solution.aspx" title="帮助中心">帮助中心</a></li><li><a href="/contact.aspx" title="联系我们">联系我们</a></li>
        </ul>
	</div>
</div>
<div style=" background:#31C3D3; height:330px;">
    <div class="w_anquan"><img style=" position:relative;top: -1px;" src="/themes/images/safe_top_bg.png" alt="安全交易保障体系" /></div>
</div>

    <div class="security_bg">
        <div class="w_anquan">
            <div class="security_title_bg"></div>
            <p class="security_title_p" >密码保护</p>
            <p class="security_p">密码保护问题主要用于密码找回时的安全验证机制，建议您尽快绑定。<br/>
            绑定方式：<br/>
            1、登录用户系统 > 安全管理<br/>
            2、选择问题并填写答案<br/>
            3、完成绑定。
</p> 
<div class="security_right"><img src="/themes/images/security_pic01.png" alt="汇付益投网贷" /></div>

        <div class="cl"></div>
        <div style=" margin:80px 0 0 0">
        <div class="security_title_bg"></div>
            <p class="security_title_p" id="safe3">邮箱绑定</p>
            <div class="security_right" style=" margin:60px 0 0 0"><img src="/themes/images/security_pic02.png" alt="风险控制" /></div>
            <p class="security_p" style=" margin:95px 0 0 80px">邮箱绑定主要用于修改交易密码和更换银行卡开户人等重要操作，是非常重要的安全保障机制，建议您尽快绑定。<br/>
            绑定方式：<br/>
            1、注册时使用邮箱注册，注册完成后即可绑定；<br/>
             2、普通注册的用户，填写的QQ号码默认为该QQ账号的邮箱，请注册时您常用的QQ号码。
</p> 


        <div class="cl"></div>
        </div>
            <div style=" margin:80px 0 0 0">
         <div class="security_title_bg"></div>
            <p class="security_title_p" >交易密码</p>
            <p class="security_p">交易密码用于添加银行卡、申请结算时输入的二次密码验证机制，是非常重要的安全保护措施。用户首次登录系统时需要必须设置。建议您不要设置的过于简单，尽量使用字母+数字+符号组成的。</p> 
<div class="security_right"><img src="/themes/images/security_pic03.png" alt="本息垫付" /></div>

        <div class="cl"></div>
        </div>



       </div>
    </div>





<uc1:footer ID="footer1" runat="server" showtype="news" />
</body>
</html>
