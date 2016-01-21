$(document).ready(function () {
    // plugins
        //datePicker
            $(".datePicker").datepicker({
                dateFormat: "dd/mm/yy"
            });
    // eventos
        // click
            //$(".rowPadreParaBusqueda")
            $(document).on("click", ".divDesplegableFiltro", function () {
                // vars 
                    var targetDiv = $('.rowPadreParaBusqueda ');
                    var targetIco = $(this).find(".icoDesplegableNoticia");
                    console.log("Este es el target ico",targetIco.attr("class"));
                // do it 
                    if (targetDiv.is(":visible")) {
                        targetDiv.hide("slow");
                        targetIco.removeClass("fa-angle-up");
                        targetIco.addClass("fa-angle-down");
                    } else {
                        targetDiv.show("slow");
                        targetIco.removeClass("fa-angle-down");
                        targetIco.addClass("fa-angle-up");
                    }
            })
            $(document).on("click", ".btnCancelarBusqueda", function () {
                //var frm = serializeSection($(this).parents(".rowPadreParaBusqueda"));
                var frm = {
                    pagina: 1, // pagina 
                    cn: $(".txtHdRango").val()
                }
                cancelarBuscando(frm);
            })
            $(document).on("click",".btnBuscar",function(){
                var buscando = $(".txtHdBuscando").val();
                //if (buscando == 0) {
                var frm = serializeSection($(this).parents(".rowPadreParaBusqueda"));
                console.log("form enviado a la hora de buscar", frm);
                if (frm.dtpFin != "" || frm.dtpInicio != "" || frm.txtTituloNoticia != "") {
                    btnBuscar(frm, $(this));
                } else {
                    var mjsErrorFilter = $(".mjs-error-ponerFiltroBusqueda").val();
                    printMessage($(".divMensajesBusqueda"), mjsErrorFilter, false);
                }
                //
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
                    console.log("D: no hay");
                    printMessage($(".divResultadoNumeritos"), $(".mjs-error-noResultados").val(), false);
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