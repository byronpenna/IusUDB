$(document).ready(function () {
    
    // eventos 
        // click
            $(document).on("click", ".menuLateral li", function () {
                //$(this).addClass("activeRecurso");

                $(".activeRecurso").find(".imgLateral").attr("src", $(".activeRecurso").find(".txtHdNormal").val());
                $(".activeRecurso").removeClass("activeRecurso");
        
                $(this).find(".imgLateral").attr("src", $(this).find(".txtHdRoja").val());
                $(this).addClass("activeRecurso");
                var frm = serializeSection($(this));
                window.history.pushState({}, "Titulo", RAIZ+"Repositorio/index/" + frm.txtHdIdTipoArchivo);
                $(".tituloCuerpo").empty().append($(this).find(".texto").text());
                getArchivos(frm);
            })
        // keyup
            $(document).on("keyup", ".txtSearch", function (e) {
                var charCode = e.which;
                console.log("el valor es: ", $(this).val());
                if ($(this).val() == "") {
                    console.log("Entro aqui");
                    $(".trFichero").removeClass("hidden");

                } else {
                    console.log("Entro por aca");
                    buscarCarpeta($(this).val());
                }
            
            })
            iniciales();
})