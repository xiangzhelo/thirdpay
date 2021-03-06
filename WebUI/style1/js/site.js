$(function () {
    //首页
    var images = $("#lunbo").find(".images a");
    var init = function () {
        var height = $(window).height() - 30;
        //1920*787
        if (height / 787 * 1920 < $(window).width()) { height = $(window).width() / 1920 * 787; }
        images.find("img").height(height);
        images.height(height);
        $("#lunbo").height(height);
    };
    init();
    $(window).resize(function () {
        init();
    });
    var btns = $("#lunbo").find(".indicators a");
    var index = 0;
    var lunbo = function () {
        var i1 = index - 1;
        if (i1 < 0) { i1 = images.length - 1; }
        images.hide();
        var w = images.eq(i1).width();
        images.eq(i1).css({ left: 0 }).show().animate({ left: 0 - w });
        images.eq(index).css({ left: w }).show().animate({ left: 0 });
        btns.removeClass("selected").eq(index).addClass("selected");

        index++;
        if (index >= btns.length) { index = 0; }
    };
    lunbo();
    var timer = setInterval(lunbo, 5000);
    btns.click(function () {
        clearInterval(timer);
        index = $(this).index("#lunbo .indicators a");
        lunbo();
        timer = setInterval(lunbo, 5000);
    });

    $("#username").css({ color: '#999' }).val("商户账号").focus(function () {
        if ($(this).val() == "商户账号") {
            $(this).val("");
            $(this).removeAttr("style");
        }
    }).blur(function () {
        if ($(this).val() == "") {
            $(this).val("商户账号");
            $(this).css({ color: '#999' });
        }
    });

    $("#yanzm").css({ color: '#999' }).val("验证码").focus(function () {
        if ($(this).val() == "验证码") {
            $(this).val("");
            $(this).removeAttr("style");
        }
    }).blur(function () {
        if ($(this).val() == "") {
            $(this).val("验证码");
            $(this).css({ color: '#999' });
        }
    });

    //内容页
    var tabs = $("#nrtabs");
    var tabheada = tabs.find("header a");
    var tabitemdiv = tabs.find(".tabs .item");
    tabheada.click(function () {
        tabheada.removeClass("selected");
        $(this).addClass("selected");
        var index = $(this).index("#nrtabs header a");
        tabitemdiv.hide();
        tabitemdiv.eq(index).show();
    });

});