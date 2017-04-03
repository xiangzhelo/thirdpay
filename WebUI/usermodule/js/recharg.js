

jQuery(document).ready(function (jQuery) {
    jQuery("#rechargeMoney").numeral();

    jQuery("#rechargeMoney").blur(function () {
        CalculateIncome();
    });

    jQuery('input:radio[name="bank_list"]').change(function () {
        CalculateIncome();
    });
});

function CheckForm() {
    var money = jQuery("#rechargeMoney").val();
    var limitAmount = jQuery(".txtxe").html();
    if (money == "") {
        jQuery("#callinfo").onfocus();
        jQuery("#callinfo").text("请输入充值金额");
        return false;
    }
        //else   if (limitAmount != "")
        //{


        //    if (money > limitAmount)
        //    {
        //        alert("充值金额请不要超过最大充值限额！");
        //        jQuery("#callinfo").onfocus();
        //        jQuery("#callinfo").text("充值金额请不要超过最大充值限额！");
        //        return false;
        //    }
        //}
    else { return true; }

}

function CalculateIncome() {
    jQuery(".txtc").html("0.00");
    var rmoney = jQuery("#rechargeMoney").val();
    var bankno = jQuery('input:radio[name="bank_list"]:checked').val();

    if (rmoney != "") {
        jQuery.get("/usermodule/ws/RechargeCalculateIncome.ashx?t=" + Math.random(), { rechargemoney: rmoney, bankno: bankno },
            function (data) {

                var strs = data.split(",");
                //alert('rechargemoney=' + rmoney + ',bankno=' + bankno);
                //alert(data);
                //alert(strs[1]);
                //alert(rmoney > strs[1]);
                jQuery("#rechargeMoney").val(strs[1]);

                jQuery(".txtc").html(strs[0]);
                jQuery(".txtxe").html(strs[1]);
                //if (rmoney > strs[1])//大于限制金额
                //{
             
                //    jQuery(".txtc").html(strs[0]);
                //    jQuery(".txtxe").html(strs[1]);
                //}
                //else {
                //    jQuery(".txtc").html(strs[0]);
                //    jQuery(".txtxe").html(strs[1]);

                //}

            });
    }
}