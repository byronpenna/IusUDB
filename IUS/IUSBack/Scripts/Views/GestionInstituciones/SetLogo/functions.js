// acciones script
    function frmMiniatura(data, url) {
        
        accionAjaxWithImage(url, data, function (data) {
            console.log("respuesta", data);

        })
    }