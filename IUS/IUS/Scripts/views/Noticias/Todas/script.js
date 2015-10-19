$(document).ready(function () {
    // plugins
        //datePicker
            $(".datePicker").datepicker({
                dateFormat: "dd/mm/yy"
            });
    // eventos
        // click
            //$(".rowPadreParaBusqueda")
            $(document).on("click",".btnBuscar",function(){
                var frm = serializeSection($(this).parents(".rowPadreParaBusqueda"));
                console.log(frm);
                btnBuscar(frm);
            })
            
            $(document).on("click", ".flecha", function () {
                var padre       = $(this).parents(".divContenedorNumeros");
                var target      = padre.find(".grupoNumActivo");
                var siguiente   = null;
                
                
                if ($(this).hasClass("fechaLeft")) {
                    /*console.log(target.prev())
                    target.prev().addClass("grupoNumActivo");*/
                    siguiente = target.prev();
                } else if ($(this).hasClass("flechaRight")) {
                    /*console.log(target.next())
                    target.next().addClass("grupoNumActivo");*/
                    siguiente = target.next();
                }
                console.log(siguiente);
                console.log(siguiente.hasClass("divContenedorNumeritos"));
                if (siguiente.hasClass("divContenedorNumeritos")) {
                    target.addClass("hidden");
                    target.removeClass("grupoNumActivo");

                    siguiente.addClass("grupoNumActivo");
                    siguiente.removeClass("hidden");
                } else {
                    //console.log("D: ");
                    printMessage($(".divResultadoNumeritos"), "No hay mas resultados en esa dirección", false);
                }
            })
            $(document).on("click", ".numPaginacion", function () {
                var frm = {
                    pagina: $(this).text(), // pagina 
                    cn:     $(".txtHdRango").val()
                }
                console.log("formulario a enviar", frm);
                $(".activeNum").removeClass("activeNum");
                $(this).addClass("activeNum");
                numPaginacion(frm);
            })
            
})