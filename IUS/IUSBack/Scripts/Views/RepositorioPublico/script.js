﻿$(document).ready(function () {
    // eventos
        //click 
            $(document).on("click", ".icoNuevaCarpeta", function (e) {
                e.preventDefault();
                div = getDivNewFolder();
                $(".folders").prepend(div);
            })
            // entrar a carpeta
                $(document).on("click", ".cuadritoCarpeta", function () {
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
                    btnGuardarCarpeta(frm, seccion);
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
        // doble click
            $(document).on("dblclick", ".ttlNombreCarpeta", function (e) {
                e.cancelBubble = true;
                seccion = $(this).parents(".detalleCarpeta");
                nombre = $(this).text();
                ttlNombreCarpeta(seccion, nombre);
            })
})