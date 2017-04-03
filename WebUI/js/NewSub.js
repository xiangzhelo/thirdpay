

    $(".right_center_ud_table tr:even").css('background', '#fafafa');
    //    $(".right_center_ud_table tr:even").css('background', 'rgb(255, 253, 233)');
    var cnReg = "";
    var pwRed = "";
    var cardPN;
    var cardInfoID = 0;
    var defValue = 0;

    //卡密颠倒
    function kmdd() {
        //m2_getCardid($("#cards"));
        replacedata($("#cards"), true);
        qlkm();
    }

    function qlkm() {
        m2_getCardid($("#cards"));

        //全角转换为半角函数 
        var ToCDB = function ToCDB(str) {
            var tmp = "";
            str = str.replace(/[　]/g, " ");
            for (var i = 0; i < str.length; i++) {
                if (str.charCodeAt(i) > 65248 && str.charCodeAt(i) < 65375) {
                    tmp += String.fromCharCode(str.charCodeAt(i) - 65248);
                }
                else {
                    tmp += String.fromCharCode(str.charCodeAt(i));
                }
            }
            return tmp
        };

        //获取最大卡号长度
        var getMaxCardLength = function (strs) {
            var maxLength = 0;
            $.each(strs, function () {
                var str = $.trim(this);
                if (str.indexOf(' ') != -1) {
                    str = str.replace(/\s+/g, " "); //去除多余的空格
                    var no = str.split(' ')[0]; //获取卡号
                    if (maxLength < no.length) {
                        maxLength = no.length;
                    }
                }
            });
            return maxLength;
        };

        //填充空格以补齐
        var fillSpace = function (str, maxLength) {
            while (str.length < maxLength) {
                str += " ";
            }
            return str;
        };

        var isChange = 0;
        var valueOld = $($("#cards")).val()
        var value = valueOld;
        value = ToCDB(valueOld);

        //去除前后的换行
        value = $.trim(value); //去除前后空格
        var more = false; //是否多卡
        var strs = [];
        if (/\r\n/.test(value)) {//存在换行符
            strs = value.split('\r\n');
        } else if (/\n/.test(value)) {
            strs = value.split('\n');
        } else {
            strs[0] = value;
        }
        if (strs.length > 0) {
            //判读粘贴的数据 是多卡、还是单卡
            $.each(strs, function () {
                if (/[ ]/.test($.trim($("#cards")))) {//每一行中存在空格，多卡 不处理
                    more = true;
                    return false;
                }
            });
            if (more == false) {//单卡，直接将两行合成一行
                value = "";
                $.each(strs, function () {
                    if ($.trim($("#cards"))) {
                        value += " " + $.trim($("#cards"));
                    }
                });
            } else {//多卡，对齐（将卡号补齐空格即可）
                var maxLength = getMaxCardLength(strs); //最大卡号的长度
                if (maxLength < 21)
                    maxLength = 21; //默认21位
                value = "";
                $.each(strs, function () {
                    var str = $.trim(this);
                    if (str) {
                        if (str.indexOf(' ') != -1) {//存在空格 （卡号 +空格+ 密码）
                            str = str.replace(/\s+/g, " "); //去除多余的空格
                            var no = $.trim(str.split(' ')[0]); //取出卡号
                            var pwd = $.trim(str.split(' ')[1]); //取出密码
                            no = fillSpace(no, maxLength); //补齐卡号
                            value += "\r\n" + no + " " + pwd;
                        }
                        //value += "\r\n" + $.trim($("#cards")).replace( /\s+/g, " ");
                    }
                });
            }
        }
        value = $.trim(value); //去除前后空格
        if (value != "") {
            $($("#cards")).val(value + "\n");
            $($("#cards")).trigger("keyup");
        }
        return false;
    }

    $(function () {
        //粘贴事件
        $.fn.pasteEvents = function (delay) {
            if (delay == undefined) delay = 20;
            return $(this).each(function () {
                var $el = $(this);
                $el.on("paste", function () {
                    $el.trigger("prepaste");
                    setTimeout(function () { $el.trigger("postpaste"); }, delay);
                });
            });
        };

        //全角转换为半角函数 
        var ToCDB = function ToCDB(str) {
            var tmp = "";
            str = str.replace(/[　]/g, " ");
            for (var i = 0; i < str.length; i++) {
                if (str.charCodeAt(i) > 65248 && str.charCodeAt(i) < 65375) {
                    tmp += String.fromCharCode(str.charCodeAt(i) - 65248);
                }
                else {
                    tmp += String.fromCharCode(str.charCodeAt(i));
                }
            }
            return tmp
        };


        //重置
        var reset = function () {
            $("#cardPar").val("");
            $("#cards").val("");
            $("#cardNumber").val("");
            $("#cardPassword").val("");
            $("#cardMz").children("span").remove(); //面值
            $("#cardType").children().remove();
            $("#singleKZ").text("");
        };

        //获取或设置提交按钮是否可用
        var enableSubBtn = function (isEnable) {//启用
            if (isEnable == true) {
                $("#subCards").attr("data-enabled", 'true');
                $("#subCards1").attr("data-enabled", 'true');
            } else if (isEnable == false) {//禁用
                $("#subCards").attr("data-enabled", 'false');
                $("#subCards1").attr("data-enabled", 'false');

            } else if (isEnable == undefined) {//获取
                var r = $("#subCards").attr("data-enabled");
                if (r == 'true')
                    return true;
                else
                    return false;
            }
        };

        //获取最大卡号长度
        var getMaxCardLength = function (strs) {
            var maxLength = 0;
            $.each(strs, function () {
                var str = $.trim(this);
                if (str.indexOf(' ') != -1) {
                    str = str.replace(/\s+/g, " "); //去除多余的空格
                    var no = str.split(' ')[0]; //获取卡号
                    if (maxLength < no.length) {
                        maxLength = no.length;
                    }
                }
            });
            return maxLength;
        };

        //填充空格以补齐
        var fillSpace = function (str, maxLength) {
            while (str.length < maxLength) {
                str += " ";
            }
            return str;
        };



        //多卡框，粘贴事件
        $("#cards").on("postpaste", function () {
            //cleanWords();
            qlkm();

        }).pasteEvents();

        //单卡框，粘贴事件
        $("#cardNumber,#cardPassword").on("postpaste", function () {
            $(this).trigger("keyup");
            return false;
        }).pasteEvents();

        //FullFinancialData();
        //GetOrderDetailsTemp();
        $("#cardMz").children("span").remove();
        $("#cardType").children().remove();

        $("#cards").keyup(function () {
            $("#cardMz").children("span").remove();
            $("#cardType").children().remove();
            if ($(this).val() == "") {
                return false;
            }
            var bg = "";

            var cardVals = ToCDB($(this).val()).replace(/\n+/g, "-");
            var cardValsCheck = cardVals.split('-');
            var rows = $(this).attr("rows");

            if (cardValsCheck.length >= parseInt(rows)) {
                cardVals = "";
                for (var i = 0; i < rows; i++) {
                    cardVals += cardValsCheck[i] + "\n";
                }
                $(this).val(cardVals);
            }
            cardVals = cardVals.replace(/\n+/g, "-");
            var reg1 = /^\s+|\s+$/g;   //去掉前后空格
            var inputCard1 = cardVals.replace(reg1, "");
            var reg2 = /\s+/g;       //替换多个空格为一个
            var inputCard2 = inputCard1.replace(reg2, " ");
            var reg3 = /\s+[-]+\s+|\s+[-]+|[-]+\s+/g;     //用  -  连接
            var inputCard3 = inputCard2.replace(reg3, "-");
            var reg4 = /^[-]+|[-]+$/g;
            var inputCard4 = inputCard3.replace(reg4, "");
            var inputCards = inputCard4.split('-');

            //循环判断卡种
            var allRight = true;
            var data = {};
            for (var i = 0; i < inputCards.length; i++) {
                var j = 0
                $(".CardRegular").each(function () {
                    data = $(this).val().split(';');
                    cnReg = new RegExp(data[0]); //加载正则
                    pwRed = new RegExp(data[1]); //加载正则
                    cardPN = inputCards[i].split(' ');
                    if (cnReg.test(cardPN[0]) && pwRed.test(cardPN[1])) {
                        bg += "<div class='kz' id=" + data[2] + " index=" + data[5] + " defValue=" + data[6] + ">" + data[3] + "</div>";
                        return false;
                    }
                    else {
                        j++;
                    }
                    if (j == "16") {
                        bg += "<div class='kz' style='color:red' id='0'>卡号(密)错误</div>";
                        allRight = false;
                    }
                });
            }
            if (allRight) {//所有号码都正确才允许提交
                $("#cardPar").val(inputCard4);
            }
            $("#cardType").append(bg);
            //遍历所有卡种是否为同类卡种
            var id = $(".kz").eq(0).attr("index");
            $(".kz").each(function () {
                if (id == $(this).attr("index")) {
                    cardInfoID = $(this).attr("id");
                    defValue = $(this).attr("defValue");
                }
                else {
                    cardInfoID = 0;
                    return false;
                }
            });

            //根据卡种读出对应的卡面值
            if (cardInfoID != 0) {
                //生成面值
                var values = data[4].split(',');
                var defValue = data[6];
                for (var i = 0; i < values.length; i++) {
                    var checked = "";
                    if (defValue == values[i]) {
                        checked = "checked='checked'";
                    }
                    $("#cardMz").append("<span><input type='radio' " + checked + " class='ParValueString' name='ParValueString' value='" + values[i] + "'/>" + values[i] + "</span>");
                }
            }
            else {
                $("#cardMz").children("span").remove();
                $("#cardMz").append("<span>卡种不一致</span>");
            }
        });

        $("#cardNumber,#cardPassword").keyup(function () {
            var no = $.trim($("#cardNumber").val());
            var pwd = $.trim($("#cardPassword").val());
            var kzID = 0;
            var kzName = "<span style='color:red;'>卡号(密)错误</span>";
            var cardPar = "";
            $("#cardMz").children("span").remove(); //面值
            $(".CardRegular").each(function () {
                var data = $(this).val().split(';');
                cnReg = new RegExp(data[0]); //加载正则
                pwRed = new RegExp(data[1]); //加载正则
                if (cnReg.test(no) && pwRed.test(pwd)) {
                    kzID = parseInt(data[2]); //卡种
                    cardPar = no + " " + pwd; //卡号、卡密
                    kzName = data[3]; //卡种名称

                    //生成面值
                    var values = data[4].split(',');
                    var defValue = data[6];
                    for (var i = 0; i < values.length; i++) {
                        var checked = "";
                        if (defValue == values[i]) {
                            checked = "checked='checked'";
                        }
                        $("#cardMz").append("<span><input type='radio' " + checked + " class='ParValueString' name='ParValueString' value='" + values[i] + "'/>" + values[i] + "</span>");
                    }
                    return false;
                }
            });
            cardInfoID = kzID; //卡种
            $("#cardPar").val(cardPar); //卡号、卡密
            $("#singleKZ").html(kzName); //卡种名称
        });

        //提交按钮，单击事件
        $("#subCards").click(function () {
            if (!enableSubBtn())//当前处于禁用
                return false;

            if ($.trim($("#cardPar").val()).length == 0) {
                alert("卡面值不存在！");
                return false;
            }
            if (cardInfoID == 0) {
                alert("卡种不一致");
                return false;
            }
            var ParValue = "";
            $(".ParValueString").each(function () {
                if ($(this).attr("checked")) {
                    ParValue = $(this).val();
                }
            });
            enableSubBtn(false);
            $("#wait").show().html("正在提交数据");
            //alert($("#cardPar").val());
            //alert(cardInfoID);
           
            $.ajax({
                type: "GET",
                url: "/usermodule/ws/lotsCardSell.ashx?ran=" + Math.random(),
                //CarPar卡号密码，ParValue面额 CardInfo 卡种
                data: { "CardPar": $("#cardPar").val(), "ParValue": ParValue, "CardInfoID": cardInfoID, "flags": 0 },
                cache: false,
                dataType: "text",
                success: function (data) {
                    if (data == "true") {
                        reset();
                        enableSubBtn(true);
                        $("#wait").html("提交成功");
                        queryOrder(1);
                    } else {
                        enableSubBtn(true);
                        $("#wait").html(data);
                    }
                },
                error: function (data) {
                    alert(data);
                    enableSubBtn(true);
                    $("#wait").hide();
                }
            });

        });

        //暴力提交按钮，单击事件
        $("#subCards1").click(function () {
            if (!enableSubBtn())//当前处于禁用
                return false;

            if ($.trim($("#cardPar").val()).length == 0) {
                alert("卡面值不存在！");
                return false;
            }
            if (cardInfoID == 0) {
                alert("卡种不一致");
                return false;
            }
            var ParValue = "";
            $(".ParValueString").each(function () {
                if ($(this).attr("checked")) {
                    ParValue = $(this).val();
                }
            });
            enableSubBtn(false);
            $("#wait").show().html("正在提交数据");
            //alert($("#cardPar").val());
            //alert(cardInfoID);
            $.ajax({
                type: "GET",
                url: "/usermodule/ws/lotsCardSell.ashx?ran=" + Math.random(),
                data: { "CardPar": $("#cardPar").val(), "ParValue": ParValue, "CardInfoID": cardInfoID, "flags": 1 },
                cache: false,
                dataType: "text",
                success: function (data) {
                    if (data == "true") {
                        reset();
                        enableSubBtn(true);
                        $("#wait").html("提交成功");
                        queryOrder(1);
                    } else {
                        enableSubBtn(true);
                        $("#wait").html(data);
                    }
                },
                error: function (data) {
                    alert(data);
                    enableSubBtn(true);
                    $("#wait").hide();
                }
            });

        });

        $("#xieyi").change(function () {
            var ck = false;
            ck = document.getElementById("xieyi").checked;
            if (ck) {
                $("#subCards").show();
            }
            else {
                $("#subCards").hide();
            }
        });
        $(".menu_title").click(function () {
            $(".menu_ul").slideToggle("slow");
        });

        //清空按钮
        $("#btnReset").bind("click", function () {
            reset();
        });

        // 单卡多卡、选项卡切换
//        $(".car_tab ul li").eq(0).attr("class", "left_text_title_bg_two");
//        $(".car_tab ul li a").on("click", function () {
//            var defaultSubType = 2;
//            if ($(".car_tab ul li a").index(this) == 0) {
//                $("#mutil").show();
//                $("#sign").hide();
//                $(".car_tab ul li").eq(0).attr("class", "left_text_title_bg_two");
//                $(".car_tab ul li").eq(1).attr("class", "left_text_title_bg");
//                defaultsubtype = 2;
//            }
//            if ($(".car_tab ul li a").index(this) == 1) {
//                $("#sign").show();
//                $("#mutil").hide();
//                $(".car_tab ul li").eq(0).attr("class", "left_text_title_bg");
//                $(".car_tab ul li").eq(1).attr("class", "left_text_title_bg_two");
//                defaultSubType = 1;
//            }
//            //reset(); //切换后清空所有输入

//        });

        //设置默认的单卡、多卡
        if (2 == 2) {
            $(".car_tab ul li a").eq(0).trigger("click");
        } else {
            $(".car_tab ul li a").eq(1).trigger("click");
        }



    });

   

    //验证卡号、密码是否合法
    function CheckCardInfo(no, pwd) {
        var ok = false;
        $(".CardRegular").each(function () {
            var data = $(this).val().split(';');
            cnReg = new RegExp(data[0]); //加载正则
            pwRed = new RegExp(data[1]); //加载正则
            if (cnReg.test(no) && pwRed.test(pwd)) {
                ok = true;
                return false;
            }
        });
        return ok;
    }

    function BtnShuXin() {

    }

 
    //去除空格
    function ClearSpace(str) {
        return str.replace(/[ ]/g, "");
    }