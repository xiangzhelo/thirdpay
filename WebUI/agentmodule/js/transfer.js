


function CalculateFee() {
    jQuery(".txtc").html("0.00");
    var tmoney = jQuery("#txtTransferMoney").val();

    if (tmoney != "") {
        jQuery.get("/usermodule/ws/TransferCalculateFee.ashx?t=" + Math.random(), { money: tmoney },
            function(data) {
                jQuery(".txtc").html(data);
            });
    }
}