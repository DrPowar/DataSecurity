namespace DataSecurity.Models
{
    internal class Role
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<Person> People { get; set; }
    }
}
