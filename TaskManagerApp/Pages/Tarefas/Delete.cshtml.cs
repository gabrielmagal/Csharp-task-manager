using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagerDomain;

namespace TaskManagerApp.Pages.Tarefas
{
    public class DeleteModel : PageModel
    {
        private readonly HttpClient _client;

        [BindProperty]
        public Tarefa? Tarefa { get; set; }

        public DeleteModel(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("TaskApiClient");
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Tarefa = await _client.GetFromJsonAsync<Tarefa>($"Tarefa/{id}");

            if (Tarefa == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            await _client.DeleteAsync($"Tarefa/{id}");
            return RedirectToPage("Index");
        }
    }
}
