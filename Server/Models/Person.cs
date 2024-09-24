using Server.Models;

public class Person
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Role Role { get; set; }

    public bool IsBlocked { get; set; }

    public string? Password { get; set; }

    public Person(string name, string password, Role role)
    {
        Id = Guid.NewGuid();
        Name = name;
        Role = role;
        Password = password;
    }

    public Person(string name, Role role)
    {
        Id = Guid.NewGuid();
        Name = name;
        Role = role;
        Password = "";
    }

    public Person() { }
}
