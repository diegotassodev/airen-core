using Airen.Admin.Interfaces;
using Airen.Admin.Views.Services;

namespace Airen.Admin.Views;

public class PersonagemCore : IPersonagemView {

    public async Task Execute() {

        Console.Clear();

        string banner = """
        ╔══════════════════════════════════════════════════════════╗
        ║                                                          ║
        ║                 ⚔  A I R E N  ⚔                          ║
        ║                                                          ║
        ║     [1] Criar Personagem                                 ║
        ║     [2] Editar Personagem                                ║
        ║     [3] Excluir Personagem                               ║
        ║     [4] Buscar Personagem                                ║
        ║     [5] Ver todos os Personagens                         ║
        ║                                                          ║
        ╚══════════════════════════════════════════════════════════╝

        """;

        Console.WriteLine(banner);
        Console.Write("Escolha sua opção: ");
        int opcao; int.TryParse(Console.ReadLine()!, out opcao);

        ChangeMenuService service = new();
        await service.ChangeView(opcao);
        
    }
    
}