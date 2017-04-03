jQuery(document).ready(function(){
    jQuery('body').pngFix();
	//png透明
		jQuery('.bk').fadeOut(0);//背景消失
		jQuery('#bk1').fadeIn(1000);//当前背景进入
		jQuery('#bk1 .bText').show().animate({left: '0px'},500);//当前文字进入
        jQuery('#bk1 .bPic').animate( { left: '0'}, 400 );//当前前景图案进入
	jQuery('.banner').hover(
	  function () {
		  window.clearInterval(iID);
	  },
	  function (event) {
		if( jQuery(event.target).attr('class') != 'banner' ){
			 iID = window.setInterval(autoBigBanner,7000);
		}
	  }
	);
	
	
	jQuery('.bNav li').click(function(){
//		jQuery('.bNav li').removeClass('now');
//		jQuery(this).addClass('now');
		jQuery('.bNav li').attr('id','');
		jQuery(this).attr('id','now');
		var bNav = jQuery(this).attr('class');
		jQuery('.bk').fadeOut(900);//背景消失
		jQuery('.bText').attr('style','left:-50px;').hide();//文字消失
		jQuery('#'+bNav).fadeIn(1000);//当前背景进入
		jQuery('#'+ bNav + ' .bText').show().animate({left: '0px'},500);//当前文字进入
        jQuery('.bPic').animate( { left: '20px'}, 300 );//前景图案消失
        jQuery('#'+bNav + ' .bPic').animate( { left: '0'}, 400 );//当前前景图案进入
	});
	//鼠标点击导航
			 iID = window.setInterval(autoBigBanner,7000);

function autoBigBanner()
{
	for(i=1;i<6;i++){
		var bNavNow = jQuery('.bNav #now').attr('class');
		var bkI = 'bk'+i;
		if(bkI == bNavNow ){
		  if( i<5){
	        jQuery('.bNav li').attr('id','');
		    i++;
		    jQuery('.bNav li.bk'+i).attr('id','now');
			jQuery('.bk').fadeOut(900);//背景消失
			jQuery('.bText').attr('style','left:-50px;').hide();//文字消失
			jQuery('#bk'+i).fadeIn(1000);//当前背景进入
			jQuery('#bk'+i + ' .bText').show().animate({left: '0px'},500);//当前文字进入
			jQuery('.bPic').animate( { left: '20px'}, 300 );//前景图案消失
			jQuery('#bk'+i + ' .bPic').animate( { left: '0'}, 400 );//当前前景图案进入
		  }
		  else{
	        jQuery('.bNav li').attr('id','');
		    i = 1;
		    jQuery('.bNav li.bk'+i).attr('id','now');
			jQuery('.bk').fadeOut(900);//背景消失
			jQuery('.bText').attr('style','left:-50px;').hide();//文字消失
			jQuery('#bk1').fadeIn(1000);//当前背景进入
			jQuery('#bk1 .bText').show().animate({left: '0px'},500);//当前文字进入
			jQuery('.bPic').animate( { left: '20px'}, 300 );//前景图案消失
			jQuery('#bk1 .bPic').animate( { left: '0'}, 400 );//当前前景图案进入
		  };
		};
	}
}


		jQuery('.pt_b').removeClass('now');
	jQuery('.pt_a').click(function(){
		jQuery(this).addClass('now');
		jQuery('.pt_b').removeClass('now');
		jQuery('#w1,#w2').hide();
		jQuery('#w2').css({'visibility': 'hidden'});
		jQuery('#w1').show().css({'visibility': 'visible'});
	});
	jQuery('.pt_b').click(function(){
		jQuery(this).addClass('now');
		jQuery('.pt_a').removeClass('now');
		jQuery('#w1,#w2').hide();
		jQuery('#w1').css({'visibility': 'hidden'});
		jQuery('#w2').show().css({'visibility': 'visible'});
	});
	
	jQuery('.pro li').click(function(){
		var iNow = jQuery(this).attr('id');
		jQuery('.p1').fadeOut('fast');
		jQuery('.p2').fadeOut('fast');
		jQuery('.p3').fadeOut('fast');
		jQuery('.p4').fadeOut('fast');
		jQuery('.'+iNow).fadeIn('fast');
		jQuery('.'+iNow).hoverIntent(
			function (){
			},
			function(){
				jQuery(this).fadeOut('fast');
		});
	});
	
	jQuery('.prev').click(function(){
	  jQuery('.prev').removeClass('prev2');
	  jQuery('.next').addClass('next2');
	  jQuery('.solShow ul').animate( { left: '0'}, 500 )
	});
	jQuery('.next').click(function(){
	  jQuery('.prev').addClass('prev2');
	  jQuery('.next').removeClass('next2');
	  jQuery('.solShow ul').animate( { left: '-200px'}, 500 )
	});
	
	jQuery('#qiye').hoverIntent(
		function (){
			jQuery('.nav_qiye').addClass('open').slideDown(400);
		},
		function(){
			jQuery('.nav_qiye').removeClass('open').slideUp(200);
		}
	);
	jQuery('#chanpin').hoverIntent(
		function (){
			jQuery('.nav_chanpin').addClass('open').slideDown(400);
		},
		function(){
			jQuery('.nav_chanpin').removeClass('open').slideUp(200);
		}
	);
	jQuery('#anquan').hoverIntent(
		function (){
			jQuery('.nav_anquan').addClass('open').slideDown(400);
		},
		function(){
			jQuery('.nav_anquan').removeClass('open').slideUp(200);
		}
	);
	
	
});

(function(jQuery){jQuery.fn.hoverIntent=function(f,g){var cfg={sensitivity:7,interval:100,timeout:0};cfg=jQuery.extend(cfg,g?{over:f,out:g}:f);var cX,cY,pX,pY;var track=function(ev){cX=ev.pageX;cY=ev.pageY;};var compare=function(ev,ob){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t);if((Math.abs(pX-cX)+Math.abs(pY-cY))<cfg.sensitivity){jQuery(ob).unbind("mousemove",track);ob.hoverIntent_s=1;return cfg.over.apply(ob,[ev]);}else{pX=cX;pY=cY;ob.hoverIntent_t=setTimeout(function(){compare(ev,ob);},cfg.interval);}};var delay=function(ev,ob){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t);ob.hoverIntent_s=0;return cfg.out.apply(ob,[ev]);};var handleHover=function(e){var p=(e.type=="mouseover"?e.fromElement:e.toElement)||e.relatedTarget;while(p&&p!=this){try{p=p.parentNode;}catch(e){p=this;}}if(p==this){return false;}var ev=jQuery.extend({},e);var ob=this;if(ob.hoverIntent_t){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t);}if(e.type=="mouseover"){pX=ev.pageX;pY=ev.pageY;jQuery(ob).bind("mousemove",track);if(ob.hoverIntent_s!=1){ob.hoverIntent_t=setTimeout(function(){compare(ev,ob);},cfg.interval);}}else{jQuery(ob).unbind("mousemove",track);if(ob.hoverIntent_s==1){ob.hoverIntent_t=setTimeout(function(){delay(ev,ob);},cfg.timeout);}}};return this.mouseover(handleHover).mouseout(handleHover);};})(jQuery);
//导航延迟触发

