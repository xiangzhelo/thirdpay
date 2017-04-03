

jQuery(document).ready(function(jQuery) {
	jQuery("#rechargeMoney").numeral();
	
    jQuery("#rechargeMoney").blur(function() {
        CalculateIncome();
    });
    
    jQuery('input:radio[name="bank_list"]').change(function() {
        CalculateIncome();
    });
});

function CheckForm() {
	var money = jQuery("#rechargeMoney").val();
	if (money == "") {
		jQuery("#callinfo").onfocus();
		jQuery("#callinfo").text("请输入充值金额");
		return false;
	}
	return true;
}

function CalculateIncome() {
    jQuery(".txtc").html("0.00");
    var rmoney = jQuery("#rechargeMoney").val();
    var bankno = jQuery('input:radio[name="bank_list"]:checked').val();

    if (rmoney != "") {
        jQuery.get("/usermodule/ws/RechargeCalculateIncome.ashx?t=" + Math.random(), { rechargemoney: rmoney, bankno: bankno },
            function(data) {
                jQuery(".txtc").html(data);
            });
    }
}