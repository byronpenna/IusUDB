// acciones script
    function frmMiniatura(data, url,image) {
        accionAjaxWithImage(url, data, function (data) {
            console.log("respuesta", data);
            if (data.estado) {
                $(".imgThumbnail").attr("src", image.src);
            }

        })
    }