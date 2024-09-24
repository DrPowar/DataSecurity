using System.Text.Json.Serialization;

namespace Server.Models
{
    public class Role
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public List<Person> People { get; set; }

        public Role(string name)
        {
            Name = name;
        }

        public Role() { }
    }
}
