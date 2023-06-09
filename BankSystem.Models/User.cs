using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual IEnumerable<Account> Accounts { get; set; }

    public User(string name)
    {
        Name = name;
    }
}