//global
var Id_Avaliacao;
var Id_Treinamento;
var Id_Usuario;
var Questoes;
var Id_Inscricao;

//start page
$(document).ready(function () {
    Id_Avaliacao = $("#Id_Avaliacao").val();
    Id_Treinamento = $("#Id_Treinamento").val();
    Id_Usuario = $("#Id_Usuario").val();

    $.get('/api/Inscricao/list/' + Id_Treinamento + '/' + Id_Usuario, function (data, status) {
        if (status === "success") {
            Id_Inscricao = data[0].Id;
        }
    });

    LoadQuestoes();

    $("#btnEnviar").click(SalvarAvaliacao);
});

function LoadQuestoes() {
    $.get('/api/Questao/list/' + Id_Avaliacao, function (data, status) {
        if (status === "success") {
            Questoes = data;
            

            var html = '';
            var cont = 1;

            Questoes.forEach(function (item) {
                html = html + '<div class="box-questao">'
                html = html + '<h4>'+item.Enunciado+'</h4>'
                html = html + '<div class="list-group">'
                html = html + '<div class="list-group-item list-group-item-action">'
                html = html + '<input class="form-check-input" type="radio" name="questao' + cont + 'Radios" id="questao' + cont +'Radios1" value="1">'
                html = html + '<label class="form-check-label" for="questao' + cont +'Radios1">'
                html = html + item.Alternativa1
                html = html + '</label>'
                html = html + '</div>'
                html = html + '<div class="list-group-item list-group-item-action">'
                html = html + '<input class="form-check-input" type="radio" name="questao' + cont + 'Radios" id="questao' + cont +'Radios2" value="2">'
                html = html + '<label class="form-check-label" for="questao' + cont +'Radios2">'
                html = html + item.Alternativa2
                html = html + '</label>'
                html = html + '</div>'
                html = html + '<div class="list-group-item list-group-item-action">'
                html = html + '<input class="form-check-input" type="radio" name="questao' + cont + 'Radios" id="questao' + cont +'Radios3" value="3">'
                html = html + '<label class="form-check-label" for="questao' + cont +'Radios3">'
                html = html + item.Alternativa3
                html = html + '</label>'
                html = html + '</div>'
                html = html + '<div class="list-group-item list-group-item-action">'
                html = html + '<input class="form-check-input" type="radio" name="questao' + cont + 'Radios" id="questao' + cont +'Radios4" value="4">'
                html = html + '<label class="form-check-label" for="questao' + cont +'Radios4">'
                html = html + item.Alternativa4
                html = html + '</label>'
                html = html + '</div>'
                html = html + '<div class="list-group-item list-group-item-action">'
                html = html + '<input class="form-check-input" type="radio" name="questao' + cont + 'Radios" id="questao' + cont +'Radios5" value="5">'
                html = html + '<label class="form-check-label" for="questao' + cont +'Radios5">'
                html = html + item.Alternativa5
                html = html + '</label>'
                html = html + '</div>'
                html = html + '</div>'
                html = html + '</div>'

                cont++;
            });

            $("#avaliacao").html(html);
        }
    });
}

function SalvarAvaliacao() {
    var cont = 1;
    var valido = true;
    var acertos = 0;
    Questoes.forEach(function (item) {
        if (!$('input[name=questao' + cont + 'Radios]:checked').val()) {
            $("#validate").text("Existem questões que não foram respondidas");
            valido = false;
        } else {
            var resposta = parseInt($('input[name=questao' + cont + 'Radios]:checked').val());
            if (resposta === item.Cod_AlternativaCorreta) {
                acertos++;
            }
        }
        cont++;
    });

    if (valido) {
        var dados = {
            Id: 0,
            Id_Inscricao: Id_Inscricao,
            Id_Avaliacao: Id_Avaliacao,
            NumQuestoes: Questoes.length,
            NumAcertos: acertos
        };
        
        $.post("/api/Nota", dados)
            .done(function () {
                $("#avaliacao").html("");
                var html = '';
                html = html + '<div class="card text-white bg-secondary mb-3" style="max-width: 18rem;">';
                html = html + '<div class="card-body">';
                html = html + '<h5 class="card-title">Avaliação enviada com sucesso!</h5>';
                html = html + '<p class="card-text">Você acertou '+acertos+' de '+Questoes.length+' questões</p>';
                html = html + '</div>';
                html = html + '</div>';
                html = html + '<a href="/Treinamentos/Details/' + Id_Treinamento + '">Voltar para o treinamento</a>';
                $(".enviar").html(html);

            });
    } else {
        return;
    }

}