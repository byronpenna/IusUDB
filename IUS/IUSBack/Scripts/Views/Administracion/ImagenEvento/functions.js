﻿function storeCoords(c) {
    $(".x").val(c.x);
    $(".y").val(c.y);
    $(".imgAlto").val(c.h);
    $(".imgAncho").val(c.w);
};
function setFrmCoords(frm, imgQuery) {
    frm.imgAlto = frm.imgAlto / imgQuery.height();
    frm.imgAncho = frm.imgAncho / imgQuery.width();
    frm.x = frm.x / imgQuery.width();
    frm.y = frm.y / imgQuery.height();
    return frm;
}

function frmImagenEvento(data, url, image, jcrop_api) {
    var targetImg = $(".imgThumbnail"), boton = $(".btnSubir");
    accionAjaxWithImage(url, data, function (data) {
        console.log("respuesta despues de setear", data);
        if (data.estado) {
            jcrop_api.destroy();
            $(".imgThumbnail").attr("src", RAIZ + data.imagen);
            targetImg.attr("style", "");
            //
            /*targetImg.attr("src", RAIZ + "GestionInstituciones/getImageThumbLogo/" + data.id);
            console.log("url a poner es", RAIZ + "GestionInstituciones/getImageThumbLogo/" + data.id);
            targetImg.attr("style", "");*/
            boton.prop("disabled", true);
        }

    })
}