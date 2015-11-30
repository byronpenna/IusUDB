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
    function frmMiniatura(data, url,image) {
        accionAjaxWithImage(url, data, function (data) {
            console.log("respuesta", data);
            if (data.estado) {
                $(".imgThumbnail").attr("src", image.src);
            }

        })
    }