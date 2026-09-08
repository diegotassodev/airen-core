
using AirenAdmin.Interfaces;
namespace AirenAdmin.Views;

public class ErrorMessage : IPersonagemView {
    
    public async Task Execute() {
        Console.Clear();
        string banner = """
        ╔══════════════════════════════════════════════════════════╗
        ║                                                          ║
        ║                   χ  E R R O R  χ                        ║
        ║                                                          ║
        ║          Ocorreu um erro ao tente novamente.             ║
        ║                                                          ║
        ╚══════════════════════════════════════════════════════════╝

        """;

        Console.WriteLine(banner);
    }
}