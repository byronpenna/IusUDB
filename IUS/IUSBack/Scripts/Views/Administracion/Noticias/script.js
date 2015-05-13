$(document).ready(function () {
    
    bkLib.onDomLoaded(function() {
        txtAreaEditor = new nicEditor().panelInstance('editor');
    })
    //nicEditors.findEditor('editor').getContent();
    $(document).on("keypress", ".txtTamañoImagen", function (e) {
        console.log("string",String.fromCharCode(e.which));
    })
    $(document).on("click", ".nicEdit-main img", function () {
        $(".activeRichImage").removeClass("activeRichImage");
        $(this).addClass("activeRichImage");
    })
})