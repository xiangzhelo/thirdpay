seajs.use(['jquery', 'component/dialog/dialog'], function ($, dialog) {
	seajs.use(['component/jquery.hover','component/SuperSlide/SuperSlide','module/matches','component/jquery.tabs'], function(){
		//导航hover
		$('.nav-more').hoverClass('nav-item-hover');
		//首页焦点图片
		$("#focus").slide({
			titCell:".focus-nav ul",
			mainCell:".focus-images ul",
			effect:"fold",
			autoPlay:true,
			autoPage:true,
			trigger:"click"
		});
		$("#focus").hover(function(){
			$(this).find(".prev,.next").stop(true,true).fadeTo("show",0.5)
		},function(){
			$(this).find(".prev,.next").fadeOut()
		});

		//找资金项目选项卡
		$("#look-tab,#login-tab").Tabs({event:"click"});
		//优质展播
		$("#exhibition").slide({
			mainCell:".exhibition-scroll ul",
			effect:"leftLoop",
			vis:5,
			autoPlay:true
		});
		//成功数据滚动
		$("#case-total-roll").slide({
			mainCell:"ul",
			autoPlay:true,
			interTime: 5000,
			effect:"topLoop"
		});

		// 公共组件hover
            jQuery.jqhover = function(tabtit) {

                $(tabtit).hover(function(){
                    $(this).addClass("cur")
                },function(){
                    $(this).removeClass("cur")
                });
            };
            $.jqhover('.j-hover-all');
            $.jqhover('.top-allnav-icon2');

	});

	$('#J_btn_look_fund').bind('click', function () {
		var win = dialog({
			title: '填写项目信息，帮您找资金',
			width: '400px',
			fixed: true,
			content: document.getElementById('J_tmp_look_fund')
			});
		win.showModal();
	});

	$('#J_btn_look_fund_do').click(function(){
		var url = $(this).attr('url');
		var param = '';
		$('#J_tmp_look_fund').find('select').each(function(){
			var _name = $(this).attr('name');
			var _val  = $(this).val();
			if (_val)
			switch(_name)
			{
				case 'cid':
					url += '/list_'+_val+'.html';
					break;
				default:
					param += '&'+_name+'='+_val;
					break;
			}
		});
		location.href=url+'?'+param;
	});

	
    $(window).scroll(function () {
        if($(window).scrollTop() >= 300)//距离顶部多少高度显示按钮
        {
            $('#go-top').css('display','block')
        }else
        {
            $('#go-top').css('display','none');
        }
    });
	$("#go-top").click(function(){
        $('body,html').animate({scrollTop:0},500);
        return false;
    });

	$('input.J_placeholder').focus(function(){
		var _this = $(this);
		if ( _this.val() == _this.attr('tip') ){
			_this.val('').css({'color':'#333'});
		}
	}).blur(function(){
		var _this = $(this);
		if ( _this.val() == '' )_this.val( _this.attr('tip') ).css({'color':'#999'});
	}).css({'color':'#999'});

	$('button.J_search_submit').click(function(){
		var _this = $(this).prev('.J_placeholder');
		if ( _this.val() == _this.attr('tip') )_this.val('');
	});

	seajs.use(['page/common','jquery.form','component/jquery.ustore'], function () {

		if (typeof (USTORE)!='undefined'){
			USTORE.init();
				$('#float-bottom-toolbar');
			if (!USTORE.getValue('login_username'))
			{
				$('#login-tab .ui-tab-head li:eq(1)').trigger('click');
			}
			if (window.TRJCN_UID)
			{
				USTORE.setValue('login_utype', window.TRJCN_UTYPE);
			}
			else
			{
				window.TRJCN_UTYPE = USTORE.getValue('login_utype');
			}

			if (window.TRJCN_UTYPE==2)
			{
				$('#look-tab .look-tab-head .ui-tab-head-current').next().trigger('click');
			}

		}

		var ologin = new TrjcnLogin();
		ologin.init('J_loginfrm');
		ologin.jump=false;

		var pwd = $('#login_password', $('#J_loginfrm'));
		pwd.next().focus(function(){
			$(this).hide();
			pwd.show().focus();
		});
		pwd.blur(function(){
			if (pwd.val() == '' || pwd.val() == pwd.attr('tip')){
				pwd.hide();
				$(this).next().show();
			}
		});

		window.mobileCode = new TrjcnMobileCode();
		window.mobileCode.init('mobile');
		var J_regfrm = $('#J_regfrm');
		var J_mobile_info = $('#J_mobile_info',J_regfrm);
		Trjcn.cache.loading = false;
		$('#J_btn_reg',J_regfrm).click(function(){
			if (Trjcn.cache.loading)return;
			var _mobile = $('#J_mobile',J_regfrm).val();
			var _mobilecode = $('#J_mobile_code',J_regfrm).val();
			if (!_mobile || !Trjcn.Util.isMobile(_mobile))
			{
				J_mobile_info.html('请输入正确的手机号码');
				return;
			}
			if (!_mobilecode)
			{
				J_mobile_info.html('请输入验证码');
				return;
			}

			var param = J_regfrm.formSerialize();
			var success = function(res){
				Trjcn.cache.loading = false;
				if (res.code == 200)
				{
					var forword_url = $('#forword_url').val() || '';
					location.href='/register/success.html?forward='+ encodeURIComponent(forword_url);
					return;
				}
				for(var k in res.data.error_messages){
					J_mobile_info.html(res.data.error_messages[k]);
					break;
				}
			};
			Trjcn.cache.loading = true;
			Trjcn.Ajax.post("/api/reg/submit", param, success);
		});




		var J_regfrmfooter = $('#J_regfrmfooter');
		var myDate = new Date();
		if (J_regfrmfooter.length==1 && ((typeof (USTORE)!='undefined') && !USTORE.getValue('login_username') && USTORE.getValue('login_old')!=myDate.getDate()) )
		{

			$(window).scroll(function() {
		        var fixToolbar = $("#float-bottom-toolbar");
		        if (fixToolbar.length==1)
		        {
			        var headerH = 380;
			        var scrollTop = $(document).scrollTop();
			        if( scrollTop >= headerH ){
			            fixToolbar.slideDown();
			        }else if( scrollTop < headerH ){
			            fixToolbar.slideUp();
			        }
		        }
		    });

			window.phoneCode = new TrjcnMobileCode();
			window.phoneCode.init('phone');
			var J_phone_info = $('#J_phone_info',J_regfrmfooter);
			Trjcn.cache.loading = false;
			$('#J_btn_reg',J_regfrmfooter).click(function(){
				if (Trjcn.cache.loading)return;
				var _mobile = $('#J_phone',J_regfrmfooter).val();
				var _mobilecode = $('#J_phone_code',J_regfrmfooter).val();
				if (!_mobile || !Trjcn.Util.isMobile(_mobile))
				{
					J_phone_info.html('请输入正确的手机号码');
					return;
				}
				if (!_mobilecode)
				{
					J_phone_info.html('请输入验证码');
					return;
				}

				var param = J_regfrmfooter.formSerialize();
				var success = function(res){
					Trjcn.cache.loading = false;
					if (res.code == 200)
					{
						var forword_url = $('#forword_url').val() || '';
						location.href='/register/success.html?forward='+ encodeURIComponent(forword_url);
						return;
					}
					for(var k in res.data.error_messages){
						J_phone_info.html(res.data.error_messages[k]);
						break;
					}
				};
				Trjcn.cache.loading = true;
				Trjcn.Ajax.post("/api/reg/submit", param, success);
			});
			$('#float-bottom-toolbar .bottom-toolbar-close').click(function(){
				USTORE.setValue('login_old', myDate.getDate());
				$("#float-bottom-toolbar").remove();
			});
		}




	});


	$("#J_likerec").each(function(){
		var self = $('#a_zjxm_more');
		seajs.use(['jquery','module/common/deliver','module/common/invite','module/common/dialog'],function($,deliver,invite,dialog){

		    self.click(function(){get_zjxm_more();});
		    var $page=2;
		    function get_zjxm_more()
		    {
		        $.get("/ajax/zjxm/get_like.html?page="+$page,function(data){
		          if(data["data"])
		          {
		            $("#zjxm_info_div").append(data["data"]["html"]);
		             $.jqhover('.j-hover-all');
		              fav_init();
		              deliver.run();
		              invite.run();
		            $page++;
		            if(data["data"]["count"]<6)
		            {
		               self.parent().remove();
		            }
		            else if($page>3)
		            {
		               self.attr("href", self.attr('data-url'));
		            }
		          }
		          else
		          {
		            self.parent().remove();
		          }
		        },"json");
		      }

			  function fav_init()
			  {
			      $('#zjxm_info_div .addfav').click(function(){
			        var obj=$(this);
			        $.post("/ajax/fav/zjxm.html",{item_id:$(this).attr('val')},function(res){
		                  var msg = '';
		                  if(res.code==200)
		                  {
		                    obj.html('已收藏');
		                    obj.css("cursor","default");
		                    obj.unbind( "click" );
		                    msg = '收藏成功';
		                  }
		                  else
		                  {
		                    msg = res.data.message;
		                  }
		                  dialog.dialog_ok(msg);
			      },"json");

			    });
			  }
		      fav_init();
		      deliver.run();
		      invite.run();

		 });


	});
	
	seajs.use(['module/common/dialog'], function (dialog) {
		$('#J_baoxm').click(function(){
			dialog.dialog({title:false,width:'450px',content:'',innerHTML:'',ajax:{
				url: '/api/baox.html',
				type:'GET',
				dataType:'html',
				data:'_=',
				callback: function (data,dl){
					$('div[role=alertdialog]').html(data);
					dl.reset();
					window.dl = dl;
				}
			}});
		});
	});

/*
	seajs.use(['module/jquery/jquery_autocomplete'], function () {
		var J_keyword_zj = $("#J_keyword_zj");
		J_keyword_zj.autocomplete("/service/common.search_keywords", {
			   minChars: 1,
			   max:10,
			   width: 316,
			   matchCase:false,
			   autoFill: false,
			   dataType: 'json',
			   extraParams:{v:function() {return J_keyword_zj.val();},'type':4},
			   parse: function(data) {
				  data = eval(data);
				  return $.map(data.data, function(row) {
					  return {
					   data: row,
					   value: row.keyword
					 }
			   });
		},
	   formatItem: function(data, i, total) {
		  return "<table><tr><td align='left'>" + data.keyword + "</td></table>";
		}
	   }).result(function(event, data, formatted) {
				J_keyword_zj.val(data.keyword);
	   });
		var J_keyword_xm = $("#J_keyword_xm");
		J_keyword_xm.autocomplete("/service/common.search_keywords", {
			   minChars: 1,
			   max:10,
			   width: 396,
			   matchCase:false,
			   autoFill: false,
			   dataType: 'json',
			   extraParams:{v:function() {return J_keyword_xm.val();},'type':5},
			   parse: function(data) {
				  data = eval(data);
				  return $.map(data.data, function(row) {
					  return {
					   data: row,
					   value: row.keyword
					 }
			   });
		},
	   formatItem: function(data, i, total) {
		  return "<table><tr><td align='left'>" + data.keyword + "</td></table>";
		}
	   }).result(function(event, data, formatted) {
				J_keyword_xm.val(data.keyword);
	   });
	});
*/

});