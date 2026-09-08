namespace Airen.Admin.Services;

public class GenerateHttpHandler {
    
    public HttpMessageHandler Execute() {
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        return clientHandler;
    }
}