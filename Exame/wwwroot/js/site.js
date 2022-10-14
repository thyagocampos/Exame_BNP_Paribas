//Javascript/Jquery utilizado no projeto

//Carrega os produtos para o combobox produtos
function carregaProdutos() {
    var url = "MovimentoManual/GetProdutos";

    $.getJSON(url, function (data) {
        var items = '';

        items += "<option value='00000000000'>-- Selecione --</option>";

        $('#cmbProduto').empty();
        $.each(data, function (i, produto) {
            items += "<option value='" + produto.codProduto + "'>" + produto.desProduto + "</option>";
        });
        $('#cmbProduto').html(items);
    });
}

//Carrega os produtos + cosif para o respectivo campo
function carregaProdutoCosif() {

    var url = "MovimentoManual/GetProdutoCosif";

    $.getJSON(url, { CodProduto: $('#cmbProduto').val() }, function (data) {
        var items = '';

        items += "<option value='00000000000'>-- Selecione --</option>";

        $('#cmbProdutoCosif').empty();
        $.each(data, function (i, produtoCosif) {
            items += "<option value='" + produtoCosif.codCosif + "'>" + produtoCosif.codCosif + " - " + "(" + produtoCosif.codClassificacao + ")" + "</option>";
        });
        $('#cmbProdutoCosif').html(items);
    });
}

//Validação do formulário
function validaFrm() {

    var valido = true;    

    if ($('#txtMes').val() <= 0) {
        valido = false;
    }

    if ($('#txtMes').val() > 12) {
        valido = false;
    }
    

    if ($('#txtMes').val() == '') {
        valido = false;
    }

    if ($('#txtAno').val() == '') {
        valido = false;
    }

    if ($('#txtAno').val().length!=4) {
        valido = false;
    }

    if ($('#txtValor').val() == '') {
        valido = false;
    }

    if ($('#txtDescricao').val() == '') {
        valido = false;
    }

    if ($('#cmbProduto').val() == '0000') {
        valido = false;
    }

    if ($('#cmbProdutoCosif').val() == '00000000000') {
        valido = false;
    }    

    $('#btnAdiciona').prop('disabled', !valido);

}

//Função atribuida ao botão grava
function limpar() {

    $('#cmbProduto').html();
    $('#cmbProdutoCosif').html("<option value='00000000000'>-- Selecione --</option>");

    carregaProdutos();

    $('#btnAdiciona').prop('disabled', true);

    $('#txtMes').val('');

    $('#txtAno').val('');

    $('#txtValor').val('');

    $('#txtDescricao').val('');

    bloqueiaForm();

}

//Função atribuida ao botão novo
function novo() {
    $('#txtMes').removeAttr('readonly');
    $('#txtAno').removeAttr('readonly');
    $('#cmbProduto').removeAttr('readonly');
    $('#cmbProdutoCosif').removeAttr('readonly');
    $('#cmbProdutoCosif').html("<option value='00000000000'>-- Selecione --</option>");
    $('#txtValor').removeAttr('readonly');
    $('#txtDescricao').removeAttr('readonly');


    $('#cmbProduto').html();
    $('#cmbProdutoCosif').html("<option value='00000000000'>-- Selecione --</option>");        
    $('#txtMes').val('');
    $('#txtAno').val('');
    $('#txtValor').val('');
    $('#txtDescricao').val('');

    carregaProdutos();
}

//Bloqueia form
function bloqueiaForm() {
    $('#txtMes').attr('readonly',true);
    $('#txtAno').attr('readonly', true);
    $('#txtValor').attr('readonly', true);
    $('#txtDescricao').attr('readonly', true);
    $('#cmbProduto').attr('readonly', true);
    $('#cmbProdutoCosif').attr('readonly', true);
    $('#cmbProdutoCosif').html("");

    carregaProdutos();
}

//Envia os dados da form via AJAX
function enviaMovimentoManual() {
    var data = $("#formMovimentoManual").serialize();
    console.log(data); // para debugar o que foi capturado no form
    //alert(data);
    $.ajax({
        type: 'POST',
        url: '/MovimentoManual/AdicionaMovimentoManual',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', 
        data: data,
        success: function (result) {
            //alert('Ok');
            //console.log(result);
            limpar();
            bloqueiaForm();
            reloadGrid();
        },
        error: function (xhr, status, error) {            
            var err = (xhr.responseText);
            alert('Erro ao enviar dados' + err.Message);
        }
    })
}

//Recupera a lista de movimentos manuais
function reloadGrid() {
    $('#movimentosManuais').load('MovimentoManual/ReloadMovimentosManuais');    
}

//Exibe o modal de pesquisa
function showModalpesquisa() {    
    $('#modalPesquisa').modal('show');
}

//Envia os dados para pesquisa e busca os resultados
function enviaPesquisa() {
    if ($('#txtMesPesquisa').val() == '' || $('#txtAnoPesquisa').val() == '') {
        console.log('Preencha o mês e ano para rodar a pesquisa');
        alert('Preencha o mês e ano para rodar a pesquisa');
    } else {
        $('#movimentosManuais').load("MovimentoManual/ReloadMovimentosManuais?AnoPesquisa=" + $('#txtAnoPesquisa').val() + "&MesPesquisa=" + $('#txtMesPesquisa').val() );

        $('#modalPesquisa').modal('hide');

        $('#txtMesPesquisa').val('');

        $('#txtAnoPesquisa').val('');
    }    
}

//Sobrescrevendo os métodos de validação do Jquery para aceitar vírgula como separador decimal
$.validator.methods.range = function (value, element, param) {
    var globalizedValue = value.replace(",", ".");
    return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
}

$.validator.methods.number = function (value, element) {
    return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
}

//Rotinas após o carregamento da página
$(document).ready(function () {
    $('#cmbProduto').change(carregaProdutoCosif);
    $('#btnAdiciona').prop('disabled', true);

    $('#txtMes').focusout(validaFrm);
    $('#txtMes').change(validaFrm);
    $('#txtMes').keypress(validaFrm);

    $('#txtAno').focusout(validaFrm);
    $('#txtAno').change(validaFrm);
    $('#txtAno').keypress(validaFrm);

    $('#txtValor').focusout(validaFrm);
    $('#txtValor').change(validaFrm);
    $('#txtValor').keypress(validaFrm);

    $('#txtDescricao').focusout(validaFrm);
    $('#txtDescricao').change(validaFrm);
    $('#txtDescricao').keypress(validaFrm);

    $('#cmbProduto').focusout(validaFrm);
    $('#cmbProduto').change(validaFrm);

    $('#cmbProdutoCosif').focusout(validaFrm);
    $('#cmbProdutoCosif').change(validaFrm);
}

);
