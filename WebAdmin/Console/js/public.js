
function handler(tp) {
}

var mytr = document.getElementById("tab").getElementsByTagName("tr");
for (var i = 1; i < mytr.length; i++) {
    mytr[i].onmouseover = function() {
        var rows = this.childNodes.length;
        for (var row = 0; row < rows; row++) {
            this.childNodes[row].style.backgroundColor = '#E6EEFF';
        }
    };
    mytr[i].onmouseout = function() {
        var rows = this.childNodes.length;
        for (var row = 0; row < rows; row++) {
            this.childNodes[row].style.backgroundColor = '';
        }
    };
}