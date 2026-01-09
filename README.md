# 🎮 FIAP Cloud Games (FCG) – Tech Challenge

Este projeto é o MVP da plataforma **FIAP Cloud Games (FCG)**, desenvolvido como parte do **Tech Challenge** da FIAP. Ele representa a primeira fase de uma solução completa para venda de jogos digitais e gestão de servidores para partidas online.

## 📌 Objetivo

Desenvolver uma **API REST em .NET 8** que permita:

- Cadastro e autenticação de usuários
- Gerenciamento de jogos adquiridos
- Controle de acesso por níveis (usuário e administrador)
- Persistência de dados com Entity Framework Core
- Testes unitários para garantir qualidade e confiabilidade

Este MVP servirá como base para futuras funcionalidades como matchmaking e gerenciamento de servidores.

## 🚀 Funcionalidades

### 👤 Cadastro de Usuários

- Nome, e-mail e senha
- Validação de e-mail e senha segura (mínimo 8 caracteres, letras, números e caracteres especiais)

### 🔐 Autenticação e Autorização

- Autenticação via **JWT**
- Níveis de acesso:
  - **Usuário**: acesso à biblioteca de jogos
  - **Administrador**: cadastro de jogos, promoções e gerenciamento de usuários

### 🎮 Gerenciamento de Jogos

- Cadastro de jogos (admin)
- Associação de jogos à biblioteca do usuário

## 🧱 Arquitetura

- Projeto monolítico com separação em camadas:
  - `FCG.Api`: camada de apresentação (controllers)
  - `FCG.Application`: regras de negócio
  - `FCG.Domain`: entidades e interfaces
  - `FCG.Infrastructure`: persistência e serviços externos
  - `FCG.Tests`: testes unitários

- Padrões utilizados:
  - **DDD (Domain-Driven Design)**
  - **TDD (Test-Driven Development)** em módulos críticos
  - **Event Storming** para modelagem de domínio

## 🛠️ Tecnologias Utilizadas

| Tecnologia              | Finalidade                                 |
|------------------------|--------------------------------------------|
| .NET 8                 | Backend/API                                |
| Entity Framework Core  | Persistência de dados                      |
| xUnit + Moq            | Testes unitários                           |
| JWT                    | Autenticação e autorização                 |
| Swagger + ReDoc        | Documentação da API                        |
| FluentAssertions       | Validação de testes                        |

## 📄 Documentação da API

A API está documentada com Swagger e ReDoc:

- Swagger UI: [`/swagger`](https://localhost:7065/swagger)
- ReDoc: [`/docs`](https://localhost:7065/docs)

## 🧪 Testes

O projeto inclui testes unitários para os principais serviços:

- `PromocaoServiceTests`
- `UsuarioServiceTests`
- `JogoServiceTests`

Para rodar os testes:

```bash
dotnet test
```

## 🧰 Como rodar o projeto

1. Clone o repositório:

```bash
git clone https://github.com/seu-usuario/fcg-api.git
```

2. Restaure os pacotes:

```bash
dotnet restore
```

3. Execute as migrations:

```bash
dotnet ef database update
```

4. Rode o projeto:

```bash
dotnet run --project FCG.Api
```

5. Acesse a documentação:

- Swagger: `https://localhost:7065/swagger`
- ReDoc: `https://localhost:7065/docs`

## 📦 Estrutura do Projeto

```
FCG.Api/
FCG.Application/
FCG.Domain/
FCG.Infrastructure/
FCG.Tests/
```

## 📚 Referências

- [Documentação oficial do .NET](https://learn.microsoft.com/pt-br/dotnet/)
- [Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/)
- [JWT Authentication](https://jwt.io/)
- [xUnit](https://xunit.net/)
- [ReDoc](https://github.com/Redocly/redoc)

## 👥 Desenvolvedores

- Vinícius e equipe – FIAP Tech Challenge