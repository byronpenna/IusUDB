// validaciones 
function soloLetras() {
    exp = "[a-z A-Zñáéíóú]";
    return exp;
}
function test(exp,str) {
    var patt = new RegExp(exp);
    var res = patt.test(str);
    return res;
}
$(document).ready(function () {
    $(document).on("keypress", ".soloLetras", function (e) {
        var str = String.fromCharCode(e.which);
        exp = soloLetras();
        var x = test(exp,str);
        if (!x) {
            e.preventDefault();
        }
    })
});
