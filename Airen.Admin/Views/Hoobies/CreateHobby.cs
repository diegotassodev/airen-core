using System.Text;
using System.Text.Json;
using Airen.Admin.Interfaces;
using Airen.Admin.Services;
using Airen.Api.DTOs;

namespace Airen.Admin.Views;

public class CreateHobby : IHobbyView {
    
    private string continuar = "S";

    public async Task Execute(int versaoId) {
        
        while (continuar != "N") {
            ShowBanner();
            Console.Write("Digite o nome do hobby: ");
            string nome = Console.ReadLine()!;

            ShowBanner();
            Console.Write("Descreva o hobby: ");
            string descricao = Console.ReadLine()!;

            var hobbyDto = new CreateHobbyDto {
                Nome = nome,
                Descricao = descricao,
                IdVersao = versaoId,
            };

            var dtoSerializado = JsonSerializer.Serialize(hobbyDto);
            var content = new StringContent(dtoSerializado, Encoding.UTF8, "application/json");

            var handler = new GenerateHttpHandler();
            HttpClient http = new HttpClient(handler.Execute());

            string url = "https://localhost:7245/hobby";

            var response = await http.PostAsync(url,content);
            
            if (!response.IsSuccessStatusCode) {
                var erro = new ErrorMessage();
                await erro.Execute();
            }
            else {
                ShowBanner();
                Console.Write("As operações básicas foram concluídas com sucesso.");
                Console.ReadLine();

                Console.Write("Deseja adicionar mais algum hobby? [S] [N]: ");
                continuar = Console.ReadLine()!.ToUpper();
            }
        }
    }

    public void ShowBanner() {

        Console.Clear();
        string banner = """
        ╔══════════════════════════════════════════════════════════╗
        ║                                                          ║
        ║                C R I A R   H O B B Y                     ║
        ║                                                          ║
        ║    Você está na área de criação do hobbies, onde         ║
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