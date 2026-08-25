# AirenApi

## Introdução

Este repositório contém a API REST do projeto Airen, uma API feita em ASP.NET Core que expõe operações CRUD para as principais entidades do domínio (Personagem, Potentia, Afiliacao, Capitulo, Habilidade, Hobby, Parentesco, Tecnica, Versao e Volume).

Ela fornece endpoints REST para gerenciar os recursos do domínio. A API utiliza Entity Framework Core com MySQL, DTOs e métodos de extensão para mapear dados entre as requisições e os modelos.

## Configuração

- Pré-requisitos:
	- .NET SDK 10
	- MySQL (ou outro servidor compatível configurado na connection string)
	- Configurar a connection string em `appsettings.json` com a chave `AirenConnection`.
	- As dependências relevantes estão configuradas em `Program.cs` (DbContext, controllers e serviços da aplicação).

Exemplo (appsettings.json):

```
"ConnectionStrings": {
	"AirenConnection": "server=localhost;user=root;password=senha;database=airen_db"
}
```

## Compilação

Na raiz do projeto, execute:

```bash
dotnet build
dotnet run
```

Para aplicar migrations (quando necessário):

```bash
dotnet ef migrations add "migration"
dotnet ef database update
```

Certifique-se de que a connection string esteja correta antes de rodar as migrations ou iniciar a aplicação.

## Endpoints

Cada controller segue o padrão REST básico com rotas no formato `/{Controller}` e `/{Controller}/{id}`.

- `GET /Afiliacao` — listar afiliacões
- `POST /Afiliacao` — criar afiliacão
- `GET /Afiliacao/{id}` — obter afiliacão por id
- `PUT /Afiliacao/{id}` — atualizar afiliacão
- `DELETE /Afiliacao/{id}` — remover afiliacão

- `GET /Capitulo` — listar capítulos
- `POST /Capitulo` — criar capítulo
- `GET /Capitulo/{id}` — obter capítulo por id
- `PUT /Capitulo/{id}` — atualizar capítulo
- `DELETE /Capitulo/{id}` — remover capítulo

- `GET /Habilidade` — listar habilidades
- `POST /Habilidade` — criar habilidade
- `GET /Habilidade/{id}` — obter habilidade por id
- `PUT /Habilidade/{id}` — atualizar habilidade
- `DELETE /Habilidade/{id}` — remover habilidade

- `GET /Hobby` — listar hobbies
- `POST /Hobby` — criar hobby
- `GET /Hobby/{id}` — obter hobby por id
- `PUT /Hobby/{id}` — atualizar hobby
- `DELETE /Hobby/{id}` — remover hobby

- `GET /Parentesco` — listar parentescos
- `POST /Parentesco` — criar parentesco
- `GET /Parentesco/{id}` — obter parentesco por id
- `PUT /Parentesco/{id}` — atualizar parentesco
- `DELETE /Parentesco/{id}` — remover parentesco

- `GET /Personagem` — listar personagens
- `POST /Personagem` — criar personagem
- `GET /Personagem/{id}` — obter personagem por id
- `PUT /Personagem/{id}` — atualizar personagem
- `DELETE /Personagem/{id}` — remover personagem

- `GET /Potentia` — listar potentias
- `POST /Potentia` — criar potentia
- `GET /Potentia/{id}` — obter potentia por id
- `PUT /Potentia/{id}` — atualizar potentia
- `DELETE /Potentia/{id}` — remover potentia

- `GET /Tecnica` — listar técnicas
- `POST /Tecnica` — criar técnica
- `GET /Tecnica/{id}` — obter técnica por id
- `PUT /Tecnica/{id}` — atualizar técnica
- `DELETE /Tecnica/{id}` — remover técnica

- `GET /Versao` — listar versões
- `POST /Versao` — criar versão
- `GET /Versao/{id}` — obter versão por id
- `PUT /Versao/{id}` — atualizar versão
- `DELETE /Versao/{id}` — remover versão

- `GET /Volume` — listar volumes
- `POST /Volume` — criar volume
- `GET /Volume/{id}` — obter volume por id
- `PUT /Volume/{id}` — atualizar volume
- `DELETE /Volume/{id}` — remover volume

> Observação: os controllers usam `CreatedAtAction` nas criações, `Ok` nas consultas e atualizações, `NotFound` quando o recurso não existe e `NoContent` nas exclusões bem-sucedidas.

## Explicação da Arquitetura - classes

- `Controllers/` — controladores que expõem os endpoints REST (cada controller implementa operações CRUD básicas).
- `Data/AirenContext.cs` — `DbContext` do EF Core que registra os `DbSet<>` para cada entidade. Configura relacionamentos (ex.: `Parentesco` possui chaves estrangeiras para `Personagem` e `Parente` com delete behavior `Restrict`).
- `Data/DTOs/` — DTOs de entrada e saída usados para separar o modelo de domínio da interface HTTP.
- `Models/` — classes de domínio (entidades) mapeadas pelo EF Core.
- `Extensions/` — métodos de extensão `MapTo...` que convertem DTOs em entidades e entidades em DTOs de leitura.
- `Services/` — serviços responsáveis pelas consultas das entidades e pelo carregamento das relações necessárias.
- `Services/Core/QueryServicesCore.cs` — extensão que registra os serviços da aplicação no contêiner de injeção de dependência.
- `Migrations/` — histórico de migrações do EF Core.

Infraestrutura e padrões:
- Métodos de extensão para conversão DTO ↔ Model, sem dependência do AutoMapper.
- EF Core com `UseLazyLoadingProxies()` para carregamento preguiçoso quando necessário.
- MySQL como provedor de banco de dados (configurado em `Program.cs`).

## Solução resolvida

A API fornece:
- Endpoints CRUD consistentes para as entidades principais.
- Mapeamento de DTOs para manter contratos de API estáveis.
- Persistência via EF Core com migrações.
- Regras de integridade referencial explícitas (`Parentesco` restrito para evitar deletes em cascata indesejados).
