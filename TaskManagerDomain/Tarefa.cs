using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManagerDomain
{
    public class Tarefa
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public Prioridade Prioridade { get; set; }
        public DateTime DataLimite { get; set; }
        public Status Status { get; set; }
    }
}
