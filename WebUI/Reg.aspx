<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reg.aspx.cs" Inherits="viviAPI.WebUI2015.Reg"
    Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8">
    <title>
        <%=SiteName%></title>
    <meta name="keywords" content="资本、资本机构、资本投资、资本融资">
    <meta name="description" content="资本机构入驻，经过实地考察过的优质资金方和资本机构聚集地，为您定期推荐优质项目，可以免费约谈资金方，免费参加线下沙龙路演活动，想要快速寻找到优质项目信息，就入驻资本机构频道。">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="renderer" content="webkit">
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no">
    <meta name="format-detection" content="telephone=no">
    <link href="/assets/src/css/reset.css" rel="stylesheet" type="text/css" />
    <link href="/assets/src/css/v6/base.css" rel="stylesheet" type="text/css" />
    <link href="/trj/money.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 10]>
	<script src="/assets/src/lib/html5shiv.js"></script>
	<![endif]-->
    <script type="text/javascript" src="/assets/src/lib/jquery_1.10.2.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            var okico = "";
            var errico = "";
            var ldico = "";

            $("#T_reg_mobile_code2").click(function () {
                var phoneno = $("#J_mobile").val();
                if (phoneno == null || phoneno == "") {
                    $('#J_mobile_info').html('<i class="icon-money-tips"></i>请输入邮箱地址');
                    return;
                }
                $("#mobilecode").html(ldico + "正在发送验证码");
                $.get("/usermodule/ws/SendEmailVerifyCode.ashx?t=" + Math.random(), { phone: phoneno },
                    function (data, textStatus) {
                        if (data == "true") {
                            $("#mobilecode").html("验证码发送成功!");
                        } else {
                            $("#mobilecode").css({
                                color: "red"
                            });
                            $("#mobilecode").html(data + "");
                        }
                    });
            });
            $("#btnstep1").click(function () {
                var username = $("#newfullname").val();
                var tel = $("#J_mobile").val();
                var emailcode = $("#J_mobile_code").val();
                if (username == null || username == "") {
                    $('#contact_name').html('<i class="icon-money-tips"></i>请输入真实姓名');
                    return;
                }
                if (tel == null || tel == "") {
                    $('#J_mobile').html('<i class="icon-money-tips"></i>请输入邮箱地址');
                    return;
                }
                $.get("/usermodule/ws/reg.ashx?t=" + Math.random(), { email: tel, code: emailcode },
                    function (data, textStatus) {
                        if (data > 0) {
                            $("#step1").hide();
                            $("#step2").show();
                        } else {
                            $("#mobilecode").html("验证码错误！");
                        }
                    });

            }
            );
            
        }); 
    </script>
    <script language="javascript">
        function CheckForm() {
                var username = $("#newfullname").val();
                var email = $("#J_mobile").val();
                var emailcode = $("#J_mobile_code").val();
                var newusername = $("#newusername").val();
                var  password1= $("#password1").val();
                var password2 = $("#password2").val();
                var  tel= $("#newemail").val();
                var qq = $("#newqq").val();
                var  vcode= $("#txtvcode").val();
                if (username == null || username == "") {
                    $('#contact_name').html('<i class="icon-money-tips"></i>请输入真实姓名');
                    
                    return false;
                }
                if (email == null || email == "") {
                    $('#J_mobile').html('<i class="icon-money-tips"></i>请输入邮箱地址');
                    return false;
                }
                if ( newusername== null ||  newusername== "") {
                    $('#Span1').html('<i class="icon-money-tips"></i>请输入用户名');
                    return false;
                }
                if ( password1== null || password1 == "") {
                    $('#Span2').html('<i class="icon-money-tips"></i>请输入密码');
                    return false;
                }
                if ( password2== null ||  password2== "") {
                    $('#Span3').html('<i class="icon-money-tips"></i>请再次输入密码');
                    return false;
                }
                if ( tel== null ||  tel== "") {
                    $('#Span4').html('<i class="icon-money-tips"></i>请输入您的手机号');
                    return false;
                }
                if ( qq== null ||  qq== "") {
                    $('#Span5').html('<i class="icon-money-tips"></i>请输入QQ号码');
                    return false;
                }
                if (vcode == null || vcode == "") {
                    $('#Span6').html('<i class="icon-money-tips"></i>请输入验证码');
                    return false;
                }
                return true;
            }
        function refreshValidateCode(_id, url) {
            document.getElementById(_id).src = url + "?date=" + new Date();
        }
        
    </script>
</head>
<body>
<form id="form1" runat="server">
    <div id="page">
        <header id="header">
            <div class="container">
                <h1 class="logo">
                	<a href="/" title="">
                		<img src="http://www.trjcn.com.cn/assets/src/images/v6/money/logo.png" alt="" height="40" width="108">
                	</a>
                </h1>
                <div class="sub-logo"><i class="icon-sub-logo"></i>资本机构免费入驻</div>
                <dl class="hotline">
                    <dt><i class="icon-hotline"></i>全国服务热线</dt>
                    <dd>400-858-9000</dd>
                </dl>
            </div>
        </header>
        <div id="content">
            <div class="container">
                <div class="gg fn-mb-20">
                    <img src="http://www.trjcn.com.cn/assets/src/images/v6/money/banner.jpg"></div>
                <div class="money-box fn-clear" id="step1">
                    <div class="money-step money-step-01">
                        <div class="money-step-line">
                            <span></span>
                        </div>
                        <ul class="money-step-group">
                            <li class="money-step-item"><i class="icon-money-step-01"></i>账户<span>申请</span>
                            </li>
                            <li class="money-step-item"><i class="icon-money-step-02"></i><span>填写信息</span>
                            </li>
                            <li class="money-step-item"><i class="icon-money-step-03"></i><span>联系客服审核</span>
                            </li>
                        </ul>
                    </div>
                    <%--<form method="post" id="J_rzsq">--%>
                    <div class="money-form fn-clear">
                        <div class="money-form-item">
                            <label class="money-form-label">
                                <i class="money-form-required">*</i>联系人姓名：</label>
                            <span class="money-form-value">
                                <input name="contact_name" type="text" runat="server"  id="newfullname" class="money-form-text" maxlength="30" />
                                <span id="contact_name" runat="server"  class="money-form-tips"></span></span>
                        </div>
                        <div class="money-form-item">
                            <label class="money-form-label">
                                <i class="money-form-required">*</i>邮箱地址：</label>
                            <span class="money-form-value">
                                <input id="J_mobile" name="mobile" runat="server"  type="text" class="money-form-text" maxlength="50" />
                                <span id="J_mobile_info" runat="server" class="money-form-tips"></span></span>
                        </div>
                        <div class="money-form-item">
                            <label class="money-form-label">
                                <i class="money-form-required">*</i>邮箱验证码：</label>
                            <span class="money-form-value">
                                <input name="mobilecode" type="text" runat="server"  class="money-form-captcha" maxlength="8" id="J_mobile_code" />
                                <input type="button" class="btn-money-captcha btn-captcha" id="T_reg_mobile_code2"
                                    onclick="return false" runat="server" value="获取验证码" />
                                <span id="mobilecode" runat="server"  class="money-form-tips"></span></span>
                        </div>
                        <div class="money-form-item">
                            <input class="btn-money-submit" id="btnstep1" type="button" value="下一步">
                        </div>
                    </div>
                    <%--</form>--%>
                </div>
                <div class="money-box fn-clear" id="step2" style="display:none;">
                    <div class="money-step money-step-02">
                        <div class="money-step-line">
                            <span></span>
                        </div>
                        <ul class="money-step-group">
                            <li class="money-step-item"><i class="icon-money-step-01"></i>账户<span>申请</span>
                            </li>
                            <li class="money-step-item"><i class="icon-money-step-02"></i><span>填写信息</span>
                            </li>
                            <li class="money-step-item"><i class="icon-money-step-03"></i><span>联系客服审核</span>
                            </li>
                        </ul>
                    </div>
                    
                    <div class="money-form fn-clear">
                        <div class="money-form-item">
                            <label class="money-form-label">
                                <i class="money-form-required">*</i>登录帐号：</label>
                            <span class="money-form-value">
                                <input name="contact_name" type="text" id="newusername" runat="server" class="money-form-text" maxlength="30" />
                                <span id="Span1" runat="server" class="money-form-tips"></span></span>
                        </div>
                        <div class="money-form-item">
                            <label class="money-form-label">
                                <i class="money-form-required">*</i>登录密码：</label>
                            <span class="money-form-value">
                                <input name="mobile" type="password" id="password1"  runat="server" class="money-form-text" />
                                <span id="Span2" runat="server"  class="money-form-tips"></span></span>
                        </div>
                        <div class="money-form-item">
                            <label class="money-form-label">
                                <i class="money-form-required">*</i>重复密码：</label>
                            <span class="money-form-value">
                                <input name="mobile" type="password" id="password2" runat="server"  class="money-form-text"  />
                                <span id="Span3"  runat="server" class="money-form-tips"></span></span>
                        </div>
                        <div class="money-form-item">
                            <label class="money-form-label">
                                <i class="money-form-required">*</i>手机号：</label>
                            <span class="money-form-value">
                                <input  name="mobile" type="text" id="newemail" runat="server" class="money-form-text" />
                                <span id="Span4" runat="server"  class="money-form-tips"></span></span>
                        </div>
                        <div class="money-form-item">
                            <label class="money-form-label">
                                <i class="money-form-required">*</i>联系QQ：</label>
                            <span class="money-form-value">
                                <input  name="mobile" type="text"  id="newqq"  runat="server" class="money-form-text" maxlength="11" />
                                <span id="Span5" runat="server"  class="money-form-tips"></span></span>
                        </div>
                        <div class="money-form-item">
                            <label class="money-form-label">
                                <i class="money-form-required">*</i>验证码：</label>
                            <span class="money-form-value">
                                <input name="mobile" type="text" id="txtvcode" runat="server" class="money-form-text" maxlength="11" /><img id="imgValidateCode" src="/vercode.aspx" onclick="refreshValidateCode('imgValidateCode','/vercode.aspx');"" />
                                <span id="Span6" runat="server"  class="money-form-tips"></span></span>
                        </div>
                        
                        <div class="money-form-item">
                            <input class="btn-money-submit" id="btnstep2" onclick="if(!CheckForm()) return false;" runat="server" onserverclick="iBtnSubmit1_Click" type="button" value="完成注册">
                            <span id="Span7" runat="server" class="money-form-tips"></span>
                        </div>
                    </div>
                    
                </div>
                <div class="part-treatment-list">
                    <article>
                            <aside>全程<br/>免费</aside>
                            <section>
                                <p>免费开户体验</p>
                                <p>免费参加沙龙路演活动</p>
                                <p>免费获得机构及投资人展厅</p>
                            </section>
                      </article>
                    <article>
                            <aside>优质<br/>高效</aside>
                            <section>
                                <p>定期推荐优质项目</p>
                                <p>投递项目严格匹配，排除骚扰</p>
                                <p>系统自动邀请匹配项目进行投递</p>
                            </section>
                      </article>
                </div>
                <div class="enterprise-in-list fn-mt-20">
                    <h5>
                        他们已经入驻</h5>
                    <div>
                        <ul>
                            <li>
                                <img src="http://www.trjcn.com.cn/assets/src/images/zt/certification/pic280-1.jpg"
                                    alt="经纬中国"></li>
                            <li>
                                <img src="http://www.trjcn.com.cn/assets/src/images/zt/certification/pic280-2.jpg"
                                    alt="IDG资本"></li>
                            <li>
                                <img src="http://www.trjcn.com.cn/assets/src/images/zt/certification/pic280-3.jpg"
                                    alt="九鼎投资"></li>
                            <li>
                                <img src="http://www.trjcn.com.cn/assets/src/images/zt/certification/pic280-4.jpg"
                                    alt="如山创投"></li>
                            <li>
                                <img src="http://www.trjcn.com.cn/assets/src/images/zt/certification/pic280-5.jpg"
                                    alt="中国投资集团"></li>
                            <li>
                                <img src="http://www.trjcn.com.cn/assets/src/images/zt/certification/pic280-6.jpg"
                                    alt="ENJOYOR"></li>
                            <li>
                                <img src="http://www.trjcn.com.cn/assets/src/images/zt/certification/pic280-7.jpg"
                                    alt="同创伟业"></li>
                            <li>
                                <img src="http://www.trjcn.com.cn/assets/src/images/zt/certification/pic280-8.jpg"
                                    alt="浙江睿投资管理有限公司"></li>
                            <li>
                                <img src="http://www.trjcn.com.cn/assets/src/images/zt/certification/pic280-9.jpg"
                                    alt="浙商创投"></li>
                            <li>
                                <img src="http://www.trjcn.com.cn/assets/src/images/zt/certification/pic280-10.jpg"
                                    alt="浙江浙大科发股权投资管理有限公司"></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <%--<div id="float-sidebar">
            <a href="http://www.trjcn.com.cn/vote.html" target="_blank" class="go-feedback">问卷调查</a>
            <a href="http://chat.53kf.com/webCompany.php?arg=trjcn&style=1" target="_blank" rel="nofollow"
                class="go-service"></a><a href="javascript:;" class="go-top" id="go-top"></a>
        </div>--%>
        <script type="text/javascript" src="/assets/src/lib/sea.js?1"></script>
        <script>            seajs.use("page/zjxm/capital_settle")</script>
        <footer id="footer">
            <div class="container">
                <p>
	                <a href="http://www.trjcn.com.cn/about.html" title="关于" rel="nofollow" target="_blank">关于</a> |
	                <a href="http://www.trjcn.com.cn/about_notice.html" title="网站公告" rel="nofollow" target="_blank">网站公告</a> |
	                <a href="http://www.trjcn.com.cn/about_job.html" title="诚聘英才" rel="nofollow" target="_blank">诚聘英才</a> |
	                <a href="http://www.trjcn.com.cn/service.html" title="我们的服务" rel="nofollow" target="_blank">我们的服务</a> |
	                <a href="http://www.trjcn.com.cn/about_contact.html" rel="nofollow" target="_blank">联系我们</a> |
	                <a href="http://www.trjcn.com.cn/about_link.html" title="友情链接" rel="nofollow" target="_blank">友情链接</a> |
	                <a href="http://www.trjcn.com.cn/about_privacy.html" title="隐私条款" rel="nofollow" target="_blank">隐私条款</a> |
	                <a href="http://www.trjcn.com.cn/sitemap.html" title="网站地图" target="_blank">网站地图</a>
                </p>
                <p>Copyright ©2015 www.trjcn.com 版权所有 | ICP经营许可证:<a href="http://www.trjcn.com.cn/about_notice_102794.html" target="_blank" rel="nofollow">浙B2-20130239</a></p>
            </div>
        </footer>
    </div>
    <div style="display: none;">
    </div>
    </form>
</body>
</html>
