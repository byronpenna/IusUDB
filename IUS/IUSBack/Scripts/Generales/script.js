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
