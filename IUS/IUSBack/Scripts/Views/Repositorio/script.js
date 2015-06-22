$(document).ready(function () {
    // eventos 
    $(window).bind("popstate", function (e) {
        console.log("set back");
    })
        
        // submit 
            $(document).on("submit", "#frmSubir", function (e) {
                
                files = $("#flArchivos")[0].files;
                formulariohtml = $(this);
                frm = { txtHdIdCarpetaPadre: $(".txtHdIdCarpetaPadre").val() };
                console.log(files);
                e.preventDefault();
                cn = 0;
                totalFiles = files.length;
                $(".imgCargando").find("img").removeClass("hidden");
                $(".tbArchivos").empty();
                $(".porcentajeCarga").empty();
                $.each(files, function (file) {
                    frm.cn = cn;
                    data = getIndividualFormData(files[cn], frm);
                    frmSubir(data, formulariohtml.attr("action"),totalFiles);
                    cn++;
                });
                /**/
                
                
            })
        // click 
            $(document).on("click", ".cuadritoCarpeta", function () {
                /*frm = { idCarpeta: $(this).parents(".folder").find(".txtHdIdCarpeta").val() }
                cuadritoCarpeta(frm);*/
                window.location = RAIZ + "Repositorio/index/" + $(this).parents(".folder").find(".txtHdIdCarpeta").val();
                //console.log("vas a redireccionar");
            });
            // eliminar archivos 
                $(document).on("click", ".icoEliminarArchivo", function () {
                    var x = confirm("Esta seguro que desea eliminar este archivo");
                    if (x) {
                        seccion = $(this).parents(".folder");
                        frm = { idArchivo: seccion.find(".txtHdIdArchivo").val() }
                        icoEliminarArchivo(frm,seccion);
                    }
                })
            // cambiar nombre archivo 
                $(document).on("click", ".btnEditarArchivo", function (e) {
                    seccion = $(this).parents(".folder");
                    frm = {
                        idArchivo:      seccion.find(".txtHdIdArchivo").val(),
                        nombreArchivo:  seccion.find(".txtNombreCarpeta").val()
                    }
                    console.log("formulario a enviar", frm);
                    btnEditarArchivo(frm);
                })
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
                            nombre: seccion.find(".txtNombreCarpetaSave").val()
                    }
                    console.log("formulario a enviar", frm);
                    btnGuardarCarpeta(frm,seccion);
                });
                $(document).on("click", ".btnCancelarGuardarCarpeta", function (e) {
                    div = $(this).parents(".folder");
                    div.remove();
                })
            // eliminar carpeta
                $(document).on("click", ".icoEliminarCarpeta", function () {
                    seccion = $(this).parents(".folder");
                    frm = { idCarpeta: seccion.find(".txtHdIdCarpeta").val() }
                    var x = confirm("¿Esta seguro que desea eliminar esta carpeta?");
                    if (x) {
                        icoEliminarCarpeta(frm, seccion);
                    }
                    
                });
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
            // cambiar nombre carpeta
                $(document).on("click", ".sinRedirect", function (e) {
                    e.stopPropagation();
                })
        // doble click

                $(document).on("dblclick", ".ttlNombreCarpeta", function (e) {
                    e.cancelBubble = true;
                    seccion = $(this).parents(".detalleCarpeta");
                    nombre = $(this).text();
                    ttlNombreCarpeta(seccion, nombre);
                    console.log("primero aqui");
                })
})