# VirtualKeyboard
Projeto representando a simulação de um teclado virtual de um banco.

## Envolvidos: João Pedro Fachini Moreira Silva (JaumFaquinha)


# Linguagens e Tecnologias Utilizadas

## Back-End
C# -> .Net 8.0 e AspNetCore

## Front-End
HTML
CSS
JavaScript

## Banco de Dados
MongoBD

## Criptografia
  No momento não há criptografia nas tranferências de dados entre Back-End e Front-End, será implementado futuramente.

# Como Funciona
  O aplicativo se inicia na tela de login de usuário, que se é feito por meio do CPF cadastrado, onde, ao apertar no botão para Acessar é mandado uma chamada para a API que valida no banco de dados se exite ou não CPF digitado. Se validado corretamente, então o usuário é mandado para tela de senha, onde se deve digitar a senha  por meio de botões que são gerados automáticamente por uma chamada na API, após digitar a senha e clickar em acessar, será feito a chamada na API para validação da senha, onde valida se continua sendo o usuário, se a conta está bloqueada e a senha. Após a validação da senha o usuário será mandado para uma tela de sucesso. 

# Como Rodar

## Back-End
Verificar se a a versão 8.0 do .Net está instalado, caso não esteja, realizar o download e instlar (https://dotnet.microsoft.com/pt-br/download/dotnet/8.0).

Caso use o VSCode, deverá ser instalado as estensões necessárias para rodar o projeto (C# Dev Kit, C#, .NET Install Tool)

Fazer o download do projeto e utilizar ou Visual Studio ou VSCode (instalar extensões necessárias) para abrir o arquivo JPFMS_BankKeyboard.sln. Após abrir o projeto apenas rodar ele em modo debug.


## Front-End
Fazer o download do projeto e utilizar o VSCode para abrir o projeto apenas usar a extensão LiveServer pra executar.


# Dúvidas
Caso haja duvidas, por favor entrar em contato com o proprietário do repositório (JaumFaquinha).

