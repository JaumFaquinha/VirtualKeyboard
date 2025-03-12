


let password = [];
let userCpf = '';
let cpfValidated = false;
let passwordValidated = false;

let passwordFile = 'http://127.0.0.1:5500/password.html';
let successFile = 'http://127.0.0.1:5500/success.html';

//Chama o back end para gerar os botões
async function fetchAndGenerateButtons() {
    try {
        // Substitua pela URL da sua API em .NET
        const response = await fetch('https://localhost:7118/api/AuthController/generate-keyboard'); 
        
        if (!response.ok) {
            throw new Error('Erro ao buscar os dados da API');
        }
        
        const data = await response.json();
        
        //Verifica se recebe 5 valores
        if (!Array.isArray(data) || data.length !== 5) {
            throw new Error('A API deve retornar um array com exatamente 6 valores');
        }
        
        const container = document.getElementById('multi-button');
        const inputField = document.getElementById('input-password');
        const maskedValue = '*';

        for (let i = 0; i < data.length; i++) {
            let value = data[i];
            const button = document.createElement('button');

            button.textContent = value.n1 + ' ou ' + value.n2;
            button.value = value;
            // button.id = 'btn-password';
            button.addEventListener('click', () => {
                inputField.value += maskedValue; // Define o valor do input ao clicar no botão
                password.push(value);
            });
            
            container.appendChild(button);
        }
        
        createBackSpaceButton(container, inputField)

    } catch (error) {
        console.error('Erro na geração dos botões:', error.message);
    }
}

function setPasswordValue(){
    var passwordValue = getElementById('btn-password').value;
    var inputPassword = document.getElementById('input-password');

}

async function validateCpf() {
    try{    

        userCpf = document.getElementById('input-cpf').value;

        const response = await fetch('https://localhost:7118/api/AuthController/validate-cpf', {
            method: "POST",
            body: JSON.stringify({
                cpf: userCpf,
            }),
            headers: {
              "Content-type": "application/json; charset=UTF-8"
            }
        });

        const data = await response.json()

        if (data.success) {
            cpfValidated = true;
            document.location.href = 'http://127.0.0.1:5500/password.html?data=' + userCpf
        }
        else{
            this.cpfValidated = false
            this.userCpf = '';
            alert(data.message)
        }

    } catch (error) { 
        console.error('Erro:', error.message);
  }
}


async function validatePassword() {
    try{        
    
        const urlParams = new URLSearchParams(window.location.search);
        const urlData = urlParams.get('data');

        const response = await fetch('https://localhost:7118/api/AuthController/validate-password', {
            method: "POST",
            body: JSON.stringify({
              CPF: urlData ,
              Password: password,
            }),
            headers: {
              "Content-type": "application/json; charset=UTF-8"
            }
          });
        
        const data = await response.json();        
        
        if (data.success){
            this.passwordValidated = true;
            document.location.href = 'http://127.0.0.1:5500/success.html'
        }else{                
            this.passwordValidated = false;
            alert(data.message)
            document.location.reload()
        }
    }
    catch(error){
        console.error("Erro na validação de senha: ", error.message)
    } 
}


function createBackSpaceButton(container, inputField){
    const button = document.createElement('button');
    button.textContent = 'Apagar'
    button.id = 'btn-delete'
    button.addEventListener('click', () => {
        inputField.value = '' // Define o valor do input ao clicar no botão
        password = []
    });    
    container.appendChild(button);
}


// Chamada da função ao carregar a página
document.addEventListener('DOMContentLoaded', fetchAndGenerateButtons());