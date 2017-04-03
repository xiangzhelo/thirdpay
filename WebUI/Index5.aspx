<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/site.Master" CodeBehind="Index5.aspx.cs" Inherits="viviAPI.WebUI2015.Index5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <div class="header" id="lunbo">
        <div class="images cl">
            <a href="#" target="_blank" style="display: none;">
                <img src="style1/images/sc1.jpg" />
            </a>
            <a href="#" target="_blank" style="display: none;">
                <img src="style1/images/sc2.jpg" />
            </a>
            <a href="#" target="_blank" style="display:none;">
                <img src="style1/images/sc3.jpg" />
            </a>
            <a href="#" target="_blank" style="display:none;">
                <img src="style1/images/sc4.jpg" />
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
            <div class="loginnr">
                <form name="form1" method="post" action="/index.aspx?m=login" id="form1">
                <header>商户登录</header>
                <div><input type="text" name="username" id="username" /></div>
                <div><input type="password" name="password" placeholder="商户密码" /></div>
                <div class="yzm cl"><input type="text" name="imycode" id="yanzm" /><span><img alt="换一个" title="换一个" src="/vercode.aspx" onclick="this.src = '/vercode.aspx?t='+ Math.round().toString()" /></span></div>
                <div><input type="submit" value="" class="loginbtn" /></div>
                <div><a href="regedituser.aspx" style="float:left;">立即注册</a><a href="FindPwd.aspx">忘记密码？</a></div>
                </form>
            </div>
        </div>
    </div>
    <div class="content cl">
        <a class="item" href="#">
            <img src="style1/images/t_one.png" />
            <h4 class="q-faq-text">如何接入</h4>
        </a>
        <a class="item" href="#">
            <img src="style1/images/t_two.png" />
            <h4 class="q-faq-text">签约流程</h4>
        </a>
        <a class="item" href="#">
            <img src="style1/images/t_three.png" />
            <h4 class="q-faq-text">产品服务</h4>
        </a>
    </div>
    <div class="bank cl">
        <h4>合作金融机构</h4>
        <img class="bank-logo" src="style1/images/bank/bank-yinlian.png">
        <img class="bank-logo" src="style1/images/bank/bank-zhongguo.png">
        <img class="bank-logo" src="style1/images/bank/bank-gongshang.png">
        <img class="bank-logo" src="style1/images/bank/bank-jianshe.png">
        <img class="bank-logo" src="style1/images/bank/bank-nongye.png">
        <img class="bank-logo" src="style1/images/bank/bank-zhaoshang.png">
        <img class="bank-logo" src="style1/images/bank/bank-jiaotong.png">
        <img class="bank-logo" src="style1/images/bank/bank-guangda.png">
        <img class="bank-logo" src="style1/images/bank/bank-zhongxin.png">
        <img class="bank-logo" src="style1/images/bank/bank-bohai.png">
        <img class="bank-logo" src="style1/images/bank/bank-xingye.png">
        <img class="bank-logo" src="style1/images/bank/bank-mingsheng.png">
    </div>
    <div class="contact cl">
        <div class="pull-left">
            <h5><b>客服热线</b> （周一至周五9:00-17:30，节假日除外）</h5>
            <h3><span>0731-87859768</span> <a href="#"></a></h3>
        </div>
        <div class="pull-right qr-code">
            <img src="style1/images/ewm.png" />
            <div>扫一扫</div>
        </div>
    </div>
</asp:Content>