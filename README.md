# Ecommerce.API

Esta é uma API RESTful desenvolvida em .NET 7, utilizando os princípios de Domain-Driven Design (DDD) e Clean Code.

## Funcionalidades

A API oferece as seguintes funcionalidades:

### Categorias

- Listar todas as categorias
- Obter uma categoria pelo seu ID
- Criar uma nova categoria
- Atualizar uma categoria existente
- Excluir uma categoria

### Subcategorias

- Listar todas as subcategorias
- Obter uma subcategoria pelo seu ID
- Criar uma nova subcategoria
- Atualizar uma subcategoria existente
- Excluir uma subcategoria

### Produtos

- Listar todos os produtos
- Obter um produto pelo seu ID
- Criar um novo produto
- Atualizar um produto existente
- Excluir um produto

## Tecnologias Utilizadas

- .NET 7
- C#
- ASP.NET Core
- Entity Framework Core
- AutoMapper
- RabbitMQ (adicionar se for necessário)

## Configuração do Ambiente

Antes de executar a API, certifique-se de ter o .NET 7 SDK instalado em seu ambiente de desenvolvimento. Em seguida, você pode seguir as etapas abaixo:

1. Clone este repositório para o seu ambiente local.
2. Abra o projeto no Visual Studio ou em uma IDE de sua preferência.
3. Verifique se a connection string do banco de dados está configurada corretamente no arquivo `appsettings.json` do projeto `Ecommerce.API.Infrastructure`.
4. Execute as migrações do Entity Framework Core para criar o banco de dados e as tabelas necessárias.
5. Inicie a API executando o projeto `Ecommerce.API.Web`.

## Contribuição

Você é bem-vindo para contribuir com melhorias, correções de bugs ou adição de novas funcionalidades. Sinta-se à vontade para enviar pull requests ou abrir issues para relatar problemas ou sugestões.

