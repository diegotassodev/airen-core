using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using AirenAdmin.Interfaces;
using AirenAdmin.Services;
using AirenApi.Data.DTOs;
using AirenApi.Models;
namespace AirenAdmin.Views;

public class CreateHabilidade : IHabilidadeView {
    
    public async Task Execute(int potentiaId) {
        
        ShowBanner();
        Console.Write("Digite o nome da habilidade: ");
        string nome = Console.ReadLine()!;

        Console.Write("Descreva o funcionamento da habilidade em específico: ");
        string descricao = Console.ReadLine()!;

        var habilidadeDto = new CreateHabilidadeDto {
            Nome = nome,
            Descricao = descricao,
            IdPotentia = potentiaId
        };

        var dtoSerializado = JsonSerializer.Serialize(habilidadeDto);
        var content = new StringContent(dtoSerializado, Encoding.UTF8, "application/json");

        var handler = new GenerateHttpHandler();
        HttpClient http = new HttpClient(handler.Execute());

        string url = "https://localhost:7245/habilidade";

        var response = await http.PostAsync(url,content);
        var habilidade = await response.Content.ReadFromJsonAsync<Habilidade>();
        
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
        ║           C R I A R   H A B I L I D A D E                ║
        ║                                                          ║
        ║    Você está na área de criação do habilidades, onde     ║
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