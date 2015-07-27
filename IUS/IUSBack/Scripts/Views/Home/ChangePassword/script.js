$(document).ready(function () {
    // eventos 
        // submit
            $(document).on("submit", "#frmChangePass", function (e) {
                e.preventDefault();
                var frm = serializeToJson($(this).serializeArray());
                console.log(frm);
                if (frm.txtPass == frm.txtConfirmPass) {
                    frmChangePass(frm);
                }
            })
})