
// Variáveis Globais
var linhaModelo = $('#linhaModelo');
var autores = [];

// Funções Globais

// Carrega o array de autores
jqAjax("GET", "http://localhost:5035/api/Autor").done(function (data) {
    //console.log(data);
    if (data.sucesso) {
        autores = data.dados;
    }
    carregaComboAutores();
});

// Monta a Como de Autores
function carregaComboAutores() {
    var autoresSelect = '<option class="opacity-25" selected value="0" >Selecione um item...</option>';
    for (var autor of autores) {
        autoresSelect += '<option value="' + autor.autorId + '">' + autor.nome + '</option>';
    }
    $('#selAutor').html(autoresSelect);
}

function consultaLivrosPorAutor() {

    var tr = "";
    $('#tblLivro').find('tbody').html(tr);

    // http://localhost:5035/api/Livro?livroId=
    // http://localhost:5035/api/Livro/GetLivrosByAutor/
    // http://localhost:5035/api/Livro/GetLivroByAutor?autorId=1
    var url = "http://localhost:5035/api/Livro/getbyautor?id="+$('#selAutor').val();
    jqAjax("GET", "http://localhost:5035/api/Livro/GetLivrosByAutor/" + +$('#selAutor').val()).done(function (data) {
            //console.log(data);
            if (data.sucesso) {
                var linha = linhaModelo.html();
                dadosLivro = data.dados;
                for (var dado of dadosLivro) {
                    tr = $('<tr></tr>');
                    tr.html(linha);
                    tr.find('.cod').first().text(dado.livroId);
                    tr.find('.titulo').first().text(dado.titulo);
                    tr.find('.editora').first().text(dado.editora);
                    tr.find('.edicao').first().text(dado.edicao);
                    tr.find('.anoPublicacao').first().text(dado.anoPublicacao);
                    tr.find('.preco').first().text(dado.preco);

                    tr.find('.autores').first().text(dado.autores.map(function (x) { return x.nome }).join(", "));
                    $('#tblLivro').find('tbody').append(tr);
                    tr.find('.assuntos').first().text(dado.assuntos.map(function (x) { return x.descricao }).join(", "));
                    $('#tblLivro').find('tbody').append(tr);
                }
                $('#tblLivro').find('tbody').append($('<tr></tr>'))
            }
            else {
                alert(data.mensagem);
            }
        });

}


