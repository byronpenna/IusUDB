﻿$(document).ready(function () {
    $(document).on("click", ".divAreaCarrera,.cuadritoNivelEducacion", function () {
        console.log("Click en area carrera");
        //var padre = $(this).parents(".divAreaCarrera");
        if ($(this).find(".txtHdEstado").val() == 1) {
            $(this).removeClass("cuadritoSelected");
            $(this).find(".txtHdEstado").val(0)
        } else {
            $(this).addClass("cuadritoSelected");
            $(this).find(".txtHdEstado").val(1)
        }
    })
    $(document).on("click", ".btnGuardarAreaConocimiento", function () {
        var frm = serializeSection($(".divAreaCarrera"));
        frm.areaCarrera = new Array();
        $.each(frm.txtHdIdEstadoArea, function (i, val) {
            if (val == 1) {
                frm.areaCarrera.push(frm.txtHdIdArea[i]);
            }
        })
        frm.idInstitucion = $(".txtHdIdInstitucion").val();
        btnGuardarAreaConocimiento(frm);
        
    })
    $(document).on("click", ".btnGuardarNivel", function () {
        var frm = serializeSection($(".rowOpcionesNiveles"));
        frm.estadoNivel = new Array();
        $.each(frm.txtHdEstadoNivel, function (i, val) {
            if (val == 1) {
                frm.estadoNivel.push(frm.txtHdIdNivelEducacion[i]);
            }
        })
        frm.idInstitucion = $(".txtHdIdInstitucion").val();
        
        btnGuardarNivel(frm);
    })
})