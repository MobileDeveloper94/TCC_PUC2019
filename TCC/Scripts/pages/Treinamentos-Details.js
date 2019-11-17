//global
var Id_Treinamento;
var Num_Modulos;
var Id_Usuario;
var Inscricao;
var Conteudo;
var Avaliacao;

//start page
$(document).ready(function () {

    Id_Treinamento = $("#Id_Treinamento").val();
    Id_Usuario = $("#Id_Usuario").val();
    Num_Modulos = parseInt($("#Num_Modulos").val());

    VerificaInscricao();

    $("#btnAvaliar").click(AvaliarTreinamento);

    $("#btnComentario").click(ComentarioTreinamento);

    LoadComentarios();

    

});

function VerificaInscricao() {
    var url = '/api/inscricao/list/' + Id_Treinamento + '/' + Id_Usuario;
    $.get(url, function (data, status) {
        if (status === "success") {
            
            if (data[0]) {
                
                Inscricao = data[0];
                LoadPage();
            } else {
                
                var html = '<h4 class="text-danger">Você não possui inscrição neste treinamento! <a href="/Treinamentos/Inscricao/' + Id_Treinamento + '">Clique aqui para se inscrever</a></h4>'
                
                $("#modulos").html(html);
               
            }
            
        } 

    });

}

function LoadPage() {
    

    var url = '/api/conteudo/list/' + Id_Treinamento;
    $.get(url, function (data, status) {
        if (status === "success") {
            
            Conteudo = data;
            
            var html = '';
            
            for (var i = 1; i < Num_Modulos + 1; i++) {
                html = html + '<div class="card" id="modulo' + i + '">';
                html = html + '<div class="card-header" id="modulosHeading' + i + '">';
                html = html + '<h5 class="mb-0">';
                html = html + '<button class="btn btn-link" type="button" data-toggle="collapse" data-target="#modulosCollapse' + i + '" aria-expanded="true" aria-controls="modulosCollapse' + i + '">';
                html = html + 'Módulo ' + i ;
                html = html + '</button>';
                html = html + '</h5>';
                html = html + '</div>';
                if (i === 1) {
                    html = html + '<div id="modulosCollapse' + i + '" class="collapse show" aria-labelledby="modulosheading' + i + '" data-parent="#modulos">'
                } else {
                    html = html + '<div id="modulosCollapse' + i + '" class="collapse" aria-labelledby="modulosheading' + i + '" data-parent="#modulos">'

                }
                html = html + '<div class="card-body">'
                html = html + '<div id="conteudosModulo'+i+'">'
                html = html + '</div>'
                html = html + '<div id="avaliacoesModulo' + i + '">'
                html = html + '</div>'
                html = html + '</div>'
                html = html + '</div>'
                html = html + '</div>'
            }

            $("#modulos").html(html);
            
            
            for (i = 1; i < Num_Modulos + 1; i++) {
                LoadConteudos(i);

            }
            

            $.get('/api/avaliacao/list/' + Id_Treinamento, function (data, status) {
                if (status === "success") {
                    Avaliacao = data;

                    LoadProgresso();

                    for (var i = 1; i < Num_Modulos + 1; i++) {
                        LoadAvaliacoes(i);
                    }
                }

            });
            
        }
    });
}

function LoadConteudos(modulo) {
    if (Conteudo.length === 0) {
        $("#conteudosModulo" + modulo).html('<span class="text-danger">Ainda não existem conteúdos cadastrados</span>');
    } else {

        var html = '';

        html = html + '<div class="list-group list-group-flush listConteudo'+modulo+'">';
        Conteudo.forEach(function (item) {
            if (item.Cod_Modulo === modulo) {
                html = html + '<a href="#" onclick="ViewConteudo(' + item.Id + ')" class="list-group-item list-group-item-action d-flex justify-content-between align-items-center" id="'+item.Id+'">' + item.Descricao;
                html = html + '</a>';
   
            }
        });
        
        html = html + '</div>';

        $("#conteudosModulo" + modulo).html(html);

        var elms = $(".listConteudo" + modulo).find('a');
        elms.each(function (val,link) {
            $.get('/api/visualizacao/detail/' + Inscricao.Id + '/' + link.id, function (data, status) {
                if (status === "success") {

                    if (data.length > 0) {
                        link.innerHTML = link.innerHTML + '<span class="badge badge-success badge-pill"><i class="fa fa-check"></i></span>'
                    }
                }
            });
            
        });

        
    }
}

function ViewConteudo(id) {
    
    var dados = {
        Id: 0,
        Id_Inscricao: Inscricao.Id,
        Id_Conteudo: id,
        Visualizado: true
    };

    
    $.post("/api/Visualizacao", dados)
        .done(function () {
            var item = Conteudo.find(x => x.Id === id);

            if (item.ConteudoExterno === true) {
                window.open(item.Link, '_blank');
                window.location.reload();
            } else {
                window.open('/Files/Conteudo/'+item.Link, '_blank');
                window.location.reload();
            }
            
        });

}

function LoadAvaliacoes(modulo) {
    if (Avaliacao.length === 0) {
        $("#avaliacoesModulo" + modulo).html('<span class="text-danger">Ainda não existem conteúdos cadastrados</span>');
    } else {

        var html = '';

        html = html + '<div class="list-group list-group-flush listAvaliacao' + modulo + '">';
        Avaliacao.forEach(function (item) {
            if (item.Cod_Modulo === modulo) {
                html = html + '<a href="#" onclick="ViewAvaliacao(' + item.Id + ')" class="list-group-item list-group-item-info list-group-item-action d-flex justify-content-between align-items-center" id="' + item.Id + '">' + item.Descricao;
                html = html + '</a>';

            }
        });

        html = html + '</div>';

        $("#avaliacoesModulo" + modulo).html(html);

        var elms = $(".listAvaliacao" + modulo).find('a');
        elms.each(function (val, link) {
            $.get('/api/Nota/detail/' + Inscricao.Id + '/' + link.id, function (data, status) {
                if (status === "success") {
                    
                    if (data.length > 0) {
                        link.innerHTML = link.innerHTML + '<small>Acertou '+data[0].NumAcertos+'/'+data[0].NumQuestoes+'</small>'
                    }
                }
            });

        });
    }
}

function ViewAvaliacao(id) {
    if (confirm("Deseja fazer a avaliação? Os resultados anteriores serão desconsiderados.")) {
        window.location.assign('/Treinamentos/Avaliacao/' + id);
    }
}

function AvaliarTreinamento() {

    $(".modal-title").html("Avaliar treinamento");

    var html = '';
    html = html + '<p>Em uma escala onde 1 é muito ruim e 5 é muito bom, como você avalia este treinamento?</p>';
    html = html + '<div class="form-check form-check-inline">';
    html = html + '<input class="form-check-input" type="radio" name="radioAvaliacao" id="radioAvaliacao1" value="1">';
    html = html + '<label class="form-check-label" for="inlineRadio1">1</label>';
    html = html + '</div>';
    html = html + '<div class="form-check form-check-inline">';
    html = html + '<input class="form-check-input" type="radio" name="radioAvaliacao" id="radioAvaliacao2" value="2">';
    html = html + '<label class="form-check-label" for="inlineRadio2">2</label>';
    html = html + '</div>';
    html = html + '<div class="form-check form-check-inline">';
    html = html + '<input class="form-check-input" type="radio" name="radioAvaliacao" id="radioAvaliacao3" value="3">';
    html = html + '<label class="form-check-label" for="inlineRadio3">3</label>';
    html = html + '</div>';
    html = html + '<div class="form-check form-check-inline">';
    html = html + '<input class="form-check-input" type="radio" name="radioAvaliacao" id="radioAvaliacao4" value="4">';
    html = html + '<label class="form-check-label" for="inlineRadio3">4</label>';
    html = html + '</div>';
    html = html + '<div class="form-check form-check-inline">';
    html = html + '<input class="form-check-input" type="radio" name="radioAvaliacao" id="radioAvaliacao5" value="5">';
    html = html + '<label class="form-check-label" for="inlineRadio3">5</label>';
    html = html + '</div>';
    html = html + '<span class="text-danger" id="validateAvaliacao"></span>';

    $(".modal-body").html(html);

    html = '<button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>';
    html = html + '<button type="button" class="btn btn-primary" onclick="EnviarAvaliacao()">Enviar</button>';
    $(".modal-footer").html(html);
}

function ComentarioTreinamento() {

    $(".modal-title").html("Deixe um comentário");

    var html = '';
    html = html + '<form>';
    html = html + '<div class="form-group">';
    html = html + '<label for="message-text" class="col-form-label">Comentário:</label>';
    html = html + '<textarea class="form-control" id="message-text"></textarea>';
    html = html + '</div>';
    html = html + '</form>';
    html = html + '<div><span class="text-danger" id="validateComentario"></span></div>';

    $(".modal-body").html(html);

    html = '<button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>';
    html = html + '<button type="button" class="btn btn-primary" onclick="EnviarComentario()">Enviar</button>';
    $(".modal-footer").html(html);
}

function EnviarAvaliacao() {
    if (!$("input[name=radioAvaliacao]:checked").val()) {
        $("#validateAvaliacao").text("Escolha uma nota de avaliação");
        return;
    }

    Inscricao.NotaTreinamento = parseInt($("input[name=radioAvaliacao]:checked").val());

    $.post("/api/Inscricao", Inscricao)
        .done(function () {
            window.location.reload();
        });
    
}

function EnviarComentario() {
    if ($("#message-text").val() === "") {
        $("#validateComentario").text("Preencha o campo comentário");
        return;
    }

    Inscricao.AvaliacaoTreinamento = $("#message-text").val();

    $.post("/api/Inscricao", Inscricao)
        .done(function () {
            window.location.reload();
        });
}

function LoadComentarios() {
    $.get('/api/Inscricao/treinamento/' + Id_Treinamento + '/0', function (data, status) {
        if (status === "success") {
            
            var html = '';
            data.forEach(function (item) {
                if (item.AvaliacaoTreinamento) {
                    html = html + '<dd>' + item.AvaliacaoTreinamento + '</dd>';
                }
            });
            
            $("#comentariosList").append(html);
            
            var nota = 0;
            var cont = 0;
            data.forEach(function (item) {
                if (item.NotaTreinamento !== 0) {
                    nota = nota + item.NotaTreinamento;
                    cont++;
                }
            });

            var vlrNota = nota / cont;
            if (vlrNota) {
                $("#vlrNota").html(vlrNota);
            } else {
                $("#vlrNota").html("0");
            }
            

            if (Inscricao) {
                if (!Inscricao.AvaliacaoTreinamento) {
                    $("#btnComentario").show();
                }

                if (!Inscricao.NotaTreinamento) {
                    $("#btnAvaliar").show();
                }
            }
        }
    });
}

function LoadProgresso() {
    if (!Inscricao) {
        $("#progresso").hide();
        return;
    }
    
    var total = Conteudo.length;

    $.get('/api/visualizacao/list/' + Inscricao.Id + '/0', function (data, status) {
        if (status === "success") {
            var acessado = data.length;

            var html = acessado + ' de ' + total + ' conteúdos acessados';
            $("#numAcessos").html(html);

            $.get('/api/Nota/list/' + Inscricao.Id + '/0', function (notas, status) {
                if (status === "success") {
                    var acertos = 0;
                    var questoes = 0;
                    notas.forEach(function (item) {
                        acertos = acertos + item.NumAcertos;
                        questoes = questoes + item.NumQuestoes;
                    });

                    html = 'Você acertou ' + acertos + ' de ' + questoes + ' questões respondidas';

                    $("#numAcertos").html(html);
                    var notaCorte = questoes * 0.7;

                    if (notaCorte > 0 && acertos >= notaCorte && acessado === total) {
                        html = 'Você foi aprovado! ';
                        html = html + '<a class="btn btn-success btn-sm" onclick="EmitirCertificado()">Emitir certificado</a>';
                        $("#aprovado").html(html);
                    }

                    
                    
                }
            });
        }
    });

}

function EmitirCertificado() {
    
    $.get('/api/Certificado/emitir/' + Id_Treinamento + '/' + Id_Usuario, function (data, status) {
        if (status === "success") {
            console.log(data);
            window.open("/Files/PDF/" + data);
        }
    });
}