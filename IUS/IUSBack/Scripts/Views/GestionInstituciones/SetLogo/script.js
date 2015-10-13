$(document).ready(function () {
    // eventos
        // change
            $(document).on("change", "#flMiniatura", function () {
                if ($(this).val() == "") {
                    $(".btnSubir").prop("disabled", true);
                } else {
                    $(".btnSubir").prop("disabled", false);
                }
            })
        // submit
            $(document).on("submit", "#frmMiniatura", function (e) {
                var files = $("#flMiniatura")[0].files;
                frm = serializeToJson($(this).serializeArray());
                formulario = $(this);
                data = getObjFormData(files, frm);
                e.preventDefault();
                getImageFromInputFile($("#flMiniatura")[0].files[0], function (imagen) {
                    if (imagen.width == imagen.height) {
                        frmMiniatura(data, formulario.attr("action"), imagen)    
                    } else {
                        //alert("La imagen debe ser cuadrada");
                        printMessage($(".divResultado"), "La imagen debe ser cuadrada", false);
                    }
                });
            })
        // click
            
})