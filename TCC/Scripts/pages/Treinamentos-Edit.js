//global
var conteudos = [];
var Id_Treinamento;

//start page
$(document).ready(function () {
    Id_Treinamento = $("#Id").val();

    $("#flgFim").hide();
    $("#DataInicio").mask("99/99/9999");
    $("#DataFim").mask("99/99/9999");

    $("#DataFim").blur(function () {
        if ($(this).val() !== "") {
            $("#Indeterminado").prop("checked", false);

        } else {
            $("#Indeterminado").prop("checked", true);
        }
    });

    LoadConteudos();
    LoadAvaliacoes();

    $("#btnNewConteudo").click(AddNewConteudo);

    $("#btnSalvarConteudo").click(SaveConteudo);

    $("#btnNewAvaliacao").click(AddNewAvaliacao);

    $("#btnSalvarAvaliacao").click(SaveAvaliacao);

    $("#btnFecharConteudo").click(FecharConteudo);

    $("#btnFecharAvaliacao").click(FecharAvaliacao);

    $('input:file').change(function () {
        var nome_arquivo = $(this).val().split("\\").pop();
        $("#Link_Conteudo").val(nome_arquivo);
    });

});


function LoadConteudos() {
    var html = '';
    var url = '/api/conteudo/list/' + Id_Treinamento;
    $.get(url, function (data, status) {
        if (status === "success") {
            data.forEach(function (item) {
                html = html + '<tr>';
                html = html + '<th scope="row">' + item.Id + '</th>';
                html = html + '<td>' + item.Descricao + '</td>';
                html = html + '<td>' + item.Cod_Modulo + '</td>';
                if (item.ConteudoExterno) {
                    html = html + '<td><a href="' + item.Link + '" target="_blank">'+item.Link+'</a></td>';
                } else {
                    html = html + '<td><a href="/Files/Conteudo/' + item.Link + '" target="_blank">'+item.Link+'</a></td>';
                }
                html = html + '<td><a href="#" onclick="EditConteudo(' + item.Id +')"><i class="fa fa-pencil"></i></a> | <a href="#" onclick="DeleteConteudo(' + item.Id + ')"><i class="fa fa-trash"></i></a></td>';
                html = html + '</tr>';
            });

            $("#listaConteudo").html(html);
        }
    });

}


function AddNewConteudo() {
    $("#Descricao_Conteudo").val('');
    $("#Link_Conteudo").val('');
    $("#Conteudo_Externo").prop("checked", true);
    $("#Observacoes_Conteudo").val('');
    $("pills-link").addClass("active");
    $("#formConteudo").show();
    $("#Descricao_Conteudo").focus();
    $("#btnNewConteudo").hide();
}

function SaveConteudo() {
    $("#validate_Descricao_Conteudo").text("");
    $("#validate_Link_Conteudo").text("");

    if ($("#Descricao_Conteudo").val() === "") {
        $("#validate_Descricao_Conteudo").text("Campo obrigatório");
        $("#Descricao_Conteudo").focus();
        return;
    }

    if ($("#Link_Conteudo").val() === "") {
        $("#validate_Link_Conteudo").text("Link ou arquivo não carregado");
        $("#Link_Conteudo").focus();
        return;
    }

    
    var dados = {
        Id: $("#Id_Conteudo").val(),
        Id_Treinamento: Id_Treinamento,
        Descricao: $("#Descricao_Conteudo").val(),
        Cod_Modulo: $("#Modulo_Conteudo option:selected").val(),
        Link: $("#Link_Conteudo").val(),
        ConteudoExterno: $("#Conteudo_Externo").prop("checked"),
        Observacao: $("#Observacoes_Conteudo").val()
    };
    
    $.post("/api/Conteudo", dados)
        .done(function () {
            $("#btnNewConteudo").show();
            LoadConteudos();
            $("#formConteudo").hide();
            $("#formUpdate").submit();
    });
}

function EditConteudo(val) {
    $("#btnNewConteudo").hide();

    var url = '/api/conteudo/detail/' + val;
    $.get(url, function (data, status) {
        if (status === "success") {

            $("#Id_Conteudo").val(data[0].Id);
            $("#Descricao_Conteudo").val(data[0].Descricao);
            $("#Observacoes_Conteudo").val(data[0].Observacao);
            $("#Modulo_Conteudo").val(data[0].Cod_Modulo);

            $("#Link_Conteudo").val(data[0].Link);
            $("#Conteudo_Externo").prop("checked", data[0].ConteudoExterno);

            if (data[0].ConteudoExterno) {
                //LINK
                $("#formArquivo").hide();
                $("#nomArquivo").text("Link externo");
                $("#Link_Conteudo").prop("disabled", false);
                $("#Conteudo_Externo").prop("checked", true);
            }else{
                //ARQUIVO
                $("#formArquivo").show();
                $("#nomArquivo").text("");
                $("#Link_Conteudo").prop("disabled", true);
            }

            $("#formConteudo").show();
            $("#Descricao_Conteudo").focus();
        }
    });
}

function DeleteConteudo(val) {
    if (confirm("Deseja realmente deletar este Conteúdo?")) {
        var url = '/api/conteudo/delete/' + val;
        $.get(url, function (data, status) {
            if (status === "success") {
                LoadConteudos();
            }
        });
    }
}

function FecharConteudo() {
    $("#btnNewConteudo").show();
    $("#formConteudo").hide();

}

function AddNewAvaliacao() {
    $("#Descricao_Avaliacao").val('');
    $("#btnQuestoes").hide();


    $("#Descricao_Avaliacao").focus();

    $("#formAvaliacao").show();
    $("#btnNewAvaliacao").hide();
}

function SaveAvaliacao() {
    $("#validate_Descricao_Avaliacao").text("");
    

    if ($("#Descricao_Avaliacao").val() === "") {
        $("#validate_Descricao_Avaliacao").text("Campo obrigatório");
        $("#Descricao_Avaliação").focus();
        return;
    }
    
    var dados = {
        Id: $("#Id_Conteudo").val(),
        Id_Treinamento: Id_Treinamento,
        Descricao: $("#Descricao_Avaliacao").val(),
        Cod_Modulo: $("#Modulo_Avaliacao option:selected").val()        
    };
    
    $.post("/api/Avaliacao", dados)
        .done(function () {
            $("#btnNewAvaliacao").show();
            LoadAvaliacoes();
            $("#formAvaliacao").hide();
        });
}

function LoadAvaliacoes() {
    var html = '';
    var url = '/api/avaliacao/list/' + Id_Treinamento;
    $.get(url, function (data, status) {
        if (status === "success") {
            data.forEach(function (item) {
                html = html + '<tr>';
                html = html + '<th scope="row">' + item.Id + '</th>';
                html = html + '<td>' + item.Descricao + '</td>';
                html = html + '<td>' + item.Cod_Modulo + '</td>';
                html = html + '<td><a href="#" onclick="EditAvaliacao(' + item.Id + ')"><i class="fa fa-pencil"></i></a> | <a href="#" onclick="DeleteAvaliacao(' + item.Id + ')"><i class="fa fa-trash"></i></a></td>';
                html = html + '</tr>';
            });

            $("#listaAvaliacao").html(html);
        }
    });
}

function EditAvaliacao(val) {
    var html = '';
    html = html + '<embed src="/Treinamentos/_EditQuestoes/' + val + '" width="100%" height="100%">';
    $(".modal-content").html(html);
    $("#btnQuestoes").show();
    $("#btnNewAvaliacao").hide();
    var url = '/api/avaliacao/detail/' + val;
    $.get(url, function (data, status) {
        if (status === "success") {

            $("#Id_Avaliacao").val(data[0].Id);
            $("#Descricao_Avaliacao").val(data[0].Descricao);
            $("#Modulo_Avaliacao").val(data[0].Cod_Modulo);

            $("#formAvaliacao").show();
            
        }
    });

}

function DeleteAvaliacao(val) {
    if (confirm("Deseja realmente deletar esta Avaliação?")) {
        var url = '/api/avaliacao/delete/' + val;
        $.get(url, function (data, status) {
            if (status === "success") {
                LoadAvaliacoes();
            }
        });
    }
}

function FecharAvaliacao() {
    $("#btnNewAvaliacao").show();
    $("#formAvaliacao").hide();

}

function LinkOrArquivo(item) {
    if (item === 0) {
        //LINK
        $("#formArquivo").hide();
        $("#nomArquivo").text("Link externo");
        $("#Link_Conteudo").prop("disabled", false);
        $("#Conteudo_Externo").prop("checked", true);

    }else{
        //ARQUIVO
        $("#formArquivo").show();
        $("#nomArquivo").text("");
        $("#Link_Conteudo").prop("disabled", true);
        $("#Conteudo_Externo").prop("checked", false);
    }
}