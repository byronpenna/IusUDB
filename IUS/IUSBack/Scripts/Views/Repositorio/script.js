$(document).ready(function () {
    // eventos 
        // doble click
            $(document).on("dblclick", ".ttlNombreCarpeta", function (e) {
                seccion = $(this).parents(".detalleCarpeta");
                nombre = $(this).text();
                ttlNombreCarpeta(seccion, nombre);
            })
        // submit 
            $(document).on("submit", "#frmSubir", function (e) {
                
                files = $("#flArchivos")[0].files;
                formulariohtml = $(this);
                frm = { txtHdIdCarpetaPadre: $(".txtHdIdCarpetaPadre").val() };
                console.log(files);
                e.preventDefault();
                cn = 0;
                console.log(files.length);
                $.each(files, function (file) {
                    data = getIndividualFormData(files[cn], frm);
                    frmSubir(data, formulariohtml.attr("action"));
                    cn++;
                })
                
                
            })
        // click 
            $(document).on("click", ".cuadritoIcono", function () {
                
            });
            // herramientas carpetas
                $(document).on("click", ".icoNuevaCarpeta", function (e) {
                    e.preventDefault();
                    div = getDivNewFolder();
                    $(".folders").prepend(div);
                })
                $(document).on("click", ".icoSubirFichero", function (e) {
                    e.preventDefault();
                    $(".divUpload").fadeIn(400, function () {
                        
                    });
                })
            // subir archivos 
        
                $(document).on("click", ".divUpload", function (e) {
                    console.log("ocultar");
                    $(this).fadeOut();
                })
                $(document).on("click", ".contenedorUpload", function (e) {
                    e.stopPropagation();
                });
            // guardar carpeta
                $(document).on("click", ".btnGuardarCarpeta", function (e) {
                    seccion = $(this).parents(".folder");
                    frm = {
                            idCarpetaPadre: $(".txtHdIdCarpetaPadre").val(),
                            nombre: seccion.find(".txtNombreCarpeta").val()
                    }
                    btnGuardarCarpeta(frm,seccion);
                });
                $(document).on("click", ".btnCancelarGuardarCarpeta", function (e) {
                    div = $(this).parents(".folder");
                    div.remove();
                })
            // actualizar carpeta
                $(document).on("click", ".btnEditarCarpeta", function () {
                    folder = $(this).parents(".folder");
                    frm = {
                        txtHdIdCarpeta: folder.find(".txtHdIdCarpeta").val(),
                        nombre: folder.find(".txtNombreCarpeta").val()
                    }
                    console.log("Formulario a enviar", frm);
                    btnEditarCarpeta(frm, folder);
                });
                $(document).on("click", ".btnCancelarEdicionCarpeta", function () {
                    seccion = $(this).parents(".detalleCarpeta");
                    btnCancelarEdicionCarpeta(seccion);
                });

})