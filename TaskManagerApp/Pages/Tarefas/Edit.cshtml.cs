using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagerDomain;

namespace TaskManagerApp.Pages.Tarefas
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _client;

        [BindProperty]
        public Tarefa? Tarefa { get; set; }

        public EditModel(IHttpClientFactory httpClientFactory)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _client.PutAsJsonAsync($"Tarefa/{Tarefa?.Id}", Tarefa);

            return RedirectToPage("Index");
        }
    }
}
