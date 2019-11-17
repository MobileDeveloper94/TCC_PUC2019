//global
var Id_Avaliacao;

//start page
$(document).ready(function () {
    Id_Avaliacao = $("#Id_Avaliacao").val();

    LoadQuestoes();

    $("#btnNewQuestao").click(AddNewQuestao);
    $("#btnFechar").click(Fechar);
    $("#btnSalvarQuestao").click(SaveQuestao);
});

function AddNewQuestao() {
    $("#btnNewQuestao").hide();

    $("#Enunciado_Questao").val('');
    $("#Alternativa1_Questao").val('');
    $("#Alternativa2_Questao").val('');
    $("#Alternativa3_Questao").val('');
    $("#Alternativa4_Questao").val('');
    $("#Alternativa5_Questao").val('');
    $("#validate").text('');
    $("#formQuestao").show();

}

function LoadQuestoes() {
    var html = '';
    var url = '/api/questao/list/' + Id_Avaliacao;
    $.get(url, function (data, status) {
        if (status === "success") {
            data.forEach(function (item) {
                html = html + '<tr>';
                html = html + '<th scope="row">' + item.Id + '</th>';
                html = html + '<td>' + item.Enunciado + '</td>';
                
                html = html + '<td><a href="#" onclick="EditQuestao(' + item.Id + ')"><i class="fa fa-pencil"></i></a> | <a href="#" onclick="DeleteQuestao(' + item.Id + ')"><i class="fa fa-trash"></i></a></td>';
                html = html + '</tr>';
            });

            $("#listaQuestao").html(html);
        }
    });
}

function SaveQuestao() {

    $("#validate_Enunciado_Questao").text("");


    if ($("#Enunciado_Questao").val() === "" || $("#Alternativa1_Questao").val() === "" || $("#Alternativa2_Questao").val() === "" || $("#Alternativa3_Questao").val() === "" || $("#Alternativa4_Questao").val() === "" || $("#Alternativa5_Questao").val() === "") {
        $("#validate").text("Todos os campos são obrigatórios");
        
        return;
    }


    var dados = {
        Id: $("#Id_Questao").val(),
        Id_Avaliacao: Id_Avaliacao,
        Enunciado: $("#Enunciado_Questao").val(),
        Alternativa1: $("#Alternativa1_Questao").val(),
        Alternativa2: $("#Alternativa2_Questao").val(),
        Alternativa3: $("#Alternativa3_Questao").val(),
        Alternativa4: $("#Alternativa4_Questao").val(),
        Alternativa5: $("#Alternativa5_Questao").val(),
        Cod_AlternativaCorreta: $("input[name='Flg_Correta']:checked").val()
    };

    

    $.post("/api/Questao", dados)
        .done(function () {
            LoadQuestoes();
            $("#formQuestao").hide();
        });
}

function EditQuestao(val) {
    $("#btnNewQuestao").hide();

    var url = '/api/questao/detail/' + val;
    $.get(url, function (data, status) {
        if (status === "success") {

            $("#Id_Questao").val(data[0].Id);
            $("#Enunciado_Questao").val(data[0].Enunciado);
            $("#Alternativa1_Questao").val(data[0].Alternativa1);
            $("#Alternativa2_Questao").val(data[0].Alternativa2);
            $("#Alternativa3_Questao").val(data[0].Alternativa3);
            $("#Alternativa4_Questao").val(data[0].Alternativa4);
            $("#Alternativa5_Questao").val(data[0].Alternativa5);
            $('input:radio[name="Flg_Correta"]').filter('[value="' + data[0].Cod_AlternativaCorreta+'"]').attr('checked', true);
            $("#formQuestao").show();

        }
    });

}

function DeleteQuestao(val) {
    if (confirm("Deseja realmente deletar esta Questão?")) {
        var url = '/api/questao/delete/' + val;
        $.get(url, function (data, status) {
            if (status === "success") {
                LoadQuestoes();
            }
        });
    }
}

function Fechar() {
    $("#formQuestao").hide();
    $("#btnNewQuestao").show();

}