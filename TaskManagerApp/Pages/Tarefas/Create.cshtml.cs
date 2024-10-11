using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagerDomain;

namespace TaskManagerApp.Pages.Tarefas
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _client;

        [BindProperty]
        public Tarefa? NovaTarefa { get; set; }

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("TaskApiClient");
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _client.PostAsJsonAsync("Tarefa", NovaTarefa);
            return RedirectToPage("./Index");
        }
    }
}
