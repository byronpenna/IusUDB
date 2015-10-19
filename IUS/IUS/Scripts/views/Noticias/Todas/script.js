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
            $(document).on("click", ".numPaginacion", function () {
                var frm = {
                    pagina: $(this).text(), // pagina 
                    cn:     $(".txtHdRango").val()
                }
                console.log("formulario a enviar",frm);
                numPaginacion(frm);
            })
            
})