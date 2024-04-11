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
jqAjax("GET", "http://localhost:5035/api/Assunto").done(function(data){
    //console.log(data);
    if (data.sucesso){
        var linha = linhaModelo.html();
        var dados = data.dados;
        for(var dado of dados){
            var tr = $('<tr></tr>');
            tr.html(linha);
            tr.find('.cod').first().text(dado.assuntoId);
            tr.find('.descricao').first().text(dado.descricao);
            $('#tblAssunto').find('tbody').append(tr);

        }
    }else{

    }
}).fail(function(){
    console.log('erro');
})


// Funções

// Salva Dados no BD
function salvarDados(){
    var dados = {
        "assuntoId": $("#CodAs").val(),
        "descricao": $("#txtAssunto").val()
      }

      var method = 'PUT';
    if (dados.assuntoId == 0){
        method = 'POST';
      }

    jqAjax(
        method, 
        "http://localhost:5035/api/Assunto", 
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

    var url = "http://localhost:5035/api/Assunto?assuntoId=" + $("#CodAs").val();
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
$('#tblAssunto').on('click', '.btnEditar', function(e){
    var linha = $(this).parent().parent();
    var cod = linha.find('.cod').text();
    var descricao = linha.find('.descricao').text();

    $('#modalTituloLabel').text('Alteração de Dados');
    //$('#modalModelo').text('Alteração de Dados');
    $('#CodAs').val(cod);
    $('#txtAssunto').val(descricao);
    //$('.modal').show();


}); 

// Clique do botão excluir da grid
$('#tblAssunto').on('click', '.btnExcluir', function(e){
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
    $('#CodAs').val(0);
    $('#txtAssunto').val('');

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





