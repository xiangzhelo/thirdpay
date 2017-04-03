<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AliDCode.aspx.cs" Inherits="viviAPI.Gateway2018.AliDCode" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/static/css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div id="zfb_top">
  <div class="top_box">
    <div class="top_box_txt"><a href="#">您好，欢迎使用支付宝扫码支付</a> | <a href="#">如何使用</a>?</div>
  </div>
</div>
<div id="zfb_main">
  <div class="main_box">
    <div class="tb_null"></div>
    <div class="top_txt_1">
      <div class="top_txt_1_int"><img src="/static/images/zfb_bz.jpg" width="46" height="57"/></div>
      <div class="top_txt_1_txt">您正在使用支付宝扫码功能</div>
    </div>
    <div class="h_tb_null"></div>
    <div class="main_box_box">
      <div class="main_box_box_txt">应付金额：<font color="#FF6600"><%=orderAmt%></font> 元</div>
      <div class="main_box_box_txt">扫码支付</div>
      <div class="tb_null"></div>
      <div class="zfb_rwm">
        <div style="width:100%; height:48px;"></div>
        <div class="rwm_img"><img src="<%=qrcode_img_url%>" width="236" height="236" /></div>
      </div>
      <div class="tb_null"></div>
      <div class="main_box_box_txt">使用支付宝钱包扫码完成付款</div>
      <div class="main_box_box_txt2">
        <div class="main_box_box_input">我已完成支付</div>
      </div>
      <div class="main_box_box_txt">支付宝钱包下载 | 如何使用？</div>
      <div class="tb_null"></div>
      <div class="main_box_box_txt">支付宝版权所有 2014-2024 ICP证：浙B2-20140045</div>
    </div>
  </div>
</div>
</body>
</html>
