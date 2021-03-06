$(function () {
    //首页
    var menua = $(".menudiv ul li a");
    var selmenu = $(".menudiv ul li a.selected");
    menua.mouseover(function () { menua.removeClass("selected"); $(this).addClass("selected"); }).mouseout(function () {
        menua.removeClass("selected");
        selmenu.addClass("selected");
    });

    var images = $("#lunbo").find(".images a");
    images.find("img").css({ left: 0 - (1920 - $(window).width()) / 2 });
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

    var logina = $("#login .tabs a");
    var loginnr = $("#login .loginnr");
    logina.click(function () {
        logina.removeClass("selected");
        $(this).addClass("selected");
        loginnr.hide().eq($(this).index("#login .tabs a")).css({ opacity: 0 }).show().animate({ opacity: 1 }, 500);
    });

    //内容页
    var producta = $("#product a");
    var productInfo = $(".product_info");
    var accessa = $("#access a");
    var accessInfo = $(".access_info");
    var abouta = $("#about a");
    var aboutInfo = $(".about_info");
    producta.click(function () {
        accessa.removeClass("selected");
        producta.removeClass("selected");
        $(this).addClass("selected");
        accessInfo.hide();
        productInfo.hide();
        productInfo.eq($(this).index("#product a")).show();
    });

    accessa.click(function () {
        accessa.removeClass("selected");
        producta.removeClass("selected");
        $(this).addClass("selected");
        accessInfo.hide();
        productInfo.hide();
        accessInfo.eq($(this).index("#access a")).show();
    });

    abouta.click(function () {
        abouta.removeClass("selected");
        $(this).addClass("selected");
        aboutInfo.hide();
        aboutInfo.eq($(this).index("#about a")).show();
    });
});