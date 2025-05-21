# Livro de Receitas - ASP.NET Core

Este projeto é uma aplicação de **livro de receitas** desenvolvida com **ASP.NET Core**, que tem como objetivo armazenar, gerenciar e compartilhar receitas culinárias de forma prática e segura.

## Principais funcionalidades

- **API RESTful** para gerenciamento completo das receitas.
- **Autenticação e autorização** usando **Tokens JWT** para garantir segurança no acesso.
- Implementação de persistência de dados com **Entity Framework Core** e banco de dados SQL Server.
- Emulação local de serviços Azure com **Service Bus** e **Blob Storage** utilizando **Docker**, para garantir escalabilidade e integração com microsserviços e armazenamento de arquivos.
- Validação de dados usando **FluentValidation** para garantir integridade e qualidade das informações.
- Mapeamento de objetos com **AutoMapper** para facilitar a transformação entre modelos e DTOs.
- Geração de IDs únicos com **Sqids**.
- Suporte a verificação do tipo de arquivo com **File.TypeChecker**.

## Tecnologias e pacotes utilizados

- **ASP.NET Core** (.NET 9.0)
- **Entity Framework Core** (SQL Server e InMemory para testes)
- **Azure.Messaging.ServiceBus** (emulação local via Docker)
- **Azure.Storage.Blobs** (emulação local via Docker)
- **System.IdentityModel.Tokens.Jwt** (para autenticação JWT)
- **AutoMapper**
- **FluentValidation**
- **Sqids**
- **File.TypeChecker**

## Testes automatizados

- Utilização de **xUnit** para testes unitários e de integração.
- Cobertura de testes com **coverlet.collector**.
- Testes end-to-end com **Microsoft.AspNetCore.Mvc.Testing**.



