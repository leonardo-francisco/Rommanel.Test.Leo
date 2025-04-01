# Rommanel Test Leo - CRUD de Cliente

**Este projeto é um CRUD para Cadastro de Cliente, desenvolvido como parte de um teste técnico. O sistema segue os princípios do DDD, utilizando CQRS e Event Sourcing. Implementa validações no backend com FluentValidation e possui testes unitários.

## Estrutura do Projeto
Rommanel.Test.Leo
│── src
│   ├── 1-Presentation (Camada de apresentação - API)
│   ├── 2-Application (Casos de uso e validações)
│   ├── 3-Domain (Entidades e regras de negócio)
│   ├── 4-Infrastructure (Acesso a dados, repositórios e configurações)
│── test (Testes unitários)

## Backend

- **.NET 8.0+ - Framework principal para desenvolvimento da API**
- **FluentValidation - Validação dos dados do cliente**
- **CQRS (Command Query Responsibility Segregation) - Separação de comandos e consultas**
- **Event Sourcing - Persistência baseada em eventos**
- **MongoDB - Banco de dados NoSQL utilizado**
- **MediatR - Implementação do CQRS para comunicação entre camadas**
- **xUnit & Moq - Testes unitários**

## Frontend

- **Angular - Framework para desenvolvimento da interface do usuário**


## Configuração

### 1. Clonar o Repositório

```bash
git clone https://github.com/leonardo-francisco/Rommanel.Test.Leo.git
cd Rommanel.Test.Leo
```

### 2. Clonar o MongoDB
- **Localmente: Inicie o MongoDB em sua máquina**
- **Docker: Caso prefira usar Docker, você pode iniciar o MongoDB com o comando:
```bash
docker run -d -p 27017:27017 --name Rommanel.Test.Leo mongo
```

### 3. Configuração do Arquivo appsettings.json
- **No diretório Rommanel.Test.Leo.Presentation, crie um arquivo appsettings.json e configure a string de conexão do MongoDB**
```bash
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "RommanelTestLeoDB"
  }
```

## Executando a Aplicação

### 1. Restaurar Dependências
- **No diretório raiz do projeto, execute:
```bash
dotnet restore
```

### 2. Rodar a Aplicação
- **Para iniciar a API, execute o seguinte comando:
```bash
dotnet run --project RommanelDev.API
```
- **A aplicação será iniciada use ferramentas como Postman ou Swagger para testar os endpoints

### 3. Testando a Aplicação
- **Para rodar os testes:
```bash
dotnet test
```
- **Isso executará os testes em Rommanel.Test.Leo.Test e exibirá os resultados no terminal

## Endpoints Principais

### Transações
- **POST /client**: Cadastra o cliente
- **PUT /client/{id}**: Atualiza as informações do cliente.
- **GET /client**: Retorna uma lista com todos os clientes cadastrados.
- **GET /client/{id}**: Recupera um cliente pelo seu id.
- **DELETE /client/{id}**: Remove um cliente pelo seu id.
