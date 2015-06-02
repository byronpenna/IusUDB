$(document).ready(function () {
    // plugins
        // chosen
            $(".cbLlave").chosen({ no_results_text: "Esa llave no existe", width: '100%' });
            $(".cbPagina").chosen({ no_results_text: "La pagina no existe", width: '100%' });
            $(".cbIdioma").chosen({ no_results_text: "El idioma no existe", width: '100%' });
            //$(".cbEdit").chosen({ width: '100%' });
        // datatable
            $(".tableLlaveIdioma").DataTable();
    // eventos  
        // click
            $(document).on("click", ".btnAgregarLlave", function () {
                var frm = new Object();
                frm.idLlave     = $(".cbLlave").val();
                frm.idIdioma    = $(".cbIdioma").val();
                frm.traduccion  = $(".txtAreaTraduccion").val();
                btnAgregarLlave(frm)
            });
            $(document).on("click", ".btnCancelarEdit", function () {
                var x = confirm("¿Esta seguro que desea cancelar edicion del registro?");
                if (x) {
                    trTraduccion = $(this).parents("tr");
                    btnCancelarEdit(trTraduccion);
                }
            });
            $(document).on("click", ".btnEditarTraduccion", function () {
                var x = confirm("¿Esta seguro que desea editar este registro?");
                if (x) {
                    trTraduccion = $(this).parents("tr");
                    btnEditarTraduccion(trTraduccion);
                }
            });
            $(document).on("click", ".btnActualizar", function () {
                trTraduccion = $(this).parents("tr");
                btnActualizar(trTraduccion);
            });
            $(document).on("click", ".btEliminarTraduccion", function () {
                var x = confirm("¿esta seguro que desea eliminar esta traduccion");
                if (x) {
                    tr = $(this).parents("tr");
                    btEliminarTraduccion(tr);
                    resetChosen($(".cbPagina"));
                    $(".cbLlave").empty().append("<option value='-1' disabled>Seleccione pagina primero</option>");
                    resetChosen($(".cbLlave"));
                }
            });
        // change
            $(document).on("change", ".cbIdioma", function () {
                var frm = new Object();
                frm.idPaginaFront   = $(".cbPagina").val();
                frm.idIdioma        = $(this).val();
                cbIdioma(frm);
            });
            $(document).on("change", ".cbPagina", function () {
                var frm = new Object();
                frm.idPaginaFront   = $(this).val();
                frm.idIdioma        = $(".cbIdioma").val();
                cbPagina(frm);
            });
            

});