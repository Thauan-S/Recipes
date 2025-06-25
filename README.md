# Livro de Receitas - ASP.NET Core

Este projeto é uma aplicação de **livro de receitas** desenvolvida com **ASP.NET Core**, que tem como objetivo armazenar, gerenciar e compartilhar receitas culinárias de forma prática e segura.

## Principais funcionalidades

- **API RESTful** para gerenciamento completo das receitas.
- **Autenticação e autorização** usando Tokens JWT e Google Authentication para garantir segurança no acesso.
- Implementação de persistência de dados com **Entity Framework Core** e banco de dados SQL Server.
- Emulação local de serviços Azure com **Service Bus** e **Blob Storage** utilizando **Docker**, para garantir escalabilidade e integração com microsserviços e armazenamento de arquivos.
- Validação de dados usando **FluentValidation** para garantir integridade e qualidade das informações.
- Mapeamento de objetos com **AutoMapper** para facilitar a transformação entre modelos e DTOs.
- Geração de IDs únicos com **Sqids**.
- Suporte a verificação do tipo de arquivo com **File.TypeChecker**.
- análise de qualidade de código com sonarCloud através dos pipelines do azure.
## Tecnologias e pacotes utilizados
 
- **ASP.NET Core** (.NET 9.0)
- **Entity Framework Core** (SQL Server e InMemory para testes)
-  **Redis**
- **Rate Limiter**
- **Azure.Messaging.ServiceBus** (emulação local via Docker)
- **Azure.Storage.Blobs** (emulação local via Docker)
- **System.IdentityModel.Tokens.Jwt** 
- **AutoMapper**
-  **Bogus**
-  **Moq**
- **FluentValidation**
- **Sqids**
- **File.TypeChecker**
- **ElasticApm**(Para Observabilidade)
- **Microsoft.AspNetCore.Authentication.Google**
## Testes automatizados

- Utilização de **xUnit** para testes unitários e de integração.
- Cobertura de testes com **coverlet.collector**.
- Testes end-to-end com **Microsoft.AspNetCore.Mvc.Testing**.
  
## Instalando o azurite:  docker pull mcr.microsoft.com/azure-storage/azurite
[🔍 Acessar Diagrama ER no Lucidchart](https://lucid.app/lucidchart/379f450c-1d39-4052-acab-d7f8297410dc/edit?viewport_loc=-1767%2C-350%2C3327%2C1407%2C0_0&invitationId=inv_8a1053e4-dda1-4b5c-8d86-b4b5755561f5)

