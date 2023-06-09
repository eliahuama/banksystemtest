public class UserService : IUserService
{
    private readonly BankContext _context;

    public UserService(BankContext context)
    {
        _context = context;
    }

    public Guid CreateUser(string name)
    {
        var user = new User(name);
        _context.Users.Add(user);
        _context.SaveChanges();

        return user.Id;
    }

    public void DeleteUser(Guid userId)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null) return;

        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public User? GetUser(Guid userId)
    {
        return _context.Users.FirstOrDefault(u => u.Id == userId);
        
    }
}