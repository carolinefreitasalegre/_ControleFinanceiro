using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        [JsonIgnore]

        public virtual ICollection<Conta> Contas { get; set; } = new List<Conta>();

    }
}
