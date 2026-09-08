
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Airen.Admin.Interfaces;
using Airen.Admin.Services;
using Airen.Api.DTOs;
using Airen.Infrastructure.Models;

namespace Airen.Admin.Views;

public class CreateVersao : IVersaoView {
    
    public async Task Execute(int personagemId) {
        
        ShowBanner();
        Console.Write("Digite o nome da versão: ");
        string nome = Console.ReadLine()!;

        ShowBanner();
        Console.Write("Digite o gênero da versão: ");
        string genero = Console.ReadLine()!;

        ShowBanner();
        Console.Write("Digite o url da imagem da versão: ");
        string imagem = Console.ReadLine()!;

        ShowBanner();
        Console.Write("Digite a idade inicial da versão: ");
        int idadeInicial; int.TryParse(Console.ReadLine()!, out idadeInicial);

        ShowBanner();
        Console.Write("Digite a idade final da versão: ");
        int idadeFinal; int.TryParse(Console.ReadLine()!, out idadeFinal);

        ShowBanner();
        Console.Write("Descreva o estilo da versão: ");
        string descricao = Console.ReadLine()!;

        ShowBanner();
        Console.Write("Descreva a personalidade da versão: ");
        string personalidade = Console.ReadLine()!;

        var versaoDto = new CreateVersaoDto() {
            Nome = nome,
            Genero = genero,
            UrlImagem = imagem,
            IdadeInicial = idadeInicial,
            IdadeFinal = idadeFinal,
            Descricao = descricao,
            Personalidade = personalidade,
            IdPersonagem = personagemId
        };

        var dtoSerializado = JsonSerializer.Serialize(versaoDto);
        var content = new StringContent(dtoSerializado, Encoding.UTF8, "application/json");

        var handler = new GenerateHttpHandler();
        HttpClient http = new HttpClient(handler.Execute());

        string url = "https://localhost:7245/versao";

        var response = await http.PostAsync(url,content);
        var versao = await response.Content.ReadFromJsonAsync<Versao>();
        
        if (!response.IsSuccessStatusCode) {
            var erro = new ErrorMessage();
            await erro.Execute();
        }
        else {
            ShowBanner();
            Console.Write("As operações básicas foram concluídas com sucesso.");
            Console.ReadLine();
        }

        ShowBanner();
        Console.Write("Deseja se aprofundas no Hoobies da versão? [S] [N]: ");
        string opcao = Console.ReadLine()!;

        if (opcao.ToUpper() == "S") {
            Console.WriteLine("Entrando no menu de configuração de hoobies!");
            Console.Write("Pressione Enter.");

            var hobbies = new CreateHobby();
            await hobbies.Execute(versao!.Id);
            opcao = string.Empty;
        }

        ShowBanner();
        Console.Write("Deseja se aprofundas nas técnicas da versão? [S] [N]: ");
        opcao = Console.ReadLine()!;

        if (opcao.ToUpper() == "S") {
            Console.WriteLine("Entrando no menu de configuração de técnicas!");
            Console.Write("Pressione Enter.");

            var tecnicas = new CreateTecnica();
            await tecnicas.Execute(versao!.Id);
            opcao = string.Empty;
        }
    }

    public void ShowBanner() {

        Console.Clear();
        string banner = """
        ╔══════════════════════════════════════════════════════════╗
        ║                                                          ║
        ║               C R I A R   V E R S Ã O                    ║
        ║                                                          ║
        ║    Você está na área de criação do versões, onde         ║
        ║    ocorrerá o envio das informações para o banco de      ║
        ║    dados.                                                ║
        ║                                                          ║
        ║    Preencha todas as informações solicitadas. Caso       ║
        ║    alguma esteja faltando, envie uma mensagem no         ║
        ║    Discord: oriyon.                                      ║
        ║                                                          ║
        ╚══════════════════════════════════════════════════════════╝

        """;

        Console.WriteLine(banner);
    }
}