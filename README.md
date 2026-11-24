# Airen

O projeto Airen é uma solução de software para organizar e administrar o universo de personagens, relações e elementos narrativos de um universo ficcional chamado de Airen.

O repositório é dividido em cinco projetos. Cada projeto possui uma responsabilidade própria, mas todos participam do mesmo fluxo: uma interface envia dados conforme os contratos compartilhados, a API aplica as regras da aplicação e a infraestrutura consulta ou altera o banco de dados.

## Visão geral da arquitetura

```mermaid
flowchart LR
    Admin[Airen.Admin]
    Web[Airen.Web]
    Contracts[Airen.Contracts<br/>DTOs]
    API[Airen.Api<br/>API REST]
    Infra[Airen.Infrastructure<br/>Persistência]
    DB[(MySQL)]

    Admin -->|HTTP| API
    Web -->|HTTP| API
    Admin -.-> Contracts
    Web -.-> Contracts
    API -.-> Contracts
    API --> Infra --> DB
```

O fluxo de execução é `Cliente -> API -> Infraestrutura -> Banco`. Os clientes não acessam o banco diretamente: enviam e recebem JSON através da API, e os DTOs em `.Contracts` definem o formato desses payloads.

### Dependências diretas entre projetos

```text
Airen.Api          -> .Contracts, .Infrastructure
Airen.Admin        -> .Contracts, .Infrastructure
Airen.Web          -> .Contracts
Airen.Contracts    -> .Infrastructure
```

### Pacotes e tecnologias

| Projeto | Papel | Dependências principais |
| --- | --- | --- |
| `Airen.Api` | API REST | ASP.NET Core; EF Core `9.0.11`; Pomelo MySQL `9.0.0` |
| `Airen.Infrastructure` | Persistência | EF Core `9.0.11`; proxies; Pomelo MySQL `9.0.0` |
| `Airen.Contracts` | DTOs compartilhados | Referência atual para `Airen.Infrastructure` |
| `Airen.Admin` | Cliente de console | Referências para `Contracts` e `Infrastructure` |
| `Airen.Web` | Cliente Blazor WASM | Blazor WebAssembly `10.0.10`; Mud Blazor `9.9.0`; `Microsoft.Extensions.Http` `10.0.11` |




## Projetos

### Airen.Infrastructure

É a camada de persistência e representação do domínio. Sua motivação é concentrar o que descreve os dados e como eles são armazenados, evitando que controllers e interfaces precisem conhecer detalhes do banco.

Responsabilidades principais:

- Define os modelos `Personagem`, `Potentia`, `Afiliacao`, `Capitulo`, `Habilidade`, `Hobby`, `Parentesco`, `Tecnica`, `Versao` e `Volume`.
- Disponibiliza o `AirenContext`, com um `DbSet` para cada entidade.
- Configura o Entity Framework Core e os relacionamentos entre as entidades.
- Mantém as migrations usadas para versionar a estrutura do banco.
- Permite o uso de Lazy Loading por meio dos proxies do EF Core.

O relacionamento de `Parentesco` é configurado com duas referências a personagens (`Personagem` e `Parente`). As exclusões usam `Restrict` para evitar que apagar um personagem remova relações dependentes de forma inesperada.

### Airen.Contracts

É a camada de contratos compartilhados da solução. Sua motivação é separar os formatos de entrada e saída da API dos modelos persistidos, permitindo que os clientes conheçam apenas os dados necessários para consumir os endpoints.

Responsabilidades principais:

- Define DTOs de criação, leitura e atualização para as entidades do domínio.
- Mantém um formato comum de comunicação entre a API, o Admin e o cliente Web.
- Serve como referência única para os payloads enviados e recebidos pelos endpoints.

### Airen.Api

É o backend HTTP da solução. Sua motivação é oferecer uma fronteira única e estável para qualquer cliente, escondendo a persistência e centralizando as operações do domínio.

O funcionamento lógico de uma requisição é:

1. Um cliente envia uma requisição para a rota de um controller, como `/Personagem`.
2. O controller recebe os dados e identifica a operação CRUD solicitada.
3. O serviço da entidade executa a consulta ou alteração usando o `AirenContext`.
4. Os métodos de extensão fazem o mapeamento entre os DTOs de `Airen.Contracts` e os modelos persistidos.
5. O controller devolve a resposta HTTP, normalmente `Ok`, `CreatedAtAction`, `NotFound` ou `NoContent`.

Há controllers e serviços para as entidades principais. O `Program.cs` registra o contexto com MySQL, os serviços da aplicação e os controllers. A conexão é lida da chave `AirenConnection` em `appsettings.json`. Os DTOs utilizados pelos endpoints são mantidos em `Airen.Contracts`.

Os endpoints seguem o padrão REST:

```text
GET    /{recurso}       lista os registros
GET    /{recurso}/{id}  busca um registro
POST   /{recurso}       cria um registro
PUT    /{recurso}/{id}  atualiza um registro
DELETE /{recurso}/{id}  remove um registro
```

Todos os recursos abaixo possuem os cinco métodos CRUD:

| Recurso | Rotas |
| --- | --- |
| `Afiliacao` | `GET/POST /Afiliacao`, `GET/PUT/DELETE /Afiliacao/{id}` |
| `Capitulo` | `GET/POST /Capitulo`, `GET/PUT/DELETE /Capitulo/{id}` |
| `Habilidade` | `GET/POST /Habilidade`, `GET/PUT/DELETE /Habilidade/{id}` |
| `Hobby` | `GET/POST /Hobby`, `GET/PUT/DELETE /Hobby/{id}` |
| `Parentesco` | `GET/POST /Parentesco`, `GET/PUT/DELETE /Parentesco/{id}` |
| `Personagem` | `GET/POST /Personagem`, `GET/PUT/DELETE /Personagem/{id}` |
| `Potentia` | `GET/POST /Potentia`, `GET/PUT/DELETE /Potentia/{id}` |
| `Tecnica` | `GET/POST /Tecnica`, `GET/PUT/DELETE /Tecnica/{id}` |
| `Versao` | `GET/POST /Versao`, `GET/PUT/DELETE /Versao/{id}` |
| `Volume` | `GET/POST /Volume`, `GET/PUT/DELETE /Volume/{id}` |

Os resultados HTTP usam `CreatedAtAction` para criação, `Ok` para consulta e atualização, `NotFound` para IDs inexistentes e `NoContent` para exclusão. Atualmente a API não expõe Swagger/OpenAPI nem um middleware centralizado de tratamento de exceções; cada controller implementa diretamente os resultados HTTP básicos.

A API usa CORS com a política `wasm`, permitindo somente a origem `https://localhost:7268` para os métodos HTTP e os headers usados pelo cliente Web. Ao executar o Web em outra porta ou host, ajuste essa origem em `Airen.Api/Program.cs`.

### Airen.Admin

É uma aplicação de console voltada à administração manual dos dados. Sua motivação é oferecer uma ferramenta direta para cadastrar e manter o conteúdo enquanto uma interface web mais completa ainda está sendo construída - No caso, é mais uma ferramenta de estudos legado, considerando que a interface Web já está funcional.

O programa inicia em `PersonagemCore`, exibe um menu e encaminha a escolha para o fluxo correspondente.

Como o Admin acessa a API por HTTPS local, `GenerateHttpHandler` configura o `HttpClientHandler` para aceitar o certificado de desenvolvimento.

Atualmente o menu implementado oferece operações completas de personagem:

- Criar personagem
- Editar personagem
- Excluir personagem
- Buscar personagem por ID
- Listar personagens

Há implementações ou pontos de partida adicionais para habilidades, hobbies, potentias, técnicas e versões, mas ainda não há um menu consolidado que exponha essas entidades da mesma forma que o fluxo de personagens.

### Airen.Web

É o cliente web em Blazor WebAssembly. Sua motivação é oferecer uma interface visual para consulta e exploração do universo Airen, consumindo a mesma API usada pelo Admin e pelo restante da solução.

A aplicação atual já conta com uma estrutura inicial funcional de navegação e apresentação de dados:

- Layout e navbar com identidade visual própria da Fundação Potentia.
- Página inicial com apresentação editorial e modal informativo.
- Rotas de navegação para `/` (home), `/personagens` e `/volumes`.
- Tabela interativa de volumes com consulta, criação, edição e exclusão por meio de diálogos MudBlazor.
- Consulta da lista de personagens pela API.
- Serviços HTTP preparados para todos os recursos da API: Afiliacoes, Capitulos, Habilidades, Hobbies, Parentescos, Personagens, Potentias, Tecnicas, Versoes e Volumes.
- Integração com MudBlazor para UI responsiva e visual moderno.

O front-end usa `HttpClientFactory` com o nome `API` e espera a URL configurada em `Airen.Web/wwwroot/appsettings.json`:

```json
{
  "API-Server": "https://localhost:7245"
}
```

Essa configuração deve apontar para a URL em que a `Airen.Api` está rodando.

## Como executar

### Pré-requisitos

- .NET SDK 10.
- MySQL acessível pela aplicação.
- Docker em execução apenas para executar os testes de integração.
- Uma connection string chamada `AirenConnection` em `Airen.Api/appsettings.json` ou no ambiente de execução.

Exemplo:

```json
{
	"ConnectionStrings": {
		"AirenConnection": "server=localhost;user=root;password=senha;database=airen_db"
	}
}
```

Para evitar armazenar credenciais no repositório, defina a connection string por variável de ambiente ou secret local:

```bash
export ConnectionStrings__AirenConnection="server=localhost;user=USUARIO;password=SENHA;database=airen"
```

É recomendado remover senhas reais de `Airen.Api/appsettings.json` antes de compartilhar ou publicar o repositório.

### API

```bash
cd Airen.Api
dotnet run
```

O perfil HTTPS disponível em `Airen.Api/Properties/launchSettings.json` inicia em `https://localhost:7245` e `http://localhost:5027`.
O banco deve existir ou ser criado previamente; não há arquivos de migration versionados no diretório `Airen.Infrastructure` no estado atual.

### Admin

Com a API disponível, execute:

```bash
cd Airen.Admin
dotnet run
```

### Web

A aplicação web é um projeto Blazor WebAssembly que acessa a API REST via `HttpClient` configurado em `Airen.Web/wwwroot/appsettings.json`.

```bash
cd Airen.Web
dotnet run
```

Se a API estiver rodando em outra porta, ajuste o valor de `API-Server` no arquivo `Airen.Web/wwwroot/appsettings.json` antes de iniciar a aplicação web.

Rotas principais da interface:

- `/` — página inicial com apresentação temática.
- `/personagens` — lista personagens consumindo `GET /Personagem`.
- `/volumes` — lista volumes consumindo `GET /Volume`.

O Web e o Admin precisam apontar para a URL em que a API estiver executando. Antes de usar operações que alterem dados, confirme também a conexão da API com o MySQL.
O perfil HTTPS do Web usa `https://localhost:7268`; essa origem precisa corresponder à política CORS `wasm` da API.

### Testes automatizados

O repositório tem duas suítes de testes independentes:

- **Unitários**: exercitam os controllers isoladamente, com os repositórios
  simulados pelo Moq. Não precisam de Docker nem de banco de dados.
- **Integração**: validam persistência, relações e restrições contra um MySQL 8.0
  executado em container pelo Testcontainers. Precisam do Docker em execução.

Execute cada suíte a partir da raiz do repositório:

```bash
dotnet test Airen.Tests/Airen.Tests.Unit/Airen.Tests.Unit.csproj
dotnet test Airen.Tests/Airen.Tests.Integracao/Airen.Tests.Integracao.csproj
```

Consulte os detalhes de cobertura e execução em [Testes unitários](Airen.Tests/Airen.Tests.Unit/README.md)
e [Testes de integração](Airen.Tests/Airen.Tests.Integracao/README.md).
