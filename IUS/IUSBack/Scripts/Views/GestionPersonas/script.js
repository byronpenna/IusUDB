$(document).ready(function () {
    // plugins 
        // datatable 
            $(".tablePersonas").DataTable({
                "bDestroy": true,
                "bSort": false
            });
        // datepicker
            $(".dtFechaNacimiento").datepicker({
                dateFormat: "dd/mm/yy"
            });
    // eventos 
        // keydown
            $(document).on("keydown", ".txtEdit", function (e) {
                console.log("entro");
                var tr = $(this).parents(tr);
                switch(e.which) {
                    case 13: {
                        tr.find(".btnActualizar").click();
                        break;
                    }
                }
            })
            $(document).on("keydown", ".inputFormulario", function (e) {
                var tr = $(this).parents(tr);
                switch (e.which) {
                    case 13: {
                        tr.find(".btnAgregarPersona").click();
                    }
                }
            })
        // click
            // eliminar
                $(document).on("click", ".btnEliminar", function () {
                    var x = confirm("¿esta seguro que desea eliminar este registro?");
                    if (x) {
                        tr = $(this).parents("tr")
                        btnEliminar(tr);
                    }
                })
            // ingresar
                $(document).on("click", ".btnAgregarPersona", function () {
                    tr = $(this).parents("tr");
                    btnAgregarPersona(tr);
                })
            // actualizar 
                $(document).on("click", ".btnActualizarTodo", function () {
                    tabla = $(this).parents("table");
                    btnActualizarTodo(tabla);
                });
                $(document).on("click", ".btnActualizar", function () {
                    var trPersona = $(this).parents("tr");
                    actualizar(trPersona);
                })
            // editar
                $(document).on("click", ".btnEditar", function () {
                    trPersona = $(this).parents(".trPersona");
                    trPersona.find(".divResultado").empty();
                    table = $(".tablePersonas").DataTable();
                    var d = table.row($(this)).data();
                    editMode(trPersona);
                });
                $(document).on("click", ".btnCancelarEdit", function () {
                    trPersona = $(this).parents(".trPersona");
                    controlesEdit(false, trPersona);
                })
            // experiencia de usuario
                $(document).on("click", ".btnEditMode,.btnEditar", function () {
                    //cambiarEstadoControlGlobal();
                })
                $(document).on("click", ".btnCancelarGlobal", function () {
                    //cancelarGlobal();
                    //cambiarEstadoControlGlobal();
                });
    // validaciones 
        
});