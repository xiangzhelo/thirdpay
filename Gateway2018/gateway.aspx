<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gateway.aspx.cs"
    Inherits="viviAPI.Gateway2018.gateway" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 

"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>翁贝支付</title>
    <meta name="keywords" content="" />
    <link href="css/yeepaytest.css" type="text/css" rel="stylesheet" />
    <script src="jquery.min.js" type="text/javascript"></script>
</head>
<body>
    <table width="60%" border="0" align="center" cellpadding="0" cellspacing="0" style="border: solid 1px #40506b;">
        <tr>
            <td>
                <form action="paydo.aspx" method="post">
                    <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1"
                        style="border-spacing: 0;">
                        <tr>
                            <td>
                                <a href="#">
                                    <img src="images/logo.png" alt="翁贝支付" width="150" height="45"
                                        border="0" /></a>
                            </td>
                            <td style="text-align: right;">
                                <span style="color: #868B94;">感谢您使用翁贝支付平台</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="color: #fff; font-size: 14px; height: 40px; background: #2C69C1;">翁贝网产品通用支付接口演示
                            </td>
                        </tr>
                        <%--<tr>
                        <td>
                            商户订单号
                        </td>
                        <td>
                            &nbsp;&nbsp;<input size="50" type="text" name="outOrderId" 

id="outOrderId" value="123123321123" />&nbsp;<span
                                style="color: #FF0000; font-weight: 100;">*</span>
                        </td>
                    </tr>--%>
                        <tr>
                            <td>充值金额(元)
                            </td>
                            <td>&nbsp;&nbsp;<input size="50" type="text" name="totalAmount"
                                id="totalAmount" value="1" />&nbsp;<span
                                    style="color: #FF0000; font-weight: 100;">*</span>
                            </td>
                        </tr>
                        <%--<tr id="truserName">
                        <td>
                            用户名
                        </td>
                        <td>
                            &nbsp;&nbsp;<input size="50" type="text" name="userName" 

id="userName" value="" />&nbsp;<span
                                style="color: #FF0000; font-weight: 100;">*</span>
                        </td>
                    </tr>--%>
                        <%-- <tr>
                        <td>
                            商品名称
                        </td>
                        <td>
                            &nbsp;&nbsp;<input size="50" type="text" name="goodsName" 

id="goodsName" value="goodsName" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            商品描述
                        </td>
                        <td>
                            &nbsp;&nbsp;<input size="50" type="text" name="goodsExplain" 

id="goodsExplain" value="goodsExplain" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            商户取货地址
                        </td>
                        <td>
                            &nbsp;&nbsp;<input size="50" type="text" name="merUrl" id="merUrl" 

value="<%=Demo.Class.ProperConst.merUrl%>" />&nbsp;<span
                                style="color: #FF0000; font-weight: 100;">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            通知商户服务端地址
                        </td>
                        <td>
                            &nbsp;&nbsp;<input size="50" type="text" name="noticeUrl" 

id="noticeUrl" value="<%=Demo.Class.ProperConst.noticeUrl%>" />&nbsp;<span
                                style="color: #FF0000; font-weight: 100;">*</span>
                        </td>
                    </tr>--%>
                        <tr id="trcardNum" style="display: none;">
                            <td>卡号
                            </td>
                            <td>&nbsp;&nbsp;<input size="50" type="text" name="cardNo" id="cardNo"
                                value="" />&nbsp;<span
                                    style="color: #FF0000; font-weight: 100;">*</span>
                            </td>
                        </tr>
                        <tr id="trcardPwd" style="display: none;">
                            <td>卡密
                            </td>
                            <td>&nbsp;&nbsp;<input size="50" type="text" name="cardPwd" id="cardPwd"
                                value="" />&nbsp;<span
                                    style="color: #FF0000; font-weight: 100;">*</span>
                            </td>
                        </tr>

                        <%--<tr id="trjine">
                        <td>
                            金额
                        </td>
                        <td>
                            &nbsp;&nbsp;<input size="50" type="text" name="jine" id="jine" 

value="" />&nbsp;<span
                                style="color: #FF0000; font-weight: 100;">*</span>
                        </td>
                    </tr>--%>
                        <tr style="display: none;">
                            <td>支付银行卡类型
                            </td>
                            <td>&nbsp;&nbsp;<input size="50" type="text" name="bankCardType"
                                id="bankCardType" value="00" />
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td>支持银行编码
                            </td>
                            <td>&nbsp;&nbsp;<input size="50" type="text" name="bankCode"
                                id="bankCode" value="" />
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: sub;">支付方式
                            </td>
                            <td>
                                <div id="tabbox">
                                    <ul class="tabs" id="tabs">
                                        <li><a href="#" tab="tab1">银行卡支付</a></li>
                                        <li><a href="#" tab="tab2">支付宝</a></li>
                                        <li><a href="#" tab="tab3">微信</a></li>
                                        <li><a href="#" tab="tab4">财付通</a></li>
                                        <li><a href="#" tab="tab5">点卡</a></li>
                                        <li><a href="#" tab="tab6">易宝</a></li>
                                        <li><a href="#" tab="tab7">51-QQ支付</a></li>
                                        <li><a href="#" tab="tab8">易宝微信</a></li>
                                        <li><a href="#" tab="tab9">Wap微信</a></li>
                                    </ul>
                                    <ul class="tab_conbox">
                                        <li id="tab1" class="tab_con">
                                            <div style="margin-bottom: 20px;">
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="" value="963">
                                                    <img src="images/perBank/BOC.gif" />
                                                </div>
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="" value="964">
                                                    <img src="images/perBank/ABC.gif" />
                                                </div>
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="" value="1100">
                                                    <img src="images/perBank/ICBC.gif" />
                                                </div>
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="" value="965">
                                                    <img src="images/perBank/CCB.gif" />
                                                </div>
                                            </div>
                                            <div style="margin-bottom: 20px;">
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="" value="981">
                                                    <img src="images/perBank/BCM.gif" />
                                                </div>
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="" value="970">
                                                    <img src="images/perBank/CMB.gif" />
                                                </div>
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="" value="986">
                                                    <img src="images/perBank/CEB.gif" />
                                                </div>
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="" value="977">
                                                    <img src="images/perBank/SPDB.gif" />
                                                </div>
                                            </div>
                                            <div style="margin-bottom: 20px;">
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="" value="989">
                                                    <img src="images/perBank/BCCB.gif"
                                                        style="padding-right: 19px;" />
                                                </div>
                                                <%--<div class="ra-img">
                                                <input type="radio" name="" id="" value="PSBC">
                                                <img src="images/perBank/PSBC.gif" />
                                            </div>
                                            <div class="ra-img">
                                                <input type="radio" name="" id="" value="BOS">
                                                <img src="images/perBank/BOS.gif" />
                                            </div>--%>
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="" value="972">
                                                    <img src="images/perBank/CIB.gif" />
                                                </div>
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="Radio13"
                                                        value="962">
                                                    <img src="images/perBank/CITIC.gif" />
                                                </div>
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="Radio14"
                                                        value="980">
                                                    <img src="images/perBank/CMBC.gif" />
                                                </div>
                                            </div>
                                            <div style="margin-bottom: 20px;">
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="" value="985">
                                                    <img src="images/perBank/GDB.gif" />
                                                </div>
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="" value="982">
                                                    <img src="images/perBank/HXB.gif" />
                                                </div>
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="Radio15"
                                                        value="974">
                                                    <img src="images/perBank/PAB.gif" style="padding

-right: 18px;" />
                                                </div>
                                            </div>
                                        </li>
                                        <li id="tab2" class="tab_con">
                                            <div style="margin-bottom: 20px;">
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="" value="992">
                                                    <img src="images/zfb.png" />
                                                </div>
                                            </div>
                                            <div style="margin-bottom: 10px;">
                                            </div>
                                        </li>
                                        <li id="tab3" class="tab_con">
                                            <div style="margin-bottom: 20px;">
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="" value="1004">
                                                    <img src="images/weixin.png" />
                                                </div>
                                            </div>
                                        </li>
                                        <li id="tab4" class="tab_con">
                                            <div style="margin-bottom: 20px;">
                                                <div class="ra-img">
                                                    <input type="radio" name="typeid" id="Radio1"
                                                        value="993">
                                                    <img src="images/cft.png" />
                                                </div>
                                            </div>
                                        </li>
                                        <li id="tab5" class="tab_con">
                                            <div style="margin-bottom: 20px;">
                                                <div class="ra-img2">
                                                    <input type="radio" name="typeid" id="Radio7"
                                                        value="1">
                                                    <img src="images/dianka/QBCZK.png" />
                                                </div>
                                                <div class="ra-img2">
                                                    <input type="radio" name="typeid" id="Radio2"
                                                        value="2">
                                                    <img src="images/dianka/SDYKT.png" />
                                                </div>
                                                <div class="ra-img2">
                                                    <input type="radio" name="" id="Radio3"
                                                        value="3">
                                                    <img src="images/dianka/JWYKT.png" />
                                                </div>
                                                <div class="ra-img2">
                                                    <input type="radio" name="typeid" id="Radio4"
                                                        value="5">
                                                    <img src="images/dianka/WMYKT.png" />
                                                </div>
                                            </div>
                                            <div style="margin-bottom: 20px;">
                                                <div class="ra-img2">
                                                    <input type="radio" name="typeid" id="Radio5"
                                                        value="6">
                                                    <img src="images/dianka/SHYKT.png" />
                                                </div>
                                                <div class="ra-img2">
                                                    <input type="radio" name="typeid" id="Radio6"
                                                        value="7">
                                                    <img src="images/dianka/ZTYKT.png" />
                                                </div>
                                                <div class="ra-img2">
                                                    <input type="radio" name="typeid" id="Radio8"
                                                        value="8">
                                                    <img src="images/dianka/JYYKT.png" />
                                                </div>
                                                <div class="ra-img2">
                                                    <input type="radio" name="typeid" id="Radio9"
                                                        value="9">
                                                    <img src="images/dianka/WYYKT.png" />
                                                </div>
                                            </div>
                                            <div style="margin-bottom: 20px;">
                                                <div class="ra-img2">
                                                    <input type="radio" name="typeid" id="Radio10"
                                                        value="12">
                                                    <img src="images/dianka/DXGK.png" />
                                                </div>
                                                <div class="ra-img2">
                                                    <input type="radio" name="typeid" id="Radio11"
                                                        value="13">
                                                    <img src="images/dianka/YDSZX.png" />
                                                </div>
                                                <div class="ra-img2">
                                                    <input type="radio" name="typeid" id="Radio12"
                                                        value="14">
                                                    <img src="images/dianka/LTYKT.png" />
                                                </div>
                                                <div class="ra-img2">
                                                    <input type="radio" name="typeid" id="Radio16"
                                                        value="21">
                                                    <img src="images/dianka/TXYKT.png" />
                                                </div>
                                            </div>
                                            <div style="margin-bottom: 20px;">
                                                <div class="ra-img2">
                                                    <input type="radio" name="typeid" id="Radio17"
                                                        value="22">
                                                    <img src="images/dianka/THYKT.png" />
                                                </div>
                                                <div class="ra-img2">
                                                    <input type="radio" name="typeid" id="Radio18"
                                                        value="23">
                                                    <img src="images/dianka/ZYYKT.png" />
                                                </div>
                                                <div class="ra-img2">
                                                    <input type="radio" name="typeid" id="Radio20"
                                                        value="28">
                                                    <img src="images/dianka/SFYJT.png" />
                                                </div>
                                            </div>
                                        </li>
                                        <li id="tab6" class="tab_con">
                                            <div style="margin-bottom: 20px;">
                                                <div class="ra-img">
                                                    <%--<input type="radio" name="" id="radio30" value="103">--%>
                                                     <input type="radio" name="typeid" id="radio30" value="104">
                                                    <img src="images/yeepay.gif"/>
                                                </div>
                                            </div>
                                        </li>
                                         <li id="tab7" class="tab_con">
                                            <div style="margin-bottom: 20px;">
                                                <div class="ra-img">
                                                    <%--<input type="radio" name="" id="radio30" value="103">--%>
                                                     <input type="radio" name="typeid" id="radio32" value="1005"/>
                                                     51卡-QQ支付
                                                </div>
                                            </div>
                                        </li>
                                         <li id="tab8" class="tab_con">
                                            <div style="margin-bottom: 20px;">
                                                <div class="ra-img">
                                                    <%--<input type="radio" name="" id="radio30" value="103">--%>
                                                     <input type="radio" name="typeid" id="radio33" value="1006"/>
                                                     易宝微信
                                                </div>
                                            </div>
                                        </li>
                                        <li id="tab9" class="tab_con">
                                            <div style="margin-bottom: 20px;">
                                                <div class="ra-img">
                                                    <%--<input type="radio" name="" id="radio30" value="103">--%>
                                                     <input type="radio" name="typeid"  value="1007"/>
                                                     WAP微信
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                        <%--<tr>
                        <td>
                            订单生成时间
                        </td>
                        <td>
                            &nbsp;&nbsp;<input size="50" type="text" name="orderCreateTime" 

id="orderCreateTime"
                                value="20150210213410" />
                        </td>
                    </tr>--%>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;&nbsp;<input type="submit" value="马上支付" id="pay" />
                            </td>
                        </tr>
                    </table>
                </form>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(document).ready(function () {
            jQuery.jqtab = function (tabtit, tabcon) {
                $(tabcon).hide();
                $(tabtit + " li:first").addClass("thistab").show();
                $(tabcon + ":first").show();

                $(tabtit + " li").click(function () {
                    $(tabtit + " li").removeClass("thistab");
                    $(this).addClass("thistab");
                    $(tabcon).hide();
                    var activeTab = $(this).find("a").attr("tab");
                    $("#" + activeTab).fadeIn();
                    if (activeTab == "tab1") {
                        $("#trcardNum").hide();
                        $("#trcardPwd").hide();
                        //$("#trusername").hide();
                        //$("#trjine").hide();
                        $("#bankCardType").val("00");
                    } //根据支付银行类型00个人综合03企业
                    if (activeTab == "tab2") {
                        $("#trcardNum").hide();
                        $("#trcardPwd").hide();
                        //$("#trusername").hide();
                        //$("#trjine").hide();
                        $("#bankCardType").val("00");
                    }
                    if (activeTab == "tab3") {
                        $("#trcardNum").hide();
                        $("#trcardPwd").hide();
                        //$("#trusername").hide();
                        //$("#trjine").hide();
                        $("#bankCardType").val("00");
                    }
                    if (activeTab == "tab4") {
                        $("#trcardNum").hide();
                        $("#trcardPwd").hide();
                        //$("#trusername").hide();
                        //$("#trjine").hide();
                        $("#bankCardType").val("00");
                    }
                    if (activeTab == "tab5") {
                        $("#trcardNum").show();
                        $("#trcardPwd").show();
                        //$("#trusername").show();
                        //$("#trjine").show();
                        $("#bankCardType").val("01");
                    }
                    if (activeTab == "tab6") {
                        $("#trcardNum").hide();
                        $("#trcardPwd").hide();
                        //$("#trusername").show();
                        //$("#trjine").show();
                        $("#bankCardType").val("00");
                    }
                    if (activeTab == "tab7") {
                        $("#trcardNum").hide();
                        $("#trcardPwd").hide();
                        //$("#trusername").show();
                        //$("#trjine").show();
                        $("#bankCardType").val("00");
                    }
                    if (activeTab == "tab8") {
                        $("#trcardNum").hide();
                        $("#trcardPwd").hide();
                        //$("#trusername").show();
                        //$("#trjine").show();
                        $("#bankCardType").val("00");
                    }
                    if (activeTab == "tab9") {
                        $("#trcardNum").hide();
                        $("#trcardPwd").hide();
                        //$("#trusername").show();
                        //$("#trjine").show();
                        $("#bankCardType").val("00");
                    }


                    return false;
                });
            };
            //    /*调用方法如下：*/
            $.jqtab("#tabs", ".tab_con");

            $('.tab_conbox :radio').attr("checked", false);   //默认不点中
            $(':radio').click(function () {
                var raVal = $(this).attr("checked");
                if (raVal == true) {
                    $(this).parent().siblings().children(":radio").attr("checked", false)
			       .parent().parent().siblings().children().children(":radio").attr
("checked", false);
                    $("#pay").removeAttr("disabled");
                    $("#bankCode").val($(this).val()); //设置文本框银行编码
                }
            });

            if (!($(":radio").is(':checked'))) {
                $("#pay").attr("disabled", "disabled");
            }

            if ((isFirefox = navigator.userAgent.indexOf("Firefox") > 0) || (isIE =
navigator.userAgent.indexOf("MSIE") > 0) || (Object.hasOwnProperty.call(window, "ActiveXObject")
&& !window.ActiveXObject)) {
                $('.tabs').css({ "margin-bottom": "-17px" });
            }
        });


    </script>
</body>
</html>
