define(function(require){
    var $=require('jquery');
    $.fn.Tabs = function(options){
    return this.each(function(){
        // 处理参数
        options = $.extend({
            event : 'mouseover',
            timeout : 0,
            auto : 0,
            callback : null,
            switchBtn : false
        }, options);

        var self = $(this),
            tabBox = self.children( '.ui-tab-cont' ).children( 'div' ),
            menu = self.children( '.ui-tab-head' ),
            items = menu.find( 'li' ),
            timer;

        var tabHandle = function( elem ){
                elem.siblings( 'li' )
                    .removeClass( 'ui-tab-head-current' )
                    .end()
                    .addClass( 'ui-tab-head-current' );

                tabBox.siblings( 'div' )
                    .addClass( 'fn-hide' )
                    .end()
                    .eq( elem.index() )
                    .removeClass( 'fn-hide' );
            },

            delay = function( elem, time ){
                time ? setTimeout(function(){ tabHandle( elem ); }, time) : tabHandle( elem );
            },

            start = function(){
                if( !options.auto ) return;
                timer = setInterval( autoRun, options.auto );
            },

            autoRun = function( isPrev ){
                var current = menu.find( 'li.ui-tab-head-current' ),
                    firstItem = items.eq(0),
                    lastItem = items.eq(items.length - 1),
                    len = items.length,
                    index = current.index(),
                    item, i;

                if( isPrev ){
                    index -= 1;
                    item = index === -1 ? lastItem : current.prev( 'li' );
                }
                else{
                    index += 1;
                    item = index === len ? firstItem : current.next( 'li' );
                }

                i = index === len ? 0 : index;

                current.removeClass( 'ui-tab-head-current' );
                item.addClass( 'ui-tab-head-current' );

                tabBox.siblings( 'div' )
                    .addClass( 'fn-hide' )
                    .end()
                    .eq(i)
                    .removeClass( 'fn-hide' );
                if( options.callback ){
                    options.callback.call( self );
                }
            };

        items.bind( options.event, function(){
            delay( $(this), options.timeout );
            if( options.callback ){
                options.callback.call( self );
            }
        });

        if( options.auto ){
            start();
            self.hover(function(){
                clearInterval( timer );
                timer = undefined;
            },function(){
                start();
            });
        }

        if( options.switchBtn ){
            self.append( '<a href="#prev" class="tab-prev">previous</a><a href="#next" class="tab-next">next</a>' );
            var prevBtn = $( '.tab-prev', self ),
                nextBtn = $( '.tab-next', self );

            prevBtn.click(function( e ){
                autoRun( true );
                e.preventDefault();
            });

            nextBtn.click(function( e ){
                autoRun();
                e.preventDefault();
            });
        }

    });
};
});