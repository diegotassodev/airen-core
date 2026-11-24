# Testes de integração

Esta suíte valida persistência e consultas usando o Entity Framework Core com um MySQL real executado em um container Docker. Diferentemente dos testes unitários, ela verifica que o mapeamento do modelo, as chaves estrangeiras, as restrições e as consultas funcionam contra o provedor MySQL.

O `MySqlFixture` inicia um container MySQL 8.0 para a coleção e fornece um
`AirenContext` conectado a ele. Cada teste cria seu próprio contexto e apaga e
recria o banco no início do cenário com `EnsureDeletedAsync` e
`EnsureCreatedAsync`, para começar com o schema e os dados limpos. O `using`
descarta o contexto ao final do teste; os dados do teste permanecem no container
até a próxima limpeza ou o encerramento do container.

Os cenários cobrem Afiliacao, Capitulo, Habilidade, Hobby, Parentesco,
Personagem, Potentia, Tecnica, Versao e Volume. Quando necessário, criam também
as entidades relacionadas exigidas pelas chaves estrangeiras. Os testes
inserem uma lista de registros e uma entidade identificável, depois consultam
essa entidade e validam os dados persistidos. Parentesco também verifica que o
banco permite os dois sentidos da relação (por exemplo, pai e filho), mas
impede mais de um tipo para o mesmo par ordenado de personagens.

## Pré-requisitos

- Docker instalado e em execução.
- Dependências .NET restauradas.

## Execução

Na raiz do repositório:

```bash
dotnet test Airen.Tests/Airen.Tests.Integracao/Airen.Tests.Integracao.csproj
```

Ou entrando diretamente na pasta:

```bash
cd Airen.Tests
cd Airen.Tests.Integracao
dotnet run
```

Os testes unitários dos controllers estão na [suíte unitária](../Airen.Tests.Unit/README.md).
