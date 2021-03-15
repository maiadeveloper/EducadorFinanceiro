$(document).ready(function () {

    //Exibe mensagem sucesso modal
    $(function () {
        $('#modalMensagemSucesso').modal('toggle');
    });

    //Utilizado para exibir tooltip
    $(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });

    //Aplica mascara no documento pessoa fisica
    $(function () {
        $("#txtDocumento").mask("999.999.999-99");

        $("#rdbTipoFavorecidoPF").click(function () {
            $("#txtDocumento").val("");
            $("#txtDocumento").focus("");
            $("#txtDocumento").mask("999.999.999-99");
        });
        $("#rdbTipoFavorecidoPJ").click(function () {
            $("#txtDocumento").val("");
            $("#txtDocumento").focus("");
            $("#txtDocumento").mask("99.999.999/9999-99");
        });
    });

    //DataTables
    $(function () {
        $('#dataTableCategorias,#dataTableSubCategorias,#dataTableLancamentos,#dataTableFavorecidos').DataTable({
            "order": [[0, "asc"]],
            "language": {
                "lengthMenu": "Exibir _MENU_ registros por página",
                "zeroRecords": "Não existe registro para esta consulta - Tente com outro critério",
                "info": "Exibir página _PAGE_ de _PAGES_",
                "infoEmpty": "Nenhum registro disponível",
                "search": "Procurar por:",
                "paginate": {
                    "first": "Primeiro",
                    "last": "Último",
                    "next": "Próximo",
                    "previous": "Anterior"
                },
                "infoFiltered": "(filtrado do total de _MAX_ registros)"
            }
        });
    });

    //AutoComplete
    $(function () {
        $("#CategoriaId,#FavorecidoId").customselect({
            "search": true, // É pesquisavel?
            "searchblank": false,// pesquisar opções de valor em branco?
            "showblank": false, // Mostrar opções de valor em branco?
            "hoveropen": false,// Caneta a seleção ao passar o mouse?
            "showdisabled": true,// Mostrar opções desativadas?
        });
    });

    //Aplica mascara no campo valor(moeda)
    $(function () {
        $('#Valor').maskMoney({
            decimal: ',',
            thousands: '.',
            allowZero: true
        });
    });

    //Carrega listagem de subcategorias de acordo com o ID da categoria selecionada
    $(function () {
        $("#CategoriaId").change(function () {
            $.ajax({
                dataType: "json",
                type: "GET",
                url: "/SubCategoria/ObterSubcategoriasPorCategoriaId/?id=" + $("#CategoriaId").val(),
                success:
                    function (dados) {
                        $("#SubCategoriaId").empty().append("<option>Carregando...</option>");
                        setTimeout(function () {
                            $("#SubCategoriaId").empty().append("<option value> -- Selecione uma opção -- </option>");
                            $("#SubCategoriaId").focus();
                            $(dados).each(function (i) {
                                $("#SubCategoriaId").append('<option value="' + dados[i].value + '">' + dados[i].text + '</option>');
                            });
                        }, 1000);
                    }
            });
        });
    });

    //Consulta saldo resumo via ajax
    $(function () {
        $('#boxload').hide();
        $('#boxResultados').hide();

        $("#btnConsultarSaldo").click(function () {
            $('#boxload').show();
            $('#boxResultados').hide();
            $.ajax({
                dataType: "json",
                type: "GET",
                url: "/Lancamento/ConsultarResumo/?dataInicio=" + $('#txtDataInicial').val() + "&dataFim=" + $('#txtDataFim').val(),
                success:
                    function (dados) {
                        setTimeout(function () {
                            $('#lblTotalSaldo').text(dados.totalSaldo);
                            $('#lblTotalReceita').text(dados.totalReceita);
                            $('#lblTotalDespesa').text(dados.totalDespesa);
                            $('#boxload').hide();
                            $('#boxResultados').show();
                        }, 1000);
                    },
            });
        });
    });
});



