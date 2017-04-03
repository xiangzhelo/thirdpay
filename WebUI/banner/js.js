jQuery(document).ready(function(){
	jQuery('.chat_f1_expr').animate({height:'156px'}, 1000 );
    jQuery('#close').click(function() {
        jQuery('#chat_f1').hide();
        jQuery('#chat_f2').show();
    });
    jQuery('#chat_f2').click(function() {
        jQuery(this).hide();
        jQuery('#chat_f1').show();
    });
    jQuery('.name').hover(function() {
        jQuery(this).children('.detail').show();
        jQuery(this).children('.arrow').css('color', '#a00');
    }, function() {
        jQuery(this).children('.detail').hide();
        jQuery(this).children('.arrow').css('color', '#fff');
    });
})