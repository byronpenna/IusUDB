// acciones script
    function storeCoords(c) {
        //console.log(c);
        $(".x").val(c.x);
        $(".y").val(c.y);
        $(".imgAlto").val(c.h);
        $(".imgAncho").val(c.w);
        /*$(".imgAlto").val(0);
        $(".imgAncho").val(0);*/
    };
    function setFrmCoords(frm,imgQuery) {
        frm.imgAlto     = frm.imgAlto / imgQuery.height();
        frm.imgAncho    = frm.imgAncho / imgQuery.width();
        frm.x           = frm.x / imgQuery.width();
        frm.y           = frm.y / imgQuery.height();
        return frm;
    }
    function frmMiniatura(data, url, image, jcrop_api) {
        var targetImg = $(".imgThumbnail"), boton = $(".btnSubir");
        accionAjaxWithImage(url, data, function (data) {
            console.log("respuesta", data);
            if (data.estado) {
                jcrop_api.destroy();
                //$(".imgThumbnail").attr("src", image.src);
                targetImg.attr("src", RAIZ + "GestionInstituciones/getImageThumbLogo/" + data.id);
                console.log("url a poner es", RAIZ + "GestionInstituciones/getImageThumbLogo/" + data.id);
                targetImg.attr("style", "");
                boton.prop("disabled", true);
            }

        })
    }