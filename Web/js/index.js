
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


//jqAjax("GET", "http://localhost:5035/api/Livro").done(function (data) {
//    //console.log(data);
//    if (data.sucesso) {
//        var linha = linhaModelo.html();
//        dadosLivro = data.dados;
//        for (var dado of dadosLivro) {
//            var tr = $('<tr></tr>');
//            tr.html(linha);
//            tr.find('.cod').first().text(dado.livroId);
//            tr.find('.titulo').first().text(dado.titulo);
//            tr.find('.editora').first().text(dado.editora);
//            tr.find('.edicao').first().text(dado.edicao);
//            tr.find('.anoPublicacao').first().text(dado.anoPublicacao);
//            tr.find('.preco').first().text(dado.preco);

//            tr.find('.assuntos').first().text(dado.assuntos.map(function (x) { return x.descricao }).join(", "));
//            $('#tblLivro').find('tbody').append(tr);

//        }
//    }
//    else {
//        console.log(data);
//    }
//});


