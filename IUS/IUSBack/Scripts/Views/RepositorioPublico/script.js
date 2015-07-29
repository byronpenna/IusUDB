$(document).ready(function () {
    // eventos
        //keydown
            
            $(document).on("click", ".txtNombreCarpetaSave", function (e) {
                console.log(e.wich);
            })
        //click 
            // Busqueda
                /*$(document).on("click", ".spIrBuscar", function (e) {

                })*/
                $(document).on("click", ".btnEditarArchivo", function (e) {
                    var seccion = $(this).parents(".folder");
                    var frm = {
                        idArchivo: seccion.find(".txtHdIdArchivoPublico").val(),
                        nombreArchivo: seccion.find(".txtNombreCarpeta").val()
                    }
                    //console.log(frm);
                    btnEditarArchivo(frm, seccion);
                });
                $(document).on("click", ".btnCancelarGuardarCarpeta", function (e) {
                    div = $(this).parents(".folder");
                    div.remove();
                })
                $(document).on("click", ".iconoVista", function (e) {
                    e.preventDefault();
                    if ($(this).hasClass("iconoVistaCuadricula")) {
                        verCuadricula();
                    } else if ($(this).hasClass("icoVistaLista")) {
                        verLista();
                    }
                    
                })
            //
                $(document).on("click", ".icoNuevaCarpeta", function (e) {
                    //console.log("D: D: D: D:")
                    e.preventDefault();
                    div = getDivNewFolder();
                    $(".cuadriculaView").prepend(div);
                })
            // eliminar carpeta 
                $(document).on("click", ".icoEliminarArchivo", function (e) {
                    e.preventDefault();
                    var x = confirm("¿Esta seguro que desea dejar de compartir este archivo?");
                    if (x) {
                        var seccion = $(this).parents(".folder");
                        var frm = {
                            idArchivoPublico: $(".txtHdIdArchivoPublico").val()
                        }
                        icoEliminarArchivo(frm,seccion);
                    }
                })
            // entrar a carpeta
                $(document).on("click", ".cuadritoCarpeta", function (e) {
                    e.cancelBubble = true;
                    var estado = $(this).attr("id");
                    if (estado != '0') {
                        window.location = RAIZ + "RepositorioPublico/index/" + $(this).parents(".folder").find(".txtHdIdCarpeta").val();
                    }
                });
            // guardar carpeta
                $(document).on("click", ".btnGuardarCarpeta", function (e) {
                    seccion = $(this).parents(".folder");
                    frm = {
                        idCarpetaPadre: $(".txtHdIdCarpetaPadre").val(),
                        nombre: seccion.find(".txtNombreCarpetaSave").val()
                    }
                    console.log(frm);
                    if (frm.nombre != "") {
                        btnGuardarCarpeta(frm, seccion);
                    } else {
                        printMessage($(".mensajeNewFolder"), "El nombre de la carpeta no puede ser vacio", false);
                    }
                    
                })
            // cambiar nombre a carpeta
                $(document).on("click", ".btnEditarCarpeta", function (e) {
                    folder = $(this).parents(".folder");
                    frm = {
                        txtHdIdCarpeta: folder.find(".txtHdIdCarpeta").val(),
                        nombre: folder.find(".txtNombreCarpeta").val()
                    }
                    btnEditarCarpeta(frm, folder);
                })
                $(document).on("click", ".btnCancelarEdicionCarpeta", function () {
                    seccion = $(this).parents(".detalleCarpeta");
                    btnCancelarEdicionCarpeta(seccion);
                })
                $(document).on("click", ".icoEliminarCarpeta", function () {
                    seccion = $(this).parents(".folder");
                    frm = { idCarpeta: seccion.find(".txtHdIdCarpeta").val() }
                    var x = confirm("¿Esta seguro que desea eliminar esta carpeta?");
                    if (x) {
                        icoEliminarCarpeta(frm, seccion);
                    }

                });
                $(document).on("click", ".sinRedirect", function (e) {
                    e.stopPropagation();
                })
        // doble click
            $(document).on("dblclick", ".ttlNombreCarpeta", function (e) {
                e.cancelBubble = true;
                seccion = $(this).parents(".detalleCarpeta");
                nombre = $(this).text();
                ttlNombreCarpeta(seccion, nombre);
            })
})