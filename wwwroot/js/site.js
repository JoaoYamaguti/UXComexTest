// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const formPerson = document.querySelector("#formPerson")
const phoneNumberInput = document.querySelector("#phoneNumber")
const cpfInput = document.querySelector("#CPF")
const subEditPerson = document.querySelector("#subEditPerson")

const formAddress = document.querySelector("#formAddress")
const nameInput = document.querySelector("#name")
const cityInput = document.querySelector("#city")
const stateInput = document.querySelector("#state")
const cepInput = document.querySelector("#cep")
const cepBtn = document.querySelector("#cepSearch")

const toastTrigger = document.getElementById('liveToastBtn')
const toastLiveExample = document.getElementById('liveToast')

function cleanFormat(value) {
    return value.replace(/\D/g, '');
}

function maskPhoneNumber(phoneNumber) {
    const value = cleanFormat(phoneNumber)

    if (value.length == 0) {
        phoneNumberInput.value = value
    } else if (value.length <= 2) {
        phoneNumberInput.value = `+${value}`
    } else if (value.length <= 4) {
        phoneNumberInput.value = `+${value.substr(0, 2)} (${value.substr(2, 2)}`
    } else if (value.length <= 9) {
        phoneNumberInput.value = `+${value.substr(0, 2)} (${value.substr(2, 2)}) ${value.substr(4, 5)}`
    } else {
        phoneNumberInput.value = `+${value.substr(0, 2)} (${value.substr(2, 2)}) ${value.substr(4, 5)}-${value.substr(9, 4)}`
    }
}

function maskCPF(cpf) {
    const value = cleanFormat(cpf)

    if (value.length <= 3) {
        cpfInput.value = value
    } else if (value.length <= 6) {
        cpfInput.value = `${value.substr(0, 3)}.${value.substr(3, 3)}`
    } else if (value.length <= 9) {
        cpfInput.value = `${value.substr(0, 3)}.${value.substr(3, 3)}.${value.substr(6, 3)}`
    } else {
        cpfInput.value = `${value.substr(0, 3)}.${value.substr(3, 3)}.${value.substr(6, 3)}-${value.substr(9, 2)}`
    }
}

function maskCEP(cep) {
    console.log("cheguei")
    const value = cleanFormat(cep)

    if (value.length <= 5) {
        cepInput.value = value
    } else {
        cepInput.value = `${value.substr(0, 5)}-${value.substr(5, 3)}`
    }
}

// Mask for phone number
phoneNumberInput.addEventListener("input", (e) => maskPhoneNumber(e.target.value))

// Mask for CPF
cpfInput.addEventListener("input", (e) => maskCPF(e.target.value))

// clean format when submit PersonForm
formPerson.addEventListener("submit", (e) => {
    phoneNumberInput.value = cleanFormat(phoneNumberInput.value)
    cpfInput.value = cleanFormat(cpfInput.value)
})

// Mask for CEP
cepInput.addEventListener("input", (e) => maskCEP(e.target.value))

function cleanAddressForm() {
    cepInput.value = cleanFormat(cepInput.value)
}

// clean format when submit AddressForm
formAddress.addEventListener("submit", () => cleanAddressForm())

// CEP Search

//Clean CEP form inputs values.
function cleanCEPForm() {
    nameInput.value = ("");
    cityInput.value = ("");
    stateInput.value = ("");
    cepInput.value = ("");
}

//Fill CEP form inputs values.
function fillCEPForm(conteudo) {
    if (!("erro" in conteudo)) {
        //Atualiza os campos com os valores.
        nameInput.value = `${conteudo.logradouro} - ${conteudo.bairro}`;
        cityInput.value = (conteudo.localidade);
        stateInput.value = (conteudo.estado);
        cepInput.value = (conteudo.cep);
    }
    else {
        //CEP não Encontrado.
        cleanCEPForm();
        alert("CEP não encontrado.");
    }
}

async function pesquisacep() {

    //Nova variável "cep" somente com dígitos.
    var cep = cleanFormat(cepInput.value);

    //Verifica se campo cep possui valor informado.
    if (cep != "") {

        //Expressão regular para validar o CEP.
        var validacep = /^[0-9]{8}$/;

        //Valida o formato do CEP.
        if (validacep.test(cep)) {

            //Preenche os campos com "..." enquanto consulta webservice.
            nameInput.value = "...";
            cityInput.value = "...";
            stateInput.value = "...";
            cepInput.value = "...";

            const response = await (await fetch(`https://viacep.com.br/ws/${cep}/json/`)).json()

            cleanCEPForm()

            fillCEPForm(response)

            console.log(response)
        }
        else {
            //cep é inválido.
            cleanCEPForm();
            alert("Formato de CEP inválido.");
        }
    }
    else {
        //cep sem valor, limpa formulário.
        cleanCEPForm();
    }
};

cepBtn.addEventListener("click", (e) => pesquisacep(cepInput.value))
