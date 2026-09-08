
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Airen.Admin.Interfaces;
using Airen.Admin.Services;
using Airen.Api.DTOs;
using Airen.Infrastructure.Models;

namespace Airen.Admin.Views;

public class CreatePersonagem : IPersonagemView {
    
    public async Task Execute() {

        ShowBanner();
        Console.Write("Digite o nome do personagem: ");
        string nome = Console.ReadLine()!;
        string nomeFantasia = nome;

        ShowBanner();
        Console.Write("O personagem possui um nome fantasia? [S] [N]: ");
        string opcao = Console.ReadLine()!;

        if (opcao.ToUpper() == "S") {
            Console.Write("Digite o nome fantasia do personagem: ");
            nomeFantasia = Console.ReadLine()!;
            opcao = string.Empty;
        }
        
        ShowBanner();
        Console.Write("Escreva a biografia do personagem, não se preocupe com o tamanho, temos bastante espaço: ");
        string biografia = Console.ReadLine()!;

        ShowBanner();
        Console.Write("Qual a data de nascimento do personagem? Escreva no modelo dd/MM/yyy: ");
        var dataNascimento = new DateTime(); DateTime.TryParse(Console.ReadLine()!, out dataNascimento);
        
        ShowBanner();
        Console.Write("O personagem está vivo? [S] [N]: ");
        opcao = Console.ReadLine()!;

        bool estaVivo;

        if (opcao.ToUpper() == "S") {
            estaVivo = true;
            opcao = string.Empty;
        }
        else {
            estaVivo = false;
            opcao = string.Empty;
        }

                var personagemDto = new CreatePersonagemDto() {
            Nome = nomeFantasia,
            NomeOriginal = nome,
            DataNascimento = dataNascimento,
            Biografia = biografia,
            EstaVivo = estaVivo
        };

        var dtoSerializado = JsonSerializer.Serialize(personagemDto);
        var content = new StringContent(dtoSerializado, Encoding.UTF8, "application/json");

        var handler = new GenerateHttpHandler();
        HttpClient http = new HttpClient(handler.Execute());

        string url = "https://localhost:7245/personagem";

        var response = await http.PostAsync(url,content);
        var personagem = await response.Content.ReadFromJsonAsync<Personagem>();
        
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
        Console.Write("O personagem é portador de uma potentia? [S] [N]: ");
        opcao = Console.ReadLine()!;

        if (opcao.ToUpper() == "S") {
            Console.WriteLine("Entrando no menu de configuração da potentia!");
            Console.Write("Pressione Enter.");

            var potentia = new CreatePotentia();
            await potentia.Execute(personagem!.Id);
        }
        
        ShowBanner();
        Console.Write("Quer falar sobre as versões do seu personagem? [S] [N]: ");
        opcao = Console.ReadLine()!;

        if (opcao.ToUpper() == "S") {
            Console.WriteLine("Entrando no menu de configuração das versões!");
            Console.Write("Pressione Enter.");

            var versao = new CreateVersao();
            await versao.Execute(personagem!.Id);
        }
    }

    public void ShowBanner() {
        Console.Clear();
        string banner = """
        ╔══════════════════════════════════════════════════════════╗
        ║                                                          ║
        ║             C R I A R   P E R S O N A G E M              ║
        ║                                                          ║
        ║    Você está na área de criação do personagem, onde      ║
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