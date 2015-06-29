$(document).ready(function () {
    // eventos
        // submit
            $(document).on("submit", "#frmMiniatura", function (e) {
                var files = $("#flMiniatura")[0].files;
                frm = serializeToJson($(this).serializeArray());
                formulario = $(this);
                data = getObjFormData(files, frm);
                e.preventDefault();
                getImageFromInputFile($("#flMiniatura")[0].files[0], function (imagen) {
                    if (imagen.width == imagen.height) {
                        frmMiniatura(data, formulario.attr("action"))
                    } else {
                        alert("La imagen debe ser cuadrada");
                    }
                });
            })
        // click
            
})