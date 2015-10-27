$(document).ready(function () {
    // plugins 
        // chosen
            $(".cbPais").chosen({ no_results_text: "Pais no encontrado", width: '100%' });
            $(".cbActividad").chosen({ no_results_text: "No se encuentro actividad", width: '100%' });
            $(".cbCargo").chosen({ no_results_text: "No se encuentro cargo", width: '100%' });
            // solo puesto por el efecto de las etiquetas
                $(".cbNiveles").chosen({ no_results_text: "No se encontro ese nivel", width: '100%' });
                $(".cbAreas").chosen({ no_results_text: "No se encontro esa area", width: '100%' });
                $(".cbRubros").chosen({ no_results_text: "No se encontro ese rubro", width: '100%' });
                $(".cbEstadoCivil").chosen({ no_results_text: "Estado civil no encontrado", width: '100%' });
                //cbRubros
        // eventos 
            $(document).on("click", ".btnBusquedaPerfil", function () {
                var seccion = $(this).parents(".divTodoForm");
                var frm = serializeSection(seccion);
                console.log("Formulario a enviar es:", frm);
                btnBusquedaPerfil(frm);
            });
            $(document).on("click", ".btnVerFicha", function () {
                var seccion = $(this).parents("tr");
                var frm = serializeSection(seccion);
                console.log("formulario a enviar es:", frm);
                btnVerFicha(frm);
            })
            
})