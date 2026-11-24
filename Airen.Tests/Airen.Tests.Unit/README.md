# Testes unitários

Esta suíte verifica os controllers isoladamente, sem iniciar a API e sem conectar a
um banco de dados. As dependências dos controllers, como os repositórios, são
substituídas por mocks da lib Moq. Assim, os testes podem se concentrar na resposta do
controller para cada operação e evitar regressões na lógica HTTP.

Os testes seguem o padrão AAA:

- **Arrange**: prepara os DTOs, os mocks e os valores esperados.
- **Act**: chama a ação do controller.
- **Assert**: verifica o resultado HTTP, os dados retornados e as interações com
  os mocks.

Para cada recurso testado, a suíte cobre listagem, busca por ID, criação,
atualização e exclusão. Os controllers cobertos são Afiliacao, Capitulo,
Habilidade, Hobby, Parentesco, Personagem, Potentia, Tecnica, Versao e Volume.

Execute os testes a partir da raiz do repositório:

```bash
dotnet test Airen.Tests/Airen.Tests.Unit/Airen.Tests.Unit.csproj
```

Ou entrando diretamente na pasta:

```bash
cd Airen.Tests
cd Airen.Tests.Unit
dotnet run
```

Os testes de persistência no MySQL ficam na [suíte de integração](../Airen.Tests.Integracao/README.md).