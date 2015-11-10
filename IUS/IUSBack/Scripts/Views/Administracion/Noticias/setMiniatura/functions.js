var jcrop_api = null;
// generics 
    // jcrop 
        function storeCoords(c) {
            //console.log(c);
            $(".x").val(c.x);
            $(".y").val(c.y);
            $(".imgAlto").val(c.h);
            $(".imgAncho").val(c.w);
        };
        function inicialFoto() {
            $(".x").val(0);
            $(".y").val(0);
            $(".imgAlto").val(0);
            $(".imgAncho").val(0);
        }
// scripts 
    function frmMiniatura(data, url, image) {
        var targetImg = $(".imgThumbnail");
        accionAjaxWithImage(url, data, function (data) {
            console.log("reespueta", data);
            if (data.estado) {
                jcrop_api.destroy();
                targetImg.attr("src", RAIZ + "getImageThumbnail/" + data.id);
                targetImg.attr("style", "");
            }
        })
    }