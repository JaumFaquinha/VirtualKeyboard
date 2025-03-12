using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace JPFMS_BankKeyboard.Model
{
    public class Middleware
    {
        private readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Antes de chamar o próximo middleware
            Debug.WriteLine($"[Middleware] Iniciando requisição para: {context.Request.Path}");

            await _next(context); // Chama o próximo middleware no pipeline

            // Após a resposta ser gerada
            Debug.WriteLine($"[Middleware] Resposta enviada com status: {context.Response.StatusCode}");
        }
    }
}
