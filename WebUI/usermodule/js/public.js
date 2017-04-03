jQuery.fn.numeral = function() {
        this.bind("keypress",
        function() {
            if (event.keyCode == 46) {
                if (this.value.indexOf(".") != -1) {
                    return false;
                }
            } else {
                return event.keyCode >= 46 && event.keyCode <= 57;
            }
        });
        this.bind("blur",
        function() {
            if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
                this.value = this.value.substr(0, this.value.length - 1);
            } else if (isNaN(this.value)) {
                this.value = "";
            }
        });
        this.bind("paste",
        function() {
            var s = clipboardData.getData('text');
            if (!/\D/.test(s));
            value = s.replace(/^0*/, '');
            return false;
        });
        this.bind("dragenter",
        function() {
            return false;
        });
        this.bind("keyup",
            function() {
                if (/(^0+)/.test(this.value)) {
                    this.value = this.value.replace(/^0*/, '');
                }
            });
    };