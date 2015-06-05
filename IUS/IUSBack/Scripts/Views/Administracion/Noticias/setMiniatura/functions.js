function frmMiniatura(data,url,image){
    accionAjaxWithImage(url, data, function (data) {
        console.log("reespueta", data);
        if (data.estado) {
            $(".imgThumbnail").attr("src", image.src);
        }
    })
}