<%@ Page Title="" Language="C#" MasterPageFile="~/longbao/site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="viviAPI.WebUI2015.longbao.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="header" id="lunbo">
        <div class="images cl">
            <a href="#" target="_blank" style="display: none;">
                <img src="/longbao/style/images/sc1.jpg" />
            </a>
            <a href="#" target="_blank" style="display: none;">
                <img src="/longbao/style/images/sc2.jpg" />
            </a>
            <a href="#" target="_blank" style="display:none;">
                <img src="/longbao/style/images/sc3.jpg" />
            </a>
            <a href="#" target="_blank" style="display:none;">
                <img src="/longbao/style/images/sc4.jpg" />
            </a>
        </div>
        <div class="indicators">
            <a href="javascript:;" class="selected"></a>
            <a href="javascript:;"></a>
            <a href="javascript:;"></a>
            <a href="javascript:;"></a>
        </div>
        <div class="login" id="login">
            <div class="loginbg"></div>
            <div class="tabs">
                <a href="javascript:;" class="selected">账号登录</a>
                <b>|</b>
                <a href="javascript:;">微信登录</a>
            </div>
            <div class="loginnr">
                <form name="form1" method="post" action="/longbao/index.aspx?m=login" id="form1">
                <div><input type="text" name="username" id="username" placeholder="请输入用户名" /></div>
                <div><input type="password" name="password" placeholder="请输入密码" /></div>
                <div class="yzm cl"><input type="text" name="imycode" id="yanzm" placeholder="请输入图形码" /><span><img alt="换一个" title="换一个" src="/vercode.aspx" onclick="this.src = '/vercode.aspx?t='+ Math.round().toString()" /></span></div>
                <div class="cl"><a href="FindPwd.aspx">忘记密码？</a><a href="Regedit.aspx">立即注册</a></div>
                <div><input type="submit" value="" class="loginbtn" /></div>
                </form>
            </div>
            <div class="loginnr ewm" style="display:none;">
                <img src="/longbao/style/images/ewm.png" />
                <span>请使用微信扫描二维码登录</span>
            </div>
        </div>
    </div>
    <div class="content cl">
        <a class="item" href="javascript:;">
            <img src="/longbao/style/images/square1.png" />
        </a>
        <a class="item" href="javascript:;">
            <img src="/longbao/style/images/square2.png" />
        </a>
        <a class="item" href="javascript:;">
            <img src="/longbao/style/images/square3.png" />
        </a>
        <a class="item" href="javascript:;">
            <img src="/longbao/style/images/square4.png" />
        </a>
    </div>
    <div class="jieru cl">
        <div class="head">
            <span><b>微信支付服务商</b></span>
            <hr />
        </div>
        <p>微信支付官方许可,标准规范的服务，助力商户快速接入微信支付,为有需求的商户 提供微信支付接入、交易、营销等全生态链服务!</p>
        <div class="jieruitem">
            <a class="item" href="javascript:;">
                <i class="icon1"></i>
                <span>公众号支付</span>
                <b>在APP中，调起微信进行APP支付</b>
                <strong>我要接入</strong>
            </a>
            <a class="item" href="javascript:;">
                <i class="icon2"></i>
                <span>APP支付</span>
                <b>在微信内的商家页面上完成公众号支付</b>
                <strong>我要接入</strong>
            </a>
            <a class="item" href="javascript:;">
                <i class="icon3"></i>
                <span>扫码支付</span>
                <b>扫描二维码(包含PC网站)进行扫码支付</b>
                <strong>我要接入</strong>
            </a>
            <a class="item" href="javascript:;">
                <i class="icon4"></i>
                <span>刷卡支付</span>
                <b>用户展示条码，商户扫描完成刷卡支付</b>
                <strong>我要接入</strong>
            </a>
        </div>
    </div>
</asp:Content>
