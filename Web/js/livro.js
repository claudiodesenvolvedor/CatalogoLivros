// Variáveis Globais

var linhaModelo = $('#linhaModelo');

// const alertPlaceholder = document.getElementById('alertaModal');
// const appendAlert = (message) => {
//     const wrapper = document.createElement('div')
//         wrapper.innerHTML = [
//             '<div class="modal fade" id="alertaModal" tabindex="-1" aria-labelledby="alertaModalLabel" aria-hidden="true">',
//             '<div class="modal-dialog ">',
//             '<div class="modal-content ">',
//             '<div class="modal-header ">',
//             `<h5 class="modal-title fs-5" id="alertaModalLabel">${message}</h5>`,
//             '<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>',
//             '</div></div></div></div>'
//         ]
//         .join('');
//     alertPlaceholder.append(wrapper)};

    // $(function() {
    //     var alertaModal = document.getElementById('alertaModal');
    //     var myModal = new bootstrap.Modal(alertaModal);

    //     myModal.show();
    //  });



        // `<div class="alert alert-${type} alert-dismissible" role="alert">`,
            // `<div>${message}</div>`,
            // '<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>',
            // '</div>'

// const alertTrigger = document.getElementById('liveAlertBtn')
// if (alertTrigger) {
// alertTrigger.addEventListener('click', () => {
// appendAlert('Nice, you triggered this alert message!', 'success')
// })
// };

// Funções Globais
var dadosLivro = [];
jqAjax("GET", "http://localhost:5035/api/Livro").done(function(data){
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
            tr.find('.assuntos').first().text(dado.assuntos.map(function (x) { return x.descricao }).join(", "));
            tr.find('.autores').first().text(dado.autores.map(function (x) { return x.nome }).join(", "));
            $('#tblLivro').find('tbody').append(tr);

        }
    }else{

    }
}).fail(function(){
    console.log('erro');
})

var assuntos = [];
jqAjax("GET", "http://localhost:5035/api/Assunto").done(function (data) {
    //console.log(data);
    if (data.sucesso) {
        assuntos = data.dados;
    }
}).fail(function () {
    console.log('erro');
})

var autores = [];
jqAjax("GET", "http://localhost:5035/api/Autor").done(function (data) {
    //console.log(data);
    if (data.sucesso) {
        autores = data.dados;
    }
}).fail(function () {
    console.log('erro');
})


// Funções

// Salva Dados no BD
function salvarDados(){
    var dados = {
        "livroId": $("#Cod").val(),
        "titulo": $("#txtTitulo").val(),
        "editora": $("#txtEditora").val(),
        "autores": $(".checkAutor:checked").toArray().map(function (x) { return { autorId: +x.value }; }),
        "assuntos": $(".checkAssunto:checked").toArray().map(function (x) { return { assuntoId: +x.value }; })
      }

      var method = 'PUT';
      if (dados.codAs == 0){
        method = 'POST';
      }

    jqAjax(
        method, 
        "http://localhost:5035/api/Livro", 
        JSON.stringify(dados))
            .done(function(data){
                if(data.sucesso){
                    fecharModal();
                    location.reload();
                    //alert('Dados atualizados com sucesso')
                }else{
                    alert(dados.mensagem);
                }
    });
}

function ExcluirDados(){

    var url = "http://localhost:5035/api/Livro?livroId=" + $("#Cod").val();
    jqAjax(
        'DELETE', 
        url)
           .done(function(data){
                if(data.sucesso){
                    fecharModal();
                    location.reload();
                    //alert('Dados atualizados com sucesso')
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
    carregaAutoresEAssuntosModal();
    //$('.modal').show();


}); 

// Clique do botão excluir da grid
$('#tblLivro').on('click', '.btnExcluir', function(e){
    var linha = $(this).parent().parent();
    var cod = linha.find('.cod').text();
    var descricao = linha.find('.descricao').text();

    $('#CodAs').val(cod);
    $('#txtAssunto').val(descricao);

}); 

// Clique do botão de inclusão
$('.btnIncluir').on('click', function(e){
    //fecharModal();
    $('#modalTituloLabel').text('Inclusão de Dados');
    $('#Cod').val(0);
    $('#txtAssunto').val('');
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





