using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagerDomain;

namespace TaskManagerApp.Pages.Tarefas
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _client;

        public List<Tarefa>? Tarefas { get; set; }

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("TaskApiClient");
        }

        public async Task OnGetAsync()
        {
            Tarefas = await _client.GetFromJsonAsync<List<Tarefa>>("Tarefa");
        }
    }
}
