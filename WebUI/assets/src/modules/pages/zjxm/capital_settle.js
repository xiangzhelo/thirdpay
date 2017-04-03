seajs.use(['jquery'], function ($) {
	$(window).scroll(function () {
        if($(window).scrollTop() >= 300)
        {
            $('#go-top').css('display','block');
        }else{
            $('#go-top').css('display','none');
        }
    });
	$("#go-top").click(function(){
        $('body,html').animate({scrollTop:0},500);
        return false;
    });
	
	seajs.use(['page/common','jquery.form'], function () {
		var smsid = 0;
		var tt = 60, hand = '';
		window.ep=$('#J_mobile_info');
		$('#T-reg-mobile-code').click(function(){
			var tipkey = $(this).val();
			if(tipkey !== '获取验证码') return;	
				
	        var mobile = $.trim($('input[name=mobile]').val());
	        if(mobile == ''){
	        	ep.html('<i class="icon-money-tips"></i>请输入您的手机号码');
	            return;
	        }else{
	        	ep.html('');
	        }
	        if(!Trjcn.Util.isMobile(mobile)){
	        	ep.html('<i class="icon-money-tips"></i>请输入正确的手机号码');
	            return;
	        }else{
	        	ep.html('');
	        }
	        
	        Trjcn.Ajax.post("/usermodule/ws/SendPhoneVerifyCode.ashx", "mobile="+mobile, function(res){
	            // if(res.code == 200 || res.code == 203){
	                // smsid = res.data.smsid;
	                // if (res.data.time)tt = res.data.time;
	            // }
				if(res=="1"){
	                smsid = 101;
	                // if (res.data.time)tt = res.data.time;
	            }
	            else
	            {
	                ep.html('<i class="icon-money-tips"></i>'+res.data.error);
	                return;
	            }
	            if (hand)clearInterval(hand);
	            var _interval = function () {
	                tt = tt - 1;
	                if (tt > 0)
	                	$('#T-reg-mobile-code').val(tt+'秒后重新获取');
	                else{
	                    if (hand)clearInterval(hand);tt = 60;
	                    $('#T-reg-mobile-code').val('获取验证码');
	                }
	            };
	            _interval();
	            $('#T-reg-mobile-code').val(tt+'秒后重新获取');
	            hand = setInterval(_interval, 1000);
	        });
	    });
		
		var J_rzsq = $('#J_rzsq');
		var J_mobile_info = $('#J_mobile_info',J_rzsq);
		var J_mobilecode_info = $('#mobilecode',J_rzsq);
		var J_contact_name_info = $('#contact_name',J_rzsq);
		Trjcn.cache.loading = false;
		$('.btn-money-submit',J_rzsq).click(function(){
			if (Trjcn.cache.loading) return false;
			var _mobile = $('#J_mobile',J_rzsq).val();
			var _mobilecode = $('#J_mobile_code',J_rzsq).val();
			var contact_name = $('input[name=contact_name]',J_rzsq).val();
			var check_flag = true;
	        if(contact_name == ''){
	        	J_contact_name_info.html('<i class="icon-money-tips"></i>请输入联系人姓名');
	        	check_flag = false;
	        }else{
	        	J_contact_name_info.html('');
	        }
			if(_mobile == ''){
				J_mobile_info.html('<i class="icon-money-tips"></i>请输入您的手机号码');
				check_flag = false;
	        }else{
	        	if(!Trjcn.Util.isMobile(_mobile))
				{
					J_mobile_info.html('<i class="icon-money-tips"></i>请输入正确的手机号码');
					check_flag = false;
				}else{
					J_mobile_info.html('');
				}
	        }
			if (!_mobilecode)
			{
				J_mobilecode_info.html('<i class="icon-money-tips"></i>请输入验证码');
				check_flag = false;
			}else{
				J_mobilecode_info.html('');
			}
			if(check_flag === false){
				return false;
			}
			
			var param = J_rzsq.formSerialize();
			
			var success = function(res){
				Trjcn.cache.loading = false;
				if(res=="1"){
			 		location.href='/reg.aspx?id=1';
			 	}else{
					
			 		for(var k in res.data.error_messages){
			 			if(k == 'mobilecode'){
			 				J_mobilecode_info.html('<i class="icon-money-tips"></i>'+res.data.error_messages[k]);
			 			}else{
			 				J_mobile_info.html('<i class="icon-money-tips"></i>'+res.data.error_messages[k]);
			 			}
						break;
					}
			 	}
			};
			Trjcn.Ajax.post("/reg.aspx", param, success);
			return false;
		});
	});
})