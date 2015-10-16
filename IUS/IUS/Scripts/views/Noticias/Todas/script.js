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
})