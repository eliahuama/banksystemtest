using System.ComponentModel.DataAnnotations;

public class Account
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public double Balance { get; set; }
}