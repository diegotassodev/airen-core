
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using AirenAdmin.Interfaces;
using AirenAdmin.Services;
using AirenApi.Data.DTOs;
using AirenApi.Models;
namespace AirenAdmin.Views;

public class CreateTecnica : IHobbyView {
    
    public async Task Execute(int versaoId) {
        
        ShowBanner();
        Console.Write("Digite o nome da técnica: ");
        string nome = Console.ReadLine()!;

        ShowBanner();
        Console.Write("Descreva a técnica: ");
        string descricao = Console.ReadLine()!;

        ShowBanner();
        Console.Write("Digite a categoria da técnica: ");
        string categoria = Console.ReadLine()!;

        var tecnicaDto = new CreateTecnicaDto {
            Nome = nome,
            Descricao = descricao,
            Categoria = categoria,
            IdVersao = versaoId,
        };

        var dtoSerializado = JsonSerializer.Serialize(tecnicaDto);
        var content = new StringContent(dtoSerializado, Encoding.UTF8, "application/json");

        var handler = new GenerateHttpHandler();
        HttpClient http = new HttpClient(handler.Execute());

        string url = "https://localhost:7245/tecnica";

        var response = await http.PostAsync(url,content);
        
        if (!response.IsSuccessStatusCode) {
            var erro = new ErrorMessage();
            await erro.Execute();
        }
        else {
            ShowBanner();
            Console.Write("As operações básicas foram concluídas com sucesso.");
            Console.ReadLine();
        }

    }

    public void ShowBanner() {

        Console.Clear();
        string banner = """
        ╔══════════════════════════════════════════════════════════╗
        ║                                                          ║
        ║              C R I A R   T É C N I C A                   ║
        ║                                                          ║
        ║    Você está na área de criação do técnicas, onde        ║
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