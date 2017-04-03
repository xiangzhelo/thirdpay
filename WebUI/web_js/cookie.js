var Cookie = {
    /**
     * 设置Cookie
     * @param int expires 过期时间, 19700101距今的毫秒数
     */
    setCookie: function(name, value, expires, path, domain, secure) {
        expires = new Date(expires);
        var curCookie = name + "=" + escape(value) +
        ((expires) ? "; expires=" + expires.toGMTString() : "") +
        ((path) ? "; path=" + escape(path) : "") +
        ((domain) ? "; domain=" + domain : "") +
        ((secure) ? "; secure" : "");

        document.cookie = curCookie;
    },
    getCookie:function(name){
        var dc = document.cookie;
        var prefix = name + "=";
        var begin = dc.indexOf("; " + prefix);

        if (begin == -1){
            begin = dc.indexOf(prefix);
            if (begin != 0) return null;
        }else begin += 2;
        var end = document.cookie.indexOf(";", begin);
        if (end == -1) end = dc.length;
        return unescape(dc.substring(begin + prefix.length, end));
    },
    isSupported: function() {
        var period = (new Date()).getTime()+3600000*24*30;
        if(typeof navigator.cookieEnabled != "boolean") {
            Cookie.setCookie("__CookieSupport__",
                                     "CookiesAllowed", period, null);
            var cookieVal = Cookie.getCookie("__CookieSupport__");
            navigator.cookieEnabled = (cookieVal == "CookiesAllowed");
            if(navigator.cookieEnabled) {
                this.deleteCookie("__CookieSupport__");
            }
        }
        return navigator.cookieEnabled;
    },
    deleteCookie:function(name){
        Cookie.setCookie(name, "",0);
    }
}