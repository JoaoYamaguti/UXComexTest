# UXComex Teste

Este projeto é um teste para o processo seletivo da empresa UXComex para a vaga de Analista Desenvolvedor Jr.

## Funcionalidades Solicitadas

O teste consiste em:

- Tela de listagem dos cadastros já existentes, com um botão "+ Cadastro" no canto superior;
- Tela para o cadastro da pessoa, contendo os campos Nome, Telefone e CPF;
- Após você de fato cadastrar a pessoa, vai pra uma tela de edição desse cadastro, onde teremos uma nova listagem, contendo os endereços já cadastrados dessa pessoa, com um botão de "+ Endereço" no canto superior dessa listagem. Ao clicar nesse botão, se abre um modal para efetuar o cadastro do endereço. Os campos necessários são Endereço, CEP, Cidade e Estado; (usamos essa api para consulta de CEP https://viacep.com.br/ws/{0}/json/, onde o {0} deve conter o cep que você quer pesquisar o endereço. Essa parte da pesquisa do CEP é opcional);
- Nas listagens, precisa abrir uma tela para edição do cadastro da pessoa ou do endereço, quando eu clicar em cima do nome/endereço da pessoa e conter um botão "X" em cada linha, para me permitir excluir um cadastro;
- Os requisitos técnicos: Utilização de Dapper para pesquisa e tradução dos dados do banco em classes do C#; Escrita das queries em T-SQL; O teste precisa ser feito em Asp.Net MVC, banco de dados SQL Server e criação de um database project, para que você possa nos enviar o seu banco e a gente consiga reproduzir o sistema aqui.

## Tecnologias Utilizadas

- **ASP.NET Core MVC**: Framework para construção de aplicações web e APIs com C#.
- **Dapper**: Micro ORM para mapeamento objeto-relacional, utilizado para interações rápidas e eficientes com o banco de dados.
- **SQL Server**: Opção como Banco de dados escolhida.

## Pré-requisitos

Antes de rodar o projeto, verifique se você tem as seguintes ferramentas instaladas:

- [Dotnet SDK](https://dotnet.microsoft.com/download) (versão 6 ou superior)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) 

## Como Executar o Projeto

1. **Clone o repositório**

   ```bash
   git clone https://github.com/JoaoYamaguti/UxComexTest.git
   cd UxComexTest
   ```

3. **Configure a Conexão com o Banco de Dados**

No arquivo appsettings.json, configure a string de conexão para o banco de dados:

```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=DBUxcomexTest;User Id=usuario;Password=senha;"
  }
}
```

4. **Execute a aplicação**

Se estiver utilizando a linha de comando, execute o comando abaixo para rodar o projeto:

```bash
dotnet run
```

Ou, se preferir, pode rodar o projeto diretamente pelo Visual Studio Code utilizando o comando (Crtl + F5).

5. **Acessando a aplicação**

Abra seu navegador e acesse o endpoint da aplicação:

```bash
http://localhost:5257
```

## Como Configurar o Banco de Dados

Este projeto utiliza SQL Server como banco de dados relacional. Para configurar o banco de dados, você deve executar o script SQL que está disponível no repositório.

1. Localize o script do banco de dados, que pode ser encontrado no diretório ~/.

2. Execute o script no banco de dados

É possivel utilizar o SQL Server Management Studio (SSMS) ou o Azure Data Studio para executar o script.

Exemplo de execução do script no SSMS:

 1. Abra o SSMS.
 2. Conecte-se ao seu servidor de banco de dados.
 3. Selecione a base de dados que deseja utilizar (ou crie uma nova).
 4. Abra o script SQL e execute-o.
    
O script criará o banco de dados e tabelas e realizará alguns cadastros.

3. Verifique a execução

Após a execução do script, as tabelas devem estar configuradas no banco de dados. Agora, você pode continuar a execução do projeto.

## Estrutura do Projeto
A estrutura do projeto é organizada da seguinte forma:

```bash
/src
  /Controllers        # Contém os controladores que gerenciam as requisições HTTP
  /Models             # Contém os modelos de dados utilizados nas requisições e respostas
  /Views              # Views MVC
  /wwwroot            # Arquivos estáticos (CSS, JS, imagens, etc.)
/appsettings.json     # Configurações do projeto
/Program.cs           # Arquivo principal para configuração do pipeline da aplicação
/Startup.cs           # Configuração de serviços e middleware
/database_project.sql     # Script para criação do banco de dados
```
## 
 
Grato pela oportunidade :)
