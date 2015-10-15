$(document).ready(function () {
    // plugins 
        $(".cbPais").chosen({ no_results_text: "Pais no encontrado", width: '100%' });
    // eventos 
        // keydown
            $(document).on("keyup", ".txtNombreInstitucion", function (e) {
                var charCode = e.which;
                if (charCode == 27) { // tecla esc cancela todo
                    $(this).val("");
                }
                console.log("D: ",$(this).val());
                buscarInstitucionesPorNombre($(this).val());
            })
        // change
            $(document).on("change", ".cbPais", function () {
                var idPais = $(this).val();
                buscarPorPais(idPais);
            })
});