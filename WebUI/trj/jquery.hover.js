(function($){
    $.fn.hoverClass = function(b){
        var a = this;
        a.each(function(c){
            a.eq(c).hover(function(){
                $(this).addClass(b)
            },function(){
                $(this).removeClass(b)
            })
        });
        return a
    };
})