jQuery(document).ready(function () {
    //提交销卡
    $('#form_postorder button[type="reset"]').click(function () { $("#form_postorder button.green").removeAttr("disabled"); $("#callinfo").html(""); });
    $("#herewogo").click(function () {
        postCard(0);
    });
    $("#herewogo1").click(function () {
        postCard(1);
    });


    $("#arr_content").blur(function () {
        var Groupstxt = $("#arr_content").val().split('\n');
        var Groupscount = Groupstxt.length;
        //alert(Groupstxt[Groupstxt.length-1]);
        if (Groupstxt[Groupstxt.length - 1]) {
            $("#Groupscount").html(Groupscount);
        }
        else {
            $("#Groupscount").html(Groupscount - 1);
        }
    });
    $("#arr_content").keyup(function () {
        var Groupstxt = $("#arr_content").val().split('\n');
        var Groupscount = Groupstxt.length;
        //alert(Groupstxt[Groupstxt.length-1]);
        if (Groupstxt[Groupstxt.length - 1]) {
            $("#Groupscount").html(Groupscount);
        }
        else {
            $("#Groupscount").html(Groupscount - 1);
        }
    });
    //批量提交
    $("#form_Groupscard button.green").click(function () {
        var groupchannelId = $("#ctl00_ContentPlaceHolder1_g_channelId").val();
        var groupcontent = $("#arr_content").val();
        if (groupcontent == '') {
            $("#arr_content").focus();
            $("#Groupsinfo").html(errico + "请输入卡信息</span>").show(); ClearGroupsinfo();
            return false;
        };
        if (groupchannelId == '') {
            $("#Groupsinfo").html(errico + "通道信息获取失败</span>").show(); ClearGroupsinfo();
            return false;
        };
        $("#Groupsinfo").css({ color: "#666666" });
        $(this).attr("disabled", true);
        groupcontentarr = Cleartrim(groupcontent);
        var group_h = groupcontentarr.split('\n');
        var Groupbackmsg = "";
        $("#Groupsinfo").html("").hide(); ClearGroupsinfo();
        $("#arr_content").val("");
        for (var i = 0; i < group_h.length; i++) {
            var groupcard = group_h[i].split(',');
            var ino = i + 1;
            if (ino < 10) { ino = "0" + ino; }
            if (groupcard.length != 2) {
                $("#Groupsinfo_" + ino).html(errico + "第 " + ino + " 张 { 卡信息格式不正确,不予接收处理 }</span>").show();
            }
            else {
                var groupcardid = groupcard[0];
                var groupcardpass = groupcard[1];
                //var grouppaymoney = groupcard[2];
                var grouppaymoney = $("#mutiValue").val();
                if (ino < 21) { if (group_h[i]) { Groupscard(ino, groupchannelId, groupcardid, groupcardpass, grouppaymoney); } }
            }
        }
        $("#form_Groupscard button.green").removeAttr("disabled");
        queryOrder();
    });
    $('#form_Groupscard button[type="reset"]').click(function () {
        $("#form_Groupscard button.green").removeAttr("disabled");
        $("#arr_content").val("");
        $("#Groupsinfo").html("").hide(); ClearGroupsinfo();
    });
})


function postCard(flags) {
    var channeltype = $(".xk_channelId").val();
    var channelname = $(".xk_channelname").val();
    var xkcardid = $("#xk_cardId").val().replace(/[^\w\/]/ig, '');
    var xkcardpass = $("#xk_cardPass").val().replace(/[^\w\/]/ig, '');
    var xkfacevalue = $(".i_radio:checked").val();
    var cmgs = "卡号输入错误！";
    var mmgs = "密码输入错误！";
    if (xkcardid == '') { $("#xk_cardId").focus(); $("#callinfo").html(errico + "请输入卡号</span>"); return false };
    if (xkcardpass == '') { $("#xk_cardPass").focus(); $("#callinfo").html(errico + "请输入卡密码</span>"); return false };
    if (xkfacevalue == '') { $("#xk_faceValue").focus(); $("#callinfo").html(errico + "请输入正确的卡面额</span>"); return false };
    switch (channeltype) {
        case '107':
            if (xkcardid.length != 9) {
                $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为9位", lock: true, fixed: true, cancelVal: '确定', cancel: function () { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
            }
            else {
                if (xkcardpass.length != 12) {
                    $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为12位", lock: true, fixed: true, cancelVal: '确定', cancel: function () { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                }
            }
            break; //腾讯卡
        case '104':
            if (xkcardid.toUpperCase().indexOf("S") <= 0) { $.dialog({ title: lktitle, content: channelname + cmgs + "<br />以S开头", lock: true, fixed: true, cancelVal: '确定', cancel: function () { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false; }
            //else
            //{ if (xkcardpass.length != 8 && xkcardpass.length != 9) { $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为8或9位", lock: true, fixed: true, cancelVal: '确定', cancel: function () { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false; } }
            break; //盛大卡
        case '106':
            if (xkcardid.length != 16) { $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为16位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false; }
            else
            { if (xkcardpass.length != 16) { $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为16位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false; } }
            break; //骏网一卡通
        case '111':
            if (xkcardid.length != 10) {
                $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为10位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
            } else {
                if (xkcardpass.length != 15) { $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为15位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false; }
            } break; //完美一卡通
        case '112':
            if (xkcardid.length != 20) {
                $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为20位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
            } else {
                if (xkcardpass.length != 12) {
                    $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为12位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                }
            } break; //搜狐一卡通
        case '105':
            if (xkcardid.length != 16) {
                $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为16位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
            } else {
                if (xkcardpass.length != 8) {
                    $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为8位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                }
            } break; //征途游戏卡
        case '109':
            if (xkcardid.length != 13) {
                $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为13位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
            } else {
                if (xkcardpass.length != 10) {
                    $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为10位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                }
            } break; //久游一卡通
        case '110':
            if (xkcardid.length != 13) {
                $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为13位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
            } else {
                if (xkcardpass.length != 9) {
                    $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为9位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                }
            } break; //网易一卡通
        case '115':
            if (xkcardid.length != 20) {
                $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为20位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
            } else {
                if (xkcardpass.length != 8) {
                    $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为8位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                }
            } break; //光宇一卡通
        case '210':
                        if (xkcardid.length != 16) {
                            $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为16位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                        } else {
                            if (xkcardpass.length != 8) {
                                $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为8位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                            }
                        } 
            break; //盛付通一卡
        case '103':
            if (xkcardid.length != 17) {
                $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为17位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
            } else {
                if (xkcardpass.length != 18) {
                    $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为18位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                }
            } break; //神州行充值卡
        case '108':
            if (xkcardid.length != 15) {
                $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为15位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
            } else {
                if (xkcardpass.length != 19) {
                    $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为19位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                }
            } break; //联通充值卡
        case '117':
            if (xkcardid.length != 15) {
                $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为15位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
            } else {
                if (xkcardpass.length != 15) {
                    $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为15位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                }
            } break; //纵游一卡通
        case '119':
            if (xkcardid.length != 10 && xkcardid.length != 12) {
                $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为10或12位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
            } else {
                if (xkcardpass.length != 10 && xkcardpass.length != 15) {
                    $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为10或15位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                }
            } break; //天宏一卡通
        case '118':
            if (xkcardid.length != 15) {
                $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为15位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
            } else {
                if (xkcardpass.length != 8) {
                    $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为8位", lock: true, fixed: true, okVal: '确定', ok: function () { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                }
            } break; //天下一卡通

    } //switch end	
    if (parseInt(xkfacevalue) < 1 || parseInt(xkfacevalue) > 1000) { $("#xk_faceValue").focus(); $("#callinfo").html(errico + "请输入正确的卡面额</span>"); return false };
    var postorderdata = "ChannelId=" + channeltype + "&CardId=" + xkcardid + "&CardPass=" + xkcardpass + "&FaceValue=" + xkfacevalue + "&flags=" + flags;
    $(".required").val("");
    $("#callinfo").css({ color: "#666666" }); $("#callinfo").html(ldico + "正在提交</span>");
    $.ajax({
        type: "get",
        contentType: "text/html",
        url: "/usermodule/ws/lotsCardSell.ashx?t=" + Math.random(),
        data: postorderdata,
        error: function () {
            $("#callinfo").css({ color: "red" });
            $("#callinfo").html(errico + "提交出现错误</span>");
        },
        success: function (result) {
            if (result != "true") {
                $("#callinfo").html(errico + "提交失败：" + result + "</span>");
            } //失败
            else
            { $("#callinfo").html(okico + "提交成功!</span>"); } //成功
        }
    });
    queryOrder();
}


function checkflag(a) {
    setTimeout(function () {
        stopflag(a)
    },
	300);
    $("#sub" + a).attr("disabled", "disabled");
    $("#paymoney" + a).html("Loading</span>");
    $("#orderzt" + a).html("Loading</span>");
    $("#errorMsg" + a).html("Loading</span>")
}
function stopflag(c) {
    postData = "oid=" + c + "&rnd=" + Math.random();
    $.ajax({
        type: "get",
        dataType: "json",
        timeout: 10000,
        url: '/usermodule/ws/queryorderInfo.ashx',
        data: postData,
        success: function (a) {
            $("#sub" + c).removeAttr("disabled");
            $("#orderzt" + c).html(a.Success);
            $("#paymoney" + c).html(a.paymoney);
            $("#errorMsg" + c).html(a.errorMsg);
        },
        error: function (a, b) {
            $("#sub" + c).removeAttr("disabled");
            $.dialog({
                title: lktitle,
                content: '结果获取失败,请稍等重试' + b,
                lock: true,
                fixed: true,
                ok: function () {
                    window.location.reload()
                },
                icon: 'warning',
                width: '250px',
                height: '90px'
            })
        }
    });
//    $.ajax({
//        type: "get",
//        contentType: "text/html",
//        url: "/Merchant/Ajax/GetBalance.ashx?t=" + Math.random(),
//        data: "",
//        error: function () {
//            $("#Cbalance").html("提交出现错误");
//        },
//        success: function (a) {
//            if (a != "") {

//                $("#Cbalance").html(a);
//            }
//        }
//    });
}




function queryOrder(flag) {
    //alert(flag);
    //if(flag == 1){$("#txtCardNo").val("");}
    //if ($("#txtCardNo").val() == "") {
    //$("#txtCardNo").val("在这里输入卡号进行查询");
    $("#queryorder").attr("disabled", "disabled");
    $("#toporder").html("<tr><td colspan='10' class='nomsg'>Loading..</span></td></tr>");
    $.ajax({
        type: "get",
        contentType: "text/html",
        url: "/usermodule/ws/queryorders.ashx?t=" + Math.random(),
        data: "",
        error: function () {
            $("#toporder").html("<tr><td colspan='10' class='nomsg'>提交出现错误</td></tr>");
        },
        success: function (a) {
            if (a != "") {
                $("#queryorder").removeAttr("disabled");

                a.replace("已完成", "<font color='green'>已完成</font>").replace("失败", "<font color='red'>失败</font>");
                $("#toporder").html(a);
            }
        }
    });
    //}

//    $.ajax({
//        type: "get",
//        contentType: "text/html",
//        url: "/Merchant/Ajax/GetBalance.ashx?t=" + Math.random(),
//        data: "",
//        error: function () {
//            $("#Cbalance").html("提交出现错误");
//        },
//        success: function (a) {
//            if (a != "") {

//                $("#Cbalance").html(a);
//            }
//        }
//    });
}

function queryCardNo() {

    //if (!isNaN($("#txtCardNo").val().replace(/[ ]/g, ""))) {
    $("#queryorder").attr("disabled", "disabled");
    $("#toporder").html("<tr><td colspan='10' class='nomsg'>" + ldico + "Loading..</span></td></tr>");
    $.ajax({
        type: "get",
        contentType: "text/html",
        url: "/usermodule/ws/queryorders.ashx?t=" + Math.random() + "&CardNo=" + $("#txtCardNo").val().replace(/[ ]/g, ""),
        data: "",
        error: function () {
            $("#toporder").html("<tr><td colspan='10' class='nomsg'>提交出现错误</td></tr>");
        },
        success: function (a) {
            if (a != "") {
                $("#queryorder").removeAttr("disabled");

                a.replace("已完成", "<font color='green'>已完成</font>").replace("失败", "<font color='red'>失败</font>");
                $("#toporder").html(a);
            }
        }
    });
    //} else {
    //    alert("请输入正确的卡号，不能包含汉字或者特殊符号");
    //}

}


function Groupscard(b, c, d, e, f) {
    $("#Groupsinfo_" + b).html(ldico + "正在提交..</span>").show();
    postorderdata = "ChannelId=" + c + "&CardId=" + d + "&CardPass=" + e + "&FaceValue=" + f + "";
    $.ajax({
        type: "get",
        contentType: "text/html",
        url: "/usermodule/ws/CardSell.ashx?t=" + Math.random(),
        data: postorderdata,
        error: function () {
            $("#Groupsinfo").css({
                color: "red"
            });
            $("#Groupsinfo").html(errico + "提交出现错误</span>");
            ClearGroupsinfo()
        },
        success: function (a) {
            if (a != "true") {
                Groupbackmsg = errico + "第 " + b + " 张 { 卡号：" + d + " | 提交失败：" + a + " }</span>"
            } else {
                Groupbackmsg = okico + "第 " + b + " 张 { 卡号：" + d + " | 提交成功,正在处理 }</span>"
            }
            $("#Groupsload").html("").hide();
            $("#Groupsinfo_" + b).html("").hide();
            $("#Groupsinfo_" + b).html(Groupbackmsg).show()
        }
    })
}
function ClearGroupsinfo() {
    $("#Groupsinfo_01").html("").hide();
    $("#Groupsinfo_02").html("").hide();
    $("#Groupsinfo_03").html("").hide();
    $("#Groupsinfo_04").html("").hide();
    $("#Groupsinfo_05").html("").hide();
    $("#Groupsinfo_06").html("").hide();
    $("#Groupsinfo_07").html("").hide();
    $("#Groupsinfo_08").html("").hide();
    $("#Groupsinfo_09").html("").hide();
    $("#Groupsinfo_10").html("").hide();
    $("#Groupsinfo_11").html("").hide();
    $("#Groupsinfo_12").html("").hide();
    $("#Groupsinfo_13").html("").hide();
    $("#Groupsinfo_14").html("").hide();
    $("#Groupsinfo_15").html("").hide();
    $("#Groupsinfo_16").html("").hide();
    $("#Groupsinfo_17").html("").hide();
    $("#Groupsinfo_18").html("").hide();
    $("#Groupsinfo_19").html("").hide();
    $("#Groupsinfo_20").html("").hide();
    $("#Groupsload").html("").hide();
    $("#Groupscount").html("0")
}
function Cleartrim(a) {
    a = a.replace(/\s*$/, "");
    a = a.replace(/^\s*/, "");
    a = a.replace(/\s{2,}/g, ',');
    a = a.replace(/，/g, ',');
    a = a.replace(/ /g, ',');
    return a;
}