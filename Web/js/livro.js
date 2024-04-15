// Variáveis Globais

var linhaModelo = $('#linhaModelo');
var dadosLivro = [];
var autores = [];
var assuntos = [];

// Funções Globais
jqAjax("GET", "http://localhost:5035/api/Livro/GetLivros").done(function(data){
    //console.log(data);
    if (data.sucesso){
        var linha = linhaModelo.html();
        dadosLivro = data.dados;
        for (var dado of dadosLivro){
            var tr = $('<tr></tr>');
            tr.html(linha);
            tr.find('.cod').first().text(dado.livroId);
            tr.find('.titulo').first().text(dado.titulo);
            tr.find('.editora').first().text(dado.editora);
            tr.find('.edicao').first().text(dado.edicao);
            tr.find('.anoPublicacao').first().text(dado.anoPublicacao);
            tr.find('.preco').first().text(dado.preco);
            tr.find('.assuntos').first().text(dado.assuntos.map(function (x) { return x.descricao }).join(", "));
            tr.find('.autores').first().text(dado.autores.map(function (x) { return x.nome }).join(", "));
            $('#tblLivro').find('tbody').append(tr);

        }
    }else{

    }
}).fail(function(){
    console.log('erro');
})

jqAjax("GET", "http://localhost:5035/api/Assunto").done(function (data) {
    //console.log(data);
    if (data.sucesso) {
        assuntos = data.dados;
    }
}).fail(function () {
    console.log('erro');
})

jqAjax("GET", "http://localhost:5035/api/Autor").done(function (data) {
    //console.log(data);
    if (data.sucesso) {
        autores = data.dados;
    }
}).fail(function () {
    console.log('erro');
})


// Funções

// Salvar Dados no BD
function salvarDados(){

    var dados = {
        "livroId": $("#Cod").val(),
        "titulo": $("#txtTitulo").val(),
        "editora": $("#txtEditora").val(),
        "edicao": $("#txtEdicao").val(),
        "anopublicacao": $("#txtPublicacao").val(),
        "preco": $("#txtPreco").val(),
        "autores": $(".checkAutor:checked").toArray().map(function (x) { return { autorId: +x.value }; }),
        "assuntos": $(".checkAssunto:checked").toArray().map(function (x) { return { assuntoId: +x.value }; })
      }

    var method = 'PUT';
    var url = "";
    if (dados.livroId == 0) {
        method = 'POST';
        url = "http://localhost:5035/api/Livro/CreateLivro";
    } else {
        url = "http://localhost:5035/api/Livro/UpdateLivro";
    }

    jqAjax(method, url, JSON.stringify(dados)).done(function(data){
                if(data.sucesso){
                    fecharModal();
                    location.reload();
                    //alert('Dados atualizados com sucesso')
                    alert(data.mensagem);
                }else{
                    alert(data.mensagem);
                }
    });
}

function ExcluirDados(){

    var url = "http://localhost:5035/api/Livro/DeleteLivro?livroId=" + $("#Cod").val();
    jqAjax("DELETE", url).done(function(data){
                if(data.sucesso){
                    fecharModal();
                    location.reload();
                    //alert('Dados atualizados com sucesso')
                    alert(data.mensagem);
                }else{
                    alert(dados.mensagem);
                }
    });
}
 
function fecharModal(){
    $('#formModal').trigger('reset');
    $('#modalTituloLabel').text('');
    $('#CodAs').val(0);
    $('#txtAssunto').text('');

    // $(function() {
    //     var alertaModal = document.getElementById('alertaModal');
    //     var myModal = new bootstrap.Modal(alertaModal);
    //     myModal.show();
    //  });
    //$('#alertaModal').show();
    // appendAlert('Dados Alterados com Sucesso!');
    //console.log('fechou');
}

// Ações dos botões

// Clique do botão editar da grid
$('#tblLivro').on('click', '.btnEditar', function(e){
    var linha = $(this).parent().parent();
    var cod = linha.find('.cod').text();
    var livro = dadosLivro.find(function (x) { return x.livroId == cod });

    $('#modalTituloLabel').text('Alteração de Dados');
    //$('#modalModelo').text('Alteração de Dados');
    $('#Cod').val(cod);
    $('#txtTitulo').val(livro.titulo);
    $('#txtEditora').val(livro.editora);
    $('#txtEdicao').val(livro.edicao);
    $('#txtPublicacao').val(livro.anoPublicacao);
    $('#txtPreco').val(livro.preco);

    carregaAutoresEAssuntosModal();
    //$('.modal').show();


}); 

// Clique do botão excluir da grid
$('#tblLivro').on('click', '.btnExcluir', function(e){
    var linha = $(this).parent().parent();
    var cod = linha.find('.cod').text();
    var descricao = linha.find('.descricao').text();

    $('#Cod').val(cod);
    $('#txtAssunto').val(descricao);

}); 

// Clique do botão de inclusão
$('.btnIncluir').on('click', function(e){
    //fecharModal();
    $('#modalTituloLabel').text('Inclusão de Dados');
    $('#Cod').val(0);
    $('#txtTitulo').val('');
    $('#txtEditora').val('');
    $('#txtEdicao').val('');
    $('#txtPublicacao').val('');
    $('#txtPreco').val('');

    carregaAutoresEAssuntosModal();
});

// Carregar Autores e Assuntos no Modal
function carregaAutoresEAssuntosModal() {
    var cod = $('#Cod').val();
    var autoresChecks = "";
    for (var autor of autores) {
        autoresChecks += '<label><input type="checkbox" class="checkAutor" value="' + autor.autorId + '">' + autor.nome + '</label> ';
    }

    $("#modalAutores").html(autoresChecks);
    var assuntosChecks = "";
    for (var assunto of assuntos) {
        assuntosChecks += '<label><input type="checkbox" class="checkAssunto" value="' + assunto.assuntoId + '">' + assunto.descricao + '</label> ';
    }

    $("#modalAssuntos").html(assuntosChecks);
    if (cod != 0) {
        var livro = dadosLivro.find(function (x) { return x.livroId == cod });
        var assuntosDoLivro = livro.assuntos.map(function (x) { return x.assuntoId; });
        $(".checkAssunto").each(function (i) {
            if (assuntosDoLivro.indexOf(+$(this).val()) > -1) {
                $(this).prop("checked", true)
            }
        });
        var autoresDoLivro = livro.autores.map(function (x) { return x.autorId; });
        $(".checkAutor").each(function (i) {
            if (autoresDoLivro.indexOf(+$(this).val()) > -1) {
                $(this).prop("checked", true)
            }
        })
    }

}

// Clique do botão de fechar modal
$('.btnCancelarModal, .btnCloseModal').on('click', function(e){
    fecharModal();

});

// Clique do botão de salvar dados no modal
$('.btnSalvarModal').on('click', function(e){
    salvarDados();

});

// Clique do botão de excluir dados.
$('.btnExcluirModal').on('click', function(e){
    ExcluirDados();

});





