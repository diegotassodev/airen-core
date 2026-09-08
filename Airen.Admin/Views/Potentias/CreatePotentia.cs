
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using AirenAdmin.Interfaces;
using AirenAdmin.Services;
using AirenApi.Data.DTOs;
using AirenApi.Models;
namespace AirenAdmin.Views;

public class CreatePotentia : IPotentiaView {
    
    public async Task Execute(int personagemId) {
        
        ShowBanner();
        Console.Write("Digite o nome da potentia: ");
        string nome = Console.ReadLine()!;

        ShowBanner();
        Console.Write("Descreva o funcionamento da potentia: ");
        string descricao = Console.ReadLine()!;

        ShowBanner();
        Console.Write("Digite o link da música que a potentia é associada: ");
        string linkMusica = Console.ReadLine()!;

        var potentiaDto = new CreatePotentiaDto {
            Nome = nome,
            Descricao = descricao,
            LinkMusica = linkMusica,
            PersonagemId = personagemId    
        };

        var dtoSerializado = JsonSerializer.Serialize(potentiaDto);
        var content = new StringContent(dtoSerializado, Encoding.UTF8, "application/json");

        var handler = new GenerateHttpHandler();
        HttpClient http = new HttpClient(handler.Execute());

        string url = "https://localhost:7245/potentia";

        var response = await http.PostAsync(url,content);
        var potentia = await response.Content.ReadFromJsonAsync<Potentia>();
        
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
        Console.Write("Deseja explicar a lista de habilidades específicas da potentia? [S] [N]: ");
        string opcao = Console.ReadLine()!;

        if (opcao.ToUpper() == "S") {
            Console.WriteLine("Entrando no menu de configuração de habilidades!");
            Console.Write("Pressione Enter.");

            var habilidade = new CreateHabilidade();
            await habilidade.Execute(potentia!.Id);
        }
    }

    public void ShowBanner() {

        Console.Clear();
        string banner = """
        ╔══════════════════════════════════════════════════════════╗
        ║                                                          ║
        ║             C R I A R   P O T E N T I A                  ║
        ║                                                          ║
        ║    Você está na área de criação do potentias, onde       ║
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