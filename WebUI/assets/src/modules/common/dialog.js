define(function(require, exports, module){
    require('jquery');
    var dialog = require('component/dialog/dialog');
    module.exports = {
        loading:false,
        dl:null,
        close:function(){
            try{
                module.exports.dl && module.exports.dl.close().remove();
            }catch(e){}
        },
        dialog_ok:function(msg,title)
        {
            window.dlo = dialog({
                title: title || "温馨提示",
                lock:true,
                fixed:true,
                width:'480px',
                content:' <div class="popup-msg-a fn-tac"><p class="part-popup-ittext">'+msg+'</p><footer class="fn-mt-30"><a href="javascript:;" id="J_dialog_close" class="ui-btn ui-btn-red">确定</a></footer></div>',
                okValue: '确定',
                cancelValue: '取消'
            }).showModal();
            $('#J_dialog_close').click(function(){
                try{
                window.dlo.close().remove();
                }catch(e){}
            });
        },
        dialog: function(cfg,title){
            if (typeof(cfg) != 'object'){
                cfg={content:cfg};//默认为对话框内容
            }
            var dl = dialog($.extend({
                title: title || "温馨提示",
                lock:true,
                fixed:true,
                width:'480px',
                okValue: '确定',
                cancelValue: '取消',
                close: function(){
                    dl = null;
                }
            },cfg)).showModal();
            module.exports.loading=false;
            module.exports.dl = dl;
            if(!cfg.ajax) {
                return dl;
            }
            $.ajax({
                url:cfg.ajax.url||'',
                type:cfg.ajax.type||'POST',
                cache:false,
                data:cfg.ajax.data||'_=',
                dataType:cfg.ajax.dataType||'json',
                success: function (result){
                    if(cfg.ajax.callback){
                        cfg.ajax.callback(result,dl);//传入dialog对象，方便回调函数处理dialog逻辑
                    }
                },
                error:cfg.ajax.error||function(result,dl){
                    dl.close();
                }
            });
            return dl;
        },
        delDialog:function(cfg,options){/*删除确认对话框*/
            return dialog({
                id:'trj_cn_common_delte_dialog',
                title:'删除确认',
                content:'是否确定要删除?删除后，将无法恢复！',
                onclose:function(){
                    if(this._isDialogCanceled){
                        return true;
                    }
                    if(!options){
                        window.location.reload();
                    }
                    else if(options && options.removed){
                        for(var i=0;i<options.removed.length;i++){
                            $(options.removed[i]).fadeOut("slow");
                        }
                        window.location.reload();
                    }
                    return true;
                },
                cancelValue:'取消',
                cancel:function(){
                    this._isDialogCanceled=true;
                },
                okValue:'删除',
                ok: function (){
                    var that=this;
                    $.ajax({
                        type:'post',
                        url:cfg.url,
                        data:cfg.data,
                        cache:false,
                        dataType:'json',
                        success:function(result){
                            if(result.code<1 || result.code==200){
                                that.close();
                                return;
                            }
                            var msg=result.message+'(错误码:'+result.code+')';
                            that.content('');
                            that.button('');
                            that.content(msg);
                            that.ok=function(){};
                            that.okValue='确定';
                        }
                    });
                    return false;
                }
            });
        },
        confirmDialog:function(msg,title,btn_text,callback){
            window.dlo = dialog({
                title: title || "温馨提示",
                lock:true,
                fixed:true,
                width:'480px',
                content:' <div class="popup-msg-a fn-tac"><p class="part-popup-ittext">'+msg+'</p><footer class="fn-mt-30"><a href="javascript:;" id="J_dialog_btn_action"  class="ui-btn ui-btn-red">'+btn_text+'</a>&nbsp;&nbsp;<a href="javascript:;" id="J_dialog_close"  class="ui-btn ui-btn-red">取消</a></footer></div>',
                okValue: '确定',
                cancelValue: '取消',
                close: function(){
                    dl = null;
                }
            }).showModal();
            $('#J_dialog_close').click(function(){
                window.dlo.remove();
            });
            if (typeof(callback) == 'function')
            {
               $('#J_dialog_btn_action').click(function(){callback();});
            }
        },
        initDialog:function(cfg,title)
        {
          if (typeof(cfg) != 'object'){
                cfg={content:cfg};//默认为对话框内容
          }
          var dl = dialog($.extend({
                title: title || "温馨提示",
                lock:true,
                fixed:true,
                width:'480px',
                okValue: '确定',
                cancelValue: '取消',
                close:function(){dl = null;}
            },cfg));
          
          return dl;         
        }
    };
});