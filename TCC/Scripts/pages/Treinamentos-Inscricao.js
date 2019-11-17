//global
var Id_Treinamento;
var Id_Usuario;

//start page
$(document).ready(function () {
    Id_Treinamento = $("#Id_Treinamento").val();
    Id_Usuario = $("#Id_Usuario").val();

    VerificaInscricoes();

       
});

function VerificaInscricoes() {
    var url = "/api/inscricao/list/" + Id_Treinamento + "/" + Id_Usuario;

    $.get(url, function (data, status) {
        if (status === "success") {
            
            if (data.length === 0) {
                $("#formInscricao").show();
            } else {
                $("#formInscricao").html('<h4 class="text-danger">Você já possui inscrição para este treinamento.</h4>');
                $("#formInscricao").show();
                $("#btnInscrever").hide();
            }
        }
    });
}

function Inscrever() {
    if ($("#confirmaInscricao").prop("checked") === false) {
        $("#validate").text("Confirme o interesse em fazer o curso.");
        return;
    }

    var now = new Date;
    var dataAtual = now.toLocaleDateString();
    
    var dados = {
        
        Id_Usuario: Id_Usuario,
        Id_Treinamento: parseInt(Id_Treinamento),
        DataInscricao: dataAtual,
        NotaTreinamento: 0,
        AvaliacaoTreinamento: '',
        Aprovado: false      
    };

    console.log(dados);
    
    $.post("/api/inscricao", dados)
        .done(function () {
            window.location.assign("/Treinamentos/Index");
        });
}