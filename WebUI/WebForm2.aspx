<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="viviAPI.WebUI7uka.WebForm2" %>

<div aria-hidden="false" style="display: block" id="userlogin_dialog" class="modal fade in"
    role="dialog" tabindex="-1" jquery191026878185290212075="165">
    <div style="width: 670px" class="modal-dialog">
        <div class="modal-content">
            <div id="userlogin_dialog_header" class="modal-header">
                <button aria-hidden="true" class="close" type="button" data-dismiss="modal">
                    ×</button>
                <h3 id="userlogin_dialog" class="modal-title">
                    用户登录</h3>
            </div>
            <div style="overflow-y: auto; min-height: 220px; max-height: 550px" id="userlogin_dialog_content"
                class="modal-body">
                <div style="display: none">
                    <form id="alipaysubmit" method="get" name="alipaysubmit" action="https://mapi.alipay.com/gateway.do?_input_charset=utf-8">
                    <input value="utf-8" type="hidden" name="_input_charset"><input value="2088002055917261"
                        type="hidden" name="partner"><input value="http://www.sudu.cn/alipay/qlogin_return.php"
                            type="hidden" name="return_url"><input value="alipay.auth.authorize" type="hidden"
                                name="service"><input value="user.auth.quick.login" type="hidden" name="target_service"><input
                                    value="f0f183749f0ade3fa6a631646c70359b" type="hidden" name="sign"><input value="MD5"
                                        type="hidden" name="sign_type"><input value="支付宝快捷登录" type="submit"></form>
                </div>
                <div id="userloginformdiv">
                    <form id="loginform" class="form-horizontal" method="post" action="/checklogin_new.php"
                    jquery191026878185290212075="180" novalidate="novalidate">
                    <input type="hidden" name="thirdparty">
                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tbody>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td width="450" align="right">
                                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td class="th" width="90" align="right">
                                                    登录名
                                                </td>
                                                <td align="left">
                                                    <input style="width: auto; float: left; margin-right: 5px" id="username" class="form-control error"
                                                        type="text" name="username" placeholder="请输入用户名"><label class="error" for="username">请填写用户名</label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="th" width="90" align="right">
                                                    密&nbsp;&nbsp;&nbsp;&nbsp;码
                                                </td>
                                                <td align="left">
                                                    <input style="width: auto; float: left; margin-right: 5px" id="userpass" class="form-control error"
                                                        value="" type="password" name="userpass" placeholder="请输入用户密码"><label class="error"
                                                            for="userpass">请填写密码</label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-top: 6px" width="90" align="right">
                                                    验证码
                                                </td>
                                                <td align="left">
                                                    <input style="width: 160px; float: left; margin-right: 15px" id="validateCode" class="form-control error"
                                                        onfocus="refreshValidateimg(this,'validatecodeimg2')" type="text" name="validateCode"
                                                        placeholder="输入右边图片中字符"><img style="float: left" id="validatecodeimg2" onclick="refreshValidateimg(this)"
                                                            src="/function/fun_captcha.php?w=120&amp;h=50&amp;t=1407464634312"><label class="error"
                                                                for="validateCode">请填写验证码</label>
                                                    <span style="display: none; float: left" id="validateCodeErr" class="error">验证码填写有误</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <label for="https_input">
                                                        <input id="https_input" value="yes" checked type="checkbox" name="https">
                                                        安全加密通道</label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <label id="PersistentCookie_label" for="PersistentCookie" jquery191026878185290212075="172"
                                                        data-original-title="为了确保您的信息安全，请不要在网吧或者公共机房选择此项" data-placement="top" data-toggle="tooltip">
                                                        <input style="float: left; margin-right: 5px" id="PersistentCookie" onclick="return checkCookie(this)"
                                                            value="yes" type="checkbox" name="PersistentCookie">
                                                        3月内免登录</label>
                                                    <br>
                                                    <span class="text-muted"><a href="/getpass.php" target="_top">忘记密码？</a></span>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="display: none" id="note_div">
                        <p class="text-danger">
                            您浏览器的 <strong>Cookie</strong> 功能被禁用，不能保存您的信息<br>
                            <a class="text-muted" href="http://scholar.google.cn/intl/zh-CN/cookies.html" target="_blank">
                                如何启用cookie?</a></p>
                    </div>
                    <!--     <div class="form-group">
     <label class="col-sm-2 control-label">登录名</label>
     <div class="col-sm-10">
     <input type='text' class='form-control' name='username' id='username' style='float:left;width:auto;margin-right:5px;' placeholder='请输入用户名' />
     </div>
     </div>

     <div class="form-group">
    <label class="col-sm-2 control-label">密&nbsp;&nbsp;&nbsp;&nbsp;码</label>
     <div class="col-sm-10">
     <input type='password' class='form-control' name='userpass' id='userpass' style='float:left;width:auto;margin-right:5px;' placeholder='请输入用户密码' />
     </div>
     </div>
                                                                        
     <div class="form-group" id="validate_row" style="display:none">
    <label class="col-sm-2 control-label">验证码</label>
     <div class="col-sm-10">
       <input type="text" style="float:left;width:160px;margin-right:15px;" name="validateCode" id="validateCode"  class="form-control" placeholder="点击获取验证码" onfocus="refreshValidateimg(this,'validatecodeimg2')" /><img src="" id="validatecodeimg2" style="display:none;float:left" onclick="refreshValidateimg(this)" />
    </div>
    </div>
                                                                        

     <div class="form-group">
    <label class="col-sm-2 control-label">&nbsp;</label>
     <div class="col-sm-10">
<label id="PersistentCookie_label" for='PersistentCookie' data-toggle="tooltip" data-placement="right" data-original-title="为了确保您的信息安全，请不要在网吧或者公共机房选择此项"><input type='checkbox' name='PersistentCookie' id='PersistentCookie' style='float:left;margin-right:5px;' value='yes' onclick='return checkCookie(this)' /> 3月内免登录</label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="text-muted"><a target="_top" href="/getpass.php">忘记密码？</a></span>
<div id="note_div" style="display:none">
<p class="text-danger">您浏览器的 <strong>Cookie</strong> 功能被禁用，不能保存您的信息<br /><a class="text-muted" target="_blank" href="http://scholar.google.cn/intl/zh-CN/cookies.html">如何启用cookie?</a></p>
</div>                                                
     </div>
</div>
-->
                    <!--p class="text-muted">其它登录方式：
<a href="#" title="支付宝帐户登录" onclick="document.forms['alipaysubmit'].submit();return false;" class="btn btn-large btn-info sns-login-btn"><span class="sns-icon alipay"></span>支付宝帐户</a>
</p-->
                    <input style="display: none" value="提交查询内容" type="submit">
                    </form>
                </div>

                <script language="JavaScript" type="text/javascript">
//<![CDATA[
var jq=jQuery.noConflict();
jq(function(){
    Tipped.create('#PersistentCookie_label', '为了确保您的信息安全，请不要在网吧或者公共机房选择此项', {maxWidth:300,skin: 'yellow',showDelay:0,hook:'leftmiddle',offset:{x:0,y:5}});

    //jq('#PersistentCookie_label').tooltip({'container':"#userlogin_dialog_content"});
        
    window.$loginvalidator = jq( "#loginform" ).validate({
    ignore : ':hidden',
    tipClass: 'tip',
    rules:{
        username:{
            "required":true,
            "regex":"^[0-9a-zA-Z]{4,16}$"
        },
        userpass:{
            "regex":"^[0-9a-zA-Z]{4,16}$",
            "required":true
        },
        validateCode:{
            "required":true,
            'validatecode':true
        }
    },
    messages:{
        username:{
            regex:'用户名格式错误',
            required:'请填写用户名'
        },
        userpass:{
            regex:'密码格式错误',
            required:'请填写密码'
        },
        validateCode:{
            required:'请填写验证码'
        }
    },
    errorPlacement: function(error, element) {
        var elename = element.attr("name");

        if(elename=='validateCode'){
            error.insertAfter(element.next('img'));
        }else error.insertAfter(element);
    },
    submitHandler: function(form) {
        window.$loginXhr = null;
        $timeout = 6000; //登录超时时间
        window.$login_timer = window.setTimeout(function(){
                if($loginXhr){
                    $loginXhr.abort();
                    //错误信息将在error()里弹出
                }
            },$timeout);
                
        var validator = this;
        var data = jq(form).serialize();
        jq.zajaxloader(jq('#userlogin_dialog .modal-content'));

        var https_input = document.getElementById('https_input');
        var https = https_input && https_input.checked;
        var http = 'http'+(https?'s':'')+'://';
        var host = http+'www.sudu.cn';
        var systemurl = host+'/system/index.php';

        var btn = jq('button.loginbtn,input.loginbtn');
        btn.attr('disabled','disabled');
        
        $loginXhr = jq.ajax({
            url:form.action,
            data:data+'&ajax=1',
            dataType:'json',
            type:'post',
            success:function(json){
                    if(json.res){
                        if(json.thirdreturn){
                        	window.location = json.thirdreturn;
                        	return;
                        }
                        btn.attr('disabled','disabled');                        
                        returnurl = window.ReturnUrl;
                        if(returnurl){
                            if(returnurl.indexOf('/')===0){
                                returnurl = host + returnurl;
                            }
                        }
                    jq('#userlogin_dialog').modal('hide');

                    if(window.ReturnUrl){
                        window.location.replace(returnurl);
                    }else{
                        if(window.location.protocol!='https:' && https){
                            //当前非https但选择了https
                            window.location = window.location.href.replace('http://','https://');
                            return;
                        }
                        refreshPageHeader();
                        if(window.afterUserLogin && typeof(window.afterUserLogin)=='function'){
                            window.afterUserLogin();
                        }
                    }
                    return;
                }else{
                    if(json.needValidateCode /* || json.errors.validateCode */){
                        /* if(!jq('#validate_row:visible').length){ */
                        /*     jq('#validate_row').show(); */
                        /*     jq('#validateCode').click(); */
                        /* }else  */
                        jq('#validatecodeimg2').click();
                    }
                    if(json.errors.othererr){
                        Sudu.alert(json.errors.othererr);
                        delete json.errors.othererr;
                    }
                    validator.showErrors(json.errors);
                }
            },
            complete:function(){
                    btn.removeAttr('disabled');
                    jq.zajaxloader(jq('#userlogin_dialog .modal-content'),'remove');
                    window.clearTimeout(window.$login_timer);
                    window.$login_timer = null;
                    $loginXhr = null;
            },
            error:function(xhr){
                    var err = xhr.responseText;
                    if(err && err.indexOf('会员中心</title>')>-1){
                        window.location = systemurl;
                        return;
                    }
                    if(!err || !err.length) err = '系统繁忙，请重试';
                    alert(err);
                    jq.zajaxloader(jq('#userlogin_dialog .modal-content'),'remove');
            }
            });
    }
    
    });
});

function checkCookie(checkbox){
    if(!checkbox.checked||Cookie.isSupported()){
        if(checkbox.checked)
        document.getElementById('note_div').style.display = 'none';
        return true;
    }
    document.getElementById('note_div').style.display = 'block';
    return false;    
}
//]]>
                </script>

            </div>
            <div id="userlogin_dialog_footer" class="modal-footer">
                <button class="btn btn-info loginbtn" type="button" jquery191026878185290212075="162">
                    登&nbsp;&nbsp;&nbsp;录</button></div>
            <div class="otherlogmodes-left">
                <strong class="text-muted">已有帐户登录</strong><br>
                <a style="display: none" class="sudu-sns-btn btn btn-large btn-info sns-login-btn"
                    title="华夏名网帐户登录" onclick="loginWithSuduSns();return false;" href="#" jquery191026878185290212075="185">
                    <span class="sns-icon sudu"></span>本站会员</a> <a class="btn btn-large btn-info sns-login-btn"
                        title="支付宝帐户登录" onclick="document.forms['alipaysubmit'].submit();return false;"
                        href="#"><span class="sns-icon alipay"></span>支付宝帐户</a> <a class="btn btn-large btn-info sns-login-btn"
                            title="QQ帐户登录" href="https://graph.qq.com/oauth2.0/authorize?response_type=code&amp;client_id=100565171&amp;redirect_uri=http%3A%2F%2Fwww.sudu.cn%2Fqq%2Fredirect_uri.php&amp;state=ed40ff66296963c4998629eb588ac326">
                            <span class="sns-icon qq"></span>QQ帐户</a></div>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>
