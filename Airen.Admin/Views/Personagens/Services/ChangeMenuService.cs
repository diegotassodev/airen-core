using AirenAdmin.Interfaces;
namespace AirenAdmin.Views.Services;

public class ChangeMenuService {

    public async Task ChangeView(int opcao) {

        IPersonagemView? personagem = null;

        switch (opcao) {  
            case 1: personagem = new CreatePersonagem(); break;
            case 2: personagem = new UpdatePersonagem(); break;
            case 3: personagem = new DeletePersonagem(); break;
            case 4: personagem = new SearchPersonagem(); break;
            case 5: personagem = new ShowPersonagens(); break;

            default: personagem = new ErrorMessage(); break;
        }

        await personagem.Execute();
        
        Console.Write("Enter para retornar ao menu");
        Console.ReadLine();

        var retornar = new PersonagemCore();
        await retornar.Execute();  

    }
    
}