//start page
$(document).ready(function () {
    LoadRelatorio();
});

function LoadRelatorio() {
    $.get('/api/relatorio/usuarios', function (data, status) {
        if (status === "success") {
            
            var users = [];
            var cont = 0;
            

            data.forEach(function (item) {
                
                users[cont] = {
                    email: item.Email,
                    nome: item.UserName,
                    id: item.Id,
                    
                };

                cont++;
            });

            var html = '';
            cont = 1;

            users.forEach(function (item) {
                html = html + '<table class="table table-hover table-striped table-sm">';
                if (cont === 1) {
                    html = html + '<thead class="thead-dark">';
                    html = html + '<tr>';
                    html = html + '<th scope="col">#</th>';
                    html = html + '<th scope="col">Nome</th>';
                    html = html + '<th scope="col">Email</th>';
                    
                    html = html + '</tr>';
                    html = html + '</thead>';
                }

                html = html + '<tbody>';
                html = html + '<tr>';
                html = html + '<th scope="row">' + cont + '</th>';
                html = html + '<td>' + item.nome + '</td>';
                html = html + '<td>' + item.email + '</td>';
                
                html = html + '</tr>';
                html = html + '</tbody>';
                html = html + '</table>';

                html = html + '<table class="table table-sm table-hover">';
                html = html + '<thead>';
                html = html + '<tr>';
                html = html + '<th scope="col">Treinamento</th>';
                html = html + '<th scope="col">Questões certas</th>';
                html = html + '<th scope="col">Visualizado</th>';
                html = html + '<th scope="col">Aprovado</th>';
                html = html + '</tr>';
                html = html + '</thead>';
                html = html + '<tbody id="infos' + cont + '">';
                
                html = html + '</tbody>';
                html = html + '</table>';

                cont++;
            });

            $("#usuarios").html(html);

            var numUsers = users.length;

            cont = 1;
            html = '';

            users.forEach(function (item) {
                $.get('/api/Relatorio/resultados/' + item.id, function (data, status) {
                    if (status === "success") {

                        if (cont <= numUsers) {
                            console.log(data);

                            data.forEach(function (val) {
                                html = html + '<tr>';
                                html = html + '<td>' + val.Treinamento + '</td>';
                                html = html + '<td>' + val.Acertos + '/' + val.Questoes + '</td>';
                                html = html + '<td>' + val.ConteudoAssistido + '%</td>';
                                if (val.Aprovacao) {
                                    html = html + '<td>SIM</td>';
                                } else {
                                    html = html + '<td>NÃO</td>';
                                }
                                html = html + '</tr>';
                            });
                            
                            $("#infos" + cont).html(html);

                            cont++;
                            html = '';
                        }

                       
                    }
                });
            });

        }
    });

    


}



    
