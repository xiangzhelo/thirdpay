<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeCode.aspx.cs" Inherits="viviAPI.Gateway2018.WeCode" %>
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
    <div class="top_box_txt"><a href="#">微信支付如何绑定银行卡？</a></div>
  </div>
</div>
<div id="wx_main">
  <div class="main_box">
    <div class="tb_null"></div>
    <div class="h_tb_null"></div>
    <div class="ts_txt">订单提交成功，请您尽快付款！</div>
    <div class="ts_txt_txt">商户订单号：<%=orderid%> &nbsp;&nbsp;请您在提交订单后5分钟完成支付，否则订单会自动取消。</div>
    <div class="h_tb_null"></div>
    <div class="main_box_box">
      <div class="main_box_box_txt">应付金额：<font color="#FF6600"><%=orderAmt%></font> 元</div>
      <div class="main_box_box_txt">此交易委托<%=company_name%>代收款</div>
      <div class="tb_null"></div>
      <div class="zfb_rwm">
        <div style="width:100%; height:48px;"></div>
        <div class="rwm_img"><img src="<%=qrcode_img_url%>" width="249" height="247" alt=""/></div>
      </div>
      <div class="h_tb_null"></div>
      <div class="h_tb_null"></div>
        <div class="main_box_box_input_wx">请使用微信”扫一扫“扫描二维码以完成支付</div>
    </div>
    <div class="h_tb_null"></div>
    <div class="h_tb_null"></div>
    <div class="bottom_txt">支付网关版权所有 2014-2024 备案号：皖B2-20140065</div>
  </div>
</div>
    <div>
        <a href="<%=WxUrl%>" style="display:block ;width:100%;text-align:center;font-size:24px;">微信支付</a>
    </div>
</body>
</html>
