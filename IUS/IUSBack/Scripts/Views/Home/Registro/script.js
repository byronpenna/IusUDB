$(document).ready(function () {
    // plugins
        // datepicker
            $(".dtpFechaNac").datepicker({
                dateFormat: "dd/mm/yy"
            });
    // eventos
        // click
            $(document).on("submit", ".frmRegistrar", function (e) {
                e.preventDefault();
                var frm = serializeToJson($(this).serializeArray());
                console.log(frm);
                frmRegistrar(frm);
            })
});