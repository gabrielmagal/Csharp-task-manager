using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TaskManagerDomain;

namespace TaskManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly IMongoCollection<Tarefa> _tarefas;

        public TarefaController()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("local");
            _tarefas = database.GetCollection<Tarefa>("Tarefas");
        }

        [HttpGet]
        public ActionResult<List<Tarefa>> Get() => _tarefas.Find(t => true).ToList();

        [HttpGet("{id:length(24)}", Name = "GetTarefa")]
        public ActionResult<Tarefa> Get(string id)
        {
            var tarefa = _tarefas.Find(t => t.Id == id).FirstOrDefault();
            if (tarefa == null)
            {
                return NotFound();
            }
            return tarefa;
        }

        [HttpPost]
        public ActionResult<Tarefa> Create(Tarefa tarefa)
        {
            _tarefas.InsertOne(tarefa);
            return CreatedAtRoute("GetTarefa", new { id = tarefa.Id.ToString() }, tarefa);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Tarefa tarefaIn)
        {
            var tarefa = _tarefas.Find(t => t.Id == id).FirstOrDefault();
            if (tarefa == null)
            {
                return NotFound();
            }

            _tarefas.ReplaceOne(t => t.Id == id, tarefaIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var tarefa = _tarefas.Find(t => t.Id == id).FirstOrDefault();
            if (tarefa == null)
            {
                return NotFound();
            }

            _tarefas.DeleteOne(t => t.Id == id);
            return NoContent();
        }
    }
}
