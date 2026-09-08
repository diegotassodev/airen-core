using Airen.Admin.Interfaces;

namespace Airen.Admin.Views;

public class DeletePersonagem : IPersonagemView {
    
    public async Task Execute() {
        
        Console.WriteLine("Digite o ID do personagem que você deseja deletar: ");
        int id; int.TryParse(Console.ReadLine()!, out id);


    }
}