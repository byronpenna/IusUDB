// generics 
    // jcrop 
        function storeCoords(c) {
            //console.log(c);
            $(".x").val(c.x);
            $(".y").val(c.y);
            $(".imgAlto").val(c.h);
            $(".imgAncho").val(c.w);
        };
// scripts 
    function frmMiniatura(data, url, image) {
        accionAjaxWithImage(url, data, function (data) {
            console.log("reespueta", data);
            if (data.estado) {
                $(".imgThumbnail").attr("src", image.src);
            }
        })
    }