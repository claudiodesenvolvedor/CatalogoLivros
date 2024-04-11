// Variáveis Globais

var linhaModelo = $('#linhaModelo');

// Funções Globais
jqAjax("GET", "http://localhost:5035/api/Autor").done(function(data){
    //console.log(data);
    if (data.sucesso){
        var linha = linhaModelo.html();
        var dados = data.dados;
        for(var dado of dados){
            var tr = $('<tr></tr>');
            tr.html(linha);
            tr.find('.cod').first().text(dado.autorId);
            tr.find('.nome').first().text(dado.nome);
            $('#tblAutor').find('tbody').append(tr);

        }
    } else {
        alert(data.mensagem);
    }
}).fail(function (err) {
    alert("Ocorreu um erro ao carregar os dados da página");
    //console.log('erro');
})


// Funções

// Salva Dados no BD
function salvarDados(){
    var dados = {
        "autorId": $("#CodAu").val(),
        "nome": $("#txtAutor").val()
      }

      var method = 'PUT';
    if (dados.autorId == 0){
        method = 'POST';
      }

    jqAjax(
        method, 
        "http://localhost:5035/api/Autor", 
        JSON.stringify(dados))
            .done(function(data){
                if(data.sucesso){
                    fecharModal();
                    location.reload();
                    alert('Dados atualizados com sucesso')
                }else{
                    alert(dados.mensagem);
                }
    });
}

function ExcluirDados(){

    var url = "http://localhost:5035/api/Autor?codAu=" + $("#CodAu").val();
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
    $('#CodAu').val(0);
    $('#txtAutor').text('');

    
    //$('#alertaModal').show();
    //appendAlert('Dados Alterados com Sucesso!', 'success');
    //console.log('fechou');
}

// Ações dos botões

// Clique do botão editar da grid
$('#tblAutor').on('click', '.btnEditar', function(e){
    var linha = $(this).parent().parent();
    var cod = linha.find('.cod').text();
    var nome = linha.find('.nome').text();

    $('#modalTituloLabel').text('Alteração de Dados');
    //$('#modalModelo').text('Alteração de Dados');
    $('#CodAu').val(cod);
    $('#txtAutor').val(nome);
    //$('.modal').show();


}); 

// Clique do botão excluir da grid
$('#tblAutor').on('click', '.btnExcluir', function(e){
    var linha = $(this).parent().parent();
    var cod = linha.find('.cod').text();
    var nome = linha.find('.nome').text();

    $('#CodAu').val(cod);
    $('#txtAutor').val(nome);

}); 

// Clique do botão de inclusão
$('.btnIncluir').on('click', function(e){
    //fecharModal();
    $('#modalTituloLabel').text('Inclusão de Dados');
    $('#CodAu').val(0);
    $('#txtAutor').val('');

});

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