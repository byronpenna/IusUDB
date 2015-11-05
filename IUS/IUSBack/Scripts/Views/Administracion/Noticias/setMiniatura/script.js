$(document).ready(function () {
    // plugins
        // jcrop
            var jcrop_api;
            jcrop_api = $.Jcrop('.imgThumbnail', {
                onSelect: storeCoords,
                onChange: storeCoords,
                aspectRatio: 1
            });
    // eventos 
        // submit
            $(document).on("submit", "#frmMiniatura", function (e) {
                var files = $("#flMiniatura")[0].files;
                frm = serializeToJson($(this).serializeArray());
                formulario = $(this);
                data = getObjFormData(files, frm);
                e.preventDefault();
                getImageFromInputFile($("#flMiniatura")[0].files[0], function (imagen) {
                    console.log(imagen.width, imagen.height);
                    if (imagen.width == imagen.height) {
                        frmMiniatura(data, formulario.attr("action"),imagen);
                    } else {
                        alert("La imagen debe ser cuadrada");
                    }
                })
                /*
                var oFReader = new FileReader();
                oFReader.readAsDataURL($("#flMiniatura")[0].files[0]);

                oFReader.onload = function (oFREvent) {
                    //document.getElementById("flMiniatura").src = oFREvent.target.result;
                    console.log($(this));
                    imagen = new Image();
                    imagen.src = $(this)[0].result;
                    console.log(imagen.width);

                };*/
                
                //console.log("la data e enviar es ",data);
                //frmMiniatura(data, $(this).attr("action"));
            })
        // change 
            $(document).on("change", "#flMiniatura", function (e) {
                var boton = $(".botonSubir");
                console.log("asdqw");
                if ($(this).val() == "") {
                    boton.prop("disabled", true);
                } else {
                    boton.prop("disabled", false);
                }

            });
            
})