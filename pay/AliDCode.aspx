<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AliDCode.aspx.cs" Inherits="viviAPI.Gateway2018.AliDCode" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset="gb2312" />
<META name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0">
<TITLE>支付宝扫码支付 - 网上支付 安全快速！</TITLE>
<LINK rel="stylesheet" href="Static/style/excashier.base-2.3-mobileBarCode.css" charset="utf-8" media="all">
<LINK rel="stylesheet" href="Static/style/alice.components.security-core-2.1.css" charset="utf-8" media="all">
</HEAD>
<BODY>
    <form id="Form1" runat="server">
    <asp:HiddenField ID="hforderid" runat="server" />
<DIV class="topbar">
<DIV class="topbar-wrap fn-clear">
<A class="topbar-link-last" href="http://help.alipay.com/lab/help_detail.htm?help_id=258086" target="_blank" seed="goToHelp">如何使用?</A><SPAN 
class="topbar-link-first">你好，欢迎使用支付宝扫码支付！</SPAN></DIV></DIV>

<DIV id="container">

<DIV id="header">
<H1 class="logo"><SPAN class="link-logo"></SPAN></H1></DIV>

<DIV id="content" class="fn-clear">
<DIV id="J_order" data-url="" data-mode="SYNC" data-module="excashier/login/1.0.20140701/orderDetail">


<DIV id="order" class="order order-bow" data-role="order">
<DIV class="orderDetail-base" data-role="J_orderDetailBase">
<DIV class="order-extand-explain fn-clear">
	<SPAN class="ico-bow"></SPAN>
	<SPAN>您正在使用支付宝扫码支付功能</SPAN>
</DIV>
<DIV class="commodity-message-row">
	<SPAN class="first long-content">储值服务</SPAN>
	<SPAN class="second short-content">收款方：优卡联盟</SPAN>
</DIV>
	<SPAN id="J_basePriceArea" class="payAmount-area"><STRONG class=" amount-font-22 "><%=orderAmt%></STRONG> 元</SPAN></DIV>
</DIV>


<DIV class="login-switchable-container" style="text-align:center;padding-top:20px;">
  <p class="xbox-title-text">扫 码 支 付</p>
 <asp:Literal ID="litimg" runat="server"></asp:Literal>
    <p class="lt-text ft-yh"><a href="" class="user-login-account" seed="toMobileLogin">使用支付宝钱包扫码完成付款</a></p>
              <div><a href="https://mobile.alipay.com/index.htm" data-boxUrl="https://cmspromo.alipay.com/down/index.htm" data-role="dl-app" target="_blank"
                    seed="qr-pay-download">支付宝钱包下载</a> ｜ <a
                    href="http://help.alipay.com/lab/help_detail.htm?help_id=382484" target="_blank" seed="qr-pay-help">如何使用？</a>
            </div>
</DIV>


</DIV></DIV>


<DIV id="footer">
<STYLE>
.copyright,.copyright a,.copyright a:hover{color:#808080;}
</STYLE>
<DIV class="copyright">支付宝版权所有 2004-2014 <A href="http://www.alipay.com/" target="_blank">ICP证：浙B2-20120045</A></DIV>
</DIV>
</DIV>
<DIV id="partner"><IMG alt="合作机构" src="Static/images/2R3cKfrKqS.png"></DIV>
    </form>
</BODY></HTML>